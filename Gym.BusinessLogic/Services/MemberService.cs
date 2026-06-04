using Gym.BusinessLogic.ViewModels.Member;
using Gym.DataAccess.Data.OwnedType;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.Services
{
    public class MemberService(IMemberRepository memberRepository) : IMemberService
    {
        public async Task<IEnumerable<MemberIndexViewModel>> GetAllAsync(CancellationToken ct)
        {

            var member = await memberRepository.GetAllAsync(ct);
            return member.Select(m => new MemberIndexViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                Phone = m.Phone,
                Photo = m.Photo,
                JoinDate = m.JoinDate,
                Gender = m.Gender.ToString(),
            });
        }

        public async Task<bool> CreateAsync(CreateMemberViewModel model, CancellationToken ct)
        {
            if(await memberRepository.ExistsAsync(m => m.Email == model.Email, ct))
            {
                return false;
            }
            if (await memberRepository.ExistsAsync(m => m.Phone == model.Phone, ct))
            {
                return false;
            }
            var member = new Member
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                JoinDate = DateOnly.FromDateTime(DateTime.Now),
                Address = new Address
                {
                    BuildingNumber = model.BuildingNumber,
                    City = model.City,
                    Street = model.Street
                },
                HealthRecord = new HealthRecord
                {
                    Height = model.HealthRecordViewModel.Height,
                    Weight = model.HealthRecordViewModel.Weight,
                    BloodType = model.HealthRecordViewModel.BloodType,
                    Note = model.HealthRecordViewModel.Note,
                }
            };
            await memberRepository.AddAsync(member, ct);
            await memberRepository.SaveChangesAsync(ct);
            return true;
        }



    }
}
