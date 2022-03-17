using Microsoft.AspNetCore.Mvc;
using MVC.Api.Controllers;
using MVC.Business.Interfaces;

namespace MVC.Api.V1.Controllers
{
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TesteController : MainController
    {
        public TesteController(INotifier notifier,
                               IUser appUser) : base(notifier, appUser)
        {
        }

        [HttpGet]
        public string Valor()
        {
            return "Sou a v1";
        }
    }
}
