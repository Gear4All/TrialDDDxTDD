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
        public void 振替伝票を記帳するとき振替伝票の貸借が均衡していないならNotBalancingBetweenDebitAndCreditExceptionを発生させる()
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
        public void 勘定科目が振替伝票と前日の残高から今日の残高を計算する()
        {
            //テストのために前日の残高を設定する
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
            //今日の振替伝票を入力する
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

            //振替伝票と前日の残高から今日の残高を計算
            Subjects todaySubjects =  Subjects.CalcurateBalancesPerSubject(transferSlips,mockRepository.Object);

            Assert.AreEqual(100, todaySubjects.CheckBalanceTheSubjectIs("Cash")); 
            Assert.AreEqual(100, todaySubjects.CheckBalanceTheSubjectIs("SavingsAccounts") ); 
            Assert.AreEqual(-200, todaySubjects.CheckBalanceTheSubjectIs("Sale") ); 
        }
        [TestMethod]
        public void 勘定科目毎の残高から貸借対照表を計算する()
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
