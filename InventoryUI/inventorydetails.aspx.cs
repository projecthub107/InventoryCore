using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class inventorydetails : System.Web.UI.Page
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
                txtTransactionNumber.Text = utility.GetRandomString(5);
                int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
                DataClassesDataContext _db = new DataClassesDataContext();

                if (Request.QueryString.Get("pid") != null)
                {
                    int nProductId = Convert.ToInt32(Request.QueryString.Get("pid"));

                    hdnProductId.Value = nProductId.ToString();

                    int nLocationId = 0;

                    if (Request.QueryString.Get("lid") != null)
                    {
                        nLocationId = Convert.ToInt32(Request.QueryString.Get("lid"));
                        hdnLocationId.Value = nLocationId.ToString();
                    }
                    else
                    {
                        nLocationId = utility.GetDefaultLocationId(nClientId);
                        hdnLocationId.Value = nLocationId.ToString();
                    }
                    BindLocation(nClientId, nLocationId);
                    GetProductDetails(nProductId, nLocationId);
                }




            }


        }

        DropDownList ddlDefaultLocation = ((DropDownList)Master.FindControl("ddlDefaultLocation"));
        ddlDefaultLocation.SelectedIndexChanged += new EventHandler(ddlDefaultLocation_SelectedIndexChanged);
    }

    protected void GetProductDetails(int nProductId, int nLocationId)
    {
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        DataClassesDataContext _db = new DataClassesDataContext();


        string strCondition = "";

        if (nLocationId != 0)
        {
            string strLocationName = _db.Locations.FirstOrDefault(l => l.LocationId == nLocationId).LocationName;

            lblTransferFrom.Text = strLocationName;

            strCondition = " WHERE i.LocationId = " + nLocationId + " ";

            btnTransfer.Visible = true;
        }

        string strQ = "SELECT p.ProductId, p.ProductCode, p.ProductName, " +
            " a.AreaName, i.QuantityInStock, " +
            " p.ModifiedBy, p.CreatedBy, p.ClientId, p.CreatedDate, p.ModifiedDate " +
            " FROM Product AS p " +
            " INNER JOIN ( Select ProductId,  SUM(ISNULL(QuantityIn,0))-SUM(ISNULL(QuantityOut,0)) AS QuantityInStock FROM ProductTransaction AS i" +
           " INNER JOIN Location AS l on i.LocationId = l.LocationId " +
            " " + strCondition + " GROUP BY ProductId ) AS i on p.ProductId  = i.ProductId " +
            " LEFT OUTER JOIN [Area] AS a on p.AreaId = a.AreaId " +
            " WHERE i.ProductId = " + nProductId + " AND p.ClientId = " + nClientId;



        csProduct objPrd = _db.ExecuteQuery<csProduct>(strQ, string.Empty).FirstOrDefault();
        if (objPrd != null)
        {
            lblCode.Text = objPrd.ProductCode;
            lblName.Text = objPrd.ProductName;
            lblArea.Text = objPrd.AreaName;
            lblStock.Text = objPrd.QuantityInStock.ToString() + " in-stock";

            GetProductInventory(nProductId, nLocationId);

            BindTransferToLocation(nClientId, nLocationId);
        }
    }

    protected void BindLocation(int nClientId, int nLocationId)
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

        ddlLocation.Items.Insert(0, new ListItem("All", "0"));

        ddlLocation.SelectedValue = nLocationId.ToString();


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

    protected void GetProductInventory(int nProductId, int nLocationId)
    {
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        DataClassesDataContext _db = new DataClassesDataContext();

        string strCondition = "";
        if (nLocationId != 0)
        {
            strCondition = " AND i.LocationId = " + nLocationId + " ";
        }

        string strQ = "SELECT ProductTransactionId, p.ProductId, " +
                        " i.TransactionTypeId, t.TransactionName, i.TransactionDate,  " +
                        " QuantityIn, QuantityOut, l.LocationName " +
                        " FROM Product AS p " +
                        " INNER JOIN  ProductTransaction AS i on p.ProductId  = i.ProductId " +
                        " INNER JOIN  TransactionType AS t on i.TransactionTypeId = t.TransactionTypeId " +
                        " INNER JOIN  [Location] AS l on i.LocationId = l.LocationId " +
                        " WHERE i.ProductId = " + nProductId + " " + strCondition + " " +
                        " ORDER BY i.TransactionDate desc";

        IEnumerable<csInventory> list = _db.ExecuteQuery<csInventory>(strQ, string.Empty).ToList();

        grdInventoryList.DataSource = list;
        grdInventoryList.DataKeyNames = new string[] { "ProductTransactionId" };
        grdInventoryList.DataBind();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("productdetails.aspx?pid=" + Convert.ToInt32(hdnProductId.Value));
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        Response.Redirect("addinventory.aspx?pid=" + Convert.ToInt32(hdnProductId.Value) + "&&lid=" + Convert.ToInt32(ddlLocation.SelectedItem.Value));
    }

    protected void btnReomove_Click(object sender, EventArgs e)
    {
        Response.Redirect("deleteinventory.aspx?pid=" + Convert.ToInt32(hdnProductId.Value) + "&&lid=" + Convert.ToInt32(ddlLocation.SelectedItem.Value));
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("productlist.aspx");
    }

    protected void btnSaveTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
            int nProductId = Convert.ToInt32(hdnProductId.Value);
            int nProductInventoryId = 0;
            DataClassesDataContext _db = new DataClassesDataContext();
            Product objPrd = new Product();
            ProductTransaction objPrdTrans = new ProductTransaction();
            List<csInventory> listPrdInvtry = new List<csInventory>();
            string strRequired = "";
            int nMinimumQuantityStock = 0;
            int nQuantity = 0;
            decimal dUnitCost = 0;
            decimal dTotalCost = 0;
            decimal dAveragCost = 0;
            int nTotalQuantity = 0;
            DateTime dtTransactionDate = new DateTime();
            lblResult.Text = "";
            int nLocationId = utility.GetDefaultLocationId(nClientId);

            if (Convert.ToInt32(ddlTransferToLocation.SelectedItem.Value) == 0)
            {
                strRequired += "Please Select Location<br />";
            }

            if (txtTransactionDate.Text.Trim() != "")
            {
                try
                {
                    dtTransactionDate = Convert.ToDateTime(txtTransactionDate.Text.Trim());
                }
                catch
                {
                    strRequired += "Invalide Transaction Date<br />";
                }
            }
            else
            {
                strRequired += "Invalide Transaction Date<br />";
            }

            if (txtQuantity.Text.Trim() != "")
            {
                try
                {
                    nQuantity = Convert.ToInt32(txtQuantity.Text.Trim());



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





            if (_db.Products.Any(p => p.ProductId == nProductId && p.ClientId == nClientId && p.Status == 1))
            {
                objPrd = _db.Products.FirstOrDefault(p => p.ProductId == nProductId && p.ClientId == nClientId);
            }
            if (objPrd != null)
            {



                // add              

                objPrdTrans.ProductId = objPrd.ProductId;
                objPrdTrans.TransactionTypeId = 6; // Transfer In
                objPrdTrans.TransactionDate = dtTransactionDate;
                objPrdTrans.QuantityIn = nQuantity;
                objPrdTrans.QuantityOut = 0;
                objPrdTrans.UnitCost = 0;
                objPrdTrans.UnitSale = 0;
                objPrdTrans.LocationId = Convert.ToInt32(ddlTransferToLocation.SelectedItem.Value);
                objPrdTrans.ClientId = nClientId;
                objPrdTrans.TransactionNumber = "TRNS-" + txtTransactionNumber.Text.ToString();
                _db.ProductTransactions.InsertOnSubmit(objPrdTrans);


                // remove   
                objPrdTrans = new ProductTransaction();

                objPrdTrans.ProductId = objPrd.ProductId;
                objPrdTrans.TransactionTypeId = 7; // Transfer Out
                objPrdTrans.TransactionDate = dtTransactionDate;
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


            GetProductInventory(nProductId, nLocationId);

            lblResult.Text = utility.GetSystemMessage("Data Saved successfully.<br />");
        }
        catch (Exception ex)
        {
            lblResult.Text = utility.GetSystemErrorMessage(ex.Message);
        }

    }

    protected void InsertInventory(int nProductId)
    {
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        divTransfer.Visible = false;
    }

    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        divTransfer.Visible = true;
    }

    protected void ddlDefaultLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataClassesDataContext _db = new DataClassesDataContext();
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);

        DropDownList ddlDefaultLocation = ((DropDownList)Master.FindControl("ddlDefaultLocation"));

        Preference objPf = _db.Preferences.Where(s => s.ClientId == 1).SingleOrDefault();

        objPf.DefaultLocationId = Convert.ToInt32(ddlDefaultLocation.SelectedItem.Value);

        _db.SubmitChanges();


        Response.Redirect("productlist.aspx");
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {

        Response.Redirect("inventorydetails.aspx?pid=" + Convert.ToInt32(hdnProductId.Value) + "&&lid=" + Convert.ToInt32(ddlLocation.SelectedItem.Value));
    }

}