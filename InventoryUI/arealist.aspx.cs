using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class arealist : System.Web.UI.Page
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
                if (Page.User.IsInRole("sett003") == false)
                {
                    // No Permission Page.
                    Response.Redirect("nopermission.aspx");
                }

                int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
                DataClassesDataContext _db = new DataClassesDataContext();

                GetArea(0);

            }


        }
    }

    protected void GetArea(int nPageNo)
    {
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        DataClassesDataContext _db = new DataClassesDataContext();
        grdAreaList.PageIndex = nPageNo;

        string strQ = "SELECT *, CASE WHEN Status = 1  THEN 'Yes' ELSE 'No' END AS Active" +
                      " FROM Area WHERE ClientId = " + nClientId + " ";


        IEnumerable<csArea> list = _db.ExecuteQuery<csArea>(strQ, string.Empty).ToList();

        if (ddlItemPerPage.SelectedValue != "4")
        {
            grdAreaList.PageSize = Convert.ToInt32(ddlItemPerPage.SelectedValue);
        }
        else
        {
            grdAreaList.PageSize = 200;
        }
        grdAreaList.DataSource = list;
        grdAreaList.DataKeyNames = new string[] { "AreaId" };
        grdAreaList.DataBind();

        lblCurrentPageNo.Text = Convert.ToString(nPageNo + 1);
    }

    protected void grdAreaList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {        
        GetArea(e.NewPageIndex);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        Response.Redirect("areadetails.aspx");
    }

    protected void ddlItemPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        GetArea(0);
    }
}