using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FacebookTool.Helper
{
    public class InvokeControlHelper
    {
        private const int MaxLineNumber = 1000;
        private static ConcurrentDictionary<string, int> _lineNumberDic = new ConcurrentDictionary<string, int>();

        public static void UpdateLabel(Label lb, string msg, Color? color = null)
        {
            lb.ForeColor = color ?? Color.Black;
            lb.Invoke(new Action(() => lb.Text = msg));
        }

        public static void SetTextbox(TextBox tb, string text)
        {
            tb.Invoke(new Action(() =>
            {
                tb.Text = text;
            }));
            if (_lineNumberDic.ContainsKey(tb.Name))
                _lineNumberDic[tb.Name]++;
            else
                _lineNumberDic.TryAdd(tb.Name, 1);
        }

        public static void AppendTextbox(TextBox tb, string text)
        {
            if (!_lineNumberDic.ContainsKey(tb.Name))
                _lineNumberDic.TryAdd(tb.Name, 1);

            tb.Invoke((new Action(() => tb.AppendText(Environment.NewLine + _lineNumberDic[tb.Name] + ". " + DateTime.Now + " " + text))));
            _lineNumberDic[tb.Name]++;

            // reset & save text when total line reach max line number
            if (_lineNumberDic[tb.Name] % MaxLineNumber == 0)
            {
                if (tb.Name == "tbHistoryVTRL" || tb.Name == "tbSubmitIpToAdwords" || tb.Name == "txtUpdateProcess")
                    SaveCurrentTextToLog(tb);
                tb.Invoke(new Action(() => tb.Text = string.Empty));
            }
            tb.Invoke(new Action(() => tb.SelectionStart = tb.Text.Length));
            tb.Invoke(new Action(() =>  tb.ScrollToCaret()));

        }
        public static void AppendRichTextbox(RichTextBox tb, string text, Color? color = null, int maxLine = 10000)
        {


            if (!_lineNumberDic.ContainsKey(tb.Name))
                _lineNumberDic.TryAdd(tb.Name, 1);

            tb.Invoke(new Action(() =>
            {
                tb.SelectionColor = color ?? Color.Black;
                tb.AppendText(Environment.NewLine + _lineNumberDic[tb.Name] + ". " + DateTime.Now + " - " + text);
            }));
            _lineNumberDic[tb.Name]++;

            // reset & save text when total line reach max line number
            if (_lineNumberDic[tb.Name] % MaxLineNumber == 0)
            {
                if (tb.Name == "tbHistoryVTRL" || tb.Name == "tbSubmitIpToAdwords" || tb.Name == "txtUpdateProcess")
                    SaveCurrentRichTextToLog(tb);
                tb.Invoke(new Action(() => tb.Text = string.Empty));
            }

            tb.Invoke(new Action(() => tb.SelectionStart = tb.Text.Length));
            tb.Invoke(new Action(() => tb.ScrollToCaret()));

        }
        public static void AppendRichTextboxV2(RichTextBox tb, string msg, Color? color = null, int maxLine = 10000)
        {
            if (tb.IsHandleCreated)
            {

                tb.Invoke(new Action(() =>
                {
                    tb.SelectionColor = color ?? Color.Black;
                    tb.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - " + msg + Environment.NewLine);
                    if (tb.Lines.Length > maxLine)
                    {
                        tb.Clear();
                    }
                }));
            }
            else if (tb.IsDisposed)
            {

            }
            else
            {
                LogHelper.WriteLog($"DateTime:{DateTime.Now}. Nhảy vào AppendRichTextbox");
                tb.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - " + msg + Environment.NewLine);
            }
        }

        public static void AppendRichTextboxV3(RichTextBox tb, Label lb, string msg, Color? color = null, int maxLine = 10000)
        {
            if (tb.IsHandleCreated)
            {

                tb.Invoke(new Action(() =>
                {
                    tb.SelectionColor = color ?? Color.Black;
                    tb.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - " + msg + Environment.NewLine);
                    if (tb.Lines.Length > maxLine)
                    {
                        tb.Clear();
                    }
                }));
                lb.Invoke(new Action(() =>
                {
                    lb.ForeColor = color ?? Color.Black;
                    lb.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - " + msg + Environment.NewLine;
                }));
            }
            else if (tb.IsDisposed)
            {

            }
            else
            {
                LogHelper.WriteLog($"DateTime:{DateTime.Now}. Nhảy vào AppendRichTextbox");
                tb.AppendText(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - " + msg + Environment.NewLine);
            }
        }
        public static void AppendLineTextbox(TextBox tb, string text, int reset = 1)
        {
            if (!_lineNumberDic.ContainsKey(tb.Name))
                _lineNumberDic.TryAdd(tb.Name, 1);
            if (reset == 0)
            {
                _lineNumberDic[tb.Name] = 1;
                tb.Invoke(new Action(() => tb.Text = string.Empty));
            }

            tb.Invoke(new Action(() => tb.AppendText(Environment.NewLine + _lineNumberDic[tb.Name] + ". " + DateTime.Now + " " + text)));
            _lineNumberDic[tb.Name]++;
        }

        public static void AppendTextbox(TextBox tb, string text, bool onlyAppend)
        {
            tb.Invoke(new Action(() => tb.AppendText(text + Environment.NewLine)));
        }

        public static void ClearTextbox(TextBox tb)
        {
            tb.Invoke(new Action(() => tb.Clear()));
        }

        public static void DisableButton(Button bt)
        {
            bt.Invoke(new Action(() => bt.Enabled = false));
        }

        public static void EnableButton(Button bt)
        {
            bt.Invoke(new Action(() => bt.Enabled = true));
        }

        public static void SetButtonText(Button bt, string text)
        {
            bt.Invoke(new Action(() => bt.Text = text));
        }

        public static void SaveCurrentTextToLog(TextBox tb)
        {
            try
            {
                var currentText = tb.Text;
                var modifiedText = new StringBuilder();
                var excepttionText = new StringBuilder();
                using (StringReader reader = new StringReader(currentText))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains("cs:line") || line.Contains("Exception"))
                            excepttionText.AppendLine(tb.Name + " - " + line);
                        modifiedText.AppendLine(tb.Name + " - " + line);
                    }
                }

                File.AppendAllText(
                    Path.GetDirectoryName(Application.ExecutablePath) +
                    string.Format("/Logs/log_{0}_{1}_{2}.txt", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                    modifiedText.ToString());
                if (excepttionText.Length > 0)
                    File.AppendAllText(
                    Path.GetDirectoryName(Application.ExecutablePath) +
                    string.Format("/Exceptions/Exception_{0}_{1}_{2}.txt", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                    excepttionText.ToString());
            }
            catch (Exception ex)
            {
                AppendTextbox(tb, ex.ToString());
            }
        }
        public static void SaveCurrentRichTextToLog(RichTextBox tb)
        {
            try
            {
                var currentText = tb.Text;
                var modifiedText = new StringBuilder();
                var excepttionText = new StringBuilder();
                using (StringReader reader = new StringReader(currentText))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains("cs:line") || line.Contains("Exception"))
                            excepttionText.AppendLine(tb.Name + " - " + line);
                        modifiedText.AppendLine(tb.Name + " - " + line);
                    }
                }

                File.AppendAllText(
                    Path.GetDirectoryName(Application.ExecutablePath) +
                    string.Format("/Logs/log_{0}_{1}_{2}.txt", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                    modifiedText.ToString());
                if (excepttionText.Length > 0)
                    File.AppendAllText(
                    Path.GetDirectoryName(Application.ExecutablePath) +
                    string.Format("/Exceptions/Exception_{0}_{1}_{2}.txt", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                    excepttionText.ToString());
            }
            catch (Exception ex)
            {
                AppendRichTextbox(tb, ex.ToString());
            }
        }
    }

}
