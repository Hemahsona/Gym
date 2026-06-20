using Gym.BusinessLogic.Services;
using Gym.BusinessLogic.ViewModels.Session;
using Gym.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Gym.DataAccess.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gym.DataAccess.Repositories;
using Microsoft.VisualBasic;

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
        public async Task<IActionResult> Create(int id, CancellationToken ct)
        {
            var model = new SessionCreateViewModel();
            var categoriesResult = await sessionService.GetGategoryAsync(ct);
            var trainersResult = await sessionService.GetTrainersAsync(id, ct);
            model.Categories = categoriesResult.value ?? [];
            model.Trainers = trainersResult.value ?? [];
            return View(model);
        }

        //[HttpGet]
        //public async Task<IActionResult> TrainerByCategory(int id, CancellationToken ct)
        //{
        //    var result = await sessionService.GetTrainersAsync(id, ct);
        //    if(!result.success)
        //        return BadRequest(result.value);
        //    return Json(result.value);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SessionCreateViewModel model, CancellationToken ct)
        {

            //var categoriesResult = await sessionService.GetGategoryAsync(ct);
            var result = await sessionService.CreateAsync(model, ct);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken ct)       
        {
            var model = new SessionEditViewModel();
            var result = await sessionService.GetForEditAsync(id);
            var trainersResult = await sessionService.GetTrainersAsync(id, ct);
            var gategoryResult = await sessionService.GetGategoryAsync(ct);
            model.Trainers = trainersResult.value;
            model.Categories = gategoryResult.value;
            //Console.WriteLine(model.Trainers?.Count);
            //Console.WriteLine(model.Categories?.Count);
            ViewBag.Trainers = new SelectList( trainersResult.value,"Id","Name");
            ViewBag.Gategory = new SelectList(gategoryResult.value, "Id", "Name");
            return View(result.value);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SessionEditViewModel model, int id, CancellationToken ct)
        {

            if(ModelState.IsValid)
                return View(model);
            var result = await sessionService.EditAsync(model, id,ct);
            if (!result.success)
            {
                ModelState.AddModelError(string.Empty, result.error!);
                TempData["Error"] = "Session edit failed";
                var gategoryResult = await sessionService.GetGategoryAsync(ct);
                var trainersResult = await sessionService.GetTrainersAsync(id, ct);
                return View(model);
            }
            TempData["Success"] = "Session edit successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete (int id , CancellationToken ct)
        {
           

            var deletedSession = await sessionService.GetDetailsAsync(id, ct);
            if (deletedSession == null)
                return NotFound();
            ViewBag.id = deletedSession.value.Id;
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed([FromRoute] int id, CancellationToken ct)
        {
            Result result = await sessionService.DeleteAsync(id, ct);
            if (!result.success)
            {
                TempData["Error"] = "session deletion failed";
                return View();
            }
            TempData["Success"] = "session deleted successfully";
            return RedirectToAction(nameof(Index));
        }

    }
}
