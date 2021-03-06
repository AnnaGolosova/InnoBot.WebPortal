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
            ViewBag.GroupedQuestions = await _httpService.GetGroupedQuestions();
            ViewBag.Feedbacks = await _httpService.GetFeedbacksAsync();
            ViewBag.Presentations = await _httpService.GetPresentationsAsync();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}