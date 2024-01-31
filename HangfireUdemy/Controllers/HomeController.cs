using HangfireUdemy.BackgroundJobs;
using HangfireUdemy.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HangfireUdemy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SignUp()
        {
            FireAndForgetJobs.EmailSendToUserJob("1234", "Bu bir test mailidir");

            return View();
        }

        public IActionResult PhotoSave()
        {
            BackgroundJobs.RecurringJobs.ReportingJob();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo)
        {
            string newFileName = String.Empty;

            if (photo != null && photo.Length > 0)
            {
                newFileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", newFileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

                string jobId = BackgroundJobs.DelayedJobs.AddWatermarkJob(newFileName, "www.mysite.com");

                BackgroundJobs.ContinationsJobs.WriteWatermarkStatusJob(jobId, newFileName);
            }
            return View();
        }
    }
}