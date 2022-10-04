using System;
using UnitTesting.Fundamentals;
using NUnit.Framework;

namespace UnitTesting.UnitTests.Fundamentals
{
    [TestFixture]
    public class ReservationTests
    {
        [Test]
        public void CanBeCanceledBy_UserIsAdmin_ReturnsTrue()
        {
            //Arrange
            var reservation = new Reservation();
            //Act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });
            //Assert
            Assert.IsTrue(result);
        }
        [Test]
        public void CanBeCanceledBy_AnotherUserCancelling_ReturnsFalse()
        {
            //Arrange
            var reservation = new Reservation
            {
                MadeBy = new User { IsAdmin = false }
            };
            //Act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = false });
            //Assert
            Assert.IsFalse(result);
        }
        [Test]
        public void CanBeCanceledBy_UserIsCancelling_ReturnsTrue()
        {
            //Arrange
            var reservation = new Reservation
            {
                MadeBy = new User { IsAdmin = false }
            };
            //Act
            var result = reservation.CanBeCancelledBy(reservation.MadeBy);
            //Assert
            Assert.IsTrue(result);
        }
    }
}
