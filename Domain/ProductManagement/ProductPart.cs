namespace OOPCafeInventory.Domain.ProductManagement;

public partial class Product
{
    private object CreateSimpleProductRepresentation()
    {
        return $"Product {id}  ({name})";
    }

    private void Log(string message)
    {
        Console.WriteLine(message);
    }

    private void UpdateLowStock()
    {
        if (AmountInStock < 10)
        {
            IsBelowStockThreshold = true;
        }
        else if (AmountInStock > 10)
        {
            IsBelowStockThreshold = false;
        }
    }
}
