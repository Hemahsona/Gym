using Gym.DataAccess.Data.Context;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym.Persentaion.Controllers
{
    public class PlansController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PlansController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork; 
        }
        public async Task<IActionResult> Index() 
        {
            var plan = await _unitOfWork.Plans.GetAllAsync();
            return View(plan); 
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0) 
                return NotFound(); 
            var plan = await _unitOfWork.Plans.GetByIdAsync(id);
            if (plan is null) return RedirectToAction(nameof(Index));
            return View(plan); 
        }
    }
}