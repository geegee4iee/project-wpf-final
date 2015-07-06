using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Distributor.DTO.Entity;

namespace Distributor.DAO
{
    class StockDAO
    {
        public List<Stock> GetAll()
        {
            using (DistributorEntities entity = new DistributorEntities())
            {
                return entity.Stocks.ToList();
            }
        }

        public bool Add(Stock st)
        {
            try
            {
                using (DistributorEntities entity = new DistributorEntities())
                {
                    entity.Stocks.Add(st);
                    entity.SaveChanges();
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool Remove(int distributorId, int proId, int year, int month)
        {
            bool flag = true;

            using (DistributorEntities entity = new DistributorEntities())
            {
                var st = entity.Stocks.Where(s => 
                    s.ProId == proId
                    && s.Year == year
                    && s.Month == month)
                    .FirstOrDefault();
                if (st != null)
                {
                    try
                    {
                        entity.Stocks.Remove(st);
                        entity.SaveChanges();
                    }
                    catch (System.Exception ex)
                    {
                        ex.Message.ToString();
                        return false;
                    }
                }
                else
                {
                    flag = false;
                }
            }

            return flag;
        }
        public bool UpdateInfo(Stock st)
        {
            bool flag = true;

            using (DistributorEntities entity = new DistributorEntities())
            {
                var stData = entity.Stocks.Where(s =>  s.ProId == st.ProId
                    && s.Year == st.Year
                    && s.Month == st.Month)
                    .FirstOrDefault();
                if (stData != null)
                {
                    stData.Quantity = st.Quantity;
                   
                    entity.SaveChanges();
                }
                else
                {
                    flag = false;
                }
            }

            return flag;
        }

        public void AutoAdd()
        {
            int curMonth = DateTime.Today.Month;
            int curYear = DateTime.Today.Year;

            using (DistributorEntities ent = new DistributorEntities())
            {
                var lstProcs = ent.Products.ToList();
                foreach (var proc in lstProcs)
                {
                    int totalSales = 0;
                    totalSales += ent.Sales.Where(c => c.ProId == proc.Id && c.Year == curYear && c.Month == curMonth).Sum(c => c.Quantity.Value);
                    Stock stk = ent.Stocks.Where(c => c.ProId == proc.Id && c.Month == curMonth && c.Year == curYear).FirstOrDefault();
                    if (stk == null)
                    {
                        stk = new Stock
                        {
                            ProId = proc.Id,
                            Month = curMonth,
                            Year = curYear,
                            Quantity = proc.Quantity.Value - totalSales
                        };

                        ent.Stocks.Add(stk);
                    }
                    else
                    {
                        stk.Quantity = proc.Quantity.Value - totalSales;
                    }

                    ent.SaveChanges();
                }
            }
        }
    }
}
