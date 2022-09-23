using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
public class csUsers
{
    public csUsers()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int RoleId { get; set; }
    public System.DateTime LastLoginDate { get; set; }
    public int? Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string ModifiedBy { get; set; }
    public int ClientId { get; set; }
    public int? QuestionID { get; set; }
    public string Answer { get; set; }
}


