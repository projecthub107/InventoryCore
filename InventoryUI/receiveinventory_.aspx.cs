using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class receiveinventory_ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strName = string.Empty;

            if (Session["oUser"] == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings["LoginPage"].ToString());
            }
            else
            {

                int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
                DataClassesDataContext _db = new DataClassesDataContext();

                int nLocationId = utility.GetDefaultLocationId(nClientId);

                GetProduct(nClientId, nLocationId);





            }


        }
    }

    protected void GetProduct(int nClientId, int nLocationId)
    {

        DataClassesDataContext _db = new DataClassesDataContext();

        string strQ = "SELECT p.ProductId, p.ProductCode, p.ProductName, " +
                        " a.AreaName, Quantity AS StockStatus, m.ManufacturerName," +
                        " p.ModifiedBy, p.CreatedBy, p.ClientId, p.CreatedDate, p.ModifiedDate " +
                        " FROM Product AS p " +
                        " LEFT OUTER JOIN ( Select LocationName + ' ('+CONVERT(varchar(100), SUM(ISNULL(QuantityIn,0))-SUM(ISNULL(QuantityOut,0)) )+')'+'' As Quantity, ProductId FROM ProductTransaction AS i " +
                        " INNER JOIN Location AS l on i.LocationId = l.LocationId " +
                        " WHERE i.LocationId = " + nLocationId + "  GROUP BY ProductId, LocationName ) AS i on p.ProductId  = i.ProductId " +
                        " LEFT OUTER JOIN [Area] AS a on p.AreaId = a.AreaId " +
                        " LEFT OUTER JOIN Manufacturer AS m on p.ManufacturerId = m.ManufacturerId " +
                        " WHERE p.ClientId = " + nClientId;


        IEnumerable<csProduct> list = _db.ExecuteQuery<csProduct>(strQ, string.Empty).ToList();

        grdProductList.DataSource = list;
        grdProductList.DataKeyNames = new string[] { "ProductId" };
        grdProductList.DataBind();
    }
}