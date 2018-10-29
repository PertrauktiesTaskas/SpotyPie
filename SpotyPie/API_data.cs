using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RestSharp;

namespace SpotyPie
{
    public static class API_data
    {

        public static void GetSong()
        {
            try
            {
                var client = new RestClient("https://192.168.43.177:9876/api/Songs/1");
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    string a = response.Content;
                }
                else
                    Console.WriteLine("Error");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}