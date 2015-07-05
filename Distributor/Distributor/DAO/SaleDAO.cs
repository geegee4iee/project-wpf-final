using Distributor.DTO.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distributor.DAO
{
    class SaleDAO
    {
        public void AutoAdd()
        {
            using (DistributorEntities ent = new DistributorEntities())
            {
                int curYear = DateTime.Today.Year;
                int curMonth = DateTime.Today.Month;
                var lstProc = ent.Products.ToList();
                foreach (var proc in lstProc)
                {
                    var curSale = ent.Sales.Where(c => c.ProId == proc.Id && c.Month == curMonth && c.Year == curYear).FirstOrDefault();
                    var lstSellerSale = ent.SellerSales.Where(c => c.ProId == proc.Id && c.Month == curMonth && c.Year == curYear).ToList();
                    int totalQuantity = lstSellerSale.Sum(c => c.Quantity.Value);
                    decimal totalSale = lstSellerSale.Sum(c => c.Sales.Value);
                    if (curSale != null)
                    {
                        curSale.Quantity = totalQuantity;
                        curSale.Sales = totalSale;
                    }
                    else
                    {
                        curSale = new Sale
                        {
                            ProId = proc.Id,
                            Year = curYear,
                            Month = curMonth,
                            Quantity = totalQuantity,
                            Sales = totalSale
                        };
                        ent.Sales.Add(curSale);
                    }

                    ent.SaveChanges();
                }
            }
        }
    }
}
