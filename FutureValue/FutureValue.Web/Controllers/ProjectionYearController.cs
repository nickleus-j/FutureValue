using Microsoft.AspNetCore.Mvc;
using FutureValue.Web.ViewModels;

namespace FutureValue.Web.Controllers
{
    public class ProjectionYearController : Controller
    {
        public IActionResult Index([FromBody] IEnumerable<ProjectionYearViewModel> viewModel)
        {
            return PartialView(viewModel);
        }
    }
}
