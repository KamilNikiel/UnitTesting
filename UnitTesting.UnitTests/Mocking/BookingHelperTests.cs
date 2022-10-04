using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.Mocking;

namespace UnitTesting.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelper_OverlappingBookingsExistTests
    {
        private Mock<IBookingRepository> _bookingRepository;
        private Booking _existingBooking;

        [SetUp]
        public void SetUp()
        {
            _existingBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2022, 10, 4),
                DepartureDate = DepartOn(2022, 10, 9),
                Reference = "test"
            };

            _bookingRepository = new Mock<IBookingRepository>();
            _bookingRepository.Setup(br => br.GetActiveBookings(1)).Returns(new List<Booking>
            {
                _existingBooking
            }.AsQueryable());
        }

        [Test]
        public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnsEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking 
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 6),
                DepartureDate = Before(_existingBooking.ArrivalDate)
            }, _bookingRepository.Object);

            Assert.That(result, Is.Empty);
        }
        [Test]
        public void BookingStartsAndFinishesAfterAnExistingBooking_ReturnsEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking 
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.DepartureDate),
                DepartureDate = After(_existingBooking.DepartureDate, days: 6)
            }, _bookingRepository.Object);

            Assert.That(result, Is.Empty);
        }
        [Test]
        public void BookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnsExistingBookingsReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 6),
                DepartureDate = After(_existingBooking.ArrivalDate)
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }
        [Test]
        public void BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnsExistingBookingsReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 6),
                DepartureDate = After(_existingBooking.DepartureDate)
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }
        [Test]
        public void BookingStartsAndFinishesInTheMiddleOfAnExistingBooking_ReturnsExistingBookingsReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate)
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }
        [Test]
        public void BookingStartsInTheMiddleAndFinishesAfterAnExistingBooking_ReturnsExistingBookingsReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate)
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }
        [Test]
        public void BookingOverlapButNewBookingIsCancelled_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
                Status = "Cancelled"
            }, _bookingRepository.Object);

            Assert.That(result, Is.Empty);
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }
        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }
        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 15, 0, 0);
        }
    }
}
