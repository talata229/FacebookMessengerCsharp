using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Facebook.DAL;
using Facebook.DAL.Constants;
using Facebook.DAL.Helpers;
using Facebook.DAL.Responses.CrawlPostGroup;
using Facebook.DAL.Responses.TokenCookie;
using FacebookMessengerCsharp.Helper;
using FacebookTool.Helper;
using Newtonsoft.Json;

namespace FacebookTool.App
{
    public partial class frmMain : Form
    {
        //Tab CrawlGroupPost
        private Thread _threadCrawlGroupPost;
        private int totalPostCrawed = 0;

        private int _numberOfThread;
        private int _totalTokenCheck;
        private int _timeSleepCheckToken;
        private Thread[] _listThread;
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnStartGetToken_Click(object sender, EventArgs e)
        {
            #region Code cũ
            if (btnStartGetToken.Text == "Start Get TOKEN")
            {
                rtbLogGetToken.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                btnStartGetToken.Text = "Stop Get TOKEN";
                _numberOfThread = int.Parse(txtNumberOfThreadGetToken.Text);
                _timeSleepCheckToken = int.Parse(txtTimeSleepCheckToken.Text);
                _listThread = new Thread[_numberOfThread];
                for (int i = 0; i < _numberOfThread; i++)
                {
                    int index = int.Parse(i.ToString());
                    string nameOfThread = "Thread " + index;
                    _listThread[index] = new Thread(async () => await GetTokenAsync(nameOfThread))
                    {
                        Name = nameOfThread
                    };
                    _listThread[index].Start();
                }
            }
            else
            {
                btnStartGetToken.Text = "Start Get TOKEN";
                //_threadGetToken.Abort();
                rtbLogGetToken.Text = rtbLogGetToken.Text + Environment.NewLine + "Stop - " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            #endregion
            #region Code mới
            //if (btnStartGetToken.Text == "Start Get TOKEN")
            //{
            //    rtbLogGetToken.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            //    btnStartGetToken.Text = "Stop Get TOKEN";
            //    _numberOfThread = int.Parse(txtNumberOfThreadGetToken.Text);
            //    _timeSleepCheckToken = int.Parse(txtTimeSleepCheckToken.Text);
            //    listThread = new Thread[_numberOfThread];
            //    var ids = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

            //    await ids.ParallelForEachAsync(async i =>
            //        {
            //            await GetTokenAsyncV2(i);
            //        },
            //        maxDegreeOfParallelism: 10);

            //}
            //else
            //{
            //    btnStartGetToken.Text = "Start Get TOKEN";
            //    //_threadGetToken.Abort();
            //    rtbLogGetToken.Text = rtbLogGetToken.Text + Environment.NewLine + "Stop - " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //}
            #endregion
        }


        private async Task GetTokenAsync(string nameOfThread)
        {
            while (true)
            {
                try
                {
                    int baseIndex = 170;
                    for (int i = 0; i < _numberOfThread; i++)
                    {
                        if (nameOfThread.Contains(i.ToString()))
                        {
                            baseIndex += i;
                        }
                    }
                    var token = "EAAAAZAw4FxQIBA" + StringHelper.RandomStringWithNumber(baseIndex);
                    if (_totalTokenCheck % 50000 == 0 && _totalTokenCheck >= 50000)
                    {
                        token = "EAAAAZAw4FxQIBAE0zEdrwDgIf8fuZBlPiv8sZAcsySrC6q3RCXU3hmDnM7UeMOp4n5SMfojlqau95pq4ZAo0SZBhgpHPTAZCbKoDZAUe5dYWY0m2prKGNvPb8ZB4j07n1B041ZC2Hw3Tukdvp7ZCQbZBozYeiT37oH8D71UW8N2U3PfP1dZCDMs6yV9b";
                    }
                    var isLive = await CheckTokenIsLive(token);
                    if (isLive)
                    {
                        InvokeControlHelper.AppendRichTextboxV2(rtbInfoGetToken, $"{nameOfThread}: {token}", Color.Blue);
                        LogHelper.WriteLog(token);
                    }
                    else
                    {
                        InvokeControlHelper.AppendRichTextboxV2(rtbLogGetToken, $"{nameOfThread}: {token}", Color.Black);
                    }
                    _totalTokenCheck++;
                    InvokeControlHelper.UpdateLabel(lbInfoGetToken, $"Số TOKEN đã check: {_totalTokenCheck}", Color.Blue);
                }
                catch (Exception e)
                {
                    string text = $"{nameOfThread}: Exception={e.Message}, InnerException={e.InnerException?.Message}";
                    InvokeControlHelper.AppendRichTextboxV2(rtbInfoGetToken, text, Color.Red);
                    LogHelper.WriteLog(text);
                }
                Thread.Sleep(_timeSleepCheckToken);
            }
        }

        private async Task<bool> CheckTokenIsLive(string token)
        {
            try
            {
                var http = new HttpClient();
                var url = "https://graph.facebook.com/v1.0/me?access_token=" + token;
                var response = await http.GetAsync(url);
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                string text = $"CheckTokenIsLive: Exception={e.Message}, InnerException={e.InnerException?.Message}";
                InvokeControlHelper.AppendRichTextboxV2(rtbInfoGetToken, text, Color.Red);
                LogHelper.WriteLog(text);
                return false;
            }
        }

        private void btnCopyGetToken_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(rtbLogGetToken.Text);
        }

