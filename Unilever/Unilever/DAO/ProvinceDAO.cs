using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unilever.DTO.Entity;

namespace Unilever.DAO
{
    class ProvinceDAO
    {
        public List<Province> GetAll()
        {
            using (UnileverEntities entity = new UnileverEntities())
            {
                return entity.Provinces.ToList();
            }
        }

        public bool IsExistedName(Province provine)
        {
            bool flag = true;

            using (UnileverEntities ent = new UnileverEntities())
            {
                var tempProvince = ent.Provinces.Where(i => i.ProvinceName == provine.ProvinceName).FirstOrDefault();

                if (tempProvince == null)
                {
                    flag = false;
                }
                else
                {

                }
            }

            return flag;
        }

        public bool Add(Province provine)
        {
            bool flag = true;

            if (IsExistedName(provine) == true)
            {
                flag = false;
            }
            else
            {
                try
                {
                    using (UnileverEntities entity = new UnileverEntities())
                    {
                        entity.Provinces.Add(provine);
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

            using (UnileverEntities entity = new UnileverEntities())
            {
                var provine = entity.Provinces.Where(c => c.Id == id).FirstOrDefault();
                if (provine != null)
                {
                    try
                    {
                        entity.Provinces.Remove(provine);
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

        public bool Update(Province provine)
        {
            bool flag = true;

            if (IsExistedName(provine) == true)
            {
                flag = false;
            }
            else
            {
                using (UnileverEntities entity = new UnileverEntities())
                {
                    var catData = entity.Provinces.Where(i => i.Id == provine.Id).FirstOrDefault();
                    if (catData != null)
                    {
                        catData.ProvinceName = provine.ProvinceName;
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
