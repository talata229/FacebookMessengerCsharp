using FacebookMessengerCsharp.Helper;
using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace FacebookMessengerCsharp.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            #region Chay ngon
            //// Run example
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = await schedulerFactory.GetScheduler();
            IJobDetail job = JobBuilder.Create<LikePostJob>()
                .WithIdentity("name", "group")
                .Build();
            ITrigger trigger = TriggerBuilder.Create()
                //.WithCronSchedule("0 0/1 * * * ?") //1 phut 1 lan
                .WithCronSchedule("0 0 0/1 1/1 * ? *") //1 tieng 1 lan
                .StartNow()
                .Build();
            await scheduler.ScheduleJob(job, trigger);
            await scheduler.Start();

            Basic_Usage.Run().GetAwaiter().GetResult();

            //// Wait for keypress
            //System.Console.ReadKey();
            #endregion


            //// Instantiate FBClient
            //FBClient_Cookies client = new FBClient_Cookies();
            //try
            //{
            //    await client.TryLogin();
            //}
            //catch
            //{
            //    // Read email and pw from console
            //    System.Console.WriteLine("Insert Facebook email:");
            //    var email = System.Console.ReadLine();
            //    System.Console.WriteLine("Insert Facebook password:");
            //    var password = System.Console.ReadLine();

            //    // Login with username and password
            //    await client.DoLogin(email, password);
            //}

            ////send message
            //await client.SendRandomMessageToListUser(ListHelper.ListUser, ListHelper.ListMessage);
            ////// Test
            ////await FacebookToolHelper.ReactionAllPost();




            //await scheduler.Shutdown();
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
