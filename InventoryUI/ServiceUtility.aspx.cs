using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ServiceUtility : System.Web.UI.Page
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
        }
    }

    [System.Web.Services.WebMethod]
    public static List<csProductSearch> GetProductName(string keyword)
    {
        DataClassesDataContext _db = new DataClassesDataContext();
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        int nLocationId = utility.GetDefaultLocationId(nClientId);

        string strCondition = "";

        if (!keyword.ToLower().Contains("*"))
        {
            strCondition = "  AND ((UPPER(p.[ProductCode]) LIKE '" + keyword.Trim().ToUpper() + "%') OR (UPPER(p.[ProductName]) LIKE '" + keyword.Trim().ToUpper() + "%')) ";
        }
        string strQ = "SELECT p.ProductId, p.ProductCode, p.ProductName, ISNULL(QuantityInStock,0) AS QuantityInStock" +
                       " FROM Product AS p " +
                       " LEFT OUTER JOIN ( Select SUM(ISNULL(pt.QuantityIn,0))-SUM(ISNULL(pt.QuantityOut,0)) As QuantityInStock, pt.ProductId FROM ProductTransaction AS pt " +
                       " INNER JOIN Location AS l on pt.LocationId = l.LocationId " +
                       " WHERE l.LocationId = " + nLocationId + "  GROUP BY pt.ProductId ) AS t on p.ProductId  = t.ProductId " +
                       " WHERE p.ClientId = " + nClientId + " " + strCondition;


        IEnumerable<csProductSearch> item = _db.ExecuteQuery<csProductSearch>(strQ, string.Empty).ToList();

       

        return item.Distinct().OrderBy(f => f.ProductCode).ToList();


    }

    [System.Web.Services.WebMethod(true)]
    public static csProductSearch SetProduct(string product)
    {
        System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();
        csProductSearch objP = json.Deserialize<csProductSearch>(product);

        System.Web.HttpContext.Current.Session.Add("searchProduct", objP);

        return objP;
    }

    [System.Web.Services.WebMethod]
    public static List<csUserSearch> GetUserName(string keyword)
    {
        DataClassesDataContext _db = new DataClassesDataContext();
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        int nLocationId = utility.GetDefaultLocationId(nClientId);

        string strCondition = "";

        if (!keyword.ToLower().Contains("*"))
        {
            strCondition = "  AND ((UPPER(u.[UserName]) LIKE '" + keyword.Trim().ToUpper() + "%') OR (UPPER(u.[Email]) LIKE '" + keyword.Trim().ToUpper() + "%')) ";
        }
        string strQ = "SELECT u.UserId, u.UserName, u.Email " +
                       " FROM UserInfo AS u " +
                       " WHERE u.ClientId = " + nClientId + " " + strCondition;


        IEnumerable<csUserSearch> item = _db.ExecuteQuery<csUserSearch>(strQ, string.Empty).ToList();



        return item.Distinct().OrderBy(f => f.UserName).ToList();


    }

    [System.Web.Services.WebMethod(true)]
    public static csUserSearch SetUser(string user)
    {
        System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();
        csUserSearch objU = json.Deserialize<csUserSearch>(user);

        System.Web.HttpContext.Current.Session.Add("searchUser", objU);

        return objU;
    }
}