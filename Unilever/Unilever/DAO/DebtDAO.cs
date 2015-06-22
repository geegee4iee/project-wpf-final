using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unilever.DTO.Entity;

namespace Unilever.DAO
{
    class DebtDAO
    {
        public List<Debt> GetAll(int distributorId)
        {
            List<Debt> lst = new List<Debt>();

            using (UnileverEntities ent = new UnileverEntities())
            {
                lst = ent.Debts.Where(c => c.DistributorId == distributorId).ToList();
            }

            return lst;
        }
    }
}
