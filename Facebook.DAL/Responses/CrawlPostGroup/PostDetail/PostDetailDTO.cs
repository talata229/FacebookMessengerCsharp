using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.DAL.Responses.CrawlPostGroup.PostDetail
{
   public class PostDetailDTO
    {

        //var reactionCount = Regex.Match(result, "reaction_count:{count:(.*?)},").Groups[1].Value;
        //var reactionCountLIKE = Regex.Match(result, "reaction_type:\"LIKE\"},i18n_reaction_count:\"(.*?)\"").Groups[1].Value;
        //var reactionCountLOVE = Regex.Match(result, "reaction_type:\"LOVE\"},i18n_reaction_count:\"(.*?)\"").Groups[1].Value;
        //var reactionCountWOW = Regex.Match(result, "reaction_type:\"WOW\"},i18n_reaction_count:\"(.*?)\"").Groups[1].Value;
        //var reactionCountSUPPORT = Regex.Match(result, "reaction_type:\"SUPPORT\"},i18n_reaction_count:\"(.*?)\"").Groups[1].Value;
        //var reactionCountHAHA = Regex.Match(result, "reaction_type:\"HAHA\"},i18n_reaction_count:\"(.*?)\"").Groups[1].Value;
        //var reactionCountSAD = Regex.Match(result, "reaction_type:\"SORRY\"},i18n_reaction_count:\"(.*?)\"").Groups[1].Value;
        //var reactionCountANGRY = Regex.Match(result, "reaction_type:\"ANGER\"},i18n_reaction_count:\"(.*?)\"").Groups[1].Value;
        //var shareCount = Regex.Match(result, "share_count:{count:(.*?)}").Groups[1].Value;
        //var commentCount = Regex.Match(result, "comment_count:{total_count:(.*?)}").Groups[1].Value;
        //File.WriteAllText("364997627165697_1363764923955624.html", result);
        public string Name { get; set; }
        public long? UID { get; set; }
        public long? ReactionTotalCount { get; set; }
        public long? LikeCount { get; set; }
        public long? LoveCount { get; set; }
        public long? WowCount { get; set; }
        public long? SupportCount { get; set; }
        public long? HahaCount { get; set; }
        public long? SadCount { get; set; }
        public long? AngryCount { get; set; }
        public long? ShareCount { get; set; }
        public long? CommentCount { get; set; }

    }
}
