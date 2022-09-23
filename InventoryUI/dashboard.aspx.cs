using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Session["oUser"] == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings["LoginPage"].ToString());
            }
            if (Page.User.IsInRole("dash001") == false)
            {
                // No Permission Page.
                Response.Redirect("nopermission.aspx");
            }
            DataClassesDataContext _db = new DataClassesDataContext();
            int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);

           
            TotalCount(nClientId);

            // Web API CAll
            grdProductList.DataSource = APIProduct.GetProducts().Take(5).ToList();
            grdProductList.DataBind();

            grdUserList.DataSource = APIUser.GetUsers().Take(5).ToList();
            grdUserList.DataBind();
            //---------------------------------------------
        }
    }

   

    protected void TotalCount(int nClientId)
    {
        DataClassesDataContext _db = new DataClassesDataContext();


        int nProductsCount = 0;
        int nReceiveInventoryCount = 0;
        int nRemoveInventoryCount = 0;
        int nTransferInventoryCount = 0;

        if (_db.Products.Any(p => p.ClientId == nClientId && p.Status == 1))
            nProductsCount = _db.Products.Where(p => p.ClientId == nClientId && p.Status == 1).Count();

        //if (_db.Teachers.Any(t => t.ClientId == nClientId && t.Status == 1))
        //    nTeacherCount = _db.Teachers.Where(t => t.ClientId == nClientId && t.Status == 1).Count();

        //if (_db.Parents.Any(p => p.ClientId == nClientId && p.Status == 1))
        //    nParentCount = _db.Parents.Where(p => p.ClientId == nClientId && p.Status == 1).Count();

        //if (_db.Attendances.Any(s => s.SessionId == 1 && s.ClientId == nClientId && s.Status == 1))
        //    nAttendanceCount = _db.Attendances.Where(s => s.SessionId == nSchoolSessionId && s.ClientId == nClientId && s.Status == 1).Count();


        lblTotalProductsCount.Text = nProductsCount.ToString();
        lblTotalReceiveInventoryCount.Text = nReceiveInventoryCount.ToString();
        lblTotalRemoveInventoryCount.Text = nRemoveInventoryCount.ToString();
        lblTransferInventoryCount.Text = nTransferInventoryCount.ToString();
    }


}