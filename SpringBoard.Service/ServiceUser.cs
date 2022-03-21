using Microsoft.AspNetCore.Identity;
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


      

        public ServiceUser():base(utwk)
        {
         
        }

        public async Task<Utilisateur> addUserAsync(dynamic us,UserManager<Utilisateur> userManager)
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

                if (result.IsCompletedSuccessfully)
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

                if (result.IsCompletedSuccessfully)
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
                if (result.IsCompletedSuccessfully)
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
                if (result.IsCompletedSuccessfully)
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
    }
}
