using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class addinventory : System.Web.UI.Page
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


                BindTransaction(nClientId);


                if (Request.QueryString.Get("pid") != null)
                {
                    DataClassesDataContext _db = new DataClassesDataContext();
                    int nProductId = Convert.ToInt32(Request.QueryString.Get("pid"));

                    hdnProductId.Value = nProductId.ToString();


                    int nLocationId = utility.GetDefaultLocationId(nClientId);

                    if (Request.QueryString.Get("lid") != null)
                        nLocationId = Convert.ToInt32(Request.QueryString.Get("lid"));

                    string strQ = "SELECT p.ProductId, p.ProductCode, p.ProductName, " +
                        " a.AreaName, i.Quantity, i.StockStatus, m.ManufacturerName," +
                        " p.ModifiedBy, p.CreatedBy, p.ClientId, p.CreatedDate, p.ModifiedDate " +
                        " FROM Product AS p " +
                        " INNER JOIN ( Select LocationName + ' ('+CONVERT(varchar(100), SUM(ISNULL(QuantityIn,0))-SUM(ISNULL(QuantityOut,0)) )+')'+'' As StockStatus, ProductId,  SUM(ISNULL(QuantityIn,0))-SUM(ISNULL(QuantityOut,0)) AS Quantity FROM ProductTransaction AS i" +
                       " INNER JOIN Location AS l on i.LocationId = l.LocationId " +
                        " WHERE i.LocationId = " + nLocationId + "  GROUP BY ProductId, LocationName ) AS i on p.ProductId  = i.ProductId " +
                        " LEFT OUTER JOIN [Area] AS a on p.AreaId = a.AreaId " +
                        " LEFT OUTER JOIN Manufacturer AS m on p.ManufacturerId = m.ManufacturerId " +
                        " WHERE i.ProductId = " + nProductId + " AND p.ClientId = " + nClientId; 
                    //" Group By ProductInventoryId, ProductInventoryDescription, p.ProductId, i.TransactionId, TransactionDate, Quantity, UnitCost, UnitSale, AveragCost, TotalCost, "+
                    //" TotalSale, GP, TransactionSerialNumber, Notes, l.LocationName, t.TransactionName, t.TransactionType, i.ModifiedBy, i.CreatedBy, i.ClientId, i.CreatedDate, i.ModifiedDate";

                    csProduct objPrd = _db.ExecuteQuery<csProduct>(strQ, string.Empty).FirstOrDefault();

                    lblLocation.Text = _db.Locations.FirstOrDefault(l => l.LocationId == nLocationId).LocationName ?? "";
                    lblCode.Text = objPrd.ProductCode;
                    lblName.Text = objPrd.ProductName;
                    lblArea.Text = objPrd.AreaName;
                  
                    lblStock.Text = objPrd.Quantity.ToString() + " in-stock";

                }
            }


        }

        DropDownList ddlDefaultLocation = ((DropDownList)Master.FindControl("ddlDefaultLocation"));
        ddlDefaultLocation.SelectedIndexChanged += new EventHandler(ddlDefaultLocation_SelectedIndexChanged);
    }



    protected void BindTransaction(int nClientId)
    {
        DataClassesDataContext _db = new DataClassesDataContext();

        var listClass = (from t in _db.TransactionTypes
                         where t.ClientId == nClientId && t.Status == 1 && t.Type == "Receive"
                         select new
                         {
                             Id = t.TransactionTypeId,
                             Name = t.TransactionName
                         }).ToList();

        ddlTransaction.DataSource = listClass;
        ddlTransaction.DataTextField = "Name";
        ddlTransaction.DataValueField = "Id";
        ddlTransaction.DataBind();

        ddlTransaction.Items.Insert(0, "Select");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
            int nProductId = Convert.ToInt32(hdnProductId.Value);
          
            DataClassesDataContext _db = new DataClassesDataContext();
            Product objPrd = new Product();
            ProductTransaction objPrdTrans = new ProductTransaction();
            List<csInventory> listPrdInvtry = new List<csInventory>();
            string strRequired = "";
          
            int nQuantity = 0;
           
            DateTime dtTransactionDate = new DateTime();
            lblResult.Text = "";

            int nLocationId = utility.GetDefaultLocationId(nClientId);

            if (Request.QueryString.Get("lid") != null)
                nLocationId = Convert.ToInt32(Request.QueryString.Get("lid"));

            if (txtTransactionDate.Text.Trim() != "")
            {
                try
                {
                    dtTransactionDate = Convert.ToDateTime(txtTransactionDate.Text.Trim());
                }
                catch
                {
                    strRequired = "Invalide Transaction Date<br />";
                }
            }

            if (txtQuantity.Text.Trim() != "")
            {
                try
                {
                    nQuantity = Convert.ToInt32(txtQuantity.Text.Trim());
                }
                catch
                {
                    strRequired = "Invalide Quantity<br />";
                }
            }
            else
            {
                strRequired = "Quantity must be greater than 0<br />";
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
                // Insert Inventory
              
                objPrdTrans.ProductId = objPrd.ProductId;
                objPrdTrans.TransactionTypeId = Convert.ToInt32(ddlTransaction.SelectedItem.Value);
                objPrdTrans.TransactionDate = dtTransactionDate;
                objPrdTrans.QuantityIn = nQuantity;
                objPrdTrans.QuantityOut = 0;
                objPrdTrans.UnitCost = 0;
                objPrdTrans.UnitSale = 0;
                objPrdTrans.LocationId = nLocationId;
                objPrdTrans.ClientId = nClientId;
                objPrdTrans.TransactionNumber = "RCV-" + txtTransactionNumber.Text.ToString();
                _db.ProductTransactions.InsertOnSubmit(objPrdTrans);

                _db.SubmitChanges();

                utility.SetProductQuantity(nProductId);
            }

            lblResult.Text = utility.GetSystemMessage("Data Saved successfully.<br />");
        }
        catch (Exception ex)
        {
            lblResult.Text = utility.GetSystemErrorMessage(ex.Message);
        }


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString.Get("lid") != null)
            Response.Redirect("inventorydetails.aspx?pid=" + Convert.ToInt32(hdnProductId.Value) + "&&lid=" + Convert.ToInt32(Request.QueryString.Get("lid")));
        else
            Response.Redirect("inventorydetails.aspx?pid=" + Convert.ToInt32(hdnProductId.Value));       
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
}