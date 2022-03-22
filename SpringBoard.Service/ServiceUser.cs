using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SpringBoard.Data.Infrastructure;
using SpringBoard.Domaine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringBoard.Service
{
    public class ServiceUser : Service<Utilisateur>, IServiceUser
    {
        static IDatabaseFactory dbf = new DatabaseFactory();
        static IUnitOfWork utwk = new UnitOfWork(dbf);
        private UserManager<Utilisateur> userManager;


        public ServiceUser(UserManager<Utilisateur> _userManager):base(utwk)
        {
            this.userManager = _userManager;
        }
      

       


        public async Task<Utilisateur> addUserAsync(dynamic us)
        {
            string role = us.role;
            

           if(string.IsNullOrWhiteSpace(role) || string.IsNullOrWhiteSpace(us.password) || string.IsNullOrEmpty(us.email))
            {
                return null;
            }

            if (role == "RH")
            {
                GestionnaireRH user = new GestionnaireRH();
                user.Email = us.email;
                user.LastName = us.lastName;
                user.Firstname = us.firstName;
                user.UserName = us.lastName + "-" + us.firstName + "-" + us.email;
               
               var result =  await userManager.CreateAsync(user, us.password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "RH");
                }
                else
                {
                    return null;
                }
                return user;

            }

            else if (role == "Consultant")
            {
                Consultant user = new Consultant();
                user.Email = us.email;
                user.LastName = us.lastName;
                user.Firstname = us.firstName;
                var result = userManager.CreateAsync(user, us.password);

                if (result.Succeeded)
                {
                   await userManager.AddToRoleAsync(user, "Consultant");
                }
                else
                {
                    return null;
                }
                return user;
            }

            else if (role == "Commercial")
            {
                Commercial user = new Commercial();
                user.Email = us.email;
                user.LastName = us.lastName;
                user.Firstname = us.firstName;
                var result = userManager.CreateAsync(user, us.password);
                if (result.Succeeded)
                {
                  await  userManager.AddToRoleAsync(user, "Commercial");
                }
                else
                {
                    return null;
                }
                return user;
            }
            else if(role=="Administrateur")
            {
                Administrateur user = new Administrateur();
                user.Email = us.email;
                user.LastName = us.lastName;
                user.Firstname = us.firstName;

                var result = userManager.CreateAsync(user, us.password);
                if (result.Succeeded)
                {
                   await userManager.AddToRoleAsync(user, "Administrateur");
                }
                else
                {
                    return null;
                }
                return user;
            }
            else
            {
                return null;
            }
           
        }


        public async Task<Utilisateur> updateProfile(dynamic user, UserManager<Utilisateur> userManager)
        {
            Utilisateur us = await userManager.FindByEmailAsync(user.email);

            if (us == null)
            {
                return null;
            }

            us.Firstname = user.firstName;
            us.LastName = user.lastName;

            if (!string.IsNullOrEmpty(user.password) && !string.IsNullOrWhiteSpace(user.password))
            {
                await userManager.RemovePasswordAsync(us);
                await userManager.AddPasswordAsync(us, user.password);
            }

            return userManager.UpdateAsync(user);
        }

        //TODO fix those fucking problems ich fick dish mann

        public  IEnumerable<Commercial> listCommercial()
        {
            return await utwk.getRepository<Commercial>().getAll();

           

        }

        public IEnumerable<Consultant> listConsultant()
        {
            return utwk.getRepository<Consultant>().getAll();
        }

        public IEnumerable<GestionnaireRH> listGestionnaireRH()
        {
            return utwk.getRepository<GestionnaireRH>().getAll();
        }

        //TODO fix the disposed context
        public async Task<IEnumerable<Utilisateur>> Search(string keyword)
        {
            return  this.GetMany(x => x.Firstname.Contains(keyword) || x.LastName.Contains(keyword) || x.UserName.Contains(keyword));
        }

      
    }
}
