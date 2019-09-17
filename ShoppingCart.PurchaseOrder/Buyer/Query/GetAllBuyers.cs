using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Core.Query;

namespace ShoppingCart.ApplicationCore.Buyer.Query
{
    public class GetAllBuyers: IQuery<IReadOnlyList<Buyer>>
    {
    }
}
