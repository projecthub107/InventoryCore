using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class productdetails : System.Web.UI.Page
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
                if (Page.User.IsInRole("prod002") == false)
                {
                    // No Permission Page.
                    Response.Redirect("nopermission.aspx");
                }

                txtTransactionNumber.Text = utility.GetRandomString(5);
                int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);



                BindCategory(nClientId);
                BindArea(nClientId);

                BindManufacturer(nClientId);
                BindTransaction(nClientId);
                txtTransactionDate.Text = DateTime.Now.ToShortDateString();

                if (Request.QueryString.Get("pid") != null)
                {
                    imgBarCode.Visible = false;
                    divTransaction.Visible = false;
                    int nProductId = Convert.ToInt32(Request.QueryString.Get("pid"));

                    hdnProductId.Value = nProductId.ToString();

                    LoadProductDetails(nClientId, nProductId);



                    lblTitle.Text = "Product Details";
                }
                else
                {
                    divTransaction.Visible = true;

                }
            }


        }

        DropDownList ddlDefaultLocation = ((DropDownList)Master.FindControl("ddlDefaultLocation"));
        ddlDefaultLocation.SelectedIndexChanged += new EventHandler(ddlDefaultLocation_SelectedIndexChanged);
    }

    protected void LoadProductDetails(int nClientId, int nProductId)
    {
        DataClassesDataContext _db = new DataClassesDataContext();



        string strQ = "SELECT p.ProductId, p.ProductCode, p.ProductName, ISNULL(p.ProductDescription,'') AS ProductDescription, " +
                      " ISNULL(p.AreaId,0) AS AreaId, " +
                      " ISNULL(p.ManufacturerId,0) ManufacturerId, ISNULL(p.CategoryId,0) AS CategoryId," +
                      " p.ProductSize, p.ProductWight, p.ProductColor, p.ProductSerial, ISNULL(p.MinimumQuantityStock,0) AS MinimumQuantityStock, " +
                      " p.Status, p.ClientId " +
                      " FROM Product AS p " +
                      " WHERE p.ProductId = " + nProductId;


        csProductDetails objPrd = _db.ExecuteQuery<csProductDetails>(strQ, string.Empty).FirstOrDefault();

        txtProductCode.Text = objPrd.ProductCode;
        txtProductName.Text = objPrd.ProductName;
        txtProductDescription.Text = objPrd.ProductDescription;

        txtProductSize.Text = objPrd.ProductSize;
        //txtProductWight.Text = objPrd.ProductWight;
        //txtProductColor.Text = objPrd.ProductColor;

        ddlCategory.SelectedValue = objPrd.CategoryId.ToString();
        ddlArea.SelectedValue = objPrd.AreaId.ToString();

        ddlManufacturer.SelectedValue = objPrd.ManufacturerId.ToString();

        //txtProductSerial.Text = objPrd.ProductSerial;
        txtMinimumQuantityStock.Text = objPrd.MinimumQuantityStock.ToString();
        ddlStatus.SelectedValue = objPrd.Status.ToString();

    }

    protected void BindCategory(int nClientId)
    {
        DataClassesDataContext _db = new DataClassesDataContext();

        var list = (from p in _db.ProductCategories
                    where p.ClientId == nClientId && p.Status == 1
                    select new
                    {
                        Id = p.CategoryId,
                        Name = p.CategoryName
                    }).ToList();

        ddlCategory.DataSource = list;
        ddlCategory.DataTextField = "Name";
        ddlCategory.DataValueField = "Id";
        ddlCategory.DataBind();

        ddlCategory.Items.Insert(0, "Select");
    }

    protected void BindArea(int nClientId)
    {
        DataClassesDataContext _db = new DataClassesDataContext();

        var list = (from p in _db.Areas
                    where p.ClientId == nClientId && p.Status == 1
                    select new
                    {
                        Id = p.AreaId,
                        Name = p.AreaName
                    }).ToList();

        ddlArea.DataSource = list;
        ddlArea.DataTextField = "Name";
        ddlArea.DataValueField = "Id";
        ddlArea.DataBind();

        ddlArea.Items.Insert(0, "Select");
    }



    protected void BindManufacturer(int nClientId)
    {
        DataClassesDataContext _db = new DataClassesDataContext();

        var listClass = (from p in _db.Manufacturers
                         where p.ClientId == nClientId && p.Status == 1
                         select new
                         {
                             Id = p.ManufacturerId,
                             Name = p.ManufacturerName
                         }).ToList();

        ddlManufacturer.DataSource = listClass;
        ddlManufacturer.DataTextField = "Name";
        ddlManufacturer.DataValueField = "Id";
        ddlManufacturer.DataBind();

        ddlManufacturer.Items.Insert(0, "Select");
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
            string strRequired = "";
            int nMinimumQuantityStock = 0;
            int nQuantity = 0;
            int nCategoryId = 0;
            int nAreaId = 0;
            int nManufacturerId = 0;
            int nTransactionTypeId = 0;


            DateTime dtTransactionDate = new DateTime();
            lblResult.Text = "";

            if (txtProductName.Text.Trim() == "")
            {
                strRequired = "Missing required field: Name.<br />";

            }

            if (ddlCategory.SelectedItem.Value != "Select")
            {
                nCategoryId = Convert.ToInt32(ddlCategory.SelectedItem.Value);
            }

            if (ddlManufacturer.SelectedItem.Value != "Select")
            {
                nManufacturerId = Convert.ToInt32(ddlManufacturer.SelectedItem.Value);
            }


            if (ddlArea.SelectedItem.Value != "Select")
            {
                nAreaId = Convert.ToInt32(ddlArea.SelectedItem.Value);
            }

            if (txtMinimumQuantityStock.Text.Trim() != "")
            {
                try
                {
                    nMinimumQuantityStock = Convert.ToInt32(txtMinimumQuantityStock.Text.Trim());
                }
                catch
                {
                    strRequired += "Invalide Minimum Quantity Stock<br />";
                }
            }
            if (nProductId == 0)
            {
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
                    strRequired += "Missing required field: Transaction Date<br />";
                }

               

                if (ddlTransaction.SelectedItem.Value != "Select")
                {
                    nTransactionTypeId = Convert.ToInt32(ddlTransaction.SelectedItem.Value);
                }
                else
                {
                    strRequired += "Transaction Type is required<br />";
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
            }

           


            

            if (strRequired.Length > 0)
            {
                lblResult.Text = utility.GetSystemRequiredMessage(strRequired);
                return;
            }


            if (_db.Products.Any(p => p.ProductId == nProductId && p.ClientId == nClientId))
            {
                objPrd = _db.Products.FirstOrDefault(p => p.ProductId == nProductId && p.ClientId == nClientId);
            }

            // Insert Product
            objPrd.ProductCode = txtProductCode.Text;
            objPrd.ProductName = txtProductName.Text;
            objPrd.ProductDescription = txtProductDescription.Text;
            objPrd.ProductSize = txtProductSize.Text;
            objPrd.ProductWight = "";// txtProductWight.Text;
            objPrd.ProductColor = "";// txtProductColor.Text;
            objPrd.ProductSerial = "";// txtProductSerial.Text;
            objPrd.MinimumQuantityStock = nMinimumQuantityStock;
            objPrd.Status = Convert.ToInt32(ddlStatus.SelectedItem.Value);
            objPrd.CategoryId = nCategoryId;
            objPrd.ModifiedDate = DateTime.Now;
            objPrd.ModifiedBy = User.Identity.Name;
            objPrd.AreaId = nAreaId;
            objPrd.ManufacturerId = nManufacturerId;


            if (nProductId == 0)
            {



                objPrd.CreatedDate = DateTime.Now;
                objPrd.CreatedBy = User.Identity.Name;
                objPrd.ClientId = nClientId;

                _db.Products.InsertOnSubmit(objPrd);
                _db.SubmitChanges();




                // Insert Inventory


                objPrdTrans.ProductId = objPrd.ProductId;
                objPrdTrans.TransactionTypeId = nTransactionTypeId;
                objPrdTrans.TransactionDate = dtTransactionDate;
                objPrdTrans.QuantityIn = nQuantity;
                objPrdTrans.QuantityOut = 0;
                objPrdTrans.UnitCost = 0;
                objPrdTrans.UnitSale = 0;
                objPrdTrans.LocationId = utility.GetDefaultLocationId(nClientId);
                objPrdTrans.ClientId = nClientId;
                objPrdTrans.TransactionNumber = "RCV-" + txtTransactionNumber.Text.ToString();

                _db.ProductTransactions.InsertOnSubmit(objPrdTrans);
            }

            _db.SubmitChanges();

            utility.SetProductQuantity(nProductId);


            lblResult.Text = utility.GetSystemMessage("Data Saved successfully.<br />");
        }
        catch (Exception ex)
        {
            lblResult.Text = utility.GetSystemErrorMessage(ex.Message + "<br />");
        }


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("productlist.aspx");
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