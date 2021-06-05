using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibrary.DataAccess.Repository.IRepository
{
    public interface IUnitofWork:IDisposable
    {
        ICategoryRepository Category { get; }
        ICoverTypeRepository CoverType { get; }
        IProductRepository Product { get; }
        ICompanyRepository Company { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailsRepository OrderDetails { get; }
        ISP_Call SP_Call { get; }
        void Save();
    }
}
