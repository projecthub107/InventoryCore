using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class areadetails : System.Web.UI.Page
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
                if (Page.User.IsInRole("sett004") == false)
                {
                    // No Permission Page.
                    Response.Redirect("nopermission.aspx");
                }

                int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
                
                if (Request.QueryString.Get("aid") != null)
                {

                    int nAreaId = Convert.ToInt32(Request.QueryString.Get("aid"));

                    hdnAreaId.Value = nAreaId.ToString();

                    LoadAreaDetails(nClientId, nAreaId);



                    lblTitle.Text = "Area Details";
                }
               
            }


        }
    }

    protected void LoadAreaDetails(int nClientId, int nAreaId)
    {
        DataClassesDataContext _db = new DataClassesDataContext();



        string strQ = "SELECT * " +
                      " FROM Area " +
                      " WHERE AreaId = " + nAreaId;


        csArea objctg = _db.ExecuteQuery<csArea>(strQ, string.Empty).FirstOrDefault();


        txtAreaName.Text = objctg.AreaName;
     

      
       
        ddlStatus.SelectedValue = objctg.Status.ToString();

    }

   

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
            int nAreaId = Convert.ToInt32(hdnAreaId.Value);
         
            DataClassesDataContext _db = new DataClassesDataContext();
            Area objarea= new Area();
         
            string strRequired = "";
        
           
            lblResult.Text = "";

            if (txtAreaName.Text.Trim() == "")
            {
                strRequired = "Missing required field: Name.<br />";

            }

           

            if (strRequired.Length > 0)
            {
                lblResult.Text = utility.GetSystemRequiredMessage(strRequired);
                return;
            }


            if (_db.Areas.Any(p => p.AreaId == nAreaId && p.ClientId == nClientId))
            {
                objarea = _db.Areas.FirstOrDefault(p => p.AreaId == nAreaId && p.ClientId == nClientId);
            }

            // Insert Area

            objarea.AreaName = txtAreaName.Text;



            objarea.Status = Convert.ToInt32(ddlStatus.SelectedItem.Value);

            objarea.ModifiedDate = DateTime.Now;
            objarea.ModifiedBy = User.Identity.Name;

            if (nAreaId == 0)
            {
                objarea.CreatedDate = DateTime.Now;
                objarea.CreatedBy = User.Identity.Name;
                objarea.ClientId = nClientId;

                _db.Areas.InsertOnSubmit(objarea);
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
        Response.Redirect("arealist.aspx");
    }
}