using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seller.DTO.Entity;

namespace Seller.DAO
{
    public class CustomerDAO
    {
        public List<Customer> GetAll()
        {
            using (SellerEntities entity = new SellerEntities())
            {
                return entity.Customers.ToList();
            }
        }
        public bool Add(Customer dist)
        {
            using (SellerEntities entity = new SellerEntities())
            {
                entity.Customers.Add(dist);
                entity.SaveChanges();

                return true;
            }
        }

        public bool Remove(int id)
        {
            bool flag = true;

            using (SellerEntities entity = new SellerEntities())
            {
                var dist = entity.Customers.Where(d => d.Id == id).FirstOrDefault();
                if (dist != null)
                {

                    entity.Customers.Remove(dist);
                    entity.SaveChanges();
                }
                else
                {
                    flag = false;
                }
            }

            return flag;
        }

        public bool UpdateInfo(Customer dist)
        {
            bool flag = true;

            using (SellerEntities entity = new SellerEntities())
            {
                var distData = entity.Customers.Where(d => d.Id == dist.Id).FirstOrDefault();
                if (distData != null)
                {
                    distData.Name = dist.Name;
                    distData.Address = dist.Address;
                    distData.Email = dist.Email;
                    distData.Phone = dist.Phone;
                    distData.TimeLimit = dist.TimeLimit;
                    //distData.ProvinceId = dist.ProvinceId;
                    distData.AllowDebt = distData.AllowDebt;
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
