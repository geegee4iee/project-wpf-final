using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unilever.DTO.Entity;

namespace Unilever.DAO
{
    public class DistributorDAO
    {
        public List<Distributor> GetAll()
        {
            using (UnileverEntities entity = new UnileverEntities())
            {
                return entity.Distributors.ToList();
            }
        }
        public bool Add(Distributor dist)
        {
            using (UnileverEntities entity = new UnileverEntities())
            {
                entity.Distributors.Add(dist);
                entity.SaveChanges();

                return true;
            }
        }

        public bool Remove(int id)
        {
            bool flag = true;

            using (UnileverEntities entity = new UnileverEntities())
            {
                var dist = entity.Distributors.Where(d => d.Id == id).FirstOrDefault();
                if (dist != null)
                {

                    entity.Distributors.Remove(dist);
                    entity.SaveChanges();
                }
                else
                {
                    flag = false;
                }
            }

            return flag;
        }

        public bool UpdateInfo(Distributor dist)
        {
            bool flag = true;

            using (UnileverEntities entity = new UnileverEntities())
            {
                var distData = entity.Distributors.Where(d => d.Id == dist.Id).FirstOrDefault();
                if (distData != null)
                {
                    distData.Name = dist.Name;
                    distData.Address = dist.Address;
                    distData.Email = dist.Email;
                    distData.Phone = dist.Phone;
                    distData.TimeLimit = dist.TimeLimit;
                    //distData.ProvinceId = dist.ProvinceId;
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
