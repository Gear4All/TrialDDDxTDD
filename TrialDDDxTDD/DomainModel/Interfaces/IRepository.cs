using System;
using System.Collections.Generic;
using System.Text;
using TrialDDDxTDD.DomainModel;

namespace TrialDDDxTDD.DomainModel
{
  public  interface IRepository
    {
        //BalancesPerSubject LoadByDate(DateTime yesterDay);
        Subject LoadByDate(DateTime yesterDay);
        Subjects LoadSubjectsByDate(DateTime date);
    }
}
