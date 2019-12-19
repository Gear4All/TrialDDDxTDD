using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using TrialDDDxTDD.DomainModel;

namespace TrialDDDxTDD.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void U‘Ö“`•[‚ğ‹L’ ‚·‚é‚Æ‚«U‘Ö“`•[‚Ì‘İØ‚ª‹Ït‚µ‚Ä‚¢‚È‚¢‚È‚çNotBalancingBetweenDebitAndCreditException‚ğ”­¶‚³‚¹‚é()
        {
            DateTime occuredDate = DateTime.Today;
            Debit debit0 = Debit.Factory("Cash", 100);
            Debit debit1 = Debit.Factory("SavingsAccounts", 100);
            Credit credit = Credit.Factory("Sale", 100);
            try
            {
                TransferSlip transferSlip = TransferSlip.Factory(
                    occuredDate,
                   new List<Debit>() { debit0, debit1 },
                  new List<Credit>() { credit }
                );
            }
            catch (NotBalancingBetweenDebitAndCreditException)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public void Š¨’è‰È–Ú‚ªU‘Ö“`•[‚Æ‘O“ú‚Ìc‚‚©‚ç¡“ú‚Ìc‚‚ğŒvZ‚·‚é()
        {
            //ƒeƒXƒg‚Ì‚½‚ß‚É‘O“ú‚Ìc‚‚ğİ’è‚·‚é
            DateTime yesterDay = DateTime.Today.AddDays(-1);
            Debit yesterdayDebit = Debit.Factory("Cash", 100);
            Credit yesterdayCredit = Credit.Factory("Sale", 100);
            TransferSlip yesterdayTransferSlip = TransferSlip.Factory(
                yesterDay,
               new List<Debit>() { yesterdayDebit },
              new List<Credit>() { yesterdayCredit }
            );
            IEnumerable<TransferSlip> yesterdayTransferSlips = new List<TransferSlip>() { yesterdayTransferSlip };
            var yesterdayMockRepository = new Mock<IRepository>();
            yesterdayMockRepository.Setup(x => x.LoadSubjectsByDate(yesterDay))
                .Returns(new Subjects());
            Subjects yesterdaySubjects = Subjects.CalcurateBalancesPerSubject(yesterdayTransferSlips, yesterdayMockRepository.Object); 
            //¡“ú‚ÌU‘Ö“`•[‚ğ“ü—Í‚·‚é
            DateTime occuredDate = DateTime.Today;
            Debit debit = Debit.Factory("SavingsAccounts", 100);
            Credit credit = Credit.Factory("Sale", 100);
            TransferSlip transferSlip = TransferSlip.Factory(
                occuredDate,
               new List<Debit>() { debit },
              new List<Credit>() { credit }
            );
            List<TransferSlip> transferSlips = new List<TransferSlip>() { transferSlip };
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.LoadSubjectsByDate(yesterDay))
                .Returns(yesterdaySubjects);

            //U‘Ö“`•[‚Æ‘O“ú‚Ìc‚‚©‚ç¡“ú‚Ìc‚‚ğŒvZ
            Subjects todaySubjects =  Subjects.CalcurateBalancesPerSubject(transferSlips,mockRepository.Object);

            Assert.AreEqual(100, todaySubjects.CheckBalanceTheSubjectIs("Cash")); 
            Assert.AreEqual(100, todaySubjects.CheckBalanceTheSubjectIs("SavingsAccounts") ); 
            Assert.AreEqual(-200, todaySubjects.CheckBalanceTheSubjectIs("Sale") ); 
        }
        [TestMethod]
        public void Š¨’è‰È–Ú–ˆ‚Ìc‚‚©‚ç‘İØ‘ÎÆ•\‚ğŒvZ‚·‚é()
        {
            DateTime yesterDay = DateTime.Today.AddDays(-1);
            DateTime occuredDate = DateTime.Today;
            Debit debit = Debit.Factory("Cash", 100);
            Credit credit = Credit.Factory("SavingsAccounts", 100);
            TransferSlip transferSlip = TransferSlip.Factory(
                occuredDate,
               new List<Debit>() { debit },
              new List<Credit>() { credit }
            );
            List<TransferSlip> transferSlips = new List<TransferSlip>() { transferSlip };
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.LoadSubjectsByDate(yesterDay))
                .Returns(new Subjects());
            Subjects todaySubjects = Subjects.CalcurateBalancesPerSubject(transferSlips, mockRepository.Object);
            mockRepository.Setup(x => x.LoadSubjectsByDate(DateTime.Today))
                .Returns(todaySubjects);

            List<Subject> balanseSheetSubjects =  BalanceSheetService.Calcurate(DateTime.Today,mockRepository.Object);

            Assert.AreEqual(0, balanseSheetSubjects.Single(bbs=>bbs.Name=="CurrentAssets").CheckBalance());
            Assert.AreEqual(0, balanseSheetSubjects.Single(bbs => bbs.Name == "CashAndDeposits").CheckBalance()); 
        }
    }
}
