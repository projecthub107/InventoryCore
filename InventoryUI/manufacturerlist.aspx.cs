using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manufacturerlist : System.Web.UI.Page
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
                if (Page.User.IsInRole("sett005") == false)
                {
                    // No Permission Page.
                    Response.Redirect("nopermission.aspx");
                }

                int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
                DataClassesDataContext _db = new DataClassesDataContext();
                
                GetManufacturer(0);





            }


        }
    }

    protected void GetManufacturer(int nPageNo)
    {
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        DataClassesDataContext _db = new DataClassesDataContext();
        grdManufacturerList.PageIndex = nPageNo;

        string strQ = "SELECT *, CASE WHEN Status = 1   THEN 'Yes' ELSE 'No' END AS Active " +
                  " FROM Manufacturer WHERE ClientId = " + nClientId + " ";


        IEnumerable<csManufacturer> list = _db.ExecuteQuery<csManufacturer>(strQ, string.Empty).ToList();

        if (ddlItemPerPage.SelectedValue != "4")
        {
            grdManufacturerList.PageSize = Convert.ToInt32(ddlItemPerPage.SelectedValue);
        }
        else
        {
            grdManufacturerList.PageSize = 200;
        }
        grdManufacturerList.DataSource = list;
        grdManufacturerList.DataKeyNames = new string[] { "ManufacturerId" };
        grdManufacturerList.DataBind();

        lblCurrentPageNo.Text = Convert.ToString(nPageNo + 1);
    }

    protected void grdManufacturerList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        GetManufacturer(e.NewPageIndex);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        Response.Redirect("manufacturerdetails.aspx");
    }

    protected void ddlItemPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        GetManufacturer(0);
    }
}