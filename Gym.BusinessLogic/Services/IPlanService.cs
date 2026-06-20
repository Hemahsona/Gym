using Gym.BusinessLogic.ViewModels.Plan;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.Services
{
    public interface IPlanService
    {
        Task<Result<IEnumerable<IndexPlanViewModel>>> GetAllAsync(int id, CancellationToken ct);
        Task<Result<EditPlanViewModel>> GetForUpdate(int id, CancellationToken ct); 
        Task<Result> EditAsync(EditPlanViewModel model, int id, CancellationToken ct);
        Task<Result<DetailsPalnViewModel>> GetDetailsAsync(int id, CancellationToken ct);
        Task<Result> ToggleAsync(int id, CancellationToken ct);
    }
}
