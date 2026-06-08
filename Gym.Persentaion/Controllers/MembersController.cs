using Gym.BusinessLogic;
using Gym.BusinessLogic.Services;
using Gym.BusinessLogic.ViewModels.Member;
using Gym.DataAccess.Data.Context;
using Gym.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Persentaion.Controllers
{
    public class MembersController(IMemberService member) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var members = await member.GetAllAsync(ct);
            return View(members);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
            => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMemberViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View(model);
            Result result = await member.CreateAsync(model, ct);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error!);
                TempData["Error"] = "Member creation failed";
                return View(model);
            }
            TempData["Success"] = "Member created successfully";
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<IActionResult> Details(int id , CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View();
            var memberDetails = await member.GetDetailsAsync(id , ct);

            if (member == null)
            {
                return NotFound();
            }
            return View(memberDetails);
        }

        [HttpGet]
        public async Task<IActionResult> HealthRecord(int id , CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View();
            var healthRecordDetails = await member.GetHealthRecordDetailsAsync(id , ct);
            if(member is null)
                return NotFound();
            return View(healthRecordDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var editMemberDetails = await member.GetForEditAsync(id, ct);
            return View(editMemberDetails);

        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]int id, EditMemberViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View(model);
            Result result = await member.EditAsync(id, model, ct);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error!);
                TempData["Error"] = "Member creation failed";
                return View(model);
            }
            TempData["Success"] = "Member created successfully";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var deletedMember = await member.GetDetailsAsync(id, ct);
            if (deletedMember == null)
                return NotFound();
            ViewBag.id = deletedMember.Id;
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed([FromRoute] int id, CancellationToken ct)
        {
            Result result = await member.DeleteAsync(id, ct);
            if (!result.IsSuccess)
            {
                TempData["Error"] = "Member deletion failed";
                return View();
            }
            TempData["Success"] = "Member deleted successfully";
            return RedirectToAction(nameof(Index));
        }
}}
