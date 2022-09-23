using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class transferinventory : System.Web.UI.Page
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
                if (Page.User.IsInRole("inv003") == false)
                {
                    // No Permission Page.
                    Response.Redirect("nopermission.aspx");
                }

                DataClassesDataContext _db = new DataClassesDataContext();
                txtTransactionNumber.Text = utility.GetRandomString(5);
                int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
                int nLocationId = utility.GetDefaultLocationId(nClientId);
                hdnLocationId.Value = nLocationId.ToString();

                string strLocationName = _db.Locations.FirstOrDefault(l => l.LocationId == nLocationId && l.Status == 1).LocationName;

                lblTransferFrom.Text = strLocationName;

                BindTransferToLocation(nClientId, nLocationId);

               

               

                GetProduct(nClientId, nLocationId);
            }

        }

        DropDownList ddlDefaultLocation = ((DropDownList)Master.FindControl("ddlDefaultLocation"));
        ddlDefaultLocation.SelectedIndexChanged += new EventHandler(ddlDefaultLocation_SelectedIndexChanged);
    }


    protected void BindTransferToLocation(int nClientId, int nLocationId)
    {
        DataClassesDataContext _db = new DataClassesDataContext();

        var list = (from l in _db.Locations
                    where l.ClientId == nClientId && l.Status == 1 && l.LocationId != nLocationId
                    select new
                    {
                        Id = l.LocationId,
                        Name = l.LocationName
                    }).ToList();

        ddlTransferToLocation.DataSource = list;
        ddlTransferToLocation.DataTextField = "Name";
        ddlTransferToLocation.DataValueField = "Id";
        ddlTransferToLocation.DataBind();

        ddlTransferToLocation.Items.Insert(0, new ListItem("Select", "0"));
    }

    protected void GetProduct(int nClientId, int nLocationId)
    {


        string strCondition = "";
        List<string> aryProductId = new List<string>();
        string strProductId = "";
        List<csInventoryReceive> listReceive = new List<csInventoryReceive>();

        if (Session["slistTransfer"] != null)
        {
            listReceive = Session["slistTransfer"] as List<csInventoryReceive>;
        }

        if (Session["trnsAryProductId"] != null)
        {
            aryProductId = Session["trnsAryProductId"] as List<string>;
        }

        foreach (var l in aryProductId)
        {

            strProductId += l + ",";

        }

        if (strProductId.Length > 0)
        {
            strCondition = " AND p.ProductId in (" + strProductId.Trim().TrimEnd(',') + ")";
        }


        DataClassesDataContext _db = new DataClassesDataContext();

        string strQ = "SELECT p.ProductId, p.ProductCode, p.ProductName, p.MinimumQuantityStock, " +
                        " ISNULL(a.AreaName,'') AS AreaName,  ISNULL(m.ManufacturerName,'') AS ManufacturerName, ISNULL(t.LocationId,0) AS LocationId, " +
                        " ISNULL(LocationName + ' ('+CONVERT(varchar(100), ISNULL(QuantityInStock,0))+')'+'','') AS StockStatus, " +
                        " ISNULL(QuantityInStock,0) AS QuantityInStock" +
                        " FROM Product AS p " +
                        " LEFT OUTER JOIN ( Select l.LocationName, SUM(ISNULL(pt.QuantityIn,0))-SUM(ISNULL(pt.QuantityOut,0)) As QuantityInStock, pt.ProductId, l.LocationId FROM ProductTransaction AS pt " +
                        " INNER JOIN Location AS l on pt.LocationId = l.LocationId " +
                        " WHERE l.LocationId = " + nLocationId + "  GROUP BY pt.ProductId, l.LocationName, l.LocationId ) AS t on p.ProductId  = t.ProductId " +
                        " LEFT OUTER JOIN [Area] AS a on p.AreaId = a.AreaId " +
                        " LEFT OUTER JOIN Manufacturer AS m on p.ManufacturerId = m.ManufacturerId " +
                        " WHERE p.ClientId = " + nClientId + strCondition;


        IEnumerable<csProduct> list = _db.ExecuteQuery<csProduct>(strQ, string.Empty).ToList();

        var item = from p in list
                   join r in listReceive.ToList() on p.ProductId equals r.ProductId
                   select new
                   {
                       ProductId = p.ProductId,
                       ProductCode = p.ProductCode,
                       ProductName = p.ProductName,
                       AreaName = p.AreaName,
                       StockStatus = p.StockStatus,
                       ManufacturerName = p.ManufacturerName,
                       NewQuantity = r.Quantity,
                       QuantityInStock = p.QuantityInStock,
                       MinimumQuantityStock = p.MinimumQuantityStock,
                       TransactionTypeId = r.TransactionTypeId,
                       AutoIndexId = r.AutoIndexId,
                       LocationId = r.LocationId
                   };

        grdProductList.DataSource = item.ToList();
        grdProductList.DataKeyNames = new string[] { "ProductId", "QuantityInStock", "MinimumQuantityStock", "TransactionTypeId", "AutoIndexId", "LocationId" };
        grdProductList.DataBind();

        if (grdProductList.Rows.Count > 0)
        {
            btnSave.Visible = true;
        }
        else
        {
            btnSave.Visible = false;
        }
    }

    protected void grdProductList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
            int nProductId = Convert.ToInt32(grdProductList.DataKeys[e.Row.RowIndex].Values[0]);
            int nTransactionTypeId = Convert.ToInt32(grdProductList.DataKeys[e.Row.RowIndex].Values[3]);
            int nLocationId = Convert.ToInt32(grdProductList.DataKeys[e.Row.RowIndex].Values[5]);

            ImageButton btngrdRemove = (ImageButton)e.Row.FindControl("btngrdRemove");
            LinkButton lnkgrdProductDetails = (LinkButton)e.Row.FindControl("lnkgrdProductDetails");
            DropDownList ddlgrdTransferToLocation = (DropDownList)e.Row.FindControl("ddlgrdTransferToLocation");

            DataClassesDataContext _db = new DataClassesDataContext();
            var list = (from l in _db.Locations
                        where l.ClientId == nClientId && l.Status == 1 && l.LocationId != Convert.ToInt32(hdnLocationId.Value)
                        select new
                        {
                            Id = l.LocationId,
                            Name = l.LocationName
                        }).ToList();

            ddlgrdTransferToLocation.DataSource = list;
            ddlgrdTransferToLocation.DataTextField = "Name";
            ddlgrdTransferToLocation.DataValueField = "Id";
            ddlgrdTransferToLocation.DataBind();

            ddlgrdTransferToLocation.SelectedValue = nLocationId.ToString();

            btngrdRemove.CommandArgument = nProductId.ToString();
            lnkgrdProductDetails.CommandArgument = nProductId.ToString();


        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lblResult.Text = "";
            string strRequired = "";
            int nQuantity = 0;
            int nProductId = 0;
            int nQuantityInStock = 0;

            int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
            int nLocationId = utility.GetDefaultLocationId(nClientId);

            if (Session["searchProduct"] != null)
            {
                csProductSearch objP = (csProductSearch)Session["searchProduct"];
                nProductId = objP.ProductId;
                nQuantityInStock = objP.QuantityInStock;
            }

            List<string> aryProductId = new List<string>();

            if (Session["trnsAryProductId"] != null)
            {
                aryProductId = Session["trnsAryProductId"] as List<string>;
            }

            if (nProductId == 0 || txtSearch.Text.Trim() == "")
            {
                strRequired += "Missing required field: Product.<br />";

            }

            if (txtQuantity.Text.Trim() != "")
            {
                try
                {
                    nQuantity = Convert.ToInt32(txtQuantity.Text.Trim());
                    if (nQuantity > nQuantityInStock)
                    {
                        strRequired += "Invalide Quantity<br />";
                    }
                }
                catch
                {
                    strRequired += "Invalide Quantity<br />";
                }
            }
            else
            {
                strRequired += "Quantity must be greater than 0<br />";
            }

            if (Convert.ToInt32(ddlTransferToLocation.SelectedItem.Value) == 0)
            {
                strRequired += "Please Select Location<br />";
            }

            if (strRequired.Length > 0)
            {
                lblResult.Text = utility.GetSystemRequiredMessage(strRequired);
                return;
            }

            List<csInventoryReceive> listReceive = new List<csInventoryReceive>();
            csInventoryReceive objReceive = new csInventoryReceive();

            if (Session["slistTransfer"] != null)
            {
                listReceive = Session["slistTransfer"] as List<csInventoryReceive>;
            }

            if (listReceive.Count > 0)
            {
                objReceive = listReceive.Where(l => l.ProductId == nProductId && l.LocationId == Convert.ToInt32(ddlTransferToLocation.SelectedItem.Value)).FirstOrDefault();

                if (objReceive != null)
                {
                    objReceive.Quantity = objReceive.Quantity + nQuantity;
                }
                else
                {
                    objReceive = new csInventoryReceive();

                    objReceive.ProductId = nProductId;
                    objReceive.Quantity = nQuantity;
                    objReceive.LocationId = Convert.ToInt32(ddlTransferToLocation.SelectedItem.Value);
                    objReceive.TransactionTypeId = 6;
                    objReceive.AutoIndexId = Guid.NewGuid().ToString();

                    listReceive.Add(objReceive);

                    aryProductId.Add(objReceive.ProductId.ToString());
                }
            }
            else
            {

                objReceive.ProductId = nProductId;
                objReceive.Quantity = nQuantity;
                objReceive.LocationId = Convert.ToInt32(ddlTransferToLocation.SelectedItem.Value);
                objReceive.TransactionTypeId = 6;
                objReceive.AutoIndexId = Guid.NewGuid().ToString();

                listReceive.Add(objReceive);

                aryProductId.Add(objReceive.ProductId.ToString());

            }

            Session.Add("trnsAryProductId", aryProductId);
            Session.Add("slistTransfer", listReceive);

            GetProduct(nClientId, nLocationId);

            Reset();
        }
        catch (Exception ex)
        {
            lblResult.Text = utility.GetSystemErrorMessage(ex.Message);
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblResult.Text = "";
        Reset();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        lblResult.Text = "";
        Session["trnsAryProductId"] = null;
        Session["slistTransfer"] = null;
        Reset();
        GetProduct(0, 0);
    }

    protected void btngrdRemove_Click(object sender, EventArgs e)
    {
        try
        {
            List<string> aryProductId = new List<string>();

            int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
            int nLocationId = utility.GetDefaultLocationId(nClientId);
            ImageButton btngrdRemove = (ImageButton)sender;

            GridViewRow row = (GridViewRow)btngrdRemove.NamingContainer;
            int index = row.RowIndex;

            string AutoIndexId = grdProductList.DataKeys[index].Values[4].ToString();

            int nProductId = Convert.ToInt32(btngrdRemove.CommandArgument);
            List<csInventoryReceive> listReceive = new List<csInventoryReceive>();
            if (Session["slistTransfer"] != null)
            {
                var test = Session["slistTransfer"];
                listReceive = Session["slistTransfer"] as List<csInventoryReceive>;

                csInventoryReceive objReceive = listReceive.Where(l => l.ProductId == nProductId && l.AutoIndexId == AutoIndexId).FirstOrDefault();

                if (objReceive != null)
                    listReceive.Remove(objReceive);


                if (Session["trnsAryProductId"] != null)
                {
                    aryProductId = Session["trnsAryProductId"] as List<string>;
                    aryProductId.Remove(nProductId.ToString());
                    Session.Add("trnsAryProductId", aryProductId);
                }

                if (listReceive.Count > 0)
                    Session.Add("slistTransfer", listReceive);

                GetProduct(nClientId, nLocationId);
            }
        }
        catch (Exception ex)
        {
            lblResult.Text = utility.GetSystemErrorMessage(ex.Message);
        }
    }

    protected void lnkgrdProductDetails_Click(object sender, EventArgs e)
    {
        try
        {
            List<string> aryProductId = new List<string>();

            int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
            int nLocationId = utility.GetDefaultLocationId(nClientId);
            LinkButton lnkgrdProductDetails = (LinkButton)sender;
            int nProductId = Convert.ToInt32(lnkgrdProductDetails.CommandArgument);

            Response.Redirect("productdetails.aspx?pid=" + nProductId);
        }
        catch (Exception ex)
        {
            lblResult.Text = utility.GetSystemErrorMessage(ex.Message);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
            int nLocationId = utility.GetDefaultLocationId(nClientId);
            DataClassesDataContext _db = new DataClassesDataContext();

            foreach (GridViewRow gr in grdProductList.Rows)
            {
                string strRequired = "";
                int nQuantity = 0;
                TextBox txtgrdQuantity = (TextBox)gr.FindControl("txtgrdQuantity");
                DropDownList ddlgrdTransferToLocation = (DropDownList)gr.FindControl("ddlgrdTransferToLocation");
                int nProductId = Convert.ToInt32(grdProductList.DataKeys[gr.RowIndex].Values[0]);
                int nQuantityInStock = Convert.ToInt32(grdProductList.DataKeys[gr.RowIndex].Values[1]);

                if (txtgrdQuantity.Text.Trim() != "")
                {
                    try
                    {
                        nQuantity = Convert.ToInt32(txtgrdQuantity.Text.Trim());

                        if (nQuantity > nQuantityInStock)
                        {
                            strRequired += "Invalide Quantity<br />";
                        }
                    }
                    catch
                    {
                        strRequired += "Invalide Quantity<br />";
                    }
                }
                else
                {
                    strRequired += "Quantity must be greater than 0<br />";
                }

                if (Convert.ToInt32(ddlgrdTransferToLocation.SelectedItem.Value) == 0)
                {
                    strRequired += "Please Select Location<br />";
                }

                if (strRequired.Length > 0)
                {
                    lblResult.Text = utility.GetSystemRequiredMessage(strRequired);
                    return;
                }

                ProductTransaction objPrdTrans = new ProductTransaction();

                objPrdTrans.ProductId = nProductId;
                objPrdTrans.TransactionTypeId = 6;
                objPrdTrans.TransactionDate = DateTime.Now;
                objPrdTrans.QuantityIn = nQuantity;
                objPrdTrans.QuantityOut = 0;
                objPrdTrans.UnitCost = 0;
                objPrdTrans.UnitSale = 0;
                objPrdTrans.LocationId = Convert.ToInt32(ddlgrdTransferToLocation.SelectedItem.Value);
                objPrdTrans.ClientId = nClientId;
                objPrdTrans.TransactionNumber = "TRNS-" + txtTransactionNumber.Text.ToString();
                _db.ProductTransactions.InsertOnSubmit(objPrdTrans);

                // remove   
                objPrdTrans = new ProductTransaction();

                objPrdTrans.ProductId = nProductId;
                objPrdTrans.TransactionTypeId = 7; // Transfer Out
                objPrdTrans.TransactionDate = DateTime.Now;
                objPrdTrans.QuantityIn = 0;
                objPrdTrans.QuantityOut = nQuantity;
                objPrdTrans.UnitCost = 0;
                objPrdTrans.UnitSale = 0;
                objPrdTrans.LocationId = Convert.ToInt32(hdnLocationId.Value);
                objPrdTrans.ClientId = nClientId;
                objPrdTrans.TransactionNumber = "TRNS-" + txtTransactionNumber.Text.ToString();
                _db.ProductTransactions.InsertOnSubmit(objPrdTrans);

                _db.SubmitChanges();

                utility.SetProductQuantity(nProductId);
            }

            Session["slistTransfer"] = null;
            Session["trnsAryProductId"] = null;
           

            Response.Redirect("productlist.aspx");
        }
        catch (Exception ex)
        {
            lblResult.Text = utility.GetSystemErrorMessage(ex.Message);
        }
    }
   

    protected void txtgrdQuantity_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string strRequired = "";
            int nQuantity = 0;

            TextBox txtgrdQuantity = (TextBox)sender;

            if (txtgrdQuantity.Text.Trim() != "")
            {
                try
                {
                    nQuantity = Convert.ToInt32(txtgrdQuantity.Text.Trim());
                }
                catch
                {
                    strRequired += "Invalide Quantity<br />";
                }
            }
            else
            {
                strRequired += "Quantity must be greater than 0<br />";
            }



            if (strRequired.Length > 0)
            {
                lblResult.Text = utility.GetSystemRequiredMessage(strRequired);
                return;
            }
            GridViewRow row = (GridViewRow)txtgrdQuantity.NamingContainer;
            int index = row.RowIndex;

            int nProductId = Convert.ToInt32(grdProductList.DataKeys[index].Values[0]);
            string AutoIndexId = grdProductList.DataKeys[index].Values[4].ToString();

            List<csInventoryReceive> listReceive = new List<csInventoryReceive>();
            if (Session["slistTransfer"] != null)
            {
                var test = Session["slistTransfer"];
                listReceive = Session["slistTransfer"] as List<csInventoryReceive>;

                csInventoryReceive objReceive = listReceive.Where(l => l.ProductId == nProductId && l.AutoIndexId == AutoIndexId).FirstOrDefault();

                objReceive.Quantity = nQuantity;


                if (listReceive.Count > 0)
                    Session.Add("slistTransfer", listReceive);

            }
        }
        catch (Exception ex)
        {
            lblResult.Text = utility.GetSystemErrorMessage(ex.Message);
        }
    }

    protected void ddlgrdTransferToLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlgrdTransferToLocation = (DropDownList)sender;

            GridViewRow row = (GridViewRow)ddlgrdTransferToLocation.NamingContainer;
            int index = row.RowIndex;

            int nProductId = Convert.ToInt32(grdProductList.DataKeys[index].Values[0]);

            string AutoIndexId = grdProductList.DataKeys[index].Values[4].ToString();


            List<csInventoryReceive> listReceive = new List<csInventoryReceive>();
            if (Session["slistTransfer"] != null)
            {               
                listReceive = Session["slistTransfer"] as List<csInventoryReceive>;

                csInventoryReceive objReceive = listReceive.Where(l => l.ProductId == nProductId && l.AutoIndexId == AutoIndexId).FirstOrDefault();

                objReceive.LocationId = Convert.ToInt32(ddlgrdTransferToLocation.SelectedItem.Value);


                if (listReceive.Count > 0)
                    Session.Add("slistTransfer", listReceive);

            }
        }
        catch (Exception ex)
        {
            lblResult.Text = utility.GetSystemErrorMessage(ex.Message);
        }
    }

    protected void ddlDefaultLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["slistTransfer"] = null;
        Session["trnsAryProductId"] = null;      

        Reset();

        DataClassesDataContext _db = new DataClassesDataContext();
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);

        DropDownList ddlDefaultLocation = ((DropDownList)Master.FindControl("ddlDefaultLocation"));

        Preference objPf = _db.Preferences.Where(s => s.ClientId == 1).SingleOrDefault();

        objPf.DefaultLocationId = Convert.ToInt32(ddlDefaultLocation.SelectedItem.Value);

        _db.SubmitChanges();


        Response.Redirect("transferinventory.aspx");
    }

    protected void Reset()
    {
        txtSearch.Text = "";
        txtQuantity.Text = "";

        Session["searchProduct"] = null;

    }
}