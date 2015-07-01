using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unilever.DTO.Entity;

namespace Unilever.DAO
{
    class FixedRegisterDAO
    {
        public List<FixedRegister> GetAll()
        {
            using (UnileverEntities ent = new UnileverEntities())
            {
                return ent.FixedRegisters.Include("Product").Include("Distributor").ToList();
            }
        }

        public Boolean Add(FixedRegister reg)
        {
            bool success = true;

            using (UnileverEntities ent = new UnileverEntities())
            {
                if (!ent.FixedRegisters.Where(c => c.ProId == reg.ProId && c.DistributorId == reg.DistributorId).Any())
                {
                    ent.FixedRegisters.Add(reg);
                    ent.SaveChanges();
                }
                else
                {
                    success = false;
                }
            }

            return success;
        }

        public void Remove(int disId, int proId)
        {
            using (UnileverEntities ent = new UnileverEntities())
            {
                var reg = ent.FixedRegisters.Where(c => c.DistributorId == disId && c.ProId == proId).FirstOrDefault();
                ent.FixedRegisters.Remove(reg);
                ent.SaveChanges();
            }
        }

        public void Update(int disId, int proId, int quantity)
        {
            using (UnileverEntities ent = new UnileverEntities())
            {
                var reg = ent.FixedRegisters.Where(c => c.DistributorId == disId && c.ProId == proId).FirstOrDefault();
                reg.Quantity = quantity;
                ent.SaveChanges();
            }
        }

        public decimal GetTotalValue(int disId)
        {
            decimal total = 0;
            using (UnileverEntities ent = new UnileverEntities())
            {
                var lstProc = ent.FixedRegisters.Where(c => c.DistributorId == disId).Select(c => new { c.Product, c.Quantity });

                foreach (var proc in lstProc)
                {
                    total += proc.Product.Price.Value * proc.Quantity.Value;
                }
            }

            return total;
        }

        public Boolean AddOrder()
        {
            bool hasOrder = false;

            using (UnileverEntities ent = new UnileverEntities())
            {
                var lstDis = ent.FixedRegisters.GroupBy(c => c.DistributorId).Select(c => c.Key).ToList();
                foreach (int disId in lstDis)
                {
                    var dis = ent.Distributors.Where(c => c.Id == disId).FirstOrDefault();
                    DateTime currentDate = DateTime.Now;
                    DateTime firstOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

                    if (!dis.Orders.Where(c => c.DateOfIssue == firstOfMonth && c.IsFixed == 1).Any())
                    {
                        var lstReg = ent.FixedRegisters.Where(c => c.DistributorId == disId).ToList();
                        decimal total = GetTotalValue(disId);
                        Order ord = new Order
                        {
                            DistributorId = disId,
                            DateOfIssue = firstOfMonth,
                            Total = total,
                            Payment = 0,
                            Remainder = total,
                            IsFixed = 1
                        };

                        List<OrderDetail> lstOrdD = new List<OrderDetail>();

                        foreach (var reg in lstReg)
                        {
                            OrderDetail ordD = new OrderDetail
                            {
                                ProId = reg.ProId,
                                Price = reg.Product.Price,
                                Quantity = reg.Quantity,
                                Amount = reg.Product.Price.Value * reg.Quantity.Value
                            };

                            lstOrdD.Add(ordD);
                        }

                        new OrderDAO().Add(ord, lstOrdD);
                        hasOrder = true;
                    }
                }
            }

            return hasOrder;
        }
    }
}
