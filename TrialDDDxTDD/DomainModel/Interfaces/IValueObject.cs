using System;
using System.Collections.Generic;
using System.Text;

namespace TrialDDDxTDD.DomainModel
{
    public interface IValueObject<T>
    {
        bool SameValueAs(T other);
        T Add(T other);
        string ToString();
    }
}
