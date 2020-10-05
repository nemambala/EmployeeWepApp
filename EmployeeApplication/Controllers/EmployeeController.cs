using EmployeeApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace EmployeeApplication.Controllers
{
    public class EmployeeController : Controller
    {
        private string url = System.Configuration.ConfigurationManager.AppSettings["EmployeeWebApi"].ToString();
        // GET: Employee
        public ActionResult Index()
        {
            IEnumerable<EmployeeModel> employee = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                //HTTP GET
                var responseTask = client.GetAsync("employee");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<EmployeeModel>>();
                    readTask.Wait();

                    employee = readTask.Result;
                }
                else
                {

                    employee = Enumerable.Empty<EmployeeModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(employee);
        }

        public JsonResult Getemployee(int id)
        {
            IList<EmployeeModel> employee = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                //HTTP GET
                var responseTask = client.GetAsync("employee/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<EmployeeModel>>();
                    readTask.Wait();

                    employee = readTask.Result;
                }
                else
                {                   
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

            }
            return Json(employee, JsonRequestBehavior.AllowGet);         
        }

        public int DeleteEmployee(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                //HTTP DELETE
                var deleteTask = client.GetAsync("DeleteEmployee/" + id);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return 1;
                }
            }
            return 0;

        }

        public int AddEditEmployee(EmployeeModel emp)
        {           
            using (var client = new HttpClient())
            {               
                client.BaseAddress = new Uri(url);              

                //HTTP POST
                var postTask = client.PostAsJsonAsync<EmployeeModel>("InsertEmployee", emp);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return 1;
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return 0;
        }
    
    }
}