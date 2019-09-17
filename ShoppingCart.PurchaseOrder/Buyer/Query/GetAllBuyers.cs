using System.Collections.Generic;
using Infrastructure.Core.Query;

namespace ShoppingCart.ApplicationCore.Buyer.Query
{
    public class GetAllBuyers: IQuery<IReadOnlyList<Buyer>>
    {
    }
}
