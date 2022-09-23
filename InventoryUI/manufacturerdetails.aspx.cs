using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manufacturerdetails : System.Web.UI.Page
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
                if (Page.User.IsInRole("sett006") == false)
                {
                    // No Permission Page.
                    Response.Redirect("nopermission.aspx");
                }

                int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
                
                if (Request.QueryString.Get("mid") != null)
                {

                    int nManufacturerId = Convert.ToInt32(Request.QueryString.Get("mid"));

                    hdnManufacturerId.Value = nManufacturerId.ToString();

                    LoadManufacturerDetails(nClientId, nManufacturerId);

                   

                    lblTitle.Text = "Manufacturer Details";
                }
               
            }


        }
    }

    protected void LoadManufacturerDetails(int nClientId, int nManufacturerId)
    {
        DataClassesDataContext _db = new DataClassesDataContext();



        string strQ = "SELECT * " +
                      " FROM Manufacturer " +
                      " WHERE ManufacturerId = " + nManufacturerId;


        Manufacturer objMf = _db.ExecuteQuery<Manufacturer>(strQ, string.Empty).FirstOrDefault();


        txtManufacturerName.Text = objMf.ManufacturerName;
        txtAddress.Text = objMf.Address;

        txtCity.Text = objMf.City;
        txtState.Text = objMf.State;
        txtZip.Text = objMf.Zip.ToString();

       
        ddlStatus.SelectedValue = objMf.Status.ToString();

    }

   

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
            int nManufacturerId = Convert.ToInt32(hdnManufacturerId.Value);
         
            DataClassesDataContext _db = new DataClassesDataContext();
            Manufacturer objMf = new Manufacturer();
         
            string strRequired = "";
            int nZip = 0;
           
            lblResult.Text = "";

            if (txtManufacturerName.Text.Trim() == "")
            {
                strRequired = "Missing required field: Name.<br />";

            }

            if (txtZip.Text.Trim() != "")
            {
                try
                {
                    nZip = Convert.ToInt32(txtZip.Text.Trim());
                }
                catch
                {
                    strRequired = "Invalide Zip<br />";
                }
            }

            if (strRequired.Length > 0)
            {
                lblResult.Text = utility.GetSystemRequiredMessage(strRequired);
                return;
            }


            if (_db.Manufacturers.Any(p => p.ManufacturerId == nManufacturerId && p.ClientId == nClientId))
            {
                objMf = _db.Manufacturers.FirstOrDefault(p => p.ManufacturerId == nManufacturerId && p.ClientId == nClientId);
            }

            // Insert Manufacturer
           
            objMf.ManufacturerName = txtManufacturerName.Text;
            objMf.Address = txtAddress.Text;
            objMf.City = txtCity.Text;
            objMf.State = txtState.Text;
            objMf.Zip = nZip;
         
            objMf.Status = Convert.ToInt32(ddlStatus.SelectedItem.Value);
          
            objMf.ModifiedDate = DateTime.Now;
            objMf.ModifiedBy = User.Identity.Name;

            if (nManufacturerId == 0)
            {
                

              
                objMf.CreatedDate = DateTime.Now;
                objMf.CreatedBy = User.Identity.Name;
                objMf.ClientId = nClientId;

                _db.Manufacturers.InsertOnSubmit(objMf);
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
        Response.Redirect("manufacturerlist.aspx");
    }
}