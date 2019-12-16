using System;
using System.Collections.Generic;
using System.Text;

namespace TrialDDDxTDD.DomainModel
{
   public class Balance : IValueObject<Balance>
    {
        public readonly int Value;

        public Balance(int value)
        {
            this.Value = value;
        }

        public Balance Add(Balance other)
        {
            return new Balance(this.Value + other.Value);
        }

        public bool SameValueAs(Balance other)
        {
            return this.Value == other.Value;
        }
    }
}
