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
        public static async Task<List<NewfeedDTO>> GetNewFeed(string token, bool isOnlyUser = true, int countGet = 3)
        {
            List<NewfeedDTO> newfeedDTOs = new List<NewfeedDTO>();
            try
            {
                try
                {
                    HttpClient http = new HttpClient();
                    string url = $"https://graph.facebook.com/me/home?access_token={token}&fields=id,message,created_time,from,type&limit=50";
                    var response = await http.GetAsync(url);
                    var result = await response.Content.ReadAsStringAsync();
                    NewfeedRoot categoryRoot = JsonConvert.DeserializeObject<NewfeedRoot>(result);
                    //Lấy lần 1
                    newfeedDTOs = categoryRoot.Data;

                    for (int i = 0; i < countGet; i++)
                    {
                        try
                        {
                            url = categoryRoot.Paging?.Next;
                            response = await http.GetAsync(url);
                            result = await response.Content.ReadAsStringAsync();
                            categoryRoot = JsonConvert.DeserializeObject<NewfeedRoot>(result);
                            newfeedDTOs = newfeedDTOs.Concat(categoryRoot.Data).ToList();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"GetNewFeed lần {i} bị lỗi!");
                        }
                    }
                    //Save post in db
                    using (FbToolEntities db = new FbToolEntities())
                    {
                        foreach (var post in newfeedDTOs)
                        {
                            try
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
                            catch (Exception ex)
                            {
                                Console.WriteLine($"There's something went wrong GetNewFeed1. Exception = {ex.Message}. InnerException ={ex.InnerException?.Message}");
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
            catch (Exception ex)
            {
                Console.WriteLine($"There's something went wrong GetNewFeed2. Exception = {ex.Message}. InnerException ={ex.InnerException?.Message}");
            }
            return newfeedDTOs;
        }

        public static async Task ReactionAllPost()
        {
            try
            {
                List<NewfeedDTO> newfeedDTOs = await GetNewFeed(Constant.Token, true, 3);
                newfeedDTOs = newfeedDTOs.Distinct(new ComparerCustom()).ToList();
                using (FbToolEntities db = new FbToolEntities())
                {
                    foreach (var post in newfeedDTOs)
                    {
                        try
                        {
                            string type = EnumHelper.GetDescription(EnumReactionType.LIKE);
                            Random rd = new Random();
                            bool isSuccess = false;
                            var reactionType = EnumReactionType.LIKE;
                            if (rd.NextDouble() <= 0.5)
                            {
                                reactionType = EnumReactionType.LIKE;
                            }
                            else if (rd.NextDouble() <= 0.8)
                            {
                                reactionType = EnumReactionType.LOVE;
                            }
                            else
                            {
                                reactionType = EnumReactionType.HAHA;
                            }
                            isSuccess = await LikePost(Constant.Token, post.Id, reactionType);
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
                                    ConsoleLogHelper.WriteToConsole($"{reactionType} Post success - {post.Id}");
                                }
                            }
                            else
                            {
                                ConsoleLogHelper.WriteToConsole($"{reactionType} Post failed - {post.Id}");
                            }
                            Thread.Sleep(TimeSpan.FromSeconds(Constant.TIME_SLEEP_REACTION));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"There's something went wrong ReactionAllPost1. Exception = {ex.Message}. InnerException ={ex.InnerException?.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There's something went wrong GetNewFeed. ReactionAllPost2 = {ex.Message}. InnerException ={ex.InnerException?.Message}");
            }
        }
        public static async Task<bool> LikePost(string token, string postId, EnumReactionType type = EnumReactionType.LIKE)
        {
            var isSuccess = false;
            try
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

                if (arr.Length > 1)
                {
                    string url2 = $"https://graph.facebook.com/{arr[1]}/reactions?type={typeReact}&method=POST&access_token={token}";
                    var response2 = await http.GetAsync(url2);
                    var result2 = await response2.Content.ReadAsStringAsync();
                    FbSimpleResponse res2 = JsonConvert.DeserializeObject<FbSimpleResponse>(result2);
                    isSuccess = res2.Success;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There's something went wrong GetNewFeed. Exception = {ex.Message}. InnerException ={ex.InnerException?.Message}");
            }
            return isSuccess;
        }

        public static async Task<EnumFeature> CheckSpecialMessage(string message, string threadId)
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
                    return EnumFeature.NoSpecialFeature;
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
                    return EnumFeature.NoSpecialFeature;
                }
                //Stop5Min
                if (message.Equals(EnumHelper.GetDescription(EnumFeature.Stop5Min), StringComparison.InvariantCultureIgnoreCase))
                {
                    await AddUser10Min(threadId);
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
                    return EnumFeature.TroLyAo;
                }
                if (message.Equals(EnumHelper.GetDescription(EnumFeature.GirlXinh), StringComparison.InvariantCultureIgnoreCase))
                {
                    return EnumFeature.GirlXinh;
                }
                if (message.Equals(EnumHelper.GetDescription(EnumFeature.TruyenCuoi), StringComparison.InvariantCultureIgnoreCase))
                {
                    return EnumFeature.TruyenCuoi;
                }
                if (message.Equals(EnumHelper.GetDescription(EnumFeature.TinTuc), StringComparison.InvariantCultureIgnoreCase))
                {
                    return EnumFeature.TinTuc;
                }
            }
            return EnumFeature.Normal;
        }

        public static async Task<List<string>> GetListTruyenCuoi()
        {
            List<string> listTruyenCuoi = new List<string>();
            using (FbToolEntities db = new FbToolEntities())
            {
                listTruyenCuoi = await db.Fb_FunnyStory.Select(x => x.Content).ToListAsync();
            }
            return listTruyenCuoi;
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

        public static async Task AddUser10Min(string userId)
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
                        UtilTime = DateTime.Now.AddMinutes(10),
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
                ConsoleLogHelper.WriteToConsole($"Stop10Min {userId}");
                return;
            }
        }
    }
}
