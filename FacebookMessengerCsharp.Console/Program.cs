using System.Collections.Generic;
using FacebookMessengerCsharp.Helper;
using Quartz;
using Quartz.Impl;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMessengerCsharp.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            System.Console.OutputEncoding = Encoding.Unicode;
            //Dictionary<string, string> dic = TuDienHelper.GenerateVietNameseDictionary();
            #region Chay ngon
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = await schedulerFactory.GetScheduler();
            IJobDetail job = JobBuilder.Create<LikePostJob>()
                .WithIdentity("name", "group")
                .Build();
            ITrigger trigger = TriggerBuilder.Create()
                //.WithCronSchedule("0 0/1 * * * ?") //1 phut 1 lan
                //.WithCronSchedule("0 0 0/1 1/1 * ? *") //1 tieng 1 lan  //SESSION_COOKIES_core.dat
                .WithCronSchedule("0 0/30 * 1/1 * ? *") //30 phút 1 lần
                .StartNow()
                .Build();
            await scheduler.ScheduleJob(job, trigger);
            await scheduler.Start();
            Basic_Usage.Run().GetAwaiter().GetResult();
            #endregion
            System.Console.WriteLine("Done!");
            System.Console.ReadKey();
        }

        public class LikePostJob : IJob
        {
            //public string Name { get; set; }
            public Task Execute(IJobExecutionContext context)
            {
                return Task.Factory.StartNew(async () =>
                {
                    await FacebookToolHelper.ReactionAllPost();
                });
            }
        }
    }
}
