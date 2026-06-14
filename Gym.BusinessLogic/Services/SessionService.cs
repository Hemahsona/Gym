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

        public async Task<Result<IReadOnlyList<TrainerLockupItem>>> GetTrainersAsync(CancellationToken ct = default)
        {
            var trainers = await unitOfWork.Trainers.GetAllAsync(ct);
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
            var session = await unitOfWork.Sessions.GetByIdAsync(id,
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
            var result = sessions.Select(s => s.ToSessionIndexViewModel()).ToList();
            return result;
        }
        public async Task<Result> EditAsync(SessionEditViewModel model, CancellationToken ct)
        {
            if (model.EndDate <= model.StartDate)
                return Result.Failure("End date must be after start date.");

            if (!await unitOfWork.Trainers.ExistsAsync(t => t.Id == model.TrainerId))
                return Result.Failure("Trainer not found.");

            if (!await unitOfWork.Categories.ExistsAsync(c => c.Id == model.CategoryId))
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

            unitOfWork.Sessions.Update(session);
            await unitOfWork.SaveChangesAsync(ct);
            return Result.Success();
        }

        public async Task<Result<SessionEditViewModel>> GetForEditAsync(int id, CancellationToken ct = default)
        {
            var session =await unitOfWork.Sessions.GetByIdAsync(id);
            var model = new SessionEditViewModel
            {
                Description = session.Description,
                Capacity = session.Capacity,
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                TrainerId = session.TrainerId,
                CategoryId = session.CategoryId
            };
            return Result<SessionEditViewModel>.IsSuccess(model);
        }
    }
}

