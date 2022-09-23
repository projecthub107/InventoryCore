using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for userinfo
/// </summary>
public class csuserinfo
{
    public csuserinfo()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int ClientId { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int RoleId { get; set; }
    public int Status { get; set; }
    public int QuestionID { get; set; }
    public string Answer { get; set; }
    public DateTime LastLoginDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string CreatedBy { get; set; }
    public string company_email { get; set; }
    public string ModifiedBy { get; set; }
    public string Active { get; set; }
    public string Role { get; set; }
}

public class csUserSearch
{
    public csUserSearch()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int UserId { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }      
}