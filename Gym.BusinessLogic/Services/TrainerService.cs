using Gym.BusinessLogic.ViewModels.Member;
using Gym.BusinessLogic.ViewModels.Trainer;
using Gym.DataAccess.Data.Enums;
using Gym.DataAccess.Data.OwnedType;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Gym.BusinessLogic.Services
{
    public class TrainerService(IUnitOfWork unitOfWork) : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<TrainerIndexViewModel>> GetAllAsync(CancellationToken ct)
        {
            var trainers = await _unitOfWork.Trainers.GetAllAsync(ct);
            return trainers.Select(t => new TrainerIndexViewModel
            {

                Id = t.Id,
                Name = t.Name,
                Email = t.Email,
                Phone = t.Phone,
                Specialties = t.Specialties.ToString(),

            });
        }
        public async Task<Result> CreateAsync(TrainerCreateViewModel model, CancellationToken ct)
        {
            if (await unitOfWork.Trainers.IsEmailExists(model.Email, null, ct))
            {
                return Result.Failure("Email already exists.");
            }

            if (await unitOfWork.Trainers.IsPhoneExists(model.Phone, null, ct))
            {
                return Result.Failure("Phone number already exists.");
            }

            var trainer = new Trainer
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Address = new Address
                {
                    BuildingNumber = model.BuildingNumber,
                    City = model.City,
                    Street = model.Street
                },
                Specialties = model.specialties
            };

            await unitOfWork.Trainers.AddAsync(trainer, ct);
            await unitOfWork.Trainers.SaveChangesAsync(ct);
            return Result.Success();

        }


}}
