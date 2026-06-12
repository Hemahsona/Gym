using Gym.BusinessLogic.Services;
using Gym.BusinessLogic.ViewModels.Session;

using Microsoft.AspNetCore.Mvc;

namespace Gym.Persentaion.Controllers
{
    public class SessionsController(ISessionService sessionService) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var sessions = await sessionService.GetIndexItemsAsync(ct);
            return View(sessions);
        }
    }
}
