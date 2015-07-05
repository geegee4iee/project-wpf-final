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
    }
}
