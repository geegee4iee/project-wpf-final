using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unilever.DTO.Entity;

namespace Unilever.DAO
{
    class StockDAO
    {
        public List<Stock> GetAll()
        {
            using (UnileverEntities entity = new UnileverEntities())
            {
                return entity.Stocks.ToList();
            }
        }

        public bool Add(Stock st)
        {
            try
            {
                using (UnileverEntities entity = new UnileverEntities())
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

            using (UnileverEntities entity = new UnileverEntities())
            {
                var st = entity.Stocks.Where(s => s.DistributorId == distributorId 
                    && s.ProId == proId
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

            using (UnileverEntities entity = new UnileverEntities())
            {
                var stData = entity.Stocks.Where(s => s.DistributorId == st.DistributorId
                    && s.ProId == st.ProId
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
