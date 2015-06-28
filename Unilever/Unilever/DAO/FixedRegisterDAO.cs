using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unilever.DTO.Entity;

namespace Unilever.DAO
{
    class FixedRegisterDAO
    {
        public List<FixedRegister> GetAll()
        {
            using (UnileverEntities ent = new UnileverEntities())
            {
                return ent.FixedRegisters.Include("Product").Include("Distributor").ToList();
            }
        }

        public Boolean Add(FixedRegister reg)
        {
            bool success = true;

            using (UnileverEntities ent = new UnileverEntities())
            {
                if (!ent.FixedRegisters.Where(c => c.ProId == reg.ProId && c.DistributorId == reg.DistributorId).Any())
                {
                    ent.FixedRegisters.Add(reg);
                    ent.SaveChanges();
                }
                else
                {
                    success = false;
                }
            }

            return success;
        }

        public void Remove(int disId, int proId)
        {
            using (UnileverEntities ent = new UnileverEntities())
            {
                var reg = ent.FixedRegisters.Where(c => c.DistributorId == disId && c.ProId == proId).FirstOrDefault();
                ent.FixedRegisters.Remove(reg);
                ent.SaveChanges();
            }
        }

        public void Update(int disId, int proId, int quantity)
        {
            using (UnileverEntities ent = new UnileverEntities())
            {
                var reg = ent.FixedRegisters.Where(c => c.DistributorId == disId && c.ProId == proId).FirstOrDefault();
                reg.Quantity = quantity;
                ent.SaveChanges();
            }
        }
    }
}
