using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for csProductInventory
/// </summary>
public class csInventory
{
    public csInventory()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int ProductTransactionId { get; set; }

    public string ProductInventoryDescription { get; set; }

    public int ProductId { get; set; }

    public int TransactionTypeId { get; set; }

    public string TransactionName { get; set; }

    public string TransactionType { get; set; }

    public DateTime? TransactionDate { get; set; }

    public int Quantity { get; set; }

    public int QuantityIn { get; set; }
    public int QuantityOut { get; set; }

    public decimal UnitCost { get; set; }

    public decimal UnitSale { get; set; }

    public decimal AveragCost { get; set; }

    public decimal TotalCost { get; set; }

    public decimal TotalSale { get; set; }

    public decimal GP { get; set; }

    public string TransactionSerialNumber { get; set; }

    public string Notes { get; set; }



    public int ClientId { get; set; }

    public string LocationName { get; set; }
}

public class csInventoryReceive
{
    public csInventoryReceive()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public int LocationId { get; set; }

    public int TransactionTypeId { get; set; }

    public string AutoIndexId { get; set; }
}

public class csStock
{
    public csStock()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int ProductTransactionId { get; set; }
    public int ProductId { get; set; }
    public int LocationId { get; set; }

    public string StockStatus { get; set; }


}