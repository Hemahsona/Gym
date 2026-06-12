using Gym.DataAccess.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Persentaion.Controllers
{
    public class SessionsController(ISessionQueryService sessionQueryService) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var sessions = await sessionQueryService.GetIndexItemsAsync(ct);
            return View(sessions);
        }
    }
}
