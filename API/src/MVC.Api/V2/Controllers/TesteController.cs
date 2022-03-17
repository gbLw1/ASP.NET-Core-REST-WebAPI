using Microsoft.AspNetCore.Mvc;
using MVC.Api.Controllers;
using MVC.Business.Interfaces;

namespace MVC.Api.V2.Controllers
{
    [ApiVersion("2.0")]
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
            return "Sou a v2";
        }
    }
}
