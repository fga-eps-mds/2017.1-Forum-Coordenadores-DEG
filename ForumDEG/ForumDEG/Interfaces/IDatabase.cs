using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumDEG.Interfaces {
    public interface IDatabase<T> {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task<int> Save(T item);
        Task<int> Delete(T item);
    }
}
