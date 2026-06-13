using Gym.BusinessLogic.Services;
using Gym.BusinessLogic.ViewModels.Session;
using Gym.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Gym.DataAccess.Models;

namespace Gym.Persentaion.Controllers
{
    public class SessionsController(ISessionService sessionService) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var sessions = await sessionService.GetIndexItemsAsync(ct);
            return View(sessions);
        }

        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {
            var result = await sessionService.GetDetailsAsync(id, ct);
            if (!result.success)
            {
                return NotFound();
            }
            return View(result.value);
        }

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken ct)
        {
            var model = new SessionCreateViewModel();
            var categoriesResult = await sessionService.GetGategoryAsync(ct);
            var trainersResult = await sessionService.GetTrainersAsync(ct);
            model.Categories = categoriesResult.value ?? [];
            model.Trainers = trainersResult.value ?? [];
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SessionCreateViewModel model, CancellationToken ct)
        {


            var result = await sessionService.CreateAsync(model, ct);

            return RedirectToAction(nameof(Index));
        }

        //private async Task PopulateLookupsAsync(SessionCreateViewModel model, CancellationToken ct)
        //{
        //    var categoriesResult = await sessionService.GetGategoryAsync(ct);
        //    var trainersResult = await sessionService.GetTrainersAsync(ct);
        //    model.Categories = categoriesResult.value ?? [];
        //    model.Trainers = trainersResult.value ?? [];
        //}
    }
}
