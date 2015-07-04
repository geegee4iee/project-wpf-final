using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Distributor.DTO.Entity;

namespace Distributor.DAO
{
    class IssueDAO
    {
        public List<Issue> GetAll()
        {
            List<Issue> lst = new List<Issue>();

            using (DistributorEntities ent = new DistributorEntities())
            {
                lst = ent.Issues.Include("Seller").ToList();
            }

            return lst;
        }

        public Issue GetById(int sellerId, DateTime doi)
        {
            Issue iss = null;

            using (DistributorEntities ent = new DistributorEntities())
            {
                iss = ent.Issues.Where(c => c.SellerId == sellerId && c.DateOfIssue == doi).FirstOrDefault();
            }

            return iss;
        }

        public void Add(Issue iss, List<IssueDetail> lstIssD)
        {
            using (DistributorEntities ent = new DistributorEntities())
            {
                ent.Issues.Add(iss);
                ent.SaveChanges();

                foreach (IssueDetail issD in lstIssD)
                {
                    ent.IssueDetails.Add(issD);
                    Product proc = ent.Products.Where(c => c.Id == issD.ProId).FirstOrDefault();
                    proc.Quantity -= issD.Quantity;
                    ent.SaveChanges();
                }
            }
        }

        public void UpdateRemainder(int ordId, decimal payment)
        {
            using (DistributorEntities ent = new DistributorEntities())
            {
                var ord = ent.Issues.Where(c => c.Id == ordId).FirstOrDefault();
                ent.SaveChanges();
            }
        }

        public decimal GetCurrentRemainder(int ordId)
        {
            using (DistributorEntities ent = new DistributorEntities())
            {
                decimal remainder;
                decimal interest = 0;
                var curDate = DateTime.Now;
                int curYear = curDate.Year;
                var curInterest = ent.InterestOfYears.Where(c => c.Id == curYear).FirstOrDefault();
                var lastPaid = ent.PaymentDetails.Where(c => c.IssueId == ordId).IssueByDescending(c => c.PayDate).FirstOrDefault();
                var ord = ent.Issues.Where(c => c.Id == ordId).FirstOrDefault();
                var dis = ord.Distributor;
                var initDate = ord.DateOfIssue;

                if (curInterest != null)
                {
                    interest = curInterest.Interest.Value / 365;
                }

                var lastPaidDate = lastPaid.PayDate;
                int days;
                var limitDate = initDate.Value.AddDays(dis.TimeLimit.Value);

                if (lastPaidDate <= limitDate)
                {
                    days = curDate.Subtract(limitDate).Days;
                }
                else
                {
                    days = curDate.Subtract(lastPaidDate).Days;
                }

                if ((curDate.Subtract(initDate.Value)).Days > dis.TimeLimit)
                {
                    remainder = lastPaid.Remainder.Value + (days * interest * lastPaid.Remainder.Value);
                }
                else
                {
                    remainder = lastPaid.Remainder.Value;
                }



                return decimal.Round(remainder);
            }
        }

        public void Remove(int ordId)
        {
            using (DistributorEntities ent = new DistributorEntities())
            {
                var ord = ent.Issues.Where(o => o.Id == ordId).FirstOrDefault();
                var lstOrdD = ord.IssueDetails.ToList();
                var lstPayM = ord.PaymentDetails.ToList();

                foreach (IssueDetail ordD in lstOrdD)
                {
                    ent.IssueDetails.Remove(ordD);
                }

                foreach (PaymentDetail payM in lstPayM)
                {
                    ent.PaymentDetails.Remove(payM);
                }

                ent.Issues.Remove(ord);
                ent.SaveChanges();
            }
        }
    }
}
