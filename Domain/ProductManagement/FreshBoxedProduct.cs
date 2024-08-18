using OOPCafeInventory.Domain.General;

namespace OOPCafeInventory.Domain.ProductManagement;

public class FreshBoxedProduct : BoxedProduct
{
    public FreshBoxedProduct(
        int id,
        string name,
        string? description,
        Price price,
        UnitType unitType,
        int maxAmountInStock,
        int amountPerBox
    )
        : base(id, name, description, price, unitType, maxAmountInStock, amountPerBox) { }

    /* public void UseFreshBoxedProduct(int items) */
    /* { */
    /*     UseBoxedProduct(3); //sample invocation */
    /* } */
}
