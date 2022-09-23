using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for csArea
/// </summary>
public class csArea
{
	public csArea()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int AreaId { get; set; }

    public string AreaName { get; set; }

    public int? Status { get; set; }   

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public int ClientId { get; set; }

    public string Active { get; set; }
}