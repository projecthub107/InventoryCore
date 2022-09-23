using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class categorylist : System.Web.UI.Page
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
                if (Page.User.IsInRole("sett007") == false)
                {
                    // No Permission Page.
                    Response.Redirect("nopermission.aspx");
                }

                int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
                DataClassesDataContext _db = new DataClassesDataContext();

                GetCategory(0);

            }


        }
    }

    protected void GetCategory(int nPageNo)
    {
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        DataClassesDataContext _db = new DataClassesDataContext();
        grdCategoryList.PageIndex = nPageNo;

        string strQ = "SELECT *, CASE WHEN Status = 1  THEN 'Yes' ELSE 'No' END AS Active" +
                      " FROM ProductCategory WHERE ClientId = " + nClientId + " ";


        IEnumerable<csProductCategory> list = _db.ExecuteQuery<csProductCategory>(strQ, string.Empty).ToList();

        if (ddlItemPerPage.SelectedValue != "4")
        {
            grdCategoryList.PageSize = Convert.ToInt32(ddlItemPerPage.SelectedValue);
        }
        else
        {
            grdCategoryList.PageSize = 200;
        }
        grdCategoryList.DataSource = list;
        grdCategoryList.DataKeyNames = new string[] { "CategoryId" };
        grdCategoryList.DataBind();

        lblCurrentPageNo.Text = Convert.ToString(nPageNo + 1);
    }

    protected void grdCategoryList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        GetCategory(e.NewPageIndex);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        Response.Redirect("categorydetails.aspx");
    }

    protected void ddlItemPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        GetCategory(0);
    }
}