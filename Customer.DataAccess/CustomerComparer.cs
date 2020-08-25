using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Customer.DataAccess.BusinessObject;

namespace Customer.DataAccess
{
    public class CustomerComparer : IEqualityComparer<Customers>
    {
        public bool Equals(Customers x, Customers y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }
            else if (ReferenceEquals(x, null))
            {
                return false;
            }
            else if (ReferenceEquals(y, null))
            {
                return false;
            }
            else if (x.GetType() != y.GetType())
            {
                return false;
            }
            return ReferenceEquals(x.Address, y.Address) && ReferenceEquals(x.PersonalDetail, y.PersonalDetail) &&
                   ReferenceEquals(x.BankDetails, y.BankDetails);

        }

        public int GetHashCode(Customers obj)
        {
            return obj.GetHashCode();
        }
    }
}
