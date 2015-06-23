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

        public Debt Get(int distrId, int year)
        {
            Debt debt = null;

            using (UnileverEntities ent = new UnileverEntities())
            {
                debt = ent.Debts.Where(c => c.DistributorId == distrId && c.Year == year).FirstOrDefault();
            }

            return debt;
        }

        public Boolean Remove(int distrId, int year)
        {
            bool success = true;

            using (UnileverEntities ent = new UnileverEntities())
            {
                Debt curr = ent.Debts.Where(c => c.Year == year && c.DistributorId == distrId).First();

                if (curr != null)
                {
                    ent.Debts.Remove(curr);
                    ent.SaveChanges();
                }
                else
                {
                    success = false;
                }
            }

            return success;
        }

        public decimal GetCurrentDebt(int orderId)
        {
            using (UnileverEntities ent = new UnileverEntities())
            {
                Order ord = ent.Orders.Where(c => c.Id == orderId).FirstOrDefault();

                return ord.Remainder.Value;
            }
        }

        public decimal GetTotalDebts(int distrId)
        {
            decimal totalDebt = 0;

            using (UnileverEntities ent = new UnileverEntities())
            {
                List<Order> lstOrd = ent.Orders.Where(c => c.DistributorId == distrId).ToList();


                foreach (Order ord in lstOrd)
                {
                    totalDebt += ord.Remainder.Value;
                }

                return totalDebt;
            }
        }


    }
}
