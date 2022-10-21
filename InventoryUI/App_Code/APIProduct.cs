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
        string apiUrl = "https://localhost:7251/api/SP_Product";

        HttpClient client = new HttpClient();
        HttpResponseMessage response = client.GetAsync(apiUrl).Result;

        if (response.IsSuccessStatusCode)
        {
            var Result = response.Content.ReadAsStringAsync().Result;
            products = JsonConvert.DeserializeObject<List<csProduct>>(Result);
        }

        return products;
    }

    public static List<csProduct> GetProductsBySearch(string search)
    {

        List<csProduct> products = new List<csProduct>();
        string apiUrl = "https://localhost:7251/api/SP_Product/" + search;

        HttpClient client = new HttpClient();
        HttpResponseMessage response = client.GetAsync(apiUrl).Result;

        if (response.IsSuccessStatusCode)
        {
            var Result = response.Content.ReadAsStringAsync().Result;
            products = JsonConvert.DeserializeObject<List<csProduct>>(Result);
        }

        return products;
    }

    public static List<csProduct> InertProduct(SP_Product Product)
    {
        var jcProduct = JsonConvert.SerializeObject(Product);

        StringContent httpConent = new StringContent(jcProduct, Encoding.UTF8);

        List<csProduct> products = new List<csProduct>();
        string apiUrl = "https://localhost:7251/api/SP_Product/";

        HttpClient client = new HttpClient();
        HttpResponseMessage response = client.PostAsync(apiUrl, httpConent).Result;

        if (response.IsSuccessStatusCode)
        {
            var Result = response.Content.ReadAsStringAsync().Result;
            products = JsonConvert.DeserializeObject<List<csProduct>>(Result);
        }

        return products;
    }
}