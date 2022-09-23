using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Security.Principal;

public partial class Main : System.Web.UI.MasterPage
{

    public event EventHandler contentCallEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        if (!IsPostBack)
        {
            string strName = string.Empty;

            if (Session["oUser"] == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings["LoginPage"].ToString());
            }
            else
            {
                BindDefaultLocation(nClientId);
            }
        }


    }

    protected void BindDefaultLocation(int nClientId)
    {
        DataClassesDataContext _db = new DataClassesDataContext();

        List<Location> listLocation = _db.Locations.Where(l => l.ClientId == nClientId && l.Status == 1).ToList();
        Preference objPf = _db.Preferences.Where(p => p.ClientId == nClientId).SingleOrDefault();

        ddlDefaultLocation.DataSource = listLocation;
        ddlDefaultLocation.DataTextField = "LocationName";
        ddlDefaultLocation.DataValueField = "LocationId";
        ddlDefaultLocation.DataBind();

        ddlDefaultLocation.SelectedValue = objPf.DefaultLocationId.ToString();
    }

    protected void ddlDefaultLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataClassesDataContext _db = new DataClassesDataContext();

        Preference objpf = _db.Preferences.Where(s => s.ClientId == 1).SingleOrDefault();

        objpf.DefaultLocationId = Convert.ToInt32(ddlDefaultLocation.SelectedItem.Value);

        _db.SubmitChanges();


    }


}


