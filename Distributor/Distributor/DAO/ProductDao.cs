using QLBH_MVC.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Distributor.DTO.Entity;

namespace Distributor.DAO
{
    class ProductDao
    {
        public List<Product> GetAll()
        {
            using (DistributorEntities entity = new DistributorEntities())
            {
                return entity.Products.ToList();
            }
        }
        public Product GetById(int Id)
        {
            using (DistributorEntities entity = new DistributorEntities())
            {
                return entity.Products.Include("Category").Where(p => p.Id == Id).FirstOrDefault();
            }
        }

        public bool Add(Product pro)
        {
            try
            {
                using (DistributorEntities entity = new DistributorEntities())
                {
                    entity.Products.Add(pro);
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

            using (DistributorEntities entity = new DistributorEntities())
            {
                var pro = entity.Products.Where(p => p.Id == id).FirstOrDefault();
                if (pro != null)
                {
                    try
                    {
                        entity.Products.Remove(pro);
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
        public bool UpdateInfo(Product pro)
        {
            bool flag = true;

            using (DistributorEntities entity = new DistributorEntities())
            {
                var proData = entity.Products.Where(p => p.Id == pro.Id).FirstOrDefault();
                if (proData != null)
                {
                    proData.Name = pro.Name;
                    proData.Price = pro.Price;
                    proData.Quantity = pro.Quantity;
                    proData.Sale = pro.Sale;
                    proData.Description = pro.Description;
                    proData.CatId = pro.CatId;
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
