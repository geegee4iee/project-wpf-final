using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unilever.DTO.Entity;

namespace Unilever.DAO
{
    class OrderDAO
    {
        public List<Order> GetAll()
        {
            List<Order> lst = new List<Order>();

            using (UnileverEntities ent = new UnileverEntities())
            {
                lst = ent.Orders.Include("Distributor").ToList();
            }

            return lst;
        }

        public DbSet<Order> GetAllDbSet()
        {
            using (UnileverEntities ent = new UnileverEntities())
            {
                return ent.Orders;
            }
        }

        public Order GetById(int id)
        {
            Order ord = null;

            using (UnileverEntities ent = new UnileverEntities())
            {
                ord = ent.Orders.Where(c => c.Id == id).FirstOrDefault();
            }

            return ord;
        }

        public void Add(Order ord,List<OrderDetail> lstOrdD)
        {
            using (UnileverEntities ent = new UnileverEntities())
            {
                Order tempOrd = new Order
                {
                    IsFixed = ord.IsFixed,
                    DateOfIssue = ord.DateOfIssue,
                    Payment = ord.Payment,
                    Remainder = ord.Remainder,
                    Total = ord.Total,
                    DistributorId = ord.DistributorId,
                };
                ent.Orders.Add(tempOrd);
                ent.SaveChanges();
                foreach (OrderDetail ordD in lstOrdD)
                {
                    OrderDetail tempOrdD = new OrderDetail{
                        OrderId = tempOrd.Id,
                        Price = ordD.Price,
                        ProId = ordD.ProId,
                        Quantity = ordD.Quantity,
                        Amount = ordD.Amount
                    };
                    ent.OrderDetails.Add(tempOrdD);
                    ent.SaveChanges();
                } 
            }
        }
    }
}
