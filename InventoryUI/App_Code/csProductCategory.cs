using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for csProductCategory
/// </summary>
public class csProductCategory
{
	public csProductCategory()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int CategoryId { get; set; }

    public string CategoryName { get; set; }

    public string CategorySerial { get; set; }

    public int? Status { get; set; }   

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public int ClientId { get; set; }

    public string Active { get; set; }
}