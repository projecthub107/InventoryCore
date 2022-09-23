using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Schedule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


           
        }
    }

    [WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<events> GetEvents()
    {


        DataClassesDataContext _db = new DataClassesDataContext();


        List<Event> listevents = _db.Events.ToList();

        DataTable dt = utility.LINQToDataTable(listevents);

        //using (SqlConnection sqlConn = new SqlConnection("DBConnString"))
        //{
        //    using (SqlCommand sqlCmd = new SqlCommand("usp_GetEvents", sqlConn))
        //    {
        //        sqlCmd.CommandType = CommandType.StoredProcedure;
        //        sqlConn.Open();
        //        SqlDataAdapter ad = new SqlDataAdapter(sqlCmd);
        //        ad.Fill(dt);
        //        sqlConn.Close();
        //    }
        //}

        return dt.AsEnumerable().Select(datarow =>
        new events()
        {
            EventId = Convert.ToInt32(datarow["EventId"]),
            Subject = Convert.ToString(datarow["Subject"]),
            Description = Convert.ToString(datarow["Description"]),
            Start = Convert.ToDateTime(datarow["Start"]),
            End = Convert.ToDateTime(datarow["End"]),
            ThemeColor = Convert.ToString(datarow["ThemeColor"]),
            IsFullDay = false
        }
        ).ToList();
    }

    public class events
    {
        public int EventId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string ThemeColor { get; set; }
        public bool IsFullDay { get; set; }
    }

}