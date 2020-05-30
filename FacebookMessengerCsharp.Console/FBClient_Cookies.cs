using Facebook.DAL.Constants;
using FacebookMessengerCsharp.Client.API;
using FacebookMessengerCsharp.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace FacebookMessengerCsharp.Console
{
    // Like FBClient_Simple but also saves session cookies
    public class FBClient_Cookies : MessengerClient
    {
        private static readonly string appName = "FBChat-Sharp";
        private static readonly string sessionFile = "SESSION_COOKIES_core.dat";

        public FBClient_Cookies()
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
                await FacebookToolHelper.CheckSpecialMessage(message, thread_id);
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
                    if (!agreeSimsimi)
                    {

                        await this.send(new FB_Message
                        {
                            text = ListHelper.GetRandomItemInList(Constant.ListTroLyAoMessage)
                        }, thread_id, ThreadType.USER);
                        await FacebookToolHelper.AddUser5Min(thread_id);
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
                await FacebookToolHelper.CheckSpecialMessage(message, thread_id);
            }
            await Task.Yield();
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
                    string text = ListHelper.GetRandomItemInList(ListHelper.ListMessage);
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
