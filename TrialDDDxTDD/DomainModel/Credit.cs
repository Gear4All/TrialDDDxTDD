using System;
using System.Collections.Generic;
using System.Text;

namespace TrialDDDxTDD.DomainModel
{
    public class Credit
    {
        private string SubjectName;
        private Ammount Ammount;

        private Credit(string subjectName, Ammount ammount)
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

        public static Credit Factory(string subjectName, uint ammount)
        {
            return new Credit(subjectName, new Ammount(ammount));
        }
    }
}
