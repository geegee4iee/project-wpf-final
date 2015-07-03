using System;
using System.Collections.Generic;
using System.Linq;
using Seller.DTO.Entity;

namespace Seller.DAO
{
    class FixedRegisterDAO
    {
        public List<FixedRegister> GetAll()
        {
            using (SellerEntities ent = new SellerEntities())
            {
                return ent.FixedRegisters.Include("Product").Include("Customer").ToList();
            }
        }

        public Boolean Add(FixedRegister reg)
        {
            bool success = true;

            using (SellerEntities ent = new SellerEntities())
            {
                if (!ent.FixedRegisters.Where(c => c.ProId == reg.ProId && c.CusId == reg.CusId).Any())
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

        public void Remove(int cusId, int proId)
        {
            using (SellerEntities ent = new SellerEntities())
            {
                var reg = ent.FixedRegisters.Where(c => c.CusId == cusId && c.ProId == proId).FirstOrDefault();
                ent.FixedRegisters.Remove(reg);
                ent.SaveChanges();
            }
        }

        public void Update(int cusId, int proId, int quantity)
        {
            using (SellerEntities ent = new SellerEntities())
            {
                var reg = ent.FixedRegisters.Where(c => c.CusId == cusId && c.ProId == proId).FirstOrDefault();
                reg.Quantity = quantity;
                ent.SaveChanges();
            }
        }

        public decimal GetTotalValue(int cusId)
        {
            decimal total = 0;
            using (SellerEntities ent = new SellerEntities())
            {
                var lstProc = ent.FixedRegisters.Where(c => c.CusId == cusId).Select(c => new { c.Product, c.Quantity });

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

            using (SellerEntities ent = new SellerEntities())
            {
                var lstDis = ent.FixedRegisters.GroupBy(c => c.CusId).Select(c => c.Key).ToList();
                foreach (int cusId in lstDis)
                {
                    var dis = ent.Customers.Where(c => c.Id == cusId).FirstOrDefault();
                    DateTime currentDate = DateTime.Now;
                    DateTime firstOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

                    if (!dis.Orders.Where(c => c.DateOfIssue == firstOfMonth && c.IsFixed == 1).Any())
                    {
                        var lstReg = ent.FixedRegisters.Where(c => c.CusId == cusId).ToList();
                        decimal total = GetTotalValue(cusId);
                        Order ord = new Order
                        {
                            CusId = cusId,
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
