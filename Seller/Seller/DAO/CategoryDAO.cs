using System.Collections.Generic;
using System.Linq;
using Seller.DTO.Entity;

namespace Seller.DAO
{
    class CategoryDAO
    {
        public List<Category> GetAll()
        {
            using (SellerEntities entity = new SellerEntities())
            {
                return entity.Categories.ToList();
            }
        }

        public bool IsExistedName(Category cat)
        {
            bool flag = true;

            using (SellerEntities ent = new SellerEntities())
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
                    using (SellerEntities entity = new SellerEntities())
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

            using (SellerEntities entity = new SellerEntities())
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
                using (SellerEntities entity = new SellerEntities())
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
