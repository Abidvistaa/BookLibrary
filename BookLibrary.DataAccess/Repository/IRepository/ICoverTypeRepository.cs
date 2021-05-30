using BookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibrary.DataAccess.Repository.IRepository
{
    public interface ICoverTypeRepository: IRepository<CoverType>
    {
        void Update(CoverType coverType);
    }
}
