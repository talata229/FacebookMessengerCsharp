using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FacebookTool.Helper
{
    public class LogHelper
    {
        static ReaderWriterLock locker = new ReaderWriterLock();

        public static void WriteDebug(string text)
        {
            try
            {
                locker.AcquireWriterLock(int.MaxValue); //You might wanna change timeout value 
                StreamWriter log;
                FileStream fileStream = null;
                DirectoryInfo logDirInfo = null;
                FileInfo logFileInfo;

                string logFilePath = Environment.CurrentDirectory + "\\Logs\\";
                logFilePath = logFilePath + DateTime.Today.ToString("dd-MM-yyyy") + "-debug." + "txt";
                logFileInfo = new FileInfo(logFilePath);
                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();
                if (!logFileInfo.Exists)
                {
                    fileStream = logFileInfo.Create();
                }
                else
                {
                    fileStream = new FileStream(logFilePath, FileMode.Append);
                }

                log = new StreamWriter(fileStream);
                log.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff tt\t") + text);
                log.Close();
            }
            finally
            {
                locker.ReleaseWriterLock();
            }
        }

        public static async Task WriteLogAsync(string strLog)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            string logFilePath = Environment.CurrentDirectory + "\\Logs\\";
            logFilePath = logFilePath + DateTime.Today.ToString("dd-MM-yyyy") + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }

            log = new StreamWriter(fileStream);
            await log.WriteLineAsync(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff tt\t") + strLog);
            log.Close();
        }

        public static void WriteLog(string strLog)
        {
            try
            {
                locker.AcquireWriterLock(int.MaxValue); //You might wanna change timeout value 
                StreamWriter log;
                FileStream fileStream = null;
                DirectoryInfo logDirInfo = null;
                FileInfo logFileInfo;

                string logFilePath = Environment.CurrentDirectory + "\\Logs\\" + DateTime.Today.ToString("dd-MM-yyyy") +
                                     "\\";
                string fileName =
                    $"{DateTime.Now.Hour}h-{DateTime.Now.Hour + 1}h, Ngày {DateTime.Today.ToString("dd-MM-yyyy")}.txt ";
                logFilePath = logFilePath + fileName;
                logFileInfo = new FileInfo(logFilePath);
                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();
                if (!logFileInfo.Exists)
                {
                    fileStream = logFileInfo.Create();
                }
                else
                {
                    fileStream = new FileStream(logFilePath, FileMode.Append);
                }

                log = new StreamWriter(fileStream);
                log.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff tt\t") + strLog);
                log.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                locker.ReleaseWriterLock();
            }
        }


        public static void WriteLogIntoCommonFile(string strLog)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            string logFilePath = Environment.CurrentDirectory + "\\Logs\\";
            logFilePath = logFilePath + "commons.txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }

            log = new StreamWriter(fileStream);
            log.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff tt\t") + strLog);
            log.Close();
        }
    }
}
