namespace OOPCafeInventory.Domain.ProductManagement;

public partial class Product
{
    public static int StockThreshold = 10;

    public static void ChangeStockThreshold(int newStockThreshold)
    {
        if (newStockThreshold > 0)
        {
            StockThreshold = newStockThreshold;
        }
    }

    protected object CreateSimpleProductRepresentation()
    {
        return $"Product {id}  ({name})";
    }

    protected void Log(string message)
    {
        Console.WriteLine(message);
    }

    public void UpdateLowStock()
    {
        if (AmountInStock < StockThreshold)
        {
            IsBelowStockThreshold = true;
        }
        else if (AmountInStock > StockThreshold)
        {
            IsBelowStockThreshold = false;
        }
    }
}
