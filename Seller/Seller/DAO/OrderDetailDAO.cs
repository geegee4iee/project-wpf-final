using System.Collections.Generic;
using System.Linq;
using Seller.DTO.Entity;

namespace Seller.DAO
{
    class OrderDetailDAO
    {
        public List<OrderDetail> GetAll(int ordId)
        {
            using (SellerEntities ent = new SellerEntities())
            {
                return ent.OrderDetails.Include("Product").Where(c => c.OrderId == ordId).ToList();
            }
        }
    }
}
