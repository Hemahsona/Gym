using Gym.BusinessLogic.Mappings;
using Gym.BusinessLogic.ViewModels.Session;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace Gym.BusinessLogic.Services
{
    public class SessionService(IUnitOfWork unitOfWork) : ISessionService
    {

        public async Task<Result<IReadOnlyList<CategoryLockupItem>>> GetGategoryAsync(CancellationToken ct = default)
        {
            var categories = await unitOfWork.Categories.GetAllAsync(ct);
            var items = categories.Select(c => new CategoryLockupItem
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();
            return Result<IReadOnlyList<CategoryLockupItem>>.IsSuccess(items);
        }

        public async Task<Result<IReadOnlyList<TrainerLockupItem>>> GetTrainersAsync(int id, CancellationToken ct = default)
        {
            //var category = await unitOfWork.Categories.GetByIdAsync(id, trackChanger: false);
            //if (category is null)
                //return Result<IReadOnlyList<TrainerLockupItem>>.Failure("category Not Found");
            var trainers = await unitOfWork.Trainers.GetAllAsync(ct);
            //var items = trainers
            //    .Where(t => t.Specialties == category.specialties)
            //    .Select(t => new TrainerLockupItem
            //    {
            //        Id = t.Id,
            //        Name = t.Name,
            //    }).ToList();
            var items = trainers.Select(t => new TrainerLockupItem
            {
                Id = t.Id,
                Name = t.Name,
            }).ToList();
            return Result<IReadOnlyList<TrainerLockupItem>>.IsSuccess(items);
        }

        public async Task<Result> CreateAsync(SessionCreateViewModel model, CancellationToken ct = default)
        {
            if (model.EndDate <= model.StartDate)
                return Result.Failure("End date must be after start date.");

            if (!await unitOfWork.Trainers.ExistsAsync(t => t.Id == model.TrainerId, ct))
                return Result.Failure("Trainer not found.");

            if (!await unitOfWork.Categories.ExistsAsync(c => c.Id == model.CategoryId, ct))
                return Result.Failure("Category not found.");

            var session = new Session
            {
                Description = model.Description,
                Capacity = model.Capacity,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                TrainerId = model.TrainerId,
                CategoryId = model.CategoryId,
            };

            await unitOfWork.Sessions.AddAsync(session, ct);
            await unitOfWork.SaveChangesAsync(ct);
            return Result.Success();
        }

        public async Task<Result<SessionDetailsViewModel>> GetDetailsAsync(int id, CancellationToken ct = default)
        {
            var session = await unitOfWork.Sessions.GetByIdAsync(id, trackChanger: false,
                includes: [s => s.Trainer, s => s.Category, s => s.Bookings],
                cancellationToken: ct);
            var result = new SessionDetailsViewModel
            {
                CategoryName = session.Category.Name,
                Description = session.Description,
                EndDate = session.EndDate,
                //HeaderClass = session.HeaderClass,
                //MaxCapacity = session.MaxCapacity,
                StartDate = session.StartDate,
                //Status = session.Status,
                TrainerName = session.Trainer.Name,
                BookedCount = session.Bookings.Count(),
            };
            return Result<SessionDetailsViewModel>.IsSuccess(result);
        }



        public async Task<IReadOnlyList<SessionIndexViewModel>> GetIndexItemsAsync(CancellationToken ct = default)
        {

            //var includes = new Expression<Func<Session, object>>[] { s => s.Trainer, s => s.Category, s => s.Bookings };
            var sessions = await unitOfWork.Sessions.HasTrainerAsync(includes: [s => s.Trainer, s => s.Category, s => s.Bookings],ct);
            var result = sessions.Select(session => new SessionIndexViewModel
            {
                Id = session.Id,
                TrainerName = session.Trainer.Name,
                Description = session.Description,
                CategoryName = session.Category.Name,
                BookedCount = session.Bookings.Count.ToString(),
                StartDate = session.StartDate,
                EndDate = session.StartDate,
                Capacity = session.Capacity,
                Speciality = session.Category.Name,
            }).ToList();
            return result;
        }

        public async Task<Result> EditAsync(SessionEditViewModel model, int id, CancellationToken ct)
        {
            var session = await unitOfWork.Sessions.GetByIdAsync(id,
                trackChanger: true,
                null,
                ct);
            if (session == null)
                return Result.Failure("Session not found.");

            if (model.EndDate <= model.StartDate)
                return Result.Failure("End date must be after start date.");

            if (!await unitOfWork.Trainers.ExistsAsync(t => t.Id == model.TrainerId))
                return Result.Failure("Trainer not found.");

            // Optional: verify category still exists
            var category = await unitOfWork.Categories.GetByIdAsync(session.CategoryId, trackChanger: true);
            if (category == null)
                return Result.Failure("Category not found.");

            // Update the fetched entity instead of creating a new one
            session.Description = model.Description;
            session.Capacity = model.Capacity;
            session.StartDate = model.StartDate;
            session.EndDate = model.EndDate;
            session.TrainerId = model.TrainerId;

            unitOfWork.Sessions.Update(session);
            await unitOfWork.SaveChangesAsync(ct);
            return Result.Success();
        }

        public async Task<Result<SessionEditViewModel>> GetForEditAsync(int id, CancellationToken ct = default)
        {
            var session =await unitOfWork.Sessions.GetByIdAsync(id, trackChanger: true);
            if (session is null) return Result<SessionEditViewModel>.Failure("session not found");
            if(session.StartDate >= session.EndDate) return Result<SessionEditViewModel>.Failure("Start Date must be bigger than end date");
            var model = new SessionEditViewModel
            {
                Description = session.Description,
                Capacity = session.Capacity,
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                TrainerId = session.TrainerId,



            };
            return Result<SessionEditViewModel>.IsSuccess(model);
        }

        public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
        {
            var sesssion = await unitOfWork.Sessions.GetByIdIncludingDeletedAsync(id,ct);

            if (sesssion is null) return Result.Failure("session not found");
            //if(sesssion.Bookings.Count > 0) return 
            unitOfWork.Sessions.SoftDelete(sesssion);
            await unitOfWork.Sessions.SaveChangesAsync(ct);
            return Result.Success();
        }
    }
}

