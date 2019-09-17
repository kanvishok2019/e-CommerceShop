using Infrastructure.Core.Command;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Commands
{
    public class ProcessPurchaseOrderCommand : Command
    {
        public int PurchaseOrderNo { get; }
        public ProcessPurchaseOrderCommand(int purchaseOrderNo)
        {
            PurchaseOrderNo = purchaseOrderNo;
        }
    }
}