        private void btnStartCrawlGroupPost_Click(object sender, EventArgs e)
        {
            if (btnStartCrawlGroupPost.Text == "Start")
            {
                var groupId = tbGroupId.Text;
                //groupId = "2727477237475869";
                btnStartCrawlGroupPost.Text = "Stop";
                _threadCrawlGroupPost = new Thread(async () => await GetListPostInGroup(groupId));
                _threadCrawlGroupPost.Start();
                rtbCrawlGroupPostInfo.Text = rtbCrawlGroupPostInfo.Text + Environment.NewLine + "Start Crawl Data group - " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine;
            }
            else
            {
                btnStartCrawlGroupPost.Text = "Start";
                _threadCrawlGroupPost.Abort();
                rtbCrawlGroupPostInfo.Text = rtbCrawlGroupPostInfo.Text + Environment.NewLine + "Stop Crawl Influencer - " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine;
            }
        }

        private async Task GetListPostInGroup(string groupId)
        {
            using (FbToolEntities db = new FbToolEntities())
            {
                string token = ListHelper<TokenCookie>.GetRandomItemInListObject(Constant.LIST_TOKEN_COOKIE).Token;
                var url = "https://graph.facebook.com/" + groupId + "/feed?limit=100&access_token=" + token;
                CrawlPostGroupRoot root = new CrawlPostGroupRoot();
                do
                {
                    try
                    {
                        var result = "";
                        do
                        {
                            try
                            {
                                result = await HttpClientHelper.SendRequestAsync(url, ListHelper<TokenCookie>.GetRandomItemInListObject(Constant.LIST_TOKEN_COOKIE).Cookie);
                            }
                            catch (Exception e)
                            {
                                Constant.LIST_TOKEN.Remove(token);
                                token = ListHelper<TokenCookie>.GetRandomItemInListObject(Constant.LIST_TOKEN_COOKIE).Token;
                                url = "https://graph.facebook.com/" + groupId + "/feed?limit=100&access_token=" + token;
                                InvokeControlHelper.AppendRichTextboxV2(rtbCrawlGroupPostException, $"Exception trong DoWhile GetListPostInGroup.Exception={e.Message},InnerException={e.InnerException?.Message}, url={url}", Color.Red);
                            }

                        } while (string.IsNullOrEmpty(result));

                        root = JsonConvert.DeserializeObject<CrawlPostGroupRoot>(result);
                        if (root.Data == null)
                        {
                            Thread.Sleep(5000);
                            InvokeControlHelper.AppendRichTextboxV2(rtbCrawlGroupPostException, $"Nghỉ 5s sau khi lấy được {totalPostCrawed}");
                            continue;
                        }
                        foreach (var postDto in root.Data)
                        {
                            try
                            {
                                await AddOrUpdatePostGroup(postDto);
                                totalPostCrawed++;
                                InvokeControlHelper.UpdateLabel(lbCrawlGroupPostStatus, $"Crawled {totalPostCrawed} bài post", Color.Blue);
                            }
                            catch (Exception e)
                            {
                                InvokeControlHelper.AppendRichTextboxV2(rtbCrawlGroupPostException, $"Chạy vào foreach AddOrUpdatePostGroup,Exception={e.Message},InnerException={e.InnerException?.Message},Root={root},postDto.Fb_Id={postDto.Fb_Id}", Color.Red);
                            }
                        }
                        if (!string.IsNullOrEmpty(root.Paging?.Next))
                        {
                            url = Regex.Replace(root.Paging?.Next, @"EAAA\w+", ListHelper<TokenCookie>.GetRandomItemInListObject(Constant.LIST_TOKEN_COOKIE).Token);
                        }
                    }
                    catch (Exception e)
                    {
                        InvokeControlHelper.AppendRichTextboxV2(rtbCrawlGroupPostException, $"Chạy vào GetListPostInGroup,Exception={e.Message},InnerException={e.InnerException?.Message}.Root={root}", Color.Red);
                    }

                } while (root.Data?.Count > 0 || root.Data == null);
                MessageBox.Show("Xong");
            }
        }


