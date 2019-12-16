using System;
using System.Collections.Generic;
using System.Text;

namespace TrialDDDxTDD.DomainModel
{
    public class Debit
    {
        private string SubjectName;
        private Ammount Ammount;

        private Debit(string subjectName, Ammount ammount)
        {
            this.SubjectName = subjectName;
            this.Ammount = ammount;
        }
        public void CalcurateAmmount(uint ammount)
        {
            Ammount = Ammount.Add(new Ammount(ammount));
        }

        public string CheckSubjectName()
        {
            return SubjectName;
        }
        public uint CheckValueOfAmmount()
        {
            return Ammount.Value;
        }

        public static Debit Factory(string subjectName, uint ammount)
        {
            return new Debit(subjectName, new Ammount(ammount));
        }
    }
}
