using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FutureValue.Web.ViewModels;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace FutureValue.Web.Controllers
{
    public class ProjectionYearController : Controller
    {
        IConfiguration Configuration;
        public ProjectionYearController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public IActionResult Index([FromBody] IEnumerable<ProjectionYearViewModel> viewModel)
        {
            return PartialView(viewModel);
        }
        public IActionResult Preview([FromBody] ProjectionFormViewModel viewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@Configuration["ApiBaseUrl"] + "api/");
                var buffer = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(viewModel));
                var byteContent = new ByteArrayContent(buffer);
                IEnumerable<ProjectionYearViewModel> models = new List<ProjectionYearViewModel>();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HTTP Post
                var responseTask = client.PostAsync("Projection/", byteContent);
                responseTask.Wait();
                try
                {
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadFromJsonAsync<IEnumerable<ProjectionYearViewModel>>();
                        readTask.Wait();

                        models=readTask.Result;
                    }
                    else //web api sent error response 
                    {
                        //log response status here..

                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                    return PartialView("Index", viewModel);
                }
                catch (NullReferenceException ne)
                {
                    return PartialView("Index", viewModel);
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }

            }
            
        }
    }
}
