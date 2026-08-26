using Gym.BusinessLogic.Services;
using Gym.BusinessLogic.ViewModels.Plan;
using Gym.DataAccess.Data.Context;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym.Persentaion.Controllers
{
    public class PlansController(IPlanService plan) : Controller
    {

        public async Task<IActionResult> Index(int id, CancellationToken ct) 
        {
            var result = await plan.GetAllAsync(id, ct);


            return View(result.Value); 
        }

        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {

            var result = await plan.GetDetailsAsync(id, ct);
            if (!result.IsSuccess)
            {
                return NotFound();
            }
            return View(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var result = await plan.GetForUpdate(id, ct);
            if (!result.IsSuccess)
                return RedirectToAction(nameof(Index));
            return View(result.Value);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditPlanViewModel model, int id, CancellationToken ct)
        {
            var result = await plan.EditAsync(model, id, ct);
            if (!ModelState.IsValid)
                return View(model);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error!);
                TempData["Error"] = "Plan edit failed";
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]

        public async Task<IActionResult> Activate(int id, CancellationToken ct)
        {
            var result = await plan.ToggleAsync(id, ct);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error!);
                TempData["Error"] = "Cant chnage active plan";
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}