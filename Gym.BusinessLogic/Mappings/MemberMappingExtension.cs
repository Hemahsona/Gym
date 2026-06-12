using Gym.BusinessLogic.ViewModels.HealthRecord;
using Gym.BusinessLogic.ViewModels.Member;
using Gym.DataAccess.Data.Enums;
using Gym.DataAccess.Data.OwnedType;
using Gym.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.Mappings
{
    public static class MemberMappingExtension
    {
        public static EditMemberViewModel ToEditViewModel(this Member member)
        {
            return new EditMemberViewModel
            {
                Name = member.Name,
                Photo = member.Photo,
                Email = member.Email,
                Phone = member.Phone,
                DateOfBirth = member.DateOfBirth,
                Gender = member.Gender,
                BuildingNumber = member.Address.BuildingNumber,
                City = member.Address.City,
                Street = member.Address.Street,
            };
        }

        public static MemberIndexViewModel ToMemberIndexViewModel(this Member member)
        {
            return new MemberIndexViewModel
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Photo = member.Photo,
                JoinDate = member.JoinDate,
                Gender = member.Gender.ToString(),
            };
        }

        public static Member ToCreateMemberViewModel(this CreateMemberViewModel member)
        {
            return new Member
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                DateOfBirth = member.DateOfBirth,
                Gender = member.Gender,
                Address = new Address
                {
                    BuildingNumber = member.BuildingNumber,
                    City = member.City,
                    Street = member.Street
                },
                HealthRecord = new HealthRecord
                {
                    Height = member.HealthRecordViewModel.Height,
                    Weight = member.HealthRecordViewModel.Weight,
                    BloodType = member.HealthRecordViewModel.BloodType,
                    Note = member.HealthRecordViewModel.Note,
                }
            };           
        }

        public static MemberDetailsViewModel ToMemberDetailsViewModel(this Member member)
        {
            var activeMembership = ActiveMembership(member.MemberShips);

            return new MemberDetailsViewModel
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Photo = member.Photo,
                Gender = member.Gender.ToString(),
                Address = $"{member.Address.BuildingNumber} {member.Address.Street}, {member.Address.City}",
                PlanName = activeMembership?.Plan?.Name ?? "-",
                DateOfBirth = member.DateOfBirth.ToString("yyyy-MM-dd"),
                MemberShipStartDate = activeMembership?.StartDate.ToString("yyyy-MM-dd") ?? "-",
                MemberShipEndDate = activeMembership?.EndDate.ToString("yyyy-MM-dd") ?? "-",

            };
        }

        public static HealthRecordDetailsModelView ToHealthRecordDetailsModelView(this HealthRecord healthRecord)
        {
            return new HealthRecordDetailsModelView
            {

                Height = healthRecord.Height.ToString(),
                Weight = healthRecord.Weight.ToString(),
                BloodType = ToDisplayBloodType(healthRecord.BloodType),
                Note = healthRecord.Note
            };
        }
        private static MemberShip? ActiveMembership(IEnumerable<MemberShip> memberships)
        {
            var now = DateTime.Now;
            return memberships.FirstOrDefault(ms => ms.EndDate >= now);            
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

    }


}
