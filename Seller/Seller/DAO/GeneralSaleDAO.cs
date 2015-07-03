using Seller.DTO.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seller.DAO
{
    class GeneralSaleDAO
    {
        public void Add(GeneralSale sale)
        {
            using (SellerEntities ent = new SellerEntities())
            {
                var curSale = ent.GeneralSales.Where(c => c.ProId == sale.ProId && c.Year == sale.Year && c.Month == sale.Month).First();
                if (curSale != null)
                {
                    curSale.Quantity += sale.Quantity;
                    curSale.Amount += sale.Amount.Value;
                }
                else
                {
                    ent.GeneralSales.Add(sale);
                }

                ent.SaveChanges();
            }
        }
    }
}
