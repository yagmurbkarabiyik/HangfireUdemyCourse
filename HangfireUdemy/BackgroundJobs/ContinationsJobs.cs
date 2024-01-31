using System.Diagnostics;

namespace HangfireUdemy.BackgroundJobs
{
    public class ContinationsJobs
    {


        public static void WriteWatermarkStatusJob(string id, string fileName)
        {
            Hangfire.BackgroundJob.ContinueJobWith(id, () => Debug.WriteLine($"{fileName} : resim' watermark eklenmiştir."));
        }
    }
}
