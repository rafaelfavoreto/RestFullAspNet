﻿using RestFullAspNet.Model;
using System.Collections.Generic;

namespace RestFullAspNet.Business
{
    public interface IBooksBusiness 
    {
        Books Create(Books books);
        Books FindByID(long id);
        Books Update(Books books);
        List<Books> FindAll();
        void Delete(long id);
    }
}
