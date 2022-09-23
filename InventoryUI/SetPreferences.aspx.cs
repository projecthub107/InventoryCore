using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SetPreferences : System.Web.UI.Page
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
                if (Page.User.IsInRole("sett012") == false)
                {
                    // No Permission Page.
                    Response.Redirect("nopermission.aspx");
                }

                int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
                
                BindLocation(nClientId);
                BindCountry();

                LoadPreferences(nClientId);


            }


        }
    }

    protected void BindLocation(int nClientId)
    {
        DataClassesDataContext _db = new DataClassesDataContext();

        var list = (from p in _db.Locations
                    where p.ClientId == nClientId && p.Status == 1
                    select new
                    {
                        Id = p.LocationId,
                        Name = p.LocationName
                    }).ToList();

        ddlLocation.DataSource = list;
        ddlLocation.DataTextField = "Name";
        ddlLocation.DataValueField = "Id";
        ddlLocation.DataBind();

        ddlLocation.Items.Insert(0, "Select");
    }

    protected void BindCountry()
    {
        DataClassesDataContext _db = new DataClassesDataContext();

        var list = (from p in _db.Countries
                    where p.CurrencyCode != ""
                    select new
                    {
                        Id = p.CountryId,
                        Name = p.CountryName + " - " + p.CurrencyCode
                    }).ToList();

        ddlCurrency.DataSource = list.OrderBy(o => o.Name);
        ddlCurrency.DataTextField = "Name";
        ddlCurrency.DataValueField = "Id";
        ddlCurrency.DataBind();

        ddlCurrency.Items.Insert(0, "Select");
    }

    protected void LoadPreferences(int nClientId)
    {
        DataClassesDataContext _db = new DataClassesDataContext();



        string strQ = "SELECT * " +
                      " FROM Preferences " +
                      " WHERE ClientId = " + nClientId;


        Preference objPf = _db.ExecuteQuery<Preference>(strQ, string.Empty).FirstOrDefault();

        hdnPreferencesId.Value = objPf.PreferencesId.ToString();
        txtMinimumStock.Text = objPf.MinimumStock.ToString();
        ddlCurrency.SelectedValue = objPf.CurrencyId.ToString();
        ddlLocation.SelectedValue = objPf.DefaultLocationId.ToString();

    }



    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
            int nPreferencesId = Convert.ToInt32(hdnPreferencesId.Value);

            DataClassesDataContext _db = new DataClassesDataContext();
            Preference objPf = new Preference();

            string strRequired = "";
            int nMinimumStock = 0;

            lblResult.Text = "";

            if (txtMinimumStock.Text.Trim() != "")
            {
                try
                {
                    nMinimumStock = Convert.ToInt32(txtMinimumStock.Text.Trim());

                }
                catch
                {
                    strRequired = "Invalide Minimum Stock<br />";
                }
            }


            if (strRequired.Length > 0)
            {
                lblResult.Text = utility.GetSystemRequiredMessage(strRequired);
                return;
            }


            if (_db.Preferences.Any(p => p.ClientId == nClientId && p.PreferencesId == nPreferencesId))
            {
                objPf = _db.Preferences.FirstOrDefault(p => p.ClientId == nClientId && p.PreferencesId == nPreferencesId);
            }

            // Insert Category

            objPf.MinimumStock = nMinimumStock;
            objPf.CurrencyId = Convert.ToInt32(ddlCurrency.SelectedItem.Value);
            objPf.DefaultLocationId = Convert.ToInt32(ddlLocation.SelectedItem.Value);

            objPf.ModifiedDate = DateTime.Now;
            objPf.ModifiedBy = User.Identity.Name;

            if (nPreferencesId == 0)
            {


               
                objPf.CreatedDate = DateTime.Now;
                objPf.CreatedBy = User.Identity.Name;
                objPf.ClientId = nClientId;

                _db.Preferences.InsertOnSubmit(objPf);
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