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

        public async Task<Result<TrainerDetailsViewModel>> GetDetailsAsync(int id, CancellationToken ct)
        {
            var trainer = await _unitOfWork.Trainers.GetByIdAsync(id, trackChanger: false);
            var result = new TrainerDetailsViewModel()
            {
                Id = id,
                Name = trainer.Name,
                Email = trainer.Email,
                DateOfBirth = trainer.DateOfBirth,
                Phone = trainer.Phone,
                Specialties = trainer.Specialties.ToString(),
                Address = $"{trainer.Address.BuildingNumber}-{trainer.Address.Street}-{trainer.Address.City}",
            };

            return Result<TrainerDetailsViewModel>.Success(result);
        }

        public async Task<Result<TrainerEditViewModel>> GetForEditAsync(int id, CancellationToken ct)
        {
            var result = await _unitOfWork.Trainers.GetByIdAsync(id, trackChanger: false);
            var trainer = new TrainerEditViewModel()
            {
                Name = result.Name,
                Email = result.Email,
                Phone = result.Phone,
                BuildingNumber = result.Address.BuildingNumber,
                Street = result.Address.Street,
                City = result.Address.City,
                Specialties = result.Specialties
            };
            return Result<TrainerEditViewModel>.Success(trainer);
        }

        public async Task<Result> EditAsync(TrainerEditViewModel model, int id,CancellationToken ct)
        {
            var trainer  = await _unitOfWork.Trainers.GetByIdAsync( id: id, trackChanger: true, cancellationToken: ct);
            if (trainer == null) return Result.Failure("trainer not found");
            if (trainer.Name != model.Name) return Result.Failure("Name cannot be changed");
            if (await unitOfWork.Members.IsEmailExists(model.Email, id, ct)) return Result.Failure("Email already exists.");
            if (await unitOfWork.Members.IsPhoneExists(model.Phone, id, ct)) return Result.Failure("Phone already exists.");
            trainer.Email = model.Email;
            trainer.Phone = model.Phone;
            trainer.Address.BuildingNumber = model.BuildingNumber;
            trainer.Address.Street = model.Street;
            trainer.Address.City = model.City;

            _unitOfWork.Trainers.Update(trainer);
            await _unitOfWork.Trainers.SaveChangesAsync(ct);
            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id, CancellationToken ct)
        {
            var trainer = await _unitOfWork.Trainers.GetByIdIncludingDeletedAsync( id: id, cancellationToken: ct);
            if( await _unitOfWork.Sessions.ExistsAsync(s => s.TrainerId == id)) 
                return Result.Failure("Trainer has upcoming Session");
            if (trainer is null) Result.Failure("Trainer Not Found");

            _unitOfWork.Trainers.SoftDelete(trainer);
            await _unitOfWork.Trainers.SaveChangesAsync(ct);
            return Result.Success();
        }
    }
}
