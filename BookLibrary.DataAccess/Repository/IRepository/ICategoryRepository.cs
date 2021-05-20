using BookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibrary.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository: IRepository<Category>
    {
        void Update(Category category);
    }
}
