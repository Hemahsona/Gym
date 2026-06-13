using Gym.BusinessLogic;
using Gym.BusinessLogic.Services;
using Gym.BusinessLogic.ViewModels.Trainer;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Persentaion.Controllers
{
    public class TrainersController(ITrainerService trainer) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var trainers = await trainer.GetAllAsync(ct);
            return View(trainers);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
            => View();
        [HttpPost]
        public async Task<IActionResult> Create(TrainerCreateViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View(model);
            Result result = await trainer.CreateAsync(model, ct);
            if (!result.success)
            {
                ModelState.AddModelError(string.Empty, result.error!);
                TempData["Error"] = "trainer creation failed";
                return View(model);
            }
            TempData["Success"] = "trainer created successfully";
            return RedirectToAction(nameof(Index));

        }
    }
}
