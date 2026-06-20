using Gym.BusinessLogic.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic.Services
{
    public interface IDashBoardService
    {
        Task<Result<HomeDasgBoardViewModel>> GetHomePageAsync(CancellationToken ct = default!);
    }
}
