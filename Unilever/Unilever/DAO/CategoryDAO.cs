using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unilever.DTO.Entity;

namespace Unilever.DAO
{
    class CategoryDAO
    {
        public List<Category> GetAll()
        {
            using (UnileverEntities entity = new UnileverEntities())
            {
                return entity.Categories.ToList();
            }
        }

        public bool Add(Category cat)
        {
            try
            {
                using (UnileverEntities entity = new UnileverEntities())
                {
                    entity.Categories.Add(cat);
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

            using (UnileverEntities entity = new UnileverEntities())
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
    }
}