        private async Task AddOrUpdatePostGroup(CrawlPostDTO model)
        {
            using (var db = new FbToolEntities())
            {
                var postInDb = db.CrawlPostGroup_Post.Where(x => x.Fb_Id == model.Fb_Id).Include(x => x.CrawlPostGroup_Action).FirstOrDefault();
                if (postInDb == null)
                {
                    postInDb = new CrawlPostGroup_Post();
                    postInDb.TimeCreatedInDb = DateTime.Now;
                    db.CrawlPostGroup_Post.Add(postInDb);
                    db.SaveChanges();
                    InvokeControlHelper.AppendRichTextboxV2(rtbCrawlGroupPostInfo, $"Thêm mới Post với Id = {model.Fb_Id}", Color.Blue);
                }
                if (postInDb != null)
                {
                    db.Entry(postInDb).CurrentValues.SetValues(model);
                    postInDb.TimeUpdatedInDb = DateTime.Now;
                    #region Action
                    foreach (var action in postInDb.CrawlPostGroup_Action.ToList())
                    {
                        if (!model.Actions.Any(c => c.Name == action.Name))
                            db.CrawlPostGroup_Action.Remove(action);
                    }
                    // Update and Insert children
                    foreach (var actionDto in model.Actions)
                    {
                        var existingChildAction = postInDb.CrawlPostGroup_Action
                            .Where(c => c.Name == actionDto.Name)
                            .FirstOrDefault();

                        if (existingChildAction != null)
                            // Update child
                            db.Entry(existingChildAction).CurrentValues.SetValues(actionDto);
                        else
                        {
                            // Insert child
                            var newChild = new CrawlPostGroup_Action
                            {
                                Name = actionDto.Name,
                                Link = actionDto.Link
                            };
                            postInDb.CrawlPostGroup_Action.Add(newChild);
                        }
                    }
                    #endregion

                    #region Privacy
                    var existingChildPrivacy = postInDb.CrawlPostGroup_Privacy.FirstOrDefault();
                    if (existingChildPrivacy != null)
                    {
                        // Update child
                        db.Entry(existingChildPrivacy).CurrentValues.SetValues(model.Privacy);
                    }
                    else
                    {
                        // Insert child
                        var newChild = new CrawlPostGroup_Privacy()
                        {
                            Value = model.Privacy.Value,
                            Description = model.Privacy.Description,
                            Friends = model.Privacy.Friends,
                            Allow = model.Privacy.Allow,
                            Deny = model.Privacy.Deny
                        };
                        postInDb.CrawlPostGroup_Privacy.Add(newChild);
                    }
                    #endregion

                    #region Comment
                    foreach (var comment in postInDb.CrawlPostGroup_Comment.ToList())
                    {
                        if (!model.Comments.Data.Any(c => c.Fb_Id == comment.Fb_Id))
                            db.CrawlPostGroup_Comment.Remove(comment);
                    }
                    // Update and Insert children
                    if (model.Comments?.Data?.Count > 0)
                    {
                        foreach (var commentDto in model.Comments?.Data)
                        {
                            var existingChildComment = postInDb.CrawlPostGroup_Comment
                                .Where(c => c.Fb_Id == commentDto.Fb_Id)
                                .FirstOrDefault();

                            if (existingChildComment != null)
                                // Update child
                                db.Entry(existingChildComment).CurrentValues.SetValues(commentDto);
                            else
                            {
                                // Insert child
                                var newChild = new CrawlPostGroup_Comment()
                                {
                                    CreatedTime = DateTimeHelper.ConvertStringFromApiFacebookToDateTime(commentDto.CreatedTimeFromApi),
                                    Message = commentDto.Message,
                                    CanRemove = commentDto.CanRemove,
                                    LikeCount = commentDto.LikeCount,
                                    UserLike = commentDto.UserLikes,
                                    Fb_Id = commentDto.Fb_Id
                                };
                                postInDb.CrawlPostGroup_Comment.Add(newChild);
                            }
                        }
                    }
                    #endregion
                }
                db.SaveChanges();
                InvokeControlHelper.AppendRichTextboxV2(rtbCrawlGroupPostInfo, $"Update thành công bài Post với Id = {model.Fb_Id}", Color.Green);
                await UpdatePostDetail(db, postInDb);
                InvokeControlHelper.AppendRichTextboxV2(rtbCrawlGroupPostInfo, $"Update thành công Detail bài Post với Id = {model.Fb_Id}", Color.Green);
                Thread.Sleep(100);
            }
        }

