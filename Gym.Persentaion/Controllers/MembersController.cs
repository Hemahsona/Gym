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
            var result = await member.CreateAsync(model, ct);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Email or Phone already exists");
                return View(model);
            }
            return RedirectToAction(nameof(Index));

        }
        
    }
}
