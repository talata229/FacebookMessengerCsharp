using Facebook.DAL.Constants;
using Facebook.DAL.Enum;
using FacebookMessengerCsharp.Client.API;
using FacebookMessengerCsharp.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMessengerCsharp.App
{
    public class FBClient_Cookies_App : MessengerClient
    {
        private static readonly string appName = "FBChat-Sharp";
        private static readonly string sessionFile = "SESSION_COOKIES_core.dat";

        public FBClient_Cookies_App()
        {
            this.Set2FACallback(get2FACode);
        }

        private async Task<string> get2FACode()
        {
            await Task.Yield();
            System.Console.WriteLine("Insert 2FA code:");
            return System.Console.ReadLine();
        }

        protected override async Task onMessage(string mid = null, string author_id = null, string message = null, FB_Message message_object = null, string thread_id = null, ThreadType? thread_type = null, long ts = 0, JToken metadata = null, JToken msg = null)
        {

            //authorId: người gửi
            //thread_id: thread hiện tại
            var userId = this.GetUserUid();
            if (userId != author_id)
            {
                var specialMessage = await FacebookToolHelper.CheckSpecialMessage(message, thread_id);
                switch (specialMessage)
                {
                    case Facebook.DAL.Enum.EnumFeature.NoSpecialFeature:
                    case Facebook.DAL.Enum.EnumFeature.StopAll:
                    case Facebook.DAL.Enum.EnumFeature.RemoveStopAll:
                    case Facebook.DAL.Enum.EnumFeature.Stop5Min:
                        return;
                    case Facebook.DAL.Enum.EnumFeature.TroLyAo:
                        await this.send(new FB_Message
                        {
                            text = ListHelper<string>.GetRandomItemInListObject(Constant.ListConfirmAgreeUseTroLyAoMessage)
                        }, thread_id, ThreadType.USER);
                        return;
                    case Facebook.DAL.Enum.EnumFeature.GirlXinh:
                        string fileNameWithExtension = DownloadHelper.DownloadImageFromUrl(DownloadHelper.RandomImageIdGirl());
                        using (FileStream stream = File.OpenRead(fileNameWithExtension))
                        {
                            await sendLocalFiles(
                                file_paths: new Dictionary<string, Stream>() { { fileNameWithExtension, stream } },
                                message: null,
                                thread_id: author_id,
                                thread_type: ThreadType.USER);
                        }
                        return;
                    case Facebook.DAL.Enum.EnumFeature.TruyenCuoi:
                        List<string> listTruyenCuoi = await FacebookToolHelper.GetListTruyenCuoi();
                        await this.send(new FB_Message
                        {
                            text = ListHelper<string>.GetRandomItemInListObject(listTruyenCuoi)
                        }, thread_id, ThreadType.USER);
                        return;
                    case Facebook.DAL.Enum.EnumFeature.TinTuc:
                        List<NewfeedRss> newfeedRsses = RSSHelper.GetTinMoiNhat();
                        foreach (var newfeed in newfeedRsses)
                        {
                            await this.send(new FB_Message
                            {
                                text = $"{ newfeed.Title} \n{newfeed.Link}"
                            }, thread_id, ThreadType.USER);
                        }
                        return;
                    case Facebook.DAL.Enum.EnumFeature.NoiTu:
                        await FacebookToolHelper.SetUserNoiTu(author_id);
                        await this.send(new FB_Message
                        {
                            text = $"Bắt đầu chơi nối từ tiếng việt thôi!!!"
                        }, thread_id, ThreadType.USER);
                        return;
                    case Facebook.DAL.Enum.EnumFeature.StopNoiTu:
                        await FacebookToolHelper.SetUserStopNoiTu(author_id);
                        await this.send(new FB_Message
                        {
                            text = $"Đã dừng chơi nối từ tiếng việt!"
                        }, thread_id, ThreadType.USER);
                        return;
                    case Facebook.DAL.Enum.EnumFeature.NoiTuTiengAnh:
                        await FacebookToolHelper.SetUserNoiTuTiengAnh(author_id);
                        await this.send(new FB_Message
                        {
                            text = $"Bắt đầu chơi nối từ tiếng Anh thôi!!!"
                        }, thread_id, ThreadType.USER);
                        return;
                    case Facebook.DAL.Enum.EnumFeature.StopNoiTuTiengAnh:
                        await FacebookToolHelper.SetUserStopNoiTuTiengAnh(author_id);
                        await this.send(new FB_Message
                        {
                            text = $"Đã dừng chơi nối từ tiếng Anh!"
                        }, thread_id, ThreadType.USER);
                        return;
                    default:
                        break;
                }
                ConsoleLogHelper.WriteToConsole($"Got new message from {author_id}: {message}");
                var isBlock = await FacebookToolHelper.CheckIsBlockOrNot(author_id);
                if (isBlock)
                {
                    return;
                }
                else
                {
                    //Co the send message o day
                    var agreeSimsimi = await FacebookToolHelper.CheckUserAgreeSimsimi(thread_id);
                    if (await FacebookToolHelper.CheckIsNoiTuTiengVietOrNot(thread_id))
                    {
                        var dic = TuDienHelper.GenerateVietNameseDictionary();
                        var lastWord = message.LastWord();
                        var listNewWord = dic.Keys.Where(x => String.Equals(x.FirstWord(), lastWord, StringComparison.OrdinalIgnoreCase) && x.Contains(" ")).ToList();
                        string textSend = listNewWord.Count == 0 ? "Chịu thua rồi, ko tìm thấy từ nào cả :((" : $"Nối từ: {ListHelper<string>.GetRandomItemInListObject(listNewWord)}";
                        await this.send(new FB_Message
                        {
                            text = textSend
                        }, thread_id, ThreadType.USER);
                        return;
                    }
                    else if (await FacebookToolHelper.CheckIsNoiTuTiengAnhOrNot(thread_id))
                    {
                        var dic = TuDienHelper.GenerateVietNameseDictionary();
                        var lastWord = message.LastWord();
                        var listNewWord = dic.Keys.Where(x => String.Equals(x.FirstWord(), lastWord, StringComparison.OrdinalIgnoreCase) && x.Contains(" ")).ToList();
                        string textSend = listNewWord.Count == 0 ? "Chịu thua rồi, ko tìm thấy từ nào cả :((" : $"Nối từ: {ListHelper<string>.GetRandomItemInListObject(listNewWord)}";
                        await this.send(new FB_Message
                        {
                            text = textSend
                        }, thread_id, ThreadType.USER);
                        return;
                    }
                    else if (!agreeSimsimi)
                    {
                        await this.send(new FB_Message
                        {
                            text = ListHelper<string>.GetRandomItemInListObject(Constant.ListTroLyAoMessage)
                        }, thread_id, ThreadType.USER);
                        await FacebookToolHelper.AddUser10Min(thread_id);
                    }
                    else
                    {
                        string simsimiMessage = await SimsimiHelper.SendSimsimi(message);
                        await this.send(new FB_Message
                        {
                            text = "Trợ lý ảo: " + simsimiMessage
                        }, thread_id, ThreadType.USER);
                        ConsoleLogHelper.WriteToConsole($"Send message = {simsimiMessage} to {thread_id}");
                    }
                }
            }
            else
            {
                //Tự mình gửi đi
                var specialMessage = await FacebookToolHelper.CheckSpecialMessage(message, thread_id);
                switch (specialMessage)
                {
                    case EnumFeature.NoiTu:
                        await FacebookToolHelper.SetUserNoiTu(thread_id);
                        break;
                    case EnumFeature.StopNoiTu:
                        await FacebookToolHelper.SetUserStopNoiTu(thread_id);
                        break;
                }
                await Task.Yield();
            }
        }

        protected override async Task DeleteCookiesAsync()
        {
            try
            {
                await Task.Yield();
                var file = Path.Combine(UserDataFolder, sessionFile);
                File.Delete(file);
            }
            catch (Exception ex)
            {
                this.Log(ex.ToString());
            }
        }

        protected override async Task<Dictionary<string, List<Cookie>>> ReadCookiesFromDiskAsync()
        {
            try
            {
                var file = Path.Combine(UserDataFolder, sessionFile);
                using (var fileStream = File.OpenRead(file))
                {
                    await Task.Yield();
                    using (var jsonTextReader = new JsonTextReader(new StreamReader(fileStream)))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        return serializer.Deserialize<Dictionary<string, List<Cookie>>>(jsonTextReader);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Log(string.Format("Problem reading cookies from disk: {0}", ex.ToString()));
                return null;
            }
        }

        protected override async Task WriteCookiesToDiskAsync(Dictionary<string, List<Cookie>> cookieJar)
        {
            var file = Path.Combine(UserDataFolder, sessionFile);

            using (var fileStream = File.Create(file))
            {
                try
                {
                    using (var jsonWriter = new JsonTextWriter(new StreamWriter(fileStream)))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(jsonWriter, cookieJar);
                        await jsonWriter.FlushAsync();
                    }
                }
                catch (Exception ex)
                {
                    this.Log(string.Format("Problem writing cookies to disk: {0}", ex.ToString()));
                }
            }
        }

        /// <summary>
        /// Get the current user data folder
        /// </summary>
        private static string UserDataFolder
        {
            get
            {
                string folderBase = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string dir = Path.Combine(folderBase, appName.ToUpper());
                return CheckDir(dir);
            }
        }

        /// <summary>
        /// Check the specified folder, and create if it doesn't exist.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private static string CheckDir(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return dir;
        }

        /// <summary>
        /// Send random 1 tin nhắn cho 1 user trong list
        /// </summary>
        /// <param name="users">Danh sách người cần nhắn tin đến</param>
        /// <param name="messages">Tin nhắn cần gửi</param>
        /// <returns></returns>
        public async Task SendRandomMessageToListUser(List<string> users, List<string> messages)
        {
            foreach (var user in users)
            {
                try
                {
                    string text = ListHelper<string>.GetRandomItemInListObject(Constant.ListMessage);
                    await this.setTypingStatus(TypingStatus.TYPING, user, ThreadType.USER);
                    await this.send(new FB_Message { text = text }, user, ThreadType.USER);
                    await this.wave(true, user, ThreadType.USER);
                    System.Console.WriteLine($"Sent -${text}- to -{user}-");
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"Send failed to {user}");
                }

            }
        }
    }
}
