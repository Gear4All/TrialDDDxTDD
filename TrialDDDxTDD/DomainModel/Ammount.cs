using System;
using System.Collections.Generic;
using System.Text;

namespace TrialDDDxTDD.DomainModel
{
    public class Ammount:IValueObject<Ammount>
    {
        public readonly uint Value;

        public Ammount(uint value)
        {
            if (value <= 0) throw new ArgumentException("ammount value is not able be lower than 0");
            this.Value = value;
        }

        public Ammount Add(Ammount other)
        {
            return new Ammount(this.Value + other.Value);
        }

        public string ToString()
        {
            return new string(Value.ToString());
        }

        public bool SameValueAs(Ammount other)
        {
            return this.Value == other.Value;
        }
    }
}
