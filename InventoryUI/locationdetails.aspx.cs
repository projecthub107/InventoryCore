using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class locationdetails : System.Web.UI.Page
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
                if (Page.User.IsInRole("sett002") == false)
                {
                    // No Permission Page.
                    Response.Redirect("nopermission.aspx");
                }

                int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
                
                if (Request.QueryString.Get("lid") != null)
                {

                    int nLocationId = Convert.ToInt32(Request.QueryString.Get("lid"));

                    hdnLocationId.Value = nLocationId.ToString();

                    LoadLocationDetails(nClientId, nLocationId);



                    lblTitle.Text = "Location Details";
                }
               
            }


        }
    }

    protected void LoadLocationDetails(int nClientId, int nLocationId)
    {
        DataClassesDataContext _db = new DataClassesDataContext();



        string strQ = "SELECT * " +
                      " FROM Location " +
                      " WHERE LocationId = " + nLocationId;


        Location objloc = _db.ExecuteQuery<Location>(strQ, string.Empty).FirstOrDefault();


        txtLocationName.Text = objloc.LocationName;
        txtAddress.Text = objloc.Address;

        txtCity.Text = objloc.City;
        txtState.Text = objloc.State;
        txtZip.Text = objloc.Zip.ToString();

       
        ddlStatus.SelectedValue = objloc.Status.ToString();

    }

   

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
            int nLocationId = Convert.ToInt32(hdnLocationId.Value);
         
            DataClassesDataContext _db = new DataClassesDataContext();
            Location objloc = new Location();
         
            string strRequired = "";
            int nZip = 0;
           
            lblResult.Text = "";

            if (txtLocationName.Text.Trim() == "")
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


            if (_db.Locations.Any(p => p.LocationId == nLocationId && p.ClientId == nClientId))
            {
                objloc = _db.Locations.FirstOrDefault(p => p.LocationId == nLocationId && p.ClientId == nClientId);
            }

            // Insert Location
           
            objloc.LocationName = txtLocationName.Text;
            objloc.Address = txtAddress.Text;
            objloc.City = txtCity.Text;
            objloc.State = txtState.Text;
            objloc.Zip = nZip;
         
            objloc.Status = Convert.ToInt32(ddlStatus.SelectedItem.Value);
          
            objloc.ModifiedDate = DateTime.Now;
            objloc.ModifiedBy = User.Identity.Name;

            if (nLocationId == 0)
            {
                

              
                objloc.CreatedDate = DateTime.Now;
                objloc.CreatedBy = User.Identity.Name;
                objloc.ClientId = nClientId;

                _db.Locations.InsertOnSubmit(objloc);
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
        Response.Redirect("locationlist.aspx");
    }
}