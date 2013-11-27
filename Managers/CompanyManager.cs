using Data.Domain;
using Managers.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Managers
{
    public class CompanyManager : GlobalManager
    {
        public IEnumerable<Company> GetAllCompanies()
        {
            return RepoGeneric.GetAll<Company>();
        }

        public Company GetById(int id)
        {
            return RepoGeneric.FindOne<Company>(c => c.CompanyId == id);
        }

        public IUnitOfWorkResult AddCompany(Company model)
        {
            var repo = RepoGeneric;

            repo.Add<Company>(model);

            return repo.UnitOfWork.SaveChanges();
        }

        public IUnitOfWorkResult EditCompany(Company model)
        {
            var repo = RepoGeneric;

            var company = repo.FindOne<Company>(c => c.CompanyId == model.CompanyId);

            if (company == null)
                throw new Exception("Company doesn't exist");

            company.Name = model.Name;
            company.Description = model.Description;

            return repo.UnitOfWork.SaveChanges();
        }

        public IUnitOfWorkResult DeleteCompany(int id)
        {
            var repo = RepoGeneric;
            var model = repo.FindOne<Company>(c => c.CompanyId == id);

            if (model == null)
                throw new Exception(string.Format("Can't find company with id {0}", id));

            var companyCars = repo.Find<Car>(c => c.CompanyId == model.CompanyId);
            foreach (var cc in companyCars)
                repo.Delete<Car>(cc);

            repo.Delete<Company>(model);

            return repo.UnitOfWork.SaveChanges();
        }
    }
}
