using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting.Mocking
{
    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBookings(int? excludingBookingId = null);
    }

    public class BookingRepository : IBookingRepository
    {
        public IQueryable<Booking> GetActiveBookings(int? excludingBookingId = null)
        {
            var unitOfWork = new UnitOfWork();
            var bookings =
                unitOfWork.Query<Booking>()
                    .Where(
                        b => b.Status != "Cancelled");

            if (excludingBookingId.HasValue)
                bookings = bookings.Where(b => b.Id != excludingBookingId.Value);

            return bookings;
        }
    }
}
