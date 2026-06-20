using Gym.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Persentaion.Controllers
{
    public class HomeController(IDashBoardService dashBoardService) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var result = await dashBoardService.GetHomePageAsync(ct);

            return View(result.value);
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
