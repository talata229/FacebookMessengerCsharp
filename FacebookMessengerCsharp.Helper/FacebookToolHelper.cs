using Facebook.DAL;
using Facebook.DAL.Constants;
using Facebook.DAL.Enum;
using Facebook.DAL.Responses;
using Facebook.DAL.Responses.Newfeed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FacebookMessengerCsharp.Helper
{
    public class FacebookToolHelper
    {
        public static async Task<List<NewfeedDTO>> GetNewFeed(string token, bool isOnlyUser = true)
        {
            List<NewfeedDTO> newfeedDTOs = new List<NewfeedDTO>();
            try
            {
                HttpClient http = new HttpClient();
                string url = $"https://graph.facebook.com/me/home?access_token={token}&fields=id,message,created_time,from,type&limit=50";
                var response = await http.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                NewfeedRoot categoryRoot = JsonConvert.DeserializeObject<NewfeedRoot>(result);

                newfeedDTOs = categoryRoot.Data;
                //Save post in db
                using (FbToolEntities db = new FbToolEntities())
                {
                    foreach (var post in newfeedDTOs)
                    {
                        Fb_Post fb_Post = new Fb_Post
                        {
                            FacebookId = post.Id,
                            CreatedDate = DateTime.Now,
                            UpdatedDate = null,
                            FromUserId = post.From?.Id,
                            Type = post.Type,
                            Message = post.Message
                        }; ;
                        if (!db.Fb_Post.Any(x => x.FacebookId == post.Id))
                        {
                            db.Fb_Post.Add(fb_Post);
                            await db.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleLogHelper.WriteToConsole($"Exception in GetNewFeed, message = {ex.Message}");
            }
            if (!isOnlyUser)
            {
                return newfeedDTOs;
            }
            var newfeedDTOsUser = newfeedDTOs.Where(x => x.From.Category == null).ToList();
            return newfeedDTOsUser;
        }

        public static async Task ReactionAllPost()
        {
            List<NewfeedDTO> newfeedDTOs = await GetNewFeed(Constant.Token);
            using (FbToolEntities db = new FbToolEntities())
            {
                foreach (var post in newfeedDTOs)
                {
                    string type = EnumHelper.GetDescription(EnumReactionType.LIKE);
                    bool isSuccess = await LikePost(Constant.Token, post.Id, EnumReactionType.LIKE);
                    if (isSuccess)
                    {
                        //Save db
                        Fb_Post postInDb = await db.Fb_Post.FirstOrDefaultAsync(x => x.FacebookId == post.Id);
                        Fb_Like_Post fbLikePost = new Fb_Like_Post
                        {
                            IdPost = postInDb?.Id,
                            FacebookIdPost = post.Id,
                            CreatedDate = DateTime.Now,
                            UpdatedDate = null,
                            Type = type
                        };
                        if (!db.Fb_Like_Post.Any(x => x.FacebookIdPost == postInDb.FacebookId))
                        {
                            db.Fb_Like_Post.Add(fbLikePost);
                            await db.SaveChangesAsync();
                            ConsoleLogHelper.WriteToConsole($"Like Post success - {post.Id}");
                        }
                    }
                    else
                    {
                        ConsoleLogHelper.WriteToConsole($"Like Post failed - {post.Id}");
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                }
            }

        }
        public static async Task<bool> LikePost(string token, string postId, EnumReactionType type = EnumReactionType.LIKE)
        {
            HttpClient http = new HttpClient();
            string typeReact = EnumHelper.GetDescription(type);
            string url = $"https://graph.facebook.com/{postId}/reactions?type={typeReact}&method=POST&access_token={token}";
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            FbSimpleResponse res = JsonConvert.DeserializeObject<FbSimpleResponse>(result);
            if (res.Success)
            {
                return true;
            }
            string[] arr = postId.Split('_');
            var isSuccess = false;
            if (arr.Length > 1)
            {
                string url2 = $"https://graph.facebook.com/{arr[1]}/reactions?type={typeReact}&method=POST&access_token={token}";
                var response2 = await http.GetAsync(url2);
                var result2 = await response2.Content.ReadAsStringAsync();
                FbSimpleResponse res2 = JsonConvert.DeserializeObject<FbSimpleResponse>(result2);
                isSuccess = res2.Success;
            }
            return isSuccess;
        }

        public static async Task CheckSpecialMessage(string message, string threadId)
        {
            using (FbToolEntities db = new FbToolEntities())
            {
                //RemoveStopAll
                if (message.Equals(EnumHelper.GetDescription(EnumFeature.RemoveStopAll), StringComparison.InvariantCultureIgnoreCase))
                {
                    var userInDb = await db.Fb_BlockUser.FirstOrDefaultAsync(x => x.FacebookId == threadId);
                    if (userInDb != null)
                    {
                        db.Fb_BlockUser.Remove(userInDb);
                        await db.SaveChangesAsync();
                        ConsoleLogHelper.WriteToConsole($"RemoveStopAll {threadId}");
                    }
                    return;
                }
                //StopAll
                if (message.Equals(EnumHelper.GetDescription(EnumFeature.StopAll), StringComparison.InvariantCultureIgnoreCase))
                {
                    var userInDb = await db.Fb_BlockUser.FirstOrDefaultAsync(x => x.FacebookId == threadId);
                    if (userInDb == null)
                    {
                        Fb_BlockUser blockUser = new Fb_BlockUser
                        {
                            FacebookId = threadId,
                            CreatedDate = DateTime.Now,
                            UpdatedDate = null,
                            IsBlockAll = true,
                            UtilTime = null,
                        };
                        db.Fb_BlockUser.Add(blockUser);
                        await db.SaveChangesAsync();
                        ConsoleLogHelper.WriteToConsole($"StopAll {threadId}");
                    }
                    if (userInDb != null)
                    {
                        userInDb.IsBlockAll = true;
                        userInDb.UpdatedDate = DateTime.Now;
                        await db.SaveChangesAsync();
                        ConsoleLogHelper.WriteToConsole($"StopAll {threadId}");
                    }
                    return;
                }

                //Stop5Min
                if (message.Equals(EnumHelper.GetDescription(EnumFeature.Stop5Min), StringComparison.InvariantCultureIgnoreCase))
                {
                    await AddUser5Min(threadId);
                    //var userInDb = await db.Fb_BlockUser.FirstOrDefaultAsync(x => x.FacebookId == threadId);
                    //if (userInDb == null)
                    //{
                    //    Fb_BlockUser blockUser = new Fb_BlockUser
                    //    {
                    //        FacebookId = threadId,
                    //        CreatedDate = DateTime.Now,
                    //        UpdatedDate = null,
                    //        IsBlockAll = false,
                    //        UtilTime = DateTime.Now.AddMinutes(5),
                    //    };
                    //    db.Fb_BlockUser.Add(blockUser);
                    //    await db.SaveChangesAsync();
                    //}
                    //if (userInDb != null)
                    //{
                    //    userInDb.IsBlockAll = false;
                    //    userInDb.UpdatedDate = DateTime.Now;
                    //    userInDb.UtilTime = DateTime.Now.AddMinutes(5);
                    //    await db.SaveChangesAsync();
                    //}
                    //ConsoleLogHelper.WriteToConsole($"Stop5Min {threadId}");
                    //return;
                }
                //TroLyAo
                if (message.Equals(EnumHelper.GetDescription(EnumFeature.TroLyAo), StringComparison.InvariantCultureIgnoreCase))
                {
                    var userInDb = await db.Fb_User_Simsimi.FirstOrDefaultAsync(x => x.FacebookId == threadId);
                    if (userInDb == null)
                    {
                        Fb_User_Simsimi user_Simsimi = new Fb_User_Simsimi
                        {
                            FacebookId = threadId,
                            FacebookName = "",
                            IsAgree = true,
                            CreatedDate = DateTime.Now
                        };
                        db.Fb_User_Simsimi.Add(user_Simsimi);
                        await db.SaveChangesAsync();
                        ConsoleLogHelper.WriteToConsole($"TroLyAo agree - {threadId}");
                    }
                    else
                    {
                        userInDb.IsAgree = true;
                        userInDb.UpdatedDate = DateTime.Now;
                        await db.SaveChangesAsync();
                        ConsoleLogHelper.WriteToConsole($"TroLyAo agree - {threadId}");
                    }
                    return;
                }
            }
        }

        public static async Task<bool> CheckIsBlockOrNot(string userId)
        {
            using (FbToolEntities db = new FbToolEntities())
            {
                var userInDb = await db.Fb_BlockUser.FirstOrDefaultAsync(x => x.FacebookId == userId);
                if (userInDb == null)
                {
                    return false;
                }
                //Check xem có đồng ý dùng simsimi hay không
                var agreeSimsimi = await CheckUserAgreeSimsimi(userId);
                if (agreeSimsimi && !userInDb.IsBlockAll.GetValueOrDefault())
                {
                    return false;
                }
                return userInDb.IsBlockAll.GetValueOrDefault() || DateTime.Now <= userInDb.UtilTime;
            }
        }
        public static async Task<bool> CheckUserAgreeSimsimi(string userId)
        {
            using (FbToolEntities db = new FbToolEntities())
            {
                var userInDb = await db.Fb_User_Simsimi.FirstOrDefaultAsync(x => x.FacebookId == userId);
                if (userInDb == null)
                {
                    return false;
                }
                return userInDb.IsAgree.GetValueOrDefault();
            }
        }

        public static async Task AddUser5Min(string userId)
        {
            using (FbToolEntities db = new FbToolEntities())
            {
                var userInDb = await db.Fb_BlockUser.FirstOrDefaultAsync(x => x.FacebookId == userId);
                if (userInDb == null)
                {
                    Fb_BlockUser blockUser = new Fb_BlockUser
                    {
                        FacebookId = userId,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = null,
                        IsBlockAll = false,
                        UtilTime = DateTime.Now.AddMinutes(5),
                    };
                    db.Fb_BlockUser.Add(blockUser);
                    await db.SaveChangesAsync();
                }
                if (userInDb != null)
                {
                    userInDb.IsBlockAll = false;
                    userInDb.UpdatedDate = DateTime.Now;
                    userInDb.UtilTime = DateTime.Now.AddMinutes(5);
                    await db.SaveChangesAsync();
                }
                ConsoleLogHelper.WriteToConsole($"Stop5Min {userId}");
                return;
            }
        }
    }
}
