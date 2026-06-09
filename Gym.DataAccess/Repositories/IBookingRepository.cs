using Gym.DataAccess.Models;

namespace Gym.DataAccess.Repositories
{
    public interface IBookingRepository: IRepository<Booking>
    {
        Task<bool> HasUpcomingBookingAsync(int memberId, CancellationToken cancellationToken = default);
    }
}