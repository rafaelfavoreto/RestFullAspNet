using RestFullAspNet.Model;
using RestFullAspNet.Model.Base;
using System.Collections.Generic;

namespace RestFullAspNet.Repository
{ 
        public interface IRepository<T> where T : BaseEntity
        {
            T Create(T item);
            T FindByID(long id);
            List<T> FindAll();
            T Update(T item);
            void Delete(long id);
            bool Exists(long id);   
            List<T> FindWithPageSearch(string query);
            int GetCount(string query);

        }
    }

