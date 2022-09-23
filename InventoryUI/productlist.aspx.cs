using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class productlist : System.Web.UI.Page
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
                if (Page.User.IsInRole("prod001") == false)
                {
                    // No Permission Page.
                    Response.Redirect("nopermission.aspx");
                }

                int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
                DataClassesDataContext _db = new DataClassesDataContext();
                BindLocation(nClientId);
                int nLocationId = utility.GetDefaultLocationId(nClientId);

                GetProduct(nClientId, 0, 0);





            }


        }

        DropDownList ddlDefaultLocation = ((DropDownList)Master.FindControl("ddlDefaultLocation"));
        ddlDefaultLocation.SelectedIndexChanged += new EventHandler(ddlDefaultLocation_SelectedIndexChanged);
    }

    protected void BindLocation(int nClientId)
    {
        DataClassesDataContext _db = new DataClassesDataContext();

        var list = (from l in _db.Locations
                    where l.ClientId == nClientId && l.Status == 1
                    select new
                    {
                        Id = l.LocationId,
                        Name = l.LocationName
                    }).ToList();

        ddlLocation.DataSource = list;
        ddlLocation.DataTextField = "Name";
        ddlLocation.DataValueField = "Id";
        ddlLocation.DataBind();

        ddlLocation.Items.Insert(0, "All");
    }

    protected void GetProduct(int nClientId, int nProductId, int nPageNo)
    {
        string strCondition = "";
        DataClassesDataContext _db = new DataClassesDataContext();
        grdProductList.PageIndex = nPageNo;


        if (nProductId > 0)
        {
            strCondition = " AND p.ProductId =" + nProductId;
        }

        string strQ = "SELECT p.ProductId, p.ProductCode, p.ProductName, " +
                        " a.AreaName, m.ManufacturerName," +
                        " p.ModifiedBy, p.CreatedBy, p.ClientId, p.CreatedDate, p.ModifiedDate " +
                        " FROM Product AS p " +
                        " LEFT OUTER JOIN [Area] AS a on p.AreaId = a.AreaId " +
                        " LEFT OUTER JOIN Manufacturer AS m on p.ManufacturerId = m.ManufacturerId " +
                        " WHERE p.ClientId = " + nClientId + " " + strCondition;


        IEnumerable<csProduct> list = _db.ExecuteQuery<csProduct>(strQ, string.Empty).ToList();

        //foreach (var p in list)
        //{
        //    utility.SetProductQuantity((int)p.ProductId);
        //}
        if (ddlItemPerPage.SelectedValue != "4")
        {
            grdProductList.PageSize = Convert.ToInt32(ddlItemPerPage.SelectedValue);
        }
        else
        {
            grdProductList.PageSize = 200;
        }
        grdProductList.DataSource = list;
        grdProductList.DataKeyNames = new string[] { "ProductId" };
        grdProductList.DataBind();

        lblCurrentPageNo.Text = Convert.ToString(nPageNo + 1);
    }

    protected void grdProductList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            int nProductId = Convert.ToInt32(grdProductList.DataKeys[e.Row.RowIndex].Values[0]);

            GridView grd = (GridView)e.Row.FindControl("grdStockList");

            GetStockList(grd, nProductId);
        }
    }

    protected void grdProductList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        GetProduct(nClientId, 0, e.NewPageIndex);
    }


    private void GetStockList(GridView grd, int nProductId)
    {
        DataClassesDataContext _db = new DataClassesDataContext();
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        int nLocationId = utility.GetDefaultLocationId(nClientId);

        string strCondition = "";
        if (ddlLocation.SelectedItem.Value != "All")
        {
            strCondition = " AND l.LocationId = " + ddlLocation.SelectedItem.Value;
        }

        string sql = "Select l.LocationName + ' ('+CONVERT(varchar(100), SUM(ISNULL(QuantityIn,0))-SUM(ISNULL(QuantityOut,0)) )+')'+'' As StockStatus, pt.ProductId, l.LocationId FROM Location AS l " +
                        " LEFT OUTER JOIN ProductTransaction AS pt on pt.LocationId = l.LocationId " +
                        " WHERE l.Status = 1 AND pt.ProductId = " + nProductId + " " + strCondition + "  GROUP BY pt.ProductId, l.LocationName, l.LocationId ";

        IEnumerable<csStock> list = _db.ExecuteQuery<csStock>(sql, string.Empty).ToList();

        grd.DataSource = list;
        grd.DataKeyNames = new string[] { "ProductTransactionId", "ProductId", "LocationId" };
        grd.DataBind();


    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        Response.Redirect("productdetails.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        int nProductId = 0;

        if (Session["searchProduct"] != null)
        {
            csProductSearch objP = (csProductSearch)Session["searchProduct"];
            nProductId = objP.ProductId;

        }

        int nLocationId = utility.GetDefaultLocationId(nClientId);

        GetProduct(nClientId, nProductId, 0);
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        lblResult.Text = "";

        Reset();
        GetProduct(nClientId, 0,0);
    }

    protected void Reset()
    {

        txtSearch.Text = "";
        ddlLocation.SelectedItem.Value = "All";

        Session["searchProduct"] = null;


    }

    protected void ddlDefaultLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataClassesDataContext _db = new DataClassesDataContext();
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);

        DropDownList ddlDefaultLocation = ((DropDownList)Master.FindControl("ddlDefaultLocation"));

        Preference objPf = _db.Preferences.Where(s => s.ClientId == 1).SingleOrDefault();

        objPf.DefaultLocationId = Convert.ToInt32(ddlDefaultLocation.SelectedItem.Value);

        _db.SubmitChanges();


        int nDefaultLocationId = Convert.ToInt32(objPf.DefaultLocationId);
        GetProduct(nClientId, 0, 0);
    }

    protected void ddlItemPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        GetProduct(nClientId, 0, 0);
    }
}