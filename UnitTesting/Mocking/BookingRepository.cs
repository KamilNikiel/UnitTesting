using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting.Mocking
{
    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBookings(int excludingBookingId);
    }

    public class BookingRepository : IBookingRepository
    {
        public IQueryable<Booking> GetActiveBookings(int excludingBookingId)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Query<Booking>()
                .Where(
                    b => b.Id != excludingBookingId && b.Status != "Cancelled");
        }
    }
}
