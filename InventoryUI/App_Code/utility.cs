using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.IO;

using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Data.SqlClient;
using System.Reflection;
using System.Net.Mail;

/// <summary>
/// Summary description for utility
/// </summary>
public class utility
{
    public utility()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
    {
        DataTable dtReturn = new DataTable();

        // column names
        PropertyInfo[] oProps = null;

        if (varlist == null) return dtReturn;

        foreach (T rec in varlist)
        {
            // Use reflection to get property names, to create table, Only first time, others will follow
            if (oProps == null)
            {
                oProps = ((System.Type)rec.GetType()).GetProperties();
                foreach (PropertyInfo pi in oProps)
                {
                    System.Type colType = pi.PropertyType;

                    if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                    == typeof(Nullable<>)))
                    {
                        colType = colType.GetGenericArguments()[0];
                    }

                    dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                }
            }

            DataRow dr = dtReturn.NewRow();

            foreach (PropertyInfo pi in oProps)
            {
                dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                (rec, null);
            }

            dtReturn.Rows.Add(dr);
        }
        return dtReturn;
    }

    public static int GetDefaultLocationId(int nClientId)
    {
        DataClassesDataContext _db = new DataClassesDataContext();

        string strQ = " SELECT * " +
                      " FROM Preferences " +
                      " WHERE ClientId = " + nClientId;


        Preference objPf = _db.ExecuteQuery<Preference>(strQ, string.Empty).FirstOrDefault();
        return (int)objPf.DefaultLocationId;

    }

    public static string GetSystemMessage(string str)
    {
        return "<span style='color:blue;'><b>System MSG: </b></span><span style='color:green;'>" + str + "</span>";

    }
    public static string GetSystemErrorMessage(string str)
    {
        return "<span style='color:blue;'><b>System MSG: </b></span><span style='color:red;'>" + str + "</span>";
    }

    public static string GetSystemRequiredMessage(string str)
    {
        return "<span style='color:blue;'><b>System MSG: </b></span><br/><span style='color:red;'>" + str + "</span>";
    }

    public static void SendMail(string From, string To, string Cc, string Bcc, string Subject, string Body)
    {
        csEmail.SendMail(From, To, Cc, Bcc, Subject, Body);
        //MailMessage msg = new MailMessage();
        //msg.From = new MailAddress(From);
        //if (Cc.Length > 0)
        //    msg.CC.Add(Cc);
        //if (Bcc.Length > 0)
        //    msg.Bcc.Add(Bcc);
        //msg.To.Add(To);
        //msg.Subject = Subject;
        //msg.IsBodyHtml = true;
        //msg.Body = Body;
        //msg.Priority = MailPriority.High;


        //SendByLocalhost(msg);

    }

    public static void SendByLocalhost(MailMessage msg)
    {

        try
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "localhost";
            string strSendEmail = System.Configuration.ConfigurationManager.AppSettings["SendEmail"];
            if (strSendEmail == "Yes")
            {
                smtp.Send(msg);
            }

        }
        catch
        {

        }

    }



    private static Random random = new Random();
    public static string GetRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKL0123456789MNOPQRSTUVWXYZ";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static void SetProductQuantity(int nProductId)
    {
        DataClassesDataContext _db = new DataClassesDataContext();
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);

        var objPT = _db.ProductTransactions.Where(pt => pt.ProductId == nProductId && pt.ClientId == nClientId)
                        .GroupBy(x => new { x.ProductId })
                        .Select(x => new
                        {
                            QuantityInStock = x.Sum(z => z.QuantityIn) - x.Sum(z => z.QuantityOut)
                        });



        if (objPT != null)
        {
            Product objPrd = _db.Products.SingleOrDefault(p => p.ProductId == nProductId && p.ClientId == nClientId);

            if (objPrd != null)
            {
                objPrd.Quantity = objPT.SingleOrDefault().QuantityInStock;
                _db.SubmitChanges();
            }
        }


    }
}