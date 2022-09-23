using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userdetails : System.Web.UI.Page
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
                if (Page.User.IsInRole("user002") == false)
                {
                    // No Permission Page.
                    Response.Redirect("nopermission.aspx");
                }

                int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
                
                BindRole(nClientId);
                BindQuestions();


                if (Request.QueryString.Get("uid") != null)
                {

                    int nUserId = Convert.ToInt32(Request.QueryString.Get("uid"));

                    hdnUserId.Value = nUserId.ToString();

                    LoadUserDetails(nClientId, nUserId);

                    lblTitle.Text = "User Details";
                }              
               
            }


        }

   
    }

    protected void LoadUserDetails(int nClientId, int nUserId)
    {
        try
        {
            DataClassesDataContext _db = new DataClassesDataContext();

            string strQ = "SELECT u.UserId, u.UserName, u.Password,u.Email,u.RoleId,u.LastLoginDate, u.QuestionID, u.Answer, " +
                          " u.Status,u.CreatedDate,u.CreatedBy,u.ModifiedDate,u.ModifiedBy,u.ClientId " +
                          " FROM UserInfo AS u " +
                          " WHERE u.UserId = " + nUserId;


            csuserinfo objUser = _db.ExecuteQuery<csuserinfo>(strQ, string.Empty).FirstOrDefault();

            txtUser.Text = objUser.UserName;
            txtEmail.Text = objUser.Email;
            ddlRole.SelectedValue = objUser.RoleId.ToString();
            ddlStatus.SelectedValue = objUser.Status.ToString();
            ddlQuestion.SelectedValue = objUser.QuestionID.ToString();
            txtAnswer.Text = objUser.Answer;
        }
        catch (Exception ex)
        {
            lblResult.Text = utility.GetSystemErrorMessage("LoadUserDetails(), " + ex.Message);
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

            ddlRole.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblResult.Text = utility.GetSystemErrorMessage("BindRole(), " + ex.Message);
        }
    }

    private void BindQuestions()
    {
        try
        {
            DataClassesDataContext _db = new DataClassesDataContext();
            var item = from q in _db.Questions
                       select q;
            ddlQuestion.DataSource = item.ToList();
            ddlQuestion.DataTextField = "QuestionName";
            ddlQuestion.DataValueField = "QuestionID";
            ddlQuestion.DataBind();

            ddlQuestion.Items.Insert(0, new ListItem("Select", "0"));           
        }
        catch (Exception ex)
        {
            lblResult.Text = utility.GetSystemErrorMessage("BindQuestions(), " + ex.Message);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
            int nUserId = Convert.ToInt32(hdnUserId.Value);

            DataClassesDataContext _db = new DataClassesDataContext();
            UserInfo objUser = new UserInfo();
            string strRequired = "";

            int nRoleId = 0;
            int nQuestionId = 0;
           
          
            lblResult.Text = "";

            if (txtUser.Text.Trim() == "")
            {
                strRequired = "Missing required field: Name.<br />";

            }
            
            if (txtEmail.Text.Trim() == "")
            {

                strRequired += "Missing required field: Email.<br />";

            }
            else
            {
                if (_db.UserInfos.Any(c => c.Email == txtEmail.Text.Trim() && c.UserId != nUserId))
                    strRequired += "Email already exist. Please try another Email.<br/>";
            }
          

            if (nUserId == 0)// New User
            {
                if (txtPassword.Text.Trim() == "")
                {
                    strRequired += "Missing required field: Password.<br/>";
                   
                }
                else
                {
                    if (txtPassword.Text.Trim().Length < 6)
                    {
                        strRequired += "Password length should be minimum 6.<br/>";
                       
                    }
                }

                if (txtPassword.Text.Trim() != txtConfirmPass.Text.Trim())
                {
                    strRequired += "Please confirm password.<br/>";                   
                }
            }
            else // Existing user
            {
                if (txtPassword.Text.Trim() != "")
                {
                    if (txtPassword.Text.Trim().Length < 6)
                    {
                        strRequired += "Password length should be minimum 6.<br />";
                       
                    }


                    if (txtPassword.Text.Trim() != txtConfirmPass.Text.Trim())
                    {
                        strRequired += "Please confirm password.<br />";
                       
                    }
                }
            }


            if (ddlQuestion.SelectedItem.Value != "0")
            {
                nQuestionId = Convert.ToInt32(ddlQuestion.SelectedItem.Value);
            }
            else
            {
                strRequired += "Please select Question.<br />";
            }

            if (txtAnswer.Text.Trim() == "")
            {

                strRequired += "Missing required field: Answer.<br />";

            }

            if (ddlRole.SelectedItem.Value != "0")
            {
                nRoleId = Convert.ToInt32(ddlRole.SelectedItem.Value);
            }
            else
            {
                strRequired += "Please select Role.<br />";
            }

           

            if (strRequired.Length > 0)
            {
                lblResult.Text = utility.GetSystemRequiredMessage(strRequired);
                return;
            }


            if (_db.UserInfos.Any(p => p.UserId == nUserId && p.ClientId == nClientId))
            {
                objUser = _db.UserInfos.FirstOrDefault(p => p.UserId == nUserId && p.ClientId == nClientId);
            }             

            objUser.UserName = txtUser.Text.Trim();           
            if (txtPassword.Text.Trim() != "" && txtConfirmPass.Text.Trim() != "" && txtPassword.Text.Trim() == txtConfirmPass.Text.Trim())
            {
                objUser.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim(), "SHA1");
            }
            objUser.Email = txtEmail.Text.Trim();
            objUser.RoleId = nRoleId;         
            objUser.Status = Convert.ToInt32(ddlStatus.SelectedItem.Value);          
            objUser.ModifiedDate = DateTime.Now;
            objUser.ModifiedBy = User.Identity.Name;           
            objUser.QuestionID = nQuestionId;
            objUser.Answer = txtAnswer.Text.Trim();

            // Insert User
            if (nUserId == 0) // New User
            {
                objUser.ClientId = nClientId;
                objUser.LastLoginDate = DateTime.Now;
                objUser.CreatedDate = DateTime.Now;
                objUser.CreatedBy = User.Identity.Name;
                _db.UserInfos.InsertOnSubmit(objUser);

            }

            _db.SubmitChanges();

            hdnUserId.Value = objUser.UserId.ToString();

            lblResult.Text = utility.GetSystemMessage("Data Saved successfully.<br />");
        }
        catch (Exception ex)
        {
            lblResult.Text = utility.GetSystemErrorMessage("btnAdd_Click(), " + ex.Message);
        }


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("userlist.aspx");
    }
  
}