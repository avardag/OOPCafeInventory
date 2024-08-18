using OOPCafeInventory.Domain.General;

namespace OOPCafeInventory.Domain.ProductManagement;

public class BulkProduct : Product
{
    public BulkProduct(
        int id,
        string name,
        string? description,
        Price price,
        UnitType unitType,
        int maxAmountInStock
    )
        : base(id, name, description, price, UnitType.PerKg, maxAmountInStock) { }
}
