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

        public void Add(Order ord, List<OrderDetail> lstOrdD)
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
                    OrderDetail tempOrdD = new OrderDetail
                    {
                        OrderId = tempOrd.Id,
                        Price = ordD.Price,
                        ProId = ordD.ProId,
                        Quantity = ordD.Quantity,
                        Amount = ordD.Amount
                    };
                    ent.OrderDetails.Add(tempOrdD);
                    Product proc = ent.Products.Where(c => c.Id == tempOrdD.ProId).FirstOrDefault();
                    proc.Quantity -= tempOrdD.Quantity;
                    ent.SaveChanges();
                }

                PaymentDetail paym = new PaymentDetail
                {
                    OrderId = tempOrd.Id,
                    PayDate = tempOrd.DateOfIssue.Value,
                    Paid = tempOrd.Payment,
                    Remainder = tempOrd.Remainder
                };

                ent.PaymentDetails.Add(paym);
                ent.SaveChanges();
            }
        }

        public void UpdateRemainder(int ordId, decimal payment)
        {
            using (UnileverEntities ent = new UnileverEntities())
            {
                var ord = ent.Orders.Where(c => c.Id == ordId).FirstOrDefault();
                ent.SaveChanges();
            }
        }

        public decimal GetCurrentRemainder(int ordId)
        {
            using (UnileverEntities ent = new UnileverEntities())
            {
                decimal remainder;
                decimal interest = 0;
                var curDate = DateTime.Now;
                int curYear = curDate.Year;
                var curInterest = ent.InterestOfYears.Where(c => c.Id == curYear).FirstOrDefault();
                var lastPaid = ent.PaymentDetails.Where(c => c.OrderId == ordId).OrderByDescending(c => c.PayDate).FirstOrDefault();
                var ord = ent.Orders.Where(c => c.Id == ordId).FirstOrDefault();
                var dis = ord.Distributor;
                var initDate = ord.DateOfIssue;

                if (curInterest != null)
                {
                    interest = curInterest.Interest.Value / 365;
                }

                var lastPaidDate = lastPaid.PayDate;
                int days = curDate.Subtract(lastPaidDate).Days;

                if ((curDate.Subtract(initDate.Value)).Days > dis.TimeLimit)
                {
                    remainder = lastPaid.Remainder.Value + (days * interest * lastPaid.Remainder.Value);
                }
                else
                {
                    remainder = lastPaid.Remainder.Value;
                }



                return remainder;
            }
        }

        public void Remove(int ordId)
        {
            using (UnileverEntities ent = new UnileverEntities())
            {
                var ord = ent.Orders.Where(o => o.Id == ordId).FirstOrDefault();
                var lstOrdD = ord.OrderDetails.ToList();
                var lstPayM = ord.PaymentDetails.ToList();

                foreach (OrderDetail ordD in lstOrdD)
                {
                    ent.OrderDetails.Remove(ordD);
                }

                foreach (PaymentDetail payM in lstPayM)
                {
                    ent.PaymentDetails.Remove(payM);
                }

                ent.Orders.Remove(ord);
                ent.SaveChanges();
            }
        }
    }
}
