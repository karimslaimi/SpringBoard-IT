﻿using SpringBoard.Data.Infrastructure;
using SpringBoard.Domaine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpringBoard.Service
{
    public class ServiceCompteRendu : Service<CompteRendu>, IServiceCompteRendu
    {
        static IDatabaseFactory dbf = new DatabaseFactory();
        static IUnitOfWork utwk = new UnitOfWork(dbf);

        protected ServiceCompteRendu() : base(utwk)
        {
        }


        public async Task<Rapport> addRapportToCR(DateTime date, double value, string userId)
        {

            CompteRendu cr = await utwk.RepositoryCompteRendu.Get(x => x.date.Month == date.Month && x.Consultant.Id == userId);

            if (cr == null)
            {
                cr = new CompteRendu();
                cr.Consultant = await utwk.getRepository<Consultant>().Get(x => x.Id == userId);
                cr.date = new DateTime();
                cr.statut = false;
                await utwk.RepositoryCompteRendu.Add(cr);
                utwk.Commit();

            }


            Rapport rapport = new Rapport();
            rapport.CompteRendu = cr;
            rapport.date = date;
            rapport.valeur = value;
            await utwk.RepositoryRapport.Add(rapport);
            utwk.Commit();



            return rapport;
        }

        public async Task<bool> delete(int crid)
        {
            CompteRendu cr = await utwk.RepositoryCompteRendu.GetById(crid);
            if (cr != null)
            {
                await utwk.RepositoryRapport.Delete(x => x.CompteRendu == cr);
                await utwk.RepositoryCompteRendu.Delete(x => x.id == crid);
                utwk.Commit();
                return true;
            }
            else
            {
                return false;
            }

        }


        public async Task<CompteRendu> GetCRbyDateAndUser(DateTime date, string userId)
        {
            CompteRendu compteRendu = await utwk.RepositoryCompteRendu.Get(x => x.date.Month == date.Month && x.Consultant.Id == userId);
            compteRendu.Rapports = (ICollection<Rapport>)await utwk.RepositoryRapport.getMany(x => x.CompteRendu.id == compteRendu.id);
            return compteRendu;


        }

        public async Task<IEnumerable<CompteRendu>> getCRbyDate(DateTime date)
        {
            IEnumerable<CompteRendu> compteRendus = await utwk.RepositoryCompteRendu.getMany(x => x.date.Month == date.Month);

            foreach (CompteRendu cr in compteRendus)
            {
                cr.Rapports = (ICollection<Rapport>)await utwk.RepositoryRapport.getMany(x => x.CompteRendu.id == cr.id);
            }

            return compteRendus;
        }

        public async Task<IEnumerable<CompteRendu>> getUserCR(string userID)
        {
            IEnumerable<CompteRendu> compteRendus = await utwk.RepositoryCompteRendu.getMany(x => x.Consultant.Id == userID);

            foreach (CompteRendu cr in compteRendus)
            {
                cr.Rapports = (ICollection<Rapport>)await utwk.RepositoryRapport.getMany(x => x.CompteRendu.id == cr.id);
            }

            return compteRendus;
        }

        public async Task<CompteRendu> unlockCR(int crid)
        {
            CompteRendu cr = await utwk.RepositoryCompteRendu.GetById(crid);
            if (cr == null)
            {
                return null;
            }
            cr.statut = false;
            utwk.RepositoryCompteRendu.update(cr);
            utwk.Commit();
            return cr;


        }

        public async Task<CompteRendu> validateCR(int crid)
        {
            CompteRendu cr = await utwk.RepositoryCompteRendu.GetById(crid);
            if (cr == null)
            {
                return null;
            }

            cr.statut = true;
            cr.validation = new DateTime();
            utwk.RepositoryCompteRendu.update(cr);
            utwk.Commit();
            return cr;

        }
    }
}
