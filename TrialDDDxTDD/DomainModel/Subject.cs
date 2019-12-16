using System;
using System.Collections.Generic;
using System.Text;

namespace TrialDDDxTDD.DomainModel
{
    public class Subject
    {
        public string Name { get; private set; }
        private Balance Balance { get;  set; }

        public void CalcurateBalance(int ammountValue)
        {
            Balance = new Balance(Balance.Value + ammountValue);
        }
        public int CheckBalance()
        {
            return Balance.Value;
        }

        private Subject(string name, Balance balance)
        {
            Name = name;
            Balance = balance;
        }
        public static Subject Factory(string subjectName, int ammountValue)
        {
            return new Subject(subjectName, new Balance(ammountValue));
        }
    }
}
