using System.Drawing;

namespace HangfireUdemy.BackgroundJobs
{
    public class DelayedJobs
    {

        public static string  AddWatermarkJob(string fileName, string watermarksText)
        {
            return Hangfire.BackgroundJob.Schedule(() => ApplyWatermark(fileName, watermarksText), TimeSpan.FromSeconds(20));
        }

        public static void ApplyWatermark(string fileName, string watermartText)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", fileName);

            using (var bitmap = Bitmap.FromFile(path))
            {
                using (Bitmap tempBitmap = new Bitmap(bitmap.Width, bitmap.Height))
                {
                    using (Graphics grp = Graphics.FromImage(tempBitmap))
                    {
                        grp.DrawImage(bitmap, 0, 0);

                        var font = new Font(FontFamily.GenericSansSerif, 5, FontStyle.Bold);
                        var color = Color.FromArgb(255, 0, 0);
                        var brush = new SolidBrush(color);
                        var point = new Point(20, bitmap.Height-50);

                        grp.DrawString(watermartText, font, brush, point);  

                        tempBitmap.Save(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos/watermarks", fileName));
                    }
                }
            }
        }
    }
}
