using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class locationlist : System.Web.UI.Page
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
                if (Page.User.IsInRole("sett001") == false)
                {
                    // No Permission Page.
                    Response.Redirect("nopermission.aspx");
                }

                int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
                DataClassesDataContext _db = new DataClassesDataContext();

                GetLocation(0);

            }


        }
    }

    protected void GetLocation(int nPageNo)
    {
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        DataClassesDataContext _db = new DataClassesDataContext();
        grdLocationList.PageIndex = nPageNo;

        string strQ = "SELECT *, CASE WHEN Status = 1  THEN 'Yes' ELSE 'No' END AS Active" +
                      " FROM Location ";


        IEnumerable<csLocation> list = _db.ExecuteQuery<csLocation>(strQ, string.Empty).ToList();

        if (ddlItemPerPage.SelectedValue != "4")
        {
            grdLocationList.PageSize = Convert.ToInt32(ddlItemPerPage.SelectedValue);
        }
        else
        {
            grdLocationList.PageSize = 200;
        }

        grdLocationList.DataSource = list;
        grdLocationList.DataKeyNames = new string[] { "LocationId" };
        grdLocationList.DataBind();

        lblCurrentPageNo.Text = Convert.ToString(nPageNo + 1);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        Response.Redirect("locationdetails.aspx");
    }

    protected void ddlItemPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        GetLocation(0);
    }
}