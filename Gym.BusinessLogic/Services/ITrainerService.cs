using Gym.BusinessLogic.ViewModels.Member;
using Gym.BusinessLogic.ViewModels.Trainer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.Services
{
    public interface ITrainerService
    {
        public Task<IEnumerable<TrainerIndexViewModel>> GetAllAsync(CancellationToken ct);
        public Task<Result> CreateAsync(TrainerCreateViewModel model, CancellationToken ct);
    }
}
