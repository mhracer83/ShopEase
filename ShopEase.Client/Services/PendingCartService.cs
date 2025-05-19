public class PendingCartService
{
    public int? PendingProductId { get; private set; }

    public void SetPending(int productId) => PendingProductId = productId;

    public int? TakePending()
    {
        var id = PendingProductId;
        PendingProductId = null;
        return id;
    }
}
