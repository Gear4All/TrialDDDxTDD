using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrialDDDxTDD.DomainModel
{
    public static class BalanceSheetService
    {
        public static List<Subject> Calcurate(DateTime calcurateDate, IRepository repository)
        {
            Subjects subjects = repository.LoadSubjectsByDate(calcurateDate);
            List<Subject> balanceSheetSubjects = new List<Subject>();
            return balanceSheetSubjects
                    .CalcurateCashAndDeposits(subjects)
                    .CalcurateCurrentAssets()
                    .ToList();
        }
        #region
        public static List<Subject> CalcurateCashAndDeposits(this List<Subject> balanceSheetSubjects, Subjects subjects)
        {
            int cashAndDepositsAmmount = subjects.CheckBalanceTheSubjectIs("Cash") + subjects.CheckBalanceTheSubjectIs("SavingsAccounts");
            balanceSheetSubjects.Add(Subject.Factory("CashAndDeposits", cashAndDepositsAmmount));
            return balanceSheetSubjects;
        }
        private static List<Subject> CalcurateCurrentAssets(this List<Subject> balanceSheetSubjects)
        {
            int currentAssetsAmmount = balanceSheetSubjects.Single(bss => bss.Name == "CashAndDeposits").CheckBalance()/* + 他の集計科目(受取手形=NotesReceivable等) + ...*/;
            balanceSheetSubjects.Add(Subject.Factory("CurrentAssets", currentAssetsAmmount));
            return balanceSheetSubjects;
        }
        #endregion
    }
}
