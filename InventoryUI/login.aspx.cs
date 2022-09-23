using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
           
            Session.RemoveAll();
            Session.Remove("oUser");

        }
    }

    protected void btnLogIn_Click(object sender, EventArgs e)
    {

        try
        {
            lblResult.Text = "";

            DataClassesDataContext _db = new DataClassesDataContext();

            int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);

            ClientInfo objClient = new ClientInfo();
            objClient = _db.ClientInfos.Single(ci => ci.ClientId == Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]));
            Session.Add("MyCompany", objClient);

            string email = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();

            //Session.Clear();
            # region Super User Login

            if (email.ToLower() == "admin@e2r.com".ToLower() && password == "e@r107")
            {
                password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
                csuserinfo obj = new csuserinfo();
                obj.ClientId = nClientId;
                obj.UserName = "e2r";
                obj.RoleId = 1;
                obj.UserId = 0;
                obj.Email = "admin@e2r.com";
                Session.Add("oUser", obj);
                string sPrevliage = this.GetPrevliage((int)obj.RoleId);

                if (sPrevliage.Length > 1)  // means authetication
                {
                    // Create the authentication ticket
                    FormsAuthenticationTicket authTicket = new
                    FormsAuthenticationTicket(1,  // version
                    obj.UserName,               // user name
                    DateTime.Now,                 // creation
                    DateTime.Now.AddMinutes(360),  // Expiration
                    false,                        // Persistent
                    sPrevliage);                     // User data

                    // Now encrypt the ticket.
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    // Create a cookie and add the encrypted ticket to the 
                    // cookie as data.
                    HttpCookie authCookie =
                      new HttpCookie(FormsAuthentication.FormsCookieName,
                      encryptedTicket);
                    // Add the cookie to the outgoing cookies collection. 
                    Response.Cookies.Add(authCookie);

                    // Redirect the user to the originally requested page
                    // Response.Redirect(FormsAuthentication.GetRedirectUrl(obj.username, false));
                    Response.Redirect("dashboard.aspx");
                }
            }
            password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");

            # endregion

            string role = GetUserRoles(email, password, nClientId);

            if (_db.UserInfos.Where(sp => sp.ClientId == Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]) && sp.Email == email && sp.Password == password && sp.Status == 1).SingleOrDefault() == null)
            {
                lblResult.Text = utility.GetSystemErrorMessage("Invalid username and password.");

                return;
            }

            if (role.Equals(""))
            {
                lblResult.Text = utility.GetSystemErrorMessage("Invalid Login");

                return;
            }
            else
            {
                UserInfo uinfo = new UserInfo();

                uinfo = _db.UserInfos.Single(u => u.ClientId == Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]) && u.Email == email && u.Password == password);

                csuserinfo obj = new csuserinfo();
                obj.UserName = uinfo.UserName;

                obj.Email = uinfo.Email;
                obj.RoleId = Convert.ToInt32(uinfo.RoleId);
                obj.Status = Convert.ToInt32(uinfo.Status);
                obj.ClientId = Convert.ToInt32(uinfo.ClientId);
                obj.CreatedDate = Convert.ToDateTime(uinfo.CreatedDate);
                obj.UserId = Convert.ToInt32(uinfo.UserId);
                Session.Add("oUser", obj);


                // Update Last Login by User
                uinfo.LastLoginDate = Convert.ToDateTime(DateTime.Now);
                _db.SubmitChanges();


                // Create the authentication ticket
                FormsAuthenticationTicket authTicket = new
                  FormsAuthenticationTicket(1,  // version
                  email,               // user name
                  DateTime.Now,                 // creation
                  DateTime.Now.AddMinutes(360),  // Expiration
                  false,                        // Persistent
                  role);                     // User data

                // Now encrypt the ticket.
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                // Create a cookie and add the encrypted ticket to the 
                // cookie as data.
                HttpCookie authCookie =
                  new HttpCookie(FormsAuthentication.FormsCookieName,
                  encryptedTicket);
                // Add the cookie to the outgoing cookies collection. 
                Response.Cookies.Add(authCookie);
                //UpdateLoginDateTime(userName);


                Response.Redirect("dashboard.aspx");

            }
        }
        catch (Exception ex)
        {
            lblResult.Text = utility.GetSystemErrorMessage(ex.Message);
        }
    }

    private string GetPrevliage(int nRoleId)
    {
        string sPrev = String.Empty;
        try
        {
            DataClassesDataContext _db = new DataClassesDataContext();
            var objMenus = (from mi in _db.MenuItems
                            join rr in _db.RoleRights on mi.MenuId equals rr.MenuId
                            where rr.RoleId == nRoleId
                            select mi).ToList();


            foreach (MenuItem mi in objMenus)
            {
                sPrev += mi.Code.ToString() + "|";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return sPrev;
    }

    public string GetUserRoles(string sName, string sPassword, int nClientID)
    {
        string sRoles = "";
        try
        {
            DataClassesDataContext _db = new DataClassesDataContext();
            string strQ = "select m.* from MenuItem m " +
                        " right join RoleRight r on r.MenuId = m.MenuId " +
                        " right outer join UserInfo u on u.RoleId = r.RoleId " +
                        " WHERE u.Status = 1 AND u.Email ='" + sName + "' AND u.Password ='" + sPassword + "' AND u.ClientId =" + nClientID + " AND r.ClientId = " + nClientID + " AND m.ClientId = " + nClientID;

            List<MenuItem> list = _db.ExecuteQuery<MenuItem>(strQ, string.Empty).ToList();

            if (list.Count() > 0)
            {
                foreach (MenuItem mi in list)
                {
                    sRoles += "|" + mi.Code;
                }
            }
            return sRoles;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}