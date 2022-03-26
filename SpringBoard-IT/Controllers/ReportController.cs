using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpringBoard.Service;

namespace SpringBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {

        private IServiceCompteRendu serviceCompteRendu;


        public ReportController(IServiceCompteRendu _serviceCompteRendu)
        {
            this.serviceCompteRendu = _serviceCompteRendu;

        }







    }
}
