using Gym.BusinessLogic.ViewModels.MebmerShip;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.Services
{
    internal class MemberShipService(IUnitOfWork unitOfWork) : IMemberShipService
    {
        public async Task<Result<IReadOnlyList<MemberLockupItem>>> GetMemberLockupAsync(CancellationToken ct)
        {
            var member = await unitOfWork.Members.GetAllAsync(ct);
            var result = member.Select(m => new MemberLockupItem
            {
                Id = m.Id,
                Name = m.Name,
            }).ToList();
            return Result<IReadOnlyList<MemberLockupItem>>.Success(result);
        }
        public async Task<Result<IReadOnlyList<PlanLockupItem>>> GetPlanLockupAsync(CancellationToken ct)
        {
            var plan = await unitOfWork.Plans.GetAllAsync(ct);
            var result = plan.Select(p => new PlanLockupItem
            {
                Id = p.Id,
                Name = p.Name,
            }).ToList();
            return Result<IReadOnlyList<PlanLockupItem>>.Success(result);

        }

        public async Task<Result> CreateAsync(CreateMemberShipViewModel model, CancellationToken ct)
        {
            //var member = await unitOfWork.MembersShips.HasPlanAndMemebrAsync(includes: [m => m.Member, m => m.Plan]);

            //var membership = await unitOfWork.MembersShips.GetAllAsync(ct);
            if (!await unitOfWork.Members.ExistsAsync(m => m.Id == model.MemberId, ct))
                return Result.Failure("Member not found");
            if (await unitOfWork.MembersShips.ExistsAsync(ms => ms.MemberId == model.MemberId))
                return Result.Failure("Member cant go in two plan");
            if (!await unitOfWork.Plans.ExistsAsync(p => p.Id == model.PlanId))
                return Result.Failure("Plan not found");
            var plan = await unitOfWork.Plans.GetByIdAsync(model.PlanId, trackChanger: true, cancellationToken: ct);
            //if (plan is null) 
            //    return Result.Failure("Plan not found");
            model.StartDate = DateTime.Now;
            var DisplayEndDate = model.StartDate.AddDays(plan.DurationDays);
            var result = new MemberShip
            {
                MemberId = model.MemberId,
                PlanId = model.PlanId,
                StartDate = model.StartDate,
                EndDate = DisplayEndDate,
            };
            await unitOfWork.MembersShips.AddAsync(result);
            await unitOfWork.Members.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<IEnumerable<IndexMemberShipViewModel>>> GetAllAsync(CancellationToken ct)
        {
            var member = await unitOfWork.MembersShips.HasPlanAndMemebrAsync(includes: [m => m.Member , m => m.Plan]);
            //var plan = await unitOfWork.MembersShips.HasPlanAsync(includes: [p => p.Id]);
            //var memberShip = await unitOfWork.MembersShips.GetAllAsync(ct);
            
            var result = member.Select(ms => new IndexMemberShipViewModel
            {
                Id = ms.Id,
                Name = ms.Member.Name,
                PlanName = ms.Plan.Name,
                StartDate = ms.StartDate,
                EndDate = ms.StartDate.AddDays(ms.Plan.DurationDays),
                Price = ms.Plan.Price,                
            });
            return Result<IEnumerable<IndexMemberShipViewModel>>.Success(result);
        }

        public async Task<Result> DeleteAsync(int id, CancellationToken ct)
        {
            var result =await unitOfWork.MembersShips.GetByIdIncludingDeletedAsync(id: id, cancellationToken: ct);
            if (result is null) return Result.Failure("session not found");
            //if (string.Equals(result.Status, "Expired", StringComparison.OrdinalIgnoreCase))
            //    return Result.Failure("Can't cancel expired membership");
            //var endUtc = DateTime.SpecifyKind(result.EndDate, DateTimeKind.Utc);
            //var endLocal = DateTime.SpecifyKind(result.EndDate, DateTimeKind.Local);
            //var time = 

            if (result.EndDate < DateTime.Now)
                return Result.Failure("Cant cancal expierd MemberShip");
            unitOfWork.MembersShips.SoftDelete(result);
            await unitOfWork.SaveChangesAsync(ct);
            return Result.Success();
        }

        public async Task<Result<IndexMemberShipViewModel>> GetById(int id, CancellationToken ct)
        {
            var membership = await unitOfWork.MembersShips.GetByIdAsync(id, trackChanger: false);
            if (membership is null)
                return Result<IndexMemberShipViewModel>.Failure("membership not found");
            var result = new IndexMemberShipViewModel
            {
                Id = membership.Id,
            };
            //if (result is null) return Result<IndexMemberShipViewModel>.Failure("session not found");
            return Result<IndexMemberShipViewModel>.Success(result);
        }
    }
}
