using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unilever.DTO.Entity;

namespace Unilever.DAO
{
    class OrderDetailDAO
    {
        public List<OrderDetail> GetAll(int ordId)
        {
            using (UnileverEntities ent = new UnileverEntities())
            {
                return ent.OrderDetails.Include("Product").Where(c => c.OrderId == ordId).ToList();
            }
        }
    }
}
