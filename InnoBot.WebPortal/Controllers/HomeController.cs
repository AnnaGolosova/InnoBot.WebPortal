using InnoBot.WebPortal.Models;
using InnoBot.WebPortal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InnoBot.WebPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpService _httpService;

        public HomeController(ILogger<HomeController> logger, IHttpService httpService)
        {
            _logger = logger;
            _httpService=httpService;
        }

        public async Task<IActionResult> Index()
        {
            var currentMeetupId = new Guid("7ef9eade-92a2-4277-94df-45b802157ef3");
            var presentations = await _httpService.GetPresentationsForMeetupAsync(currentMeetupId);
            var presentationIds = presentations.Select(x => x.Id).ToList();

            ViewBag.GroupedQuestions = await _httpService.GetGroupedQuestions(currentMeetupId, presentationIds);
            ViewBag.Feedbacks = await _httpService.GetFeedbacksAsync(currentMeetupId);
            ViewBag.Presentations = presentations;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}