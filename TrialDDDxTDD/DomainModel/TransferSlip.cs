using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TrialDDDxTDD.DomainModel;

namespace TrialDDDxTDD.DomainModel
{
    public class TransferSlip
    {
        DateTime OccuredDate;
        List<Debit> Debits;
        List<Credit> Credits;

        private TransferSlip(DateTime occuredDate, IEnumerable<Debit> debits, IEnumerable<Credit> credits)
        {
            OccuredDate = occuredDate;
            Debits = debits.ToList();
            Credits = credits.ToList();
        }

        public uint CheckAmmountTheSubjectIs(string subjectName)
        {
            foreach (var debit in Debits) if (debit.CheckSubjectName() == subjectName) return debit.CheckValueOfAmmount();
            foreach (var credit in Credits) if (credit.CheckSubjectName() == subjectName) return credit.CheckValueOfAmmount();
            throw new CheckedSubjectNameIsNotFoundException();
        }

        internal IQueryable<Debit> CheckDebits()
        {
            return Debits.AsQueryable();
        }
        internal IQueryable<Credit> CheckCredits()
        {
            return Credits.AsQueryable();
        }

        public static TransferSlip Factory(DateTime occuredDate, IEnumerable<Debit> debits, IEnumerable<Credit> credits)
        {
            if (debits.Select(x => int.Parse(x.CheckValueOfAmmount().ToString())).Sum() != credits.Select(x => int.Parse(x.CheckValueOfAmmount().ToString())).Sum()) throw new NotBalanceBetweenDebitAndCreditException();
            return new TransferSlip(occuredDate,debits, credits);
        }

    }
}
