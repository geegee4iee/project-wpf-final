using System.Collections.Generic;
using System.Linq;
using Seller.DTO.Entity;

namespace Unilever.DAO
{
    class InterestOfYearDAO
    {
        public List<InterestOfYear> GetAll()
        {
            using (SellerEntities entity = new SellerEntities())
            {
                return entity.InterestOfYears.ToList();
            }
        }

        public bool Add(InterestOfYear ioy)
        {
            try
            {
                using (SellerEntities entity = new SellerEntities())
                {
                    entity.InterestOfYears.Add(ioy);
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

        public bool Remove(int id)
        {
            bool flag = true;

            using (SellerEntities entity = new SellerEntities())
            {
                var ioy = entity.InterestOfYears.Where(c => c.Id == id).FirstOrDefault();
                if (ioy != null)
                {
                    try
                    {
                        entity.InterestOfYears.Remove(ioy);
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

        public bool Update(InterestOfYear ioy)
        {
            bool flag = true;

            using (SellerEntities entity = new SellerEntities())
            {
                var ioyData = entity.InterestOfYears.Where(i => i.Id == ioy.Id).FirstOrDefault();
                if(ioyData != null)
                {
                    ioyData.Interest = ioy.Interest;
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
