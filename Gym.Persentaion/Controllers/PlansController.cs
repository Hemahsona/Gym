using Gym.DataAccess.Data.Context;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Gym.Persentaion.Controllers
{
    public class PlansController : Controller
    {
        private readonly IRepository<Plan> _planRepository;
        public PlansController(IRepository<Plan> planRepository) 
        { 
            _planRepository = planRepository; 
        }
        public async Task<IActionResult> Index() 
        {
            var plan = await _planRepository.GetAllAsync();
            return View(plan); 
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0) 
                return NotFound(); 
            var plan = await _planRepository.GetByIdAsync(id);
            if (plan is null) return RedirectToAction(nameof(Index));
            return View(plan); 
        }
    }
}