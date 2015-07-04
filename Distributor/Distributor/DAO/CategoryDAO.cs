using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Distributor.DTO.Entity;

namespace Distributor.DAO
{
    class CategoryDAO
    {
        public List<Category> GetAll()
        {
            using (DistributorEntities entity = new DistributorEntities())
            {
                return entity.Categories.ToList();
            }
        }

        public bool IsExistedName(Category cat)
        {
            bool flag = true;

            using (DistributorEntities ent = new DistributorEntities())
            {
                var tempCat = ent.Categories.Where(i => i.Name == cat.Name).FirstOrDefault();
                
                if(tempCat == null)
                {
                    flag = false;
                }
                else
                {
                    
                }
            }

            return flag;
        }

        public bool Add(Category cat)
        {
            bool flag = true;

            if (IsExistedName(cat) == true)
            {
                flag = false;
            }
            else
            {
                try
                {
                    using (DistributorEntities entity = new DistributorEntities())
                    {
                        entity.Categories.Add(cat);
                        entity.SaveChanges();
                    }
                }
                catch (System.Exception ex)
                {
                    ex.Message.ToString();
                    flag = false;
                }
            }

            return flag;
        }

        public bool Remove(int id)
        {
            bool flag = true;

            using (DistributorEntities entity = new DistributorEntities())
            {
                var cat = entity.Categories.Where(c => c.Id == id).FirstOrDefault();
                if (cat != null)
                {
                    try
                    {
                        entity.Categories.Remove(cat);
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

        public bool Update(Category cat)
        {
            bool flag = true;

            if (IsExistedName(cat) == true)
            {
                flag = false;
            }
            else
            {
                using (DistributorEntities entity = new DistributorEntities())
                {
                    var catData = entity.Categories.Where(i => i.Id == cat.Id).FirstOrDefault();
                    if (catData != null)
                    {
                        catData.Name = cat.Name;
                        entity.SaveChanges();
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }

            return flag;
        }
    }
}
