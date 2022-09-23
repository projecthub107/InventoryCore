using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userlist : System.Web.UI.Page
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
                if (Page.User.IsInRole("user001") == false)
                {
                    // No Permission Page.
                    Response.Redirect("nopermission.aspx");
                }

                int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
                DataClassesDataContext _db = new DataClassesDataContext();
                BindRole(nClientId);


                GetUser(nClientId, 0, 0);





            }


        }


    }

    protected void BindRole(int nClientId)
    {
        try
        {
            DataClassesDataContext _db = new DataClassesDataContext();

            var list = (from p in _db.Roles
                        where p.ClientId == nClientId && p.Status == 1
                        select new
                        {
                            Id = p.RoleId,
                            Name = p.Name
                        }).ToList();

            ddlRole.DataSource = list;
            ddlRole.DataTextField = "Name";
            ddlRole.DataValueField = "Id";
            ddlRole.DataBind();

            ddlRole.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblResult.Text = utility.GetSystemErrorMessage("BindRole(), " + ex.Message);
        }
    }

    protected void GetUser(int nClientId, int nUserId, int nPageNo)
    {
        string strCondition = " WHERE u.ClientId = " + nClientId;
        DataClassesDataContext _db = new DataClassesDataContext();
        grdUserList.PageIndex = nPageNo;


        if (nUserId > 0)
        {
            strCondition += " AND u.UserId =" + nUserId;
        }

        if (ddlStatus.SelectedItem.Value != "2")
        {
            strCondition += " AND u.Status = " + Convert.ToInt32(ddlStatus.SelectedItem.Value);
        }

        if (ddlRole.SelectedItem.Value != "0")
        {
            strCondition += " AND u.RoleId = " + Convert.ToInt32(ddlRole.SelectedItem.Value);
        }

        string strQ = "SELECT u.UserId, u.UserName, u.Password,u.Email,u.RoleId,u.LastLoginDate, u.QuestionID, u.Answer, " +
                          " u.Status,u.CreatedDate,u.CreatedBy,u.ModifiedDate,u.ModifiedBy,u.ClientId, " +
                          " CASE WHEN u.Status = 0 THEN 'No' ELSE 'Yes' END AS [Active], " +
                          " r.Name AS Role" +
                          " FROM UserInfo AS u " +
                          " INNER JOIN Role AS r on u.RoleId = r.RoleId " +
                          " " + strCondition;


        IEnumerable<csuserinfo> list = _db.ExecuteQuery<csuserinfo>(strQ, string.Empty).ToList();


        if (ddlItemPerPage.SelectedValue != "4")
        {
            grdUserList.PageSize = Convert.ToInt32(ddlItemPerPage.SelectedValue);
        }
        else
        {
            grdUserList.PageSize = 200;
        }
        grdUserList.DataSource = list;
        grdUserList.DataKeyNames = new string[] { "UserId" };
        grdUserList.DataBind();

        lblCurrentPageNo.Text = Convert.ToString(nPageNo + 1);
    }

    protected void grdUserList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        GetUser(nClientId, 0, e.NewPageIndex);
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {

        Response.Redirect("userdetails.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        int nUserId = 0;

        if (Session["searchUser"] != null)
        {
            csUserSearch objP = (csUserSearch)Session["searchUser"];
            nUserId = objP.UserId;

        }

        int nLocationId = utility.GetDefaultLocationId(nClientId);

        GetUser(nClientId, nUserId, 0);
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        lblResult.Text = "";

        Reset();
        GetUser(nClientId, 0, 0);
    }

    protected void Reset()
    {

        txtUserSearch.Text = "";
        ddlStatus.SelectedValue = "1";
        ddlRole.SelectedValue = "0";

        Session["searchUser"] = null;


    }

    protected void ddlItemPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        GetUser(nClientId, 0, 0);
    }

}