using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for csProductInventory
/// </summary>
public class csProduct
{
    public csProduct()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int? ProductId { get; set; }

    public string ProductCode { get; set; }

    public string ProductName { get; set; }

    public string ManufacturerName { get; set; }   
   
    public int Quantity { get; set; }   

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public int ClientId { get; set; }

    public string LocationName { get; set; }

    public string AreaName { get; set; }

    public string StockStatus { get; set; }

    public int LocationId { get; set; }

    public int QuantityInStock { get; set; }

    public int MinimumQuantityStock { get; set; }

    public int TransactionTypeId { get; set; }

    public string AutoIndexId { get; set; }
}

public class csProductDetails
{
    public csProductDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int? ProductId { get; set; }

    public string ProductCode { get; set; }

    public string ProductName { get; set; }

    public string ProductDescription { get; set; }

    public string ProductSize { get; set; }

    public string ProductWight { get; set; }

    public string ProductColor { get; set; }

    public int CategoryId { get; set; }

    public int AreaId { get; set; }

    public int LocationId { get; set; }

    public int ManufacturerId { get; set; }       

    public string ProductSerial { get; set; }

    public int MinimumQuantityStock { get; set; }

    public int Status { get; set; }   

    public int ClientId { get; set; }
}

public class csProductSearch
{
    public csProductSearch()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int ProductId { get; set; }

    public string ProductCode { get; set; }

    public string ProductName { get; set; }

    public int QuantityInStock { get; set; }  
}

public class SP_Product
{
    public int ProductId { get; set; }
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public string AreaName { get; set; }
    public string ManufacturerName { get; set; }
    public string ModifiedBy { get; set; }
    public string CreatedBy { get; set; }
    public int ClientId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}

