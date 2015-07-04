using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Distributor.DTO.Entity;

namespace Distributor.DAO
{
    class DebtDAO
    {
        public List<Debt> GetAll(int SellerId)
        {
            List<Debt> lst = new List<Debt>();

            using (DistributorEntities ent = new DistributorEntities())
            {
                lst = ent.Debts.Where(c => c.SellerId == SellerId).ToList();
            }

            return lst;
        }

        public Debt Get(int distrId, int year)
        {
            Debt debt = null;

            using (DistributorEntities ent = new DistributorEntities())
            {
                debt = ent.Debts.Where(c => c.SellerId == distrId && c.Year == year).FirstOrDefault();
            }

            return debt;
        }

        public Boolean Remove(int distrId, int year)
        {
            bool success = true;

            using (DistributorEntities ent = new DistributorEntities())
            {
                Debt curr = ent.Debts.Where(c => c.Year == year && c.SellerId == distrId).First();

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

        public void Update(int distrId, int year, int month, decimal debtValue)
        {
            using (DistributorEntities ent = new DistributorEntities())
            {
                var debt = ent.Debts.Where(c => c.Year == year && c.SellerId == distrId).FirstOrDefault();

                switch (month)
                {
                    case 1: debt.Month1 = debtValue;
                        break;
                    case 2: debt.Month2 = debtValue;
                        break;
                    case 3: debt.Month3 = debtValue;
                        break;
                    case 4: debt.Month4 = debtValue;
                        break;
                    case 5: debt.Month5 = debtValue;
                        break;
                    case 6: debt.Month6 = debtValue;
                        break;
                    case 7: debt.Month7 = debtValue;
                        break;
                    case 8: debt.Month8 = debtValue;
                        break;
                    case 9: debt.Month9 = debtValue;
                        break;
                    case 10: debt.Month10 = debtValue;
                        break;
                    case 11: debt.Month11 = debtValue;
                        break;
                    case 12: debt.Month12 = debtValue;
                        break;
                    default: break;
                }

                ent.SaveChanges();
            }
        }

        public decimal GetCurrentDebt(int orderId)
        {
            using (DistributorEntities ent = new DistributorEntities())
            {
                Order ord = ent.Orders.Where(c => c.Id == orderId).FirstOrDefault();

                return ord.Remainder.Value;
            }
        }

        public decimal GetTotalDebts(int distrId)
        {
            decimal totalDebt = 0;

            using (DistributorEntities ent = new DistributorEntities())
            {
                List<Order> lstOrd = ent.Orders.Where(c => c.SellerId == distrId).ToList();


                foreach (Order ord in lstOrd)
                {
                    totalDebt += ord.Remainder.Value;
                }

                return totalDebt;
            }
        }

        public Boolean Add(int distrId, int year)
        {
            Boolean success = true;

            using (DistributorEntities ent = new DistributorEntities())
            {
                var debt = ent.Debts.Where(c => c.SellerId == distrId && c.Year == year).First();
                if (debt == null)
                {
                    ent.Debts.Add(new Debt { Year = year, SellerId = distrId });
                    ent.SaveChanges();
                }
                else
                {
                    success = false;
                }
            }

            return success;
        }

        public Boolean UpdateMonth(int sellerId, int year, decimal debtVal, int month)
        {
            Boolean success = true;

            using (DistributorEntities ent = new DistributorEntities())
            {
                var debt = ent.Debts.Where(c => c.SellerId == sellerId && c.Year == year).First();

                if (debt != null)
                {
                    switch (month)
                    {
                        case 1: debt.Month1 = debtVal;
                            break;
                        case 2: debt.Month2 = debtVal;
                            break;
                        case 3: debt.Month3 = debtVal;
                            break;
                        case 4: debt.Month4 = debtVal;
                            break;
                        case 5: debt.Month5 = debtVal;
                            break;
                        case 6: debt.Month6 = debtVal;
                            break;
                        case 7: debt.Month7 = debtVal;
                            break;
                        case 8: debt.Month8 = debtVal;
                            break;
                        case 9: debt.Month9 = debtVal;
                            break;
                        case 10: debt.Month10 = debtVal;
                            break;
                        case 11: debt.Month11 = debtVal;
                            break;
                        case 12: debt.Month12 = debtVal;
                            break;
                    }
                }
                else
                {
                    success = false;
                }


                ent.SaveChanges();
            }

            return success;
        }

        public void AutoAdd()
        {
            int curYear = DateTime.Now.Year;

            using (DistributorEntities ent = new DistributorEntities())
            {
                var lstSel = ent.Sellers.ToList();
                foreach (var sel in lstSel)
                {
                    if (!sel.Debts.Where(c => c.Year == curYear).Any())
                    {
                        Debt debt = new Debt
                        {
                            SellerId = sel.Id,
                            Year = curYear
                        };

                        ent.Debts.Add(debt);
                    }
                    else { }
                }

                ent.SaveChanges();
            }
        }

        public void AutoUpdate()
        {
            int curMonth = DateTime.Now.Month;
            int curYear = DateTime.Now.Year;

            using (DistributorEntities ent = new DistributorEntities())
            {
                var lstSel = ent.Sellers.ToList();
                foreach (var dis in lstSel)
                {
                    var lstOrd = dis.Orders.Where(c => c.Remainder != 0).Select(c => c.Id).ToList();
                    decimal totalDebt = 0;
                    foreach (var ordId in lstOrd)
                    {
                        totalDebt += new OrderDAO().GetCurrentRemainder(ordId);
                    }

                    Update(dis.Id, curYear, curMonth, totalDebt);
                }
            }
        }
    }
}
