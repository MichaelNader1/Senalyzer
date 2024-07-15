using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;

namespace Demo.PL.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Test(string input)
        {
            string pythonScriptPath = @"C:\Users\LapStore\source\repos\Demo MVC\Demo.PL\wwwroot\Scripts\ReviewsScraping.py";
            string result = "";

            try
            {
                // Create a process to run the Python script
                ProcessStartInfo start = new ProcessStartInfo
                {
                    FileName = "python",  // Assumes 'python' is in your PATH environment variable
                    Arguments = $"\"{pythonScriptPath}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        result = reader.ReadToEnd();
                    }
                    using (StreamReader reader = process.StandardError)
                    {
                        string errors = reader.ReadToEnd();
                        if (!string.IsNullOrEmpty(errors))
                        {
                            throw new Exception(errors);
                        }
                    }
                }

                ViewBag.Result = result;
                return View("Test");
            }
            catch (Exception ex)
            {
                ViewBag.Result = $"An error occurred: {ex.Message}";
                return View("Test");
            }
        }
    }
}

