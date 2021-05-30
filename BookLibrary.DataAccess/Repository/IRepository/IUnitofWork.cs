using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibrary.DataAccess.Repository.IRepository
{
    public interface IUnitofWork:IDisposable
    {
        ICategoryRepository Category { get; }
        ICoverTypeRepository CoverType { get; }
        ISP_Call SP_Call { get; }
        void Save();
    }
}