        private async Task UpdatePostDetail(FbToolEntities db, CrawlPostGroup_Post dbPostInDb)
        {
            try
            {
                var postDetailInDb = db.CrawlPostGroup_PostDetail.FirstOrDefault(x => x.CrawlPostGroup_PostId == dbPostInDb.Id);
                //string cookie =
                //    "sb=srBAX2Yf70eNGh8YE48Rh9uV; datr=MbZAX5ch9yeZ8S7kWRJ-avy4; c_user=100001578994326; dpr=1.25; spin=r.1002600685_b.trunk_t.1598977068_s.1_v.2_; xs=44%3A-H2B8TYHbDoigw%3A2%3A1598151193%3A19558%3A6330%3A%3AAcUH9rZg5FSLhbO4I8oVAqiKtPcc_0fH95G7F6NZ2i0; fr=1nKtY59ZJSUFo6uYR.AWUxrD6u02zOlIw2BoY3euBzJOI.BfQLCy.TG.F9K.0.0.BfToJd.AWWJZZlx; wd=574x722; presence=EDvF3EtimeF1598981783EuserFA21B01578994326A2EstateFDt3F_5b_5dElm3FnullEutc3F1598981783875G598981783915CEchF_7bCC";
                var tempPostDetail = await PostGroupHelper.GetPostDetailDTOAsync(dbPostInDb.Fb_Id, ListHelper<TokenCookie>.GetRandomItemInListObject(Constant.LIST_TOKEN_COOKIE).Cookie);
                //string cookie = ListHelper.GetRandomItemInList(Constant.LIST_COOKIE);
                //var tempPostDetail = await PostGroupHelper.GetPostDetailDTOAsync(dbPostInDb.Fb_Id, cookie);
                if (postDetailInDb != null)
                {
                    db.Entry(postDetailInDb).CurrentValues.SetValues(tempPostDetail);
                    postDetailInDb.TimeUpdatedInDb = DateTime.Now;
                }
                else
                {
                    var newPostDetail = new CrawlPostGroup_PostDetail();
                    newPostDetail.TimeCreatedInDb = DateTime.Now;
                    newPostDetail.CrawlPostGroup_PostId = dbPostInDb.Id;
                    newPostDetail.Name = tempPostDetail.Name;
                    newPostDetail.UID = tempPostDetail.UID;
                    newPostDetail.ReactionTotalCount = tempPostDetail.ReactionTotalCount;
                    newPostDetail.LikeCount = tempPostDetail.LikeCount;
                    newPostDetail.LoveCount = tempPostDetail.LoveCount;
                    newPostDetail.WowCount = tempPostDetail.WowCount;
                    newPostDetail.SupportCount = tempPostDetail.SupportCount;
                    newPostDetail.HahaCount = tempPostDetail.HahaCount;
                    newPostDetail.SadCount = tempPostDetail.SadCount;
                    newPostDetail.AngryCount = tempPostDetail.AngryCount;
                    newPostDetail.ShareCount = tempPostDetail.ShareCount;
                    newPostDetail.CommentCount = tempPostDetail.CommentCount;
                    db.CrawlPostGroup_PostDetail.Add(newPostDetail);
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                InvokeControlHelper.AppendRichTextboxV2(rtbInfoGetToken, $"Exception UpdatePostDetail.PostId = {dbPostInDb?.Id}. Exception = {e.Message}, InnerException ={e.InnerException?.Message}", Color.Red);
            }

        }

        private void btnStartGetDetailPostGroup_Click(object sender, EventArgs e)
        {

        }
    }
}
