using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.DataAccess.BusinessObject;

namespace Customer.API
{
    public interface IResult<TBusinessObject, TModel> where TBusinessObject : BusinessObjectBase where TModel : class, new()
    {
        Task<TModel> Handler();
    }
}
