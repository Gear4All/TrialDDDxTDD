using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrialDDDxTDD.DomainModel
{
    public class Subjects 
    { 
        public readonly DateTime RecordDate;
        private List<Subject> _Subjects = new List<Subject>();
        public IEnumerable<Subject> EnumerableSubjects => _Subjects.AsEnumerable();
        public void Add(Subject subject)
        {
            if (subject == null) throw new ArgumentNullException();
            _Subjects.Add(subject);
        }
        //TODO:クエリメソッドなので、ドメインモデルからは削除し、クエリスタックのモデルに置くこと。
        public int CheckBalanceTheSubjectIs(string subjectName)
        {
            //return BalancePerSubjects[subjectName];
            return EnumerableSubjects.Single(x => x.Name == subjectName).CheckBalance();
        }

        #region private method
        private void CalcurateSubjectBalanceWhenEnteringDebits(IQueryable debits)
        {
            foreach (Debit debit in debits)
            {
                int tranlatedBalanceFromAmmount = int.Parse(debit.CheckValueOfAmmount().ToString());
                string subjectName = debit.CheckSubjectName();
                if (_Subjects.Any(x => x.Name == subjectName)) _Subjects.Single(x => x.Name == subjectName).CalcurateBalance(tranlatedBalanceFromAmmount);
                else _Subjects.Add(Subject.Factory(subjectName, tranlatedBalanceFromAmmount));
            }
        }
        private void CalcurateSubjectBalanceWhenEnteringCredits(IQueryable credits)
        {
            foreach (Credit credit in credits)
            {
                int tranlatedBalanceFromAmmount = TranslateToBalanceFromCreditAmmountByEnterRuleOfBalance(credit.CheckValueOfAmmount());
                string subjectName = credit.CheckSubjectName();
                if (_Subjects.Any(x => x.Name == subjectName)) _Subjects.Single(x => x.Name == subjectName).CalcurateBalance(tranlatedBalanceFromAmmount);
                else _Subjects.Add(Subject.Factory(subjectName, tranlatedBalanceFromAmmount));
            }
        }
        private int TranslateToBalanceFromCreditAmmountByEnterRuleOfBalance(uint subjectCreditAmount)
        {
            return -1 * int.Parse(subjectCreditAmount.ToString());
        }
        #endregion

        public Subjects()
        {
            RecordDate = DateTime.Today;
        }

        public static Subjects CalcurateBalancesPerSubject(IEnumerable<TransferSlip> transferSlips,IRepository repository)
        {
            if (transferSlips == null) throw new ArgumentNullException();
            if (repository == null) throw new ArgumentNullException();
            Subjects yesterdaySubjects = repository.LoadSubjectsByDate(DateTime.Today.AddDays(-1));
            Subjects todaySubjects = new Subjects();
            todaySubjects._Subjects =new List<Subject>( yesterdaySubjects.EnumerableSubjects);

            foreach (var ts in transferSlips)
            {
                todaySubjects.CalcurateSubjectBalanceWhenEnteringDebits(ts.CheckDebits());
                todaySubjects.CalcurateSubjectBalanceWhenEnteringCredits(ts.CheckCredits());
            }
            return todaySubjects;
        }
    }
}
