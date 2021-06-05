using BookLibrary.DataAccess.Data;
using BookLibrary.DataAccess.Repository.IRepository;
using BookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.DataAccess.Repository
{
    public class ShopppingCartRepository: Repository<ShoppingCart>,IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ShopppingCartRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(ShoppingCart obj)
        {
            _db.Update(obj);

        }
    }
}
