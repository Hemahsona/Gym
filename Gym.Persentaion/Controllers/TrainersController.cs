using Gym.BusinessLogic;
using Gym.BusinessLogic.Services;
using Gym.BusinessLogic.ViewModels.Trainer;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error!);
                TempData["Error"] = "trainer creation failed";
                return View(model);
            }
            TempData["Success"] = "trainer created successfully";
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {
            var result = await trainer.GetDetailsAsync(id, ct);
            if (!result.IsSuccess)
                return NotFound();
            return View(result.Value);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var result = await trainer.GetForEditAsync(id, ct);
            if(!result.IsSuccess)
                return RedirectToAction(nameof(Index));
            return View(result.Value);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(TrainerEditViewModel model, int id, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await trainer.EditAsync(model, id, ct);
            if(!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error!);
                TempData["Error"] = "trainer edit failed";
                return View(model);
            }
            TempData["success"] = "Trainer Edit successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var result = await trainer.GetDetailsAsync(id, ct);
            if (result == null)
                return NotFound();
            ViewBag.id = result.Value.Id;
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
        {
            var result = await trainer.DeleteAsync(id, ct);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error!);
                TempData["Error"] = "trainer deletion failed";
                return View();
            }
            TempData["Success"] = "trainer deleted successfully";
            return RedirectToAction(nameof(Index));
        }

    }
}
