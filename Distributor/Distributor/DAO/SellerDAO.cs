using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Distributor.DTO.Entity;

namespace Distributor.DAO
{
    public class DistributorDAO
    {
        public List<Seller> GetAll()
        {
            using (DistributorEntities entity = new DistributorEntities())
            {
                return entity.Sellers.ToList();
            }
        }
        public bool Add(Seller sel)
        {
            using (DistributorEntities entity = new DistributorEntities())
            {
                entity.Sellers.Add(sel);
                entity.SaveChanges();

                return true;
            }
        }

        public bool Remove(int id)
        {
            bool flag = true;

            using (DistributorEntities entity = new DistributorEntities())
            {
                var sel = entity.Sellers.Where(d => d.Id == id).FirstOrDefault();
                if (sel != null)
                {

                    entity.Sellers.Remove(sel);
                    entity.SaveChanges();
                }
                else
                {
                    flag = false;
                }
            }

            return flag;
        }

        public bool UpdateInfo(Seller sel)
        {
            bool flag = true;

            using (DistributorEntities entity = new DistributorEntities())
            {
                var selData = entity.Sellers.Where(d => d.Id == sel.Id).FirstOrDefault();
                if (selData != null)
                {
                    selData.Name = sel.Name;
                    selData.Address = sel.Address;
                    selData.Email = sel.Email;
                    //selData.ProvinceId = sel.ProvinceId;
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
