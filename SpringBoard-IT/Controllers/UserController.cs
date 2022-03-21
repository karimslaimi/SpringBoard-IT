using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpringBoard.API.Model;
using SpringBoard.Domaine;
using SpringBoard.Service;

namespace SpringBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IServiceUser serviceUser;
        private UserManager<Utilisateur> userManager;

        public UserController(IServiceUser _serviceUser,UserManager<Utilisateur> _userManager)
        {
            this.serviceUser = _serviceUser;
            this.userManager = _userManager;

        }

        [HttpPost]
        public async Task<object> addUserAsync([FromBody] RegisterModel register)
        {
            if (!register.valid())
            {
                return false;
            }

           return await this.serviceUser.addUserAsync((dynamic)register,this.userManager)!=null;
        }
    }
}
