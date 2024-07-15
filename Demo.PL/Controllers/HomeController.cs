using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Test([FromBody] UrlInputModel input)
        {
            string pythonScriptPath = @"C:\Users\LapStore\source\repos\Demo MVC\Demo.PL\wwwroot\Scripts\ReviewsScraping.py";
            string result = "";
            string url = input.Url;

            try
            {
                ProcessStartInfo start = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = $"\"{pythonScriptPath}\" \"{url}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        result = await reader.ReadToEndAsync();
                    }
                    using (StreamReader reader = process.StandardError)
                    {
                        string errors = await reader.ReadToEndAsync();
                        if (!string.IsNullOrEmpty(errors))
                        {
                            throw new Exception(errors);
                        }
                    }
                }

                return Json(new { success = true, result = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        public class UrlInputModel
        {
            public string Url { get; set; }
        }
    }
}

//using Demo.PL.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;

//namespace Demo.PL.Controllers
//{

//    [Authorize]
//    public class HomeController : Controller
//    {
//        [HttpGet]
//        public ActionResult Index()
//        {
//            return View();
//        }

//        //[HttpPost]
//        //public async Task<ActionResult> Index(string review)
//        //{
//        //    // Base URL of the sentiment prediction API
//        //    string apiUrl = "https://ml-api-final-1.onrender.com/";

//        //    try
//        //    {
//        //        using (HttpClient client = new HttpClient())
//        //        {
//        //            // Construct the full API URL with the review text
//        //            string requestUrl = apiUrl + "predict/" + review;

//        //            // Make a GET request to the API
//        //            HttpResponseMessage response = await client.GetAsync(requestUrl);

//        //            // Check if the request was successful
//        //            if (response.IsSuccessStatusCode)
//        //            {
//        //                // Read the response content as string
//        //                string jsonResponse = await response.Content.ReadAsStringAsync();

//        //                // Return the JSON response to the client
//        //                return Content(jsonResponse, "application/json");
//        //            }
//        //            else
//        //            {
//        //                // If the request was not successful, return an error response with status code 500
//        //                return Content("Failed to fetch data from API", "text/plain");
//        //            }
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        // If an exception occurred during the request, return a server error response
//        //        return Content("An error occurred: " + ex.Message, "text/plain");
//        //    }
//        //}




//    //public IActionResult Privacy()
//    //{
//    //    return View();
//    //}

//    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}
