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
public class APIProduct
{
    public APIProduct()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static List<csProduct> GetProducts()
    {

        List<csProduct> products = new List<csProduct>();
        string apiUrl = "https://localhost:7251/api/Products";

        HttpClient client = new HttpClient();
        HttpResponseMessage response = client.GetAsync(apiUrl).Result;

        if (response.IsSuccessStatusCode)
        {
            products = JsonConvert.DeserializeObject<List<csProduct>>(response.Content.ReadAsStringAsync().Result);
        }

        return products;
    }
}