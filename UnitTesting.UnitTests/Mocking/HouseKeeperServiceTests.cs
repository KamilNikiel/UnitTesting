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
    public class HouseKeeperServiceTests
    {
        private HouseKeeperService _houseKeeperService;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _xtraMessageBox;
        private Housekeeper _housekeeper;
        private DateTime _statementDate = new DateTime(2022, 10, 5);
        private string _statementFileName;

        [SetUp]
        public void SetUp()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            _housekeeper = new Housekeeper { 
                Email = "email@gmail.com",
                FullName = "Houskeeper's FullName",
                Oid = 1,
                StatementEmailBody = "Statement Email" };
            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _housekeeper
            }.AsQueryable());
            _statementFileName = "statemementFileName";
            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(() => _statementFileName);

            _emailSender = new Mock<IEmailSender>();
            _xtraMessageBox = new Mock<IXtraMessageBox>();

            _houseKeeperService = new HouseKeeperService(
                unitOfWork.Object,
                _statementGenerator.Object,
                _emailSender.Object,
                _xtraMessageBox.Object);
        }
        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _houseKeeperService.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => 
            sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate));
        }
        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_WhenCalled_ShouldNotGenerateStatement(string email)
        {
            _housekeeper.Email = email;

            _houseKeeperService.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg =>
            sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate),
            Times.Never);
        }
        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _houseKeeperService.SendStatementEmails(_statementDate);

            _emailSender.Verify(es => es.EmailFile(
                _housekeeper.Email,
                _housekeeper.StatementEmailBody,
                _statementFileName,
                It.IsAny<string>()));
        }
        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_WhenCalled_ShouldNotEmailTheStatement(string statementFileName)
        {
            _statementFileName = statementFileName;

            _houseKeeperService.SendStatementEmails(_statementDate);

            _emailSender.Verify(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
                Times.Never);
        }
        [Test]
        public void SendStatementEmails_EmailSendingFails_ShouldDisplayAMessageBox()
        {
            _emailSender.Setup(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>())).Throws<Exception>();

            _houseKeeperService.SendStatementEmails(_statementDate);

            _xtraMessageBox.Verify(xmb => xmb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }
    }
}
