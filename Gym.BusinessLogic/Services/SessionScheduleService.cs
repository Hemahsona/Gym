using Gym.BusinessLogic.ViewModels.Session;
using Gym.BusinessLogic.ViewModels.SessionSchedule;
using Gym.BusinessLogic.ViewModels.SessionSchedules;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.Services
{
    public class SessionScheduleService(IUnitOfWork unitOfWork) : ISessionScheduleService
    {

        public async Task<Result<IEnumerable<IndexSessionScheduleViewModel>>> GetAllAsync(CancellationToken ct)
        {
            var sessions = await unitOfWork.Sessions.HasTrainerAsync(includes: [s => s.Trainer, s => s.Category, s => s.Bookings], ct);
            
            var result = sessions.Select(session => new IndexSessionScheduleViewModel
            {
                Id = session.Id,
                TrainerName = session.Trainer.Name,
                Description = session.Description,
                CategoryName = session.Category.Name,
                BookedCount = session.Bookings.Count.ToString(),
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                Capacity = session.Capacity,
                Speciality = session.Category.Name,
                
            }).ToList();
            return Result<IEnumerable<IndexSessionScheduleViewModel>>.Success(result);

        }
        public async Task<Result> CreateAsync(CreateSessionScheduleViewModel model, CancellationToken ct = default)
        {
            var member = await unitOfWork.Members.GetAllAsync(ct);
            var session = await unitOfWork.Sessions.GetByIdAsync(model.SessionId, trackChanger: true, includes: [s => s.Bookings],cancellationToken: ct);
            if (!await unitOfWork.MembersShips.ExistsAsync(ms => ms.MemberId == model.MemberId))
                return Result.Failure("member dont has membership");
            //if (await unitOfWork.Bookings.ExistsAsync(b => b.MemberId == model.MemberId))
            //    return Result.Failure("member must has only one session");
            var bookingCount = session.Bookings.Count();
            if (bookingCount >= session.Capacity)
                return Result.Failure("session compeleted");

             model.StartDate = DateTime.Now;
            
            var result = new Booking
            {
                MemberId = model.MemberId,
                SessionId = model.SessionId,
                Date = model.StartDate,

            };
            Console.WriteLine(model.MemberId);
            Console.WriteLine(model.SessionId);
            await unitOfWork.Bookings.AddAsync(result);
            await unitOfWork.SaveChangesAsync();
            return Result.Success();


        }



        public async Task<Result<IEnumerable<UpcomingMemberSessionScheduleViewModel>>> GetBookingsBySeesionId(int id, CancellationToken ct)
        {
            var bookings = await unitOfWork.SessionSchedules.GetById(id: id, trackerchange: true, includes: [s => s.Session, s => s.Member], cancellationToken: ct);
            var result = bookings.Select(bookings => new UpcomingMemberSessionScheduleViewModel
            {
                Id = bookings.Id,
                Name = bookings.Member.Name,
                StartDate = bookings.Date,
            });

            return Result<IEnumerable<UpcomingMemberSessionScheduleViewModel>>.Success(result);
        }

        public async Task<Result<IReadOnlyList<MemberLockupItem>>> GetMemberAsync(CancellationToken ct = default)
        {
            var member = await unitOfWork.Members.GetAllAsync(ct);
            var result = member.Select(m => new MemberLockupItem
            {
                Id = m.Id,
                Name = m.Name,
            }).ToList();
            return Result<IReadOnlyList<MemberLockupItem>>.Success(result);
        }

        public async Task<Result<UpcomingSessionMemberSessionScheduleViewModel>> GetSessionById(int id, CancellationToken ct)
        {
            var session = await unitOfWork.Sessions.GetByIdAsync(id, trackChanger: true, cancellationToken: ct);
            var result = new UpcomingSessionMemberSessionScheduleViewModel
            {
                Id = session.Id,
            };
            return Result<UpcomingSessionMemberSessionScheduleViewModel>.Success(result);
        }

        public async Task<Result<int>> DeleteAsync(int id, CancellationToken ct)
        {
            var result = await unitOfWork.Bookings.GetByIdIncludingDeletedAsync(id, ct);
            if (result is null) return Result<int>.Failure("session not found");
            int sessionId = result.SessionId;
            unitOfWork.SessionSchedules.SoftDelete(result);
            await unitOfWork.SaveChangesAsync(ct);
            return Result<int>.Success(sessionId);
        }

        public async Task<Result<UpcomingMemberSessionScheduleViewModel>> GetBookingByIdAsync(int id, CancellationToken ct)
        {
            var bookings = await unitOfWork.SessionSchedules.GetByIdAsync(id: id, trackChanger: true, includes: [s => s.Session, s => s.Member], cancellationToken: ct);
            var result = new UpcomingMemberSessionScheduleViewModel
            {
                Id = bookings.Id,
                Name = bookings.Member.Name,
                StartDate = bookings.Date,
            };
            return Result<UpcomingMemberSessionScheduleViewModel>.Success(result);
        }

        public async Task<Result<IEnumerable<OngoingMemberSessionScheduleViewModel>>> GetOngoingByBookingId(int id, CancellationToken ct = default)
        {
            var bookings = await unitOfWork.SessionSchedules.GetById(id: id, trackerchange: true, includes: [s => s.Session, s => s.Member], cancellationToken: ct);
            var result = bookings.Select(b => new OngoingMemberSessionScheduleViewModel
            {
                Id = b.Id,
                MemberName = b.Member.Name,
                Attendence = b.IsAttented,
            });
            return Result<IEnumerable<OngoingMemberSessionScheduleViewModel>>.Success(result);
        }

        public async Task<Result> ToggleAttendanceAsync(int id, OngoingMemberSessionScheduleViewModel model, CancellationToken ct = default)
        {
            var bookings = await unitOfWork.Bookings.GetByIdAsync(id, trackChanger: true, cancellationToken: ct);
            bookings.Id = model.Id;
            bookings.IsAttented = !bookings.IsAttented;
            unitOfWork.Bookings.Update(bookings);
            await unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }
    }
    

