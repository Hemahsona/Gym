using Gym.BusinessLogic.ViewModels.Member;
using Gym.BusinessLogic.ViewModels.Trainer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.Services
{
    public interface ITrainerService
    {
        Task<IEnumerable<TrainerIndexViewModel>> GetAllAsync(CancellationToken ct);
        Task<Result> CreateAsync(TrainerCreateViewModel model, CancellationToken ct);
        Task<Result<TrainerDetailsViewModel>> GetDetailsAsync(int id, CancellationToken ct);
        Task<Result<TrainerEditViewModel>> GetForEditAsync(int id, CancellationToken ct);
        Task<Result> EditAsync(TrainerEditViewModel model, int id, CancellationToken ct);
        Task<Result> DeleteAsync(int id, CancellationToken ct);
    }
}
