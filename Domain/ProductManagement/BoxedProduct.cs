using System.Text;
using OOPCafeInventory.Domain.Contracts;
using OOPCafeInventory.Domain.General;

namespace OOPCafeInventory.Domain.ProductManagement;

public class BoxedProduct : Product, ISaveable, ILogable
{
    public int AmountPerBox { get; set; }

    public BoxedProduct(
        int id,
        string name,
        string? description,
        Price price,
        int maxAmountInStock,
        int amountPerBox
    )
        : base(id, name, description, price, UnitType.PerBox, maxAmountInStock)
    {
        AmountPerBox = amountPerBox;
    }

    public override string DisplayDetailsfull()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("Boxed Product\n");

        sb.Append($"{Id} {Name} \n{Description}\n{Price}\n{AmountInStock} items in stock");

        if (IsBelowStockThreshold)
        {
            sb.Append("\n!!STOCK LOW!!");
        }

        return sb.ToString();
    }

    public override void UseProduct(int items)
    {
        int smallestMultiple = 0;
        int batchSize;

        while (true)
        {
            smallestMultiple++;
            if (smallestMultiple * AmountPerBox > items)
            {
                batchSize = smallestMultiple * AmountPerBox;
                break;
            }
        }
        base.UseProduct(items);
    }

    /* public void UseBoxedProduct(int items) */
    /* { */
    /*     int smallestMultiple = 0; */
    /*     int batchSize; */
    /**/
    /*     while (true) */
    /*     { */
    /*         smallestMultiple++; */
    /*         if (smallestMultiple * AmountPerBox > items) */
    /*         { */
    /*             batchSize = smallestMultiple * AmountPerBox; */
    /*             break; */
    /*         } */
    /*     } */
    /**/
    /*     UseProduct(batchSize); //using base classes method */
    /* } */

    public override void IncreaseStock()
    {
        /* AmountInStock += AmountPerBox; */
        IncreaseStock(1);
    }

    public override void IncreaseStock(int amount)
    {
        //it is possible to call the bade here too, but we are assuming that this is handled differently
        int newStock = AmountInStock + amount * AmountPerBox;

        if (newStock <= maxItemsInStock)
        {
            AmountInStock += amount * AmountPerBox;
        }
        else
        {
            AmountInStock = maxItemsInStock; //we only store the possible items, overstock is not stored
            Log(
                $"{CreateSimpleProductRepresentation()} stock overflow. {newStock - AmountInStock} items ordered that couldn't be stored."
            );
        }
        UpdateLowStock();
    }

    public string ConvertToStringForSaving()
    {
        return $"{Id};{Name};{Description};{maxItemsInStock};{Price.ItemPrice};{(int)Price.Currency};{(int)UnitType};1;{AmountPerBox};";
    }

    void ILogable.Log(string message)
    {
        throw new NotImplementedException();
    }

    public override object Clone()
    {
        return new BoxedProduct(
            0,
            this.Name,
            this.Description,
            new Price() { ItemPrice = this.Price.ItemPrice, Currency = this.Price.Currency },
            this.maxItemsInStock,
            this.AmountPerBox
        );
    }
}
