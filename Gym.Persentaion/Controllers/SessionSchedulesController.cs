using Gym.BusinessLogic;
using Gym.BusinessLogic.Services;
using Gym.BusinessLogic.ViewModels.SessionSchedules;
using Gym.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym.Persentaion.Controllers
{
    public class SessionSchedulesController(ISessionScheduleService sessionSchedule) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var sessionSchedules = await sessionSchedule.GetAllAsync(ct);
            return View(sessionSchedules.Value);
        }
        public async Task<IActionResult> UpcomingMember(int id, CancellationToken ct)
        {
            //var result = await sessionSchedule.GetById(id, ct);
            var booking = await sessionSchedule.GetBookingsBySeesionId(id, ct);
            var session = await sessionSchedule.GetSessionById(id, ct);
            var result = new UpcomingMemberPageViewModel
            {
                Bookings = booking.Value.ToList(),
                Session = session.Value
            };
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id, CancellationToken ct) 
        {
            //var session = await sessionSchedule.GetById(id, ct);

            var model = new CreateSessionScheduleViewModel()
            {
                SessionId = id
            };
            //model.SessionId = session.value.Id;
            var member = await sessionSchedule.GetMemberAsync(ct);

            model.Members = member.Value;
            ViewBag.Members = new SelectList(member.Value, "Id", "Name");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSessionScheduleViewModel model, CancellationToken ct)
        {
            Console.WriteLine($"SessionId = {model.SessionId}");
            Console.WriteLine($"MemberId = {model.MemberId}");
            var result = await sessionSchedule.CreateAsync(model, ct);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error!);
                TempData["Error"] = "Booking Creation failed";
                return RedirectToAction(nameof(Index));
            }
            TempData["Success"] = "Booking Creation succes";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var booking = await sessionSchedule.GetBookingByIdAsync(id, ct);
            if (booking == null)
                return NotFound();
            ViewBag.id = booking.Value.Id;
            return View(booking.Value);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
        {
            var result = await sessionSchedule.DeleteAsync(id, ct);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error);
                TempData["Error"] = "Booking deletion failed";
                return RedirectToAction(nameof(UpcomingMemberPageViewModel));
            }
            TempData["Success"] = "Booking deletion succes";
            return RedirectToAction(nameof(UpcomingMember),
                new {id = result.Value});
        }

        public async Task<IActionResult> Ongoing(int id, CancellationToken ct)
        {
            var booking = await sessionSchedule.GetOngoingByBookingId(id, ct);
            var session = await sessionSchedule.GetSessionById(id, ct);
            return View(booking.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Attendance(int id, OngoingMemberSessionScheduleViewModel model, CancellationToken ct)
        {
            var result = await sessionSchedule.ToggleAttendanceAsync(id, model, ct);
            //var session = await sessionSchedule.GetSessionById(id, ct);

            return View(result);
        }

    }
}

