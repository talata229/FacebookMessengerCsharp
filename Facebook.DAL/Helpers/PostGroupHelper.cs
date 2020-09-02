using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Facebook.DAL.Responses.CrawlPostGroup.PostDetail;
using FacebookTool.Helper;

namespace Facebook.DAL.Helpers
{
    public class PostGroupHelper
    {
        public static async Task<PostDetailDTO> GetPostDetailDTOAsync(string postId, string cookie)
        {
            PostDetailDTO postDetailDTO = new PostDetailDTO();
            try
            {
                string link = "https://www.facebook.com/groups/" + ModifyLink(postId).Item1 + "/permalink/" + ModifyLink(postId).Item2;
                //var html = await HttpClientHelper.SendRequestAsync(link, cookie);
                HttpClientHelper2 clientHelper2 = new HttpClientHelper2();
                var html = await clientHelper2.SendRequestAsync(link, cookie);
                string name = Regex.Match(html, "data-hovercard-prefer-more-content-show=\"1\" data-hovercard-referer=\"(.*?)\" role=\"button\">(.*?)</a></span>").Groups[2].Value;
                string uid = Regex.Match(html, "data-hovercard=\"/ajax/hovercard/user.php\\?id=(.*?)&amp;").Groups[1].Value;
                string reactionCount = Regex.Match(html, "reaction_count:{count:(.*?)},").Groups[1].Value;
                string reactionCountLIKE = Regex.Match(html, "reaction_type:\"LIKE\"},i18n_reaction_count:\"(.*?)\"").Groups[1].Value;
                string reactionCountLOVE = Regex.Match(html, "reaction_type:\"LOVE\"},i18n_reaction_count:\"(.*?)\"").Groups[1].Value;
                string reactionCountWOW = Regex.Match(html, "reaction_type:\"WOW\"},i18n_reaction_count:\"(.*?)\"").Groups[1].Value;
                string reactionCountSUPPORT = Regex.Match(html, "reaction_type:\"SUPPORT\"},i18n_reaction_count:\"(.*?)\"").Groups[1].Value;
                string reactionCountHAHA = Regex.Match(html, "reaction_type:\"HAHA\"},i18n_reaction_count:\"(.*?)\"").Groups[1].Value;
                string reactionCountSAD = Regex.Match(html, "reaction_type:\"SORRY\"},i18n_reaction_count:\"(.*?)\"").Groups[1].Value;
                string reactionCountANGRY = Regex.Match(html, "reaction_type:\"ANGER\"},i18n_reaction_count:\"(.*?)\"").Groups[1].Value;
                string shareCount = Regex.Match(html, "share_count:{count:(.*?)}").Groups[1].Value;
                string commentCount = Regex.Match(html, "comment_count:{total_count:(.*?)}").Groups[1].Value;

                if (string.IsNullOrEmpty(name))
                {
                    name = Regex.Match(html, "\"__typename\":\"User\",\"name\":\"(.*?)\"").NextMatch().Groups[1].Value;
                    if (string.IsNullOrEmpty(name))
                    {
                        name = Regex.Match(html, "\"__typename\":\"User\",\"name\":\"(.*?)\"").Groups[1].Value;
                    }
                    name = Regex.Unescape(name);
                    uid = Regex.Match(html, "\"owning_profile\":{\"__typename\":\"User\",\"id\":\"(.*?)\"}").Groups[1].Value;
                    reactionCount = Regex.Match(html, "\"reaction_count\":{\"count\":(.*?),").Groups[1].Value;
                    reactionCountLIKE = Regex.Match(html, "\"reaction_type\":\"LIKE\",\"id\":\"(.*?)\"},\"reaction_count\":(.*?)}").Groups[2].Value;
                    reactionCountLOVE = Regex.Match(html, "\"reaction_type\":\"LOVE\",\"id\":\"(.*?)\"},\"reaction_count\":(.*?)}").Groups[2].Value;
                    reactionCountWOW = Regex.Match(html, "\"reaction_type\":\"WOW\",\"id\":\"(.*?)\"},\"reaction_count\":(.*?)}").Groups[2].Value;
                    reactionCountSUPPORT = Regex.Match(html, "\"reaction_type\":\"SUPPORT\",\"id\":\"(.*?)\"},\"reaction_count\":(.*?)}").Groups[2].Value;
                    reactionCountHAHA = Regex.Match(html, "\"reaction_type\":\"HAHA\",\"id\":\"(.*?)\"},\"reaction_count\":(.*?)}").Groups[2].Value;
                    reactionCountSAD = Regex.Match(html, "\"reaction_type\":\"SORRY\",\"id\":\"(.*?)\"},\"reaction_count\":(.*?)}").Groups[2].Value;
                    reactionCountANGRY = Regex.Match(html, "\"reaction_type\":\"ANGER\",\"id\":\"(.*?)\"},\"reaction_count\":(.*?)}").Groups[2].Value;
                    shareCount = Regex.Match(html, "\"share_count\":{\"count\":(.*?),").Groups[1].Value;
                    commentCount = Regex.Match(html, "\"comment_count\":{\"total_count\":(.*?)},").Groups[1].Value;
                }





                postDetailDTO.Name = name;
                postDetailDTO.UID = TryConvertToLong(uid);
                postDetailDTO.ReactionTotalCount = TryConvertToLong(reactionCount);
                postDetailDTO.LikeCount = TryConvertToLong(reactionCountLIKE);
                postDetailDTO.LoveCount = TryConvertToLong(reactionCountLOVE);
                postDetailDTO.WowCount = TryConvertToLong(reactionCountWOW);
                postDetailDTO.SupportCount = TryConvertToLong(reactionCountSUPPORT);
                postDetailDTO.HahaCount = TryConvertToLong(reactionCountHAHA);
                postDetailDTO.SadCount = TryConvertToLong(reactionCountSAD);
                postDetailDTO.AngryCount = TryConvertToLong(reactionCountANGRY);
                postDetailDTO.ShareCount = TryConvertToLong(shareCount);
                postDetailDTO.CommentCount = TryConvertToLong(commentCount);
            }
            catch (Exception e)
            {
            }
            return postDetailDTO;

        }

        public static long? TryConvertToLong(string input)
        {
            try
            {
                return long.Parse(input);
            }
            catch (Exception e)
            {
            }
            return null;
        }

        public static (string, string) ModifyLink(string link)
        {
            var arr = link.Split('_');
            return (arr[0], arr[1]);
        }
    }
}
