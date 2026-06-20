using Gym.BusinessLogic.Services;
using Gym.BusinessLogic.ViewModels.MebmerShip;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym.Persentaion.Controllers
{
    public class MemberShipsController(IMemberShipService memberShip) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var memberShips = await memberShip.GetAllAsync(ct);
            return View(memberShips.value);
        }

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken ct)
        {
            var model = new CreateMemberShipViewModel();
            var memberResult = await memberShip.GetMemberLockupAsync(ct);
            var planResult = await memberShip.GetPlanLockupAsync(ct);
            model.Members = memberResult.value;
            model.Plans = planResult.value;
            ViewBag.Members = new SelectList(memberResult.value, "Id", "Name");
            ViewBag.Plans = new SelectList(planResult.value, "Id", "Name");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMemberShipViewModel model, CancellationToken ct)
        {
            var result = await memberShip.CreateAsync(model, ct);
            if (!result.success)
            {
                ModelState.AddModelError(string.Empty, result.error!);
                TempData["Error"] = "memebrShip Creation failed";
                return RedirectToAction(nameof(Index));
            }
            TempData["Success"] = "MemberShip Creation succes";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var deletedmembership = await memberShip.GetById(id, ct);
            if (deletedmembership == null)
                return NotFound();
            ViewBag.id = deletedmembership.value.Id;
            return View(deletedmembership.value);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed( int id, CancellationToken ct)
        {
            var result = await memberShip.DeleteAsync( id, ct);
            if(!result.success)
            {
                ModelState.AddModelError(string.Empty, result.error);
                TempData["Error"] = "memebrShip deletion failed";
                return RedirectToAction(nameof(Index));
            }
            TempData["Success"] = "memebrShip deletion succes";
            return RedirectToAction(nameof(Index));

        }


    }
}


