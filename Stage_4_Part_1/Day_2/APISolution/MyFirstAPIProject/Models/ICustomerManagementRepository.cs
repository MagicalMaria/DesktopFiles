using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstAPIProject.Models
{
    public interface ICustomerManagementRepository<TEntity>
    {
        public IEnumerable<TEntity> GetAllData();
        public TEntity GetById(int id);
        public void Add(TEntity entity);
        public void Update(int id, TEntity entity);
        public void Delete(int id);
    }
}
