using Gym.BusinessLogic.Mappings;
using Gym.BusinessLogic.ViewModels.HealthRecord;
using Gym.BusinessLogic.ViewModels.Member;
using Gym.DataAccess.Data.Enums;
using Gym.DataAccess.Data.OwnedType;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.Services
{
    public class MemberService(IUnitOfWork unitOfWork) : IMemberService
    {
        public async Task<IEnumerable<MemberIndexViewModel>> GetAllAsync(CancellationToken ct)
        {

            var member = await unitOfWork.Members.GetAllAsync(ct);
            return member.Select(m => m.ToMemberIndexViewModel());
        }

        public async Task<Result> CreateAsync(CreateMemberViewModel model, CancellationToken ct)
        {
            if (await unitOfWork.Members.IsEmailExists(model.Email, null, ct))
            {
                return Result.Failure("Email already exists.");
            }

            if (await unitOfWork.Members.IsPhoneExists(model.Phone, null, ct))
            {
                return Result.Failure("Phone number already exists.");
            }

            var member = model.ToCreateMemberViewModel();

            await unitOfWork.Members.AddAsync(member, ct);
            await unitOfWork.Members.SaveChangesAsync(ct);
            return Result.Success();
        }



        public async Task<MemberDetailsViewModel> GetDetailsAsync(int id, CancellationToken ct)
        {
            var member = await unitOfWork.Members.GetByIdAsync(id: id, includes: [m => m.MemberShips], cancellationToken: ct);
            if (member is null) return null;

            var now = DateTime.Now;

            var activeMembership = member.MemberShips.FirstOrDefault(ms => ms.EndDate >= now);

            return new MemberDetailsViewModel
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Photo = member.Photo,
                Gender = member.Gender.ToString(),
                Address = $"{member.Address.BuildingNumber} {member.Address.Street}, {member.Address.City}",
                PlanName = activeMembership?.Plan.Name ?? "No Active Plan",
                DateOfBirth = member.DateOfBirth.ToString("yyyy-MM-dd"),
                MemberShipStartDate = activeMembership?.StartDate.ToString("yyyy-MM-dd") ?? "-",
                MemberShipEndDate = activeMembership?.EndDate.ToString("yyyy-MM-dd") ?? "-",

            };
        }

        public async Task<HealthRecordDetailsModelView> GetHealthRecordDetailsAsync(int id, CancellationToken ct)
        {
            var health = await unitOfWork.Members.GetByIdAsync(id: id, cancellationToken: ct, includes: [h => h.HealthRecord]);
            if (health is null) return null;
            return new HealthRecordDetailsModelView
            {

                Height = health.HealthRecord.Height.ToString(),
                Weight = health.HealthRecord.Weight.ToString(),
                BloodType = ToDisplayBloodType(health.HealthRecord.BloodType),
                Note = health.HealthRecord.Note
            };
        }
        private static string ToDisplayBloodType(BloodType bloodType)
        {
            return bloodType switch
            {
                BloodType.APositive => "A+",
                BloodType.ANegative => "A-",
                BloodType.BPositive => "B+",
                BloodType.BNegative => "B-",
                BloodType.ABPositive => "AB+",
                BloodType.ABNegative => "AB-",
                BloodType.OPositive => "O+",
                BloodType.ONegative => "O-",
                _ => "Unknown"
            };
        }

        public async Task<EditMemberViewModel> GetForEditAsync(int id, CancellationToken ct)
        {
            var member = await unitOfWork.Members.GetByIdAsync(id: id, cancellationToken: ct);

            if (member is null) return null;

            return member.ToEditViewModel();

        }

        public async Task<Result> EditAsync(int id, EditMemberViewModel model, CancellationToken ct)
        {
            var member = await unitOfWork.Members.GetByIdAsync(id: id, cancellationToken: ct);

            if (member is null) return Result.Failure("member not found");
            if(member.Name != model.Name) return Result.Failure("Name cannot be changed");
            if (await unitOfWork.Members.IsEmailExists(model.Email, id, ct)) return Result.Failure("Email already exists.");
            if (await unitOfWork.Members.IsPhoneExists(model.Phone, id, ct)) return Result.Failure("Phone already exists.");

            member.Email = model.Email;
            member.Phone = model.Phone;
            member.Address.BuildingNumber = model.BuildingNumber;
            member.Address.City = model.City;
            member.Address.Street = model.Street;

            unitOfWork.Members.Update(member);
            await unitOfWork.Members.SaveChangesAsync(ct);
            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id, CancellationToken ct)
        {
            var member = await unitOfWork.Members.GetByIdIncludingDeletedAsync(id, ct);

            if (member is null) return Result.Failure("Member not found");

            if (await unitOfWork.Members.HasUpcomingBookingAsync(id, ct))
                return Result.Failure("Cannot delete member with active membership.");


            unitOfWork.Members.SoftDelete(member);
            await unitOfWork.Members.SaveChangesAsync(ct);
            return Result.Success();
        }
    }
}
