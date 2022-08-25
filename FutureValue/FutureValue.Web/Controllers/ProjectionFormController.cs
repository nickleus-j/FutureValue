using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FutureValue.Web.ViewModels;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace FutureValue.Web.Controllers
{
    public class ProjectionFormController : Controller
    {
        IConfiguration Configuration;
        public ProjectionFormController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        // GET: ProjectionFormController
        public ActionResult Index()
        {
            IEnumerable<ProjectionFormViewModel> projetions = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@Configuration["ApiBaseUrl"] + "api/");
                //HTTP GET
                var responseTask = client.GetAsync("ProjectionForm");
                responseTask.Wait();
                try
                {
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadFromJsonAsync<IEnumerable<ProjectionFormViewModel>>();
                        readTask.Wait();

                        projetions = readTask.Result;
                    }
                    else //web api sent error response 
                    {
                        //log response status here..

                        projetions = Enumerable.Empty<ProjectionFormViewModel>();

                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                    return View(projetions);
                }
                catch (NullReferenceException ne)
                {
                    return View(projetions);
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }

            }
        }
        // GET: ProjectionFormController/Details/5
        public ActionResult Details(int id)
        {
            ProjectionFormViewModel projetion = new ProjectionFormViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@Configuration["ApiBaseUrl"] + "api/");
                //HTTP GET
                var responseTask = client.GetAsync("ProjectionForm/" + id);
                responseTask.Wait();
                try
                {
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadFromJsonAsync<ProjectionFormViewModel>();
                        readTask.Wait();

                        projetion = readTask.Result;


                    }
                    else //web api sent error response 
                    {
                        //log response status here..

                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                    return View(projetion);
                }
                catch (NullReferenceException ne)
                {
                    return View(projetion);
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }

            }
        }

        // GET: ProjectionFormController/Create
        public ActionResult Create()
        {
            return View();
        }


        // GET: ProjectionFormController/Edit/5
        public ActionResult Edit(int id)
        {
            ProjectionFormViewModel projetion = new ProjectionFormViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@Configuration["ApiBaseUrl"] + "api/");
                //HTTP GET
                var responseTask = client.GetAsync("ProjectionForm/" + id);
                responseTask.Wait();
                try
                {
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadFromJsonAsync<ProjectionFormViewModel>();
                        readTask.Wait();

                        projetion = readTask.Result;


                    }
                    else //web api sent error response 
                    {
                        //log response status here..

                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                    return View(projetion);
                }
                catch (NullReferenceException ne)
                {
                    return View(projetion);
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }

            }
        }


        // GET: ProjectionFormController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

    }
}
