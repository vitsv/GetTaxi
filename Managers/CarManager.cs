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
    public class CarManager : GlobalManager
    {
        public IEnumerable<Car> GetAll()
        {
            return RepoGeneric.GetAll<Car>();
        }

        public IEnumerable<Car> GetForCompany(int companyId)
        {
            return RepoGeneric.Find<Car>(c => c.CompanyId == companyId);
        }

        public Car GetById(int id)
        {
            return RepoGeneric.FindOne<Car>(c => c.CarId == id);
        }

        public IUnitOfWorkResult Add(Car model)
        {
            var repo = RepoGeneric;

            repo.Add<Car>(model);

            return repo.UnitOfWork.SaveChanges();
        }

        public IUnitOfWorkResult Edit(Car model)
        {
            var repo = RepoGeneric;

            var record = repo.FindOne<Car>(c => c.CarId == model.CarId);

            if (record == null)
                throw new Exception("Car doesn't exist");

            record.CarNumber = model.CarNumber;
            record.Color = model.Color;
            record.DriverName = model.DriverName;
            record.Mark = model.Mark;
            record.Model = model.Model;
            record.NrOfSeats = model.NrOfSeats;

            return repo.UnitOfWork.SaveChanges();
        }

        public IUnitOfWorkResult Delete(int id)
        {
            var repo = RepoGeneric;
            var model = repo.FindOne<Car>(c => c.CarId == id);

            if (model == null)
                throw new Exception(string.Format("Can't find car with id {0}", id));


            repo.Delete<Car>(model);

            return repo.UnitOfWork.SaveChanges();
        }
    }
}
