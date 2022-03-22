using Microsoft.AspNetCore.Identity;
using SpringBoard.Domaine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringBoard.Service
{
    public interface IServiceUser:IService<Utilisateur>
    {
        public Task<Utilisateur> addUserAsync(dynamic user);
        public Task<Utilisateur> updateProfile(dynamic user, UserManager<Utilisateur> userManager);

        public IEnumerable<Consultant> listConsultant();
        public IEnumerable<Commercial> listCommercial();
        public IEnumerable<GestionnaireRH> listGestionnaireRH();
        public Task<IEnumerable<Utilisateur>> Search(string keyword);




    }
}
