using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net.Http;

/// <summary>
/// Summary description for APIUser
/// </summary>
public class APIUser
{
    public APIUser()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static List<csUsers> GetUsers()
    {

        List<csUsers> users = new List<csUsers>();
        string apiUrl = "https://localhost:7251/api/SP_UserInfo";

        HttpClient client = new HttpClient();
        HttpResponseMessage response = client.GetAsync(apiUrl).Result;

        if (response.IsSuccessStatusCode)
        {
            var Result = response.Content.ReadAsStringAsync().Result;
            users = JsonConvert.DeserializeObject<List<csUsers>>(Result);
        }

        return users;
    }
}