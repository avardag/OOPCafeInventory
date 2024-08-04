namespace OOPCafeInventory.Domain.OrderManagement;

public class Order
{
    public int Id { get; private set; }
    public DateTime OrderFullfilmentDate { get; private set; }
    public List<OrderItem> OrderItems { get; }
    public bool Fullfilled { get; set; } = false;

    public Order()
    {
        Id = new Random().Next(9999999);

        int numberOfSeconds = new Random().Next(100);
        OrderFullfilmentDate = DateTime.Now.AddSeconds(numberOfSeconds);

        OrderItems = new List<OrderItem>();
    }
}
