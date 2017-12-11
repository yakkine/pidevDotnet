using Data.Infrastructure;
using Domain.Entity;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
   public class ReclamationService : Service<reclamation>
    {
        private static IDataBaseFactory dbf = new DataBaseFactory();
        private static IUnitOfWork uow = new UnitOfWork(dbf);

        public ReclamationService() : base(uow)
        {

        }
        public void CreateReclamation(reclamation c)
        {
            uow.getRepository<reclamation>().Add(c);
            uow.Commit();
        }
        public void DeleteReclamation(reclamation c)
        {
            uow.getRepository<reclamation>().Delete(c);
            uow.Commit();
        }
        public reclamation GetReclamationById(int idReclamation)
        {
          return  uow.getRepository<reclamation>().GetById(idReclamation);
           
        }
        public void UpdateReclamation(reclamation re)
        {
            uow.getRepository<reclamation>().Update(re);
            uow.Commit();
        }
    }
}