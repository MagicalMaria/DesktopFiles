using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildPipeEditDockerProject.Models
{
    public interface IRepository<T>
    {
        public IEnumerable<T> GetItems();
        public T GetItemById(int id);
        public void AddItem(T t);
    }
}
