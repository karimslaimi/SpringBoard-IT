using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpringBoard.API.Model;
using SpringBoard.Domaine;
using SpringBoard.Service;

namespace SpringBoard.API.Controllers
{
   
    [Route("api/[controller]/[action]")]
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

           return await this.serviceUser.addUserAsync((dynamic)register)!=null;
        }

        [HttpPut]
        public async Task<Object> updateProfile([FromBody] RegisterModel profile)
        {
            if (!profile.valid())
            {
                return BadRequest("Error in the object format (null values)");
            }
            var user = await this.serviceUser.updateProfile((dynamic)profile, this.userManager);

            return user != null ? Ok(user) : BadRequest("Error occured while updating the profile");
        }


        [HttpGet]
        [Route("/listCommercial")]
        public IEnumerable<Commercial> listCommercial()
        {
            return this.serviceUser.listCommercial();
        }

        [HttpGet]
        [Route("/listConsultant")]
        public IEnumerable<Consultant> listConsultant()
        {
            return this.serviceUser.listConsultant();
        } 
        
        
        [HttpGet]
        [Route("/listRH")]
        public IEnumerable<GestionnaireRH> listRH()
        {
            return this.serviceUser.listGestionnaireRH();
        } 
        
        [HttpGet]
        [Route("/filter")]
        public async Task<IEnumerable<Utilisateur>> filter(string filter)
        {
            var result = await serviceUser.Search(filter);
            return result;
        }
    }
}
