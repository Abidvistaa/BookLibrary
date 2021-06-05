using BookLibrary.DataAccess.Data;
using BookLibrary.DataAccess.Repository.IRepository;
using BookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.DataAccess.Repository
{
    public class OrderDetailsRepository: Repository<OrderDetails>,IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderDetailsRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(OrderDetails obj)
        {
            _db.Update(obj);
            
        }
    }
}
