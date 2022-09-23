using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class categorydetails : System.Web.UI.Page
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
                if (Page.User.IsInRole("sett008") == false)
                {
                    // No Permission Page.
                    Response.Redirect("nopermission.aspx");
                }

                int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
                
                if (Request.QueryString.Get("cid") != null)
                {

                    int nCategoryId = Convert.ToInt32(Request.QueryString.Get("cid"));

                    hdnCategoryId.Value = nCategoryId.ToString();

                    LoadCategoryDetails(nClientId, nCategoryId);



                    lblTitle.Text = "Category Details";
                }
               
            }


        }
    }

    protected void LoadCategoryDetails(int nClientId, int nCategoryId)
    {
        DataClassesDataContext _db = new DataClassesDataContext();



        string strQ = "SELECT * " +
                      " FROM ProductCategory " +
                      " WHERE CategoryId = " + nCategoryId;


        ProductCategory objctg = _db.ExecuteQuery<ProductCategory>(strQ, string.Empty).FirstOrDefault();


        txtCategoryName.Text = objctg.CategoryName;
        txtSerial.Text = objctg.CategorySerial;

      
       
        ddlStatus.SelectedValue = objctg.Status.ToString();

    }

   

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
            int nCategoryId = Convert.ToInt32(hdnCategoryId.Value);
         
            DataClassesDataContext _db = new DataClassesDataContext();
            ProductCategory objctg = new ProductCategory();
         
            string strRequired = "";
            int nZip = 0;
           
            lblResult.Text = "";

            if (txtCategoryName.Text.Trim() == "")
            {
                strRequired = "Missing required field: Name.<br />";

            }

           

            if (strRequired.Length > 0)
            {
                lblResult.Text = utility.GetSystemRequiredMessage(strRequired);
                return;
            }


            if (_db.ProductCategories.Any(p => p.CategoryId == nCategoryId && p.ClientId == nClientId))
            {
                objctg = _db.ProductCategories.FirstOrDefault(p => p.CategoryId == nCategoryId && p.ClientId == nClientId);
            }

            // Insert Category
           
            objctg.CategoryName = txtCategoryName.Text;
            objctg.CategorySerial = txtSerial.Text;
         
         
            objctg.Status = Convert.ToInt32(ddlStatus.SelectedItem.Value);
          
            objctg.ModifiedDate = DateTime.Now;
            objctg.ModifiedBy = User.Identity.Name;

            if (nCategoryId == 0)
            {
                objctg.CreatedDate = DateTime.Now;
                objctg.CreatedBy = User.Identity.Name;
                objctg.ClientId = nClientId;

                _db.ProductCategories.InsertOnSubmit(objctg);
                _db.SubmitChanges();




               
            }

            _db.SubmitChanges();




            lblResult.Text = utility.GetSystemMessage("Data Saved successfully.<br />");
        }
        catch (Exception ex)
        {
            lblResult.Text = utility.GetSystemErrorMessage(ex.Message);
        }


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("categorylist.aspx");
    }
}