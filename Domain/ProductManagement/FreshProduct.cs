using System.Text;
using OOPCafeInventory.Domain.Contracts;
using OOPCafeInventory.Domain.General;

namespace OOPCafeInventory.Domain.ProductManagement;

public class FreshProduct : Product, ISaveable
{
    public FreshProduct(
        int id,
        string name,
        string? description,
        Price price,
        UnitType unitType,
        int maxAmountInStock
    )
        : base(id, name, description, price, unitType, maxAmountInStock) { }

    public DateTime ExpiryDateTime { get; set; }
    public string? StorageInstructions { get; set; }

    public override string DisplayDetailsfull(string extraDetails)
    {
        StringBuilder sb = new();
        //Todo: add price here too;
        sb.Append($"{Id}. {Name} \n{Description}\n{Price} \n{AmountInStock} items in stock");

        sb.Append(extraDetails);

        if (IsBelowStockThreshold)
        {
            sb.Append("\n!!STOCK LOW!!");
        }

        sb.AppendLine("Storage instructions: " + StorageInstructions); //since this line needs to go here , we can't call the base here
        sb.AppendLine("Expiry date: " + ExpiryDateTime.ToShortDateString());
        return sb.ToString();
    }

    public string ConvertToStringForSaving()
    {
        return $"{Id};{Name};{Description};{maxItemsInStock};{Price.ItemPrice};{(int)Price.Currency};{(int)UnitType};2;";
    }
}
