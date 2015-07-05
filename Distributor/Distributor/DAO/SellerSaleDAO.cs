using Distributor.DTO.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distributor.DAO
{
    class SellerSaleDAO
    {
        public void Add(SellerSale sale)
        {
            using (DistributorEntities ent = new DistributorEntities())
            {
                var curSale = ent.SellerSales.Where(
                    c => 
                        c.SellerId == sale.SellerId && 
                        c.Year == sale.Year && 
                        c.ProId == sale.ProId && 
                        c.Month == sale.Month).FirstOrDefault();
                if (curSale == null)
                {
                    ent.SellerSales.Add(sale);
                }
                else
                {
                    curSale.Quantity += sale.Quantity.Value;
                    curSale.Sales += sale.Sales.Value;
                }

                ent.SaveChanges();
            }
        }
    }
}
