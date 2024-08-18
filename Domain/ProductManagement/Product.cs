using System.Text;
using OOPCafeInventory.Domain.Contracts;
using OOPCafeInventory.Domain.General;

namespace OOPCafeInventory.Domain.ProductManagement;

public partial class Product : ISaveable, ICloneable
{
    private int id;
    private string name = string.Empty;
    private string? description;

    protected int maxItemsInStock = 0;

    //use Properties instead
    /* private UnitType unitType; */
    /* private int AmountInStock = 0; */
    /* private bool isBelowStockThreshold = false; */

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value.Length > 50 ? value[..50] : value; }
    }

    public string Description
    {
        get { return description; }
        set
        {
            if (value == null)
            {
                description = string.Empty;
            }
            else
            {
                description = value.Length > 250 ? value[..250] : value;
            }
        }
    }

    public UnitType UnitType { get; set; }
    public int AmountInStock { get; protected set; }
    public bool IsBelowStockThreshold { get; private set; }

    public Price Price { get; set; }

    public Product(int id)
        : this(id, string.Empty) { }

    public Product(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public Product(
        int id,
        string name,
        string? description,
        Price price,
        UnitType unitType,
        int maxAmountInStock
    )
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        UnitType = UnitType;
        maxItemsInStock = maxAmountInStock;
        UpdateLowStock();
    }

    public virtual void UseProduct(int items)
    {
        if (items <= AmountInStock)
        {
            //use items
            AmountInStock -= items;

            UpdateLowStock();

            Log($"Amount in stock updated. Now {AmountInStock} items in stock");
        }
        else
        {
            Log(
                $"Not enough items in stock for {CreateSimpleProductRepresentation()}. {AmountInStock} available but {items} requested."
            );
        }
    }

    public virtual void IncreaseStock()
    {
        AmountInStock++;
    }

    public virtual void IncreaseStock(int amount)
    {
        int newStock = AmountInStock + amount;
        if (newStock <= maxItemsInStock)
        {
            AmountInStock += amount;
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

    protected virtual void DecreaseStock(int items, string reason)
    {
        if (items <= AmountInStock)
        {
            //descrease the stock with the specified number items
            AmountInStock -= items;
        }
        else
        {
            AmountInStock = 0;
        }
        UpdateLowStock();

        Log(reason);
    }

    public virtual string DisplayDetailsShort()
    {
        return $"{id}. {name} \n{AmountInStock} items in stock";
    }

    public virtual string DisplayDetailsfull()
    {
        return DisplayDetailsfull("");
    }

    public virtual string DisplayDetailsfull(string extraDetails)
    {
        StringBuilder sb = new();
        //Todo: add price here too;
        sb.Append($"{Id}. {Name} \n{Description}\n{Price} \n{AmountInStock} items in stock");

        sb.Append(extraDetails);

        if (IsBelowStockThreshold)
        {
            sb.Append("\n!!STOCK LOW!!");
        }
        return sb.ToString();
    }

    public string ConvertToStringForSaving()
    {
        return $"{Id};{Name};{Description};{maxItemsInStock};{Price.ItemPrice};{(int)Price.Currency};{(int)UnitType};4;";
    }

    public virtual object Clone()
    {
        return new Product(
            0,
            this.Name,
            this.Description,
            new Price() { ItemPrice = this.Price.ItemPrice, Currency = this.Price.Currency },
            this.UnitType,
            this.maxItemsInStock
        );
    }
}
