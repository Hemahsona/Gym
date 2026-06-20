using Gym.BusinessLogic.ViewModels.Plan;
using Gym.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.Services
{
    public class PlanService(IUnitOfWork unitOfWork) : IPlanService
    {
        public async Task<Result<IEnumerable<IndexPlanViewModel>>> GetAllAsync(int id, CancellationToken ct)
        {
            var plans = await unitOfWork.Plans.GetAllAsync(ct);
            var model = plans.Select(p => new IndexPlanViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                DurationDays = p.DurationDays,
                IsActive = p.IsActive
            });

            return Result<IEnumerable<IndexPlanViewModel>>.IsSuccess(model);
        }
        
        public async Task<Result<EditPlanViewModel>> GetForUpdate(int id, CancellationToken ct)
        {
            var plan = await unitOfWork.Plans.GetByIdAsync(id, trackChanger: true);
            var model = new EditPlanViewModel
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                DurationDays = plan.DurationDays,
            };
            return Result<EditPlanViewModel>.IsSuccess(model);
        }

        public async Task<Result> EditAsync(EditPlanViewModel model, int id, CancellationToken ct)
        {
            var plan = await unitOfWork.Plans.GetByIdIncludingDeletedAsync( id , ct);
            if (plan == null) return Result.Failure("Plan not found");
            plan.Id = id;
            plan.Description = model.Description;
            plan.Price = model.Price;
            plan.DurationDays = model.DurationDays;

            unitOfWork.Plans.Update(plan);
            await unitOfWork.SaveChangesAsync();
            return Result.Success();

        }

        public async Task<Result<DetailsPalnViewModel>> GetDetailsAsync(int id, CancellationToken ct)
        {
            var plan = await unitOfWork.Plans.GetByIdAsync(id:id, trackChanger: true, cancellationToken: ct);
            var result = new DetailsPalnViewModel
            {
                Id = plan.Id,
                Name = plan.Name,
                Price = plan.Price,
                DurationDays = plan.DurationDays,
                Description = plan.Description,
                IsActive = plan.IsActive,
            };
            return Result<DetailsPalnViewModel>.IsSuccess(result);
        }

        public async Task<Result> ToggleAsync(int id, CancellationToken ct)
        {          
            var plan = await unitOfWork.Plans.GetByIdAsync(cancellationToken: ct, trackChanger: true, id: id);
            if (plan is null) return Result.Failure("no plan Found");

            plan.IsActive = !plan.IsActive;

            unitOfWork.Plans.Update(plan);
            await unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }
}

