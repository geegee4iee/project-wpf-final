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

        public Boolean IsExist(Issue iss)
        {
            using (DistributorEntities ent = new DistributorEntities())
            {
                return ent.Issues.Where(c => c.SellerId == iss.SellerId && c.DateOfIssue == iss.DateOfIssue).Any();
            }
        }

        public void Remove(Issue iss)
        {
            using (DistributorEntities ent = new DistributorEntities())
            {
                var curIss = ent.Issues.Where(c => c.SellerId == iss.SellerId && c.DateOfIssue == iss.DateOfIssue).FirstOrDefault();
                var lstCurIssDe = ent.IssueDetails.Where(c => c.SellerId == iss.SellerId && c.DateOfIssue == iss.DateOfIssue).ToList();
                foreach (var curIssD in lstCurIssDe)
                {
                    ent.IssueDetails.Remove(curIssD);
                }

                ent.Issues.Remove(curIss);
                ent.SaveChanges();
            }
        }

        public void Add(Issue iss, List<IssueDetail> lstIssD)
        {
            using (DistributorEntities ent = new DistributorEntities())
            {
                ent.Issues.Add(iss);
                ent.SaveChanges();

                var lstRemainIssue = ent.IssueDetails.Where(c => c.SellerId == iss.SellerId && c.Remainder != 0).ToList();

                foreach (IssueDetail issD in lstIssD)
                {
                    issD.SellerId = iss.SellerId;
                    issD.DateOfIssue = iss.DateOfIssue;
                    ent.IssueDetails.Add(issD);
                    Product proc = ent.Products.Where(c => c.Id == issD.ProId).FirstOrDefault();
                    proc.Quantity -= issD.Quantity;
                    ent.SaveChanges();
                }

                

                foreach (IssueDetail issD in lstRemainIssue)
                {
                    var tempIss = ent.IssueDetails.Where(c => c.SellerId == iss.SellerId && c.ProId == issD.ProId && c.DateOfIssue == iss.DateOfIssue).FirstOrDefault();
                    if (tempIss != null)
                    {
                        tempIss.Quantity += issD.Remainder.Value;
                        tempIss.Remainder += issD.Remainder.Value;
                        tempIss.Amount += issD.Remainder.Value * issD.Product.Price.Value;

                        ent.SaveChanges();
                    }
                    else
                    {
                        IssueDetail newIssue = new IssueDetail
                        {
                            SellerId = iss.SellerId,
                            ProId = issD.ProId,
                            DateOfIssue = iss.DateOfIssue,
                            Quantity = issD.Remainder,
                            Remainder = issD.Remainder,
                            Amount = issD.Remainder.Value * issD.Product.Price.Value
                        };
                        ent.IssueDetails.Add(newIssue);
                        ent.SaveChanges();
                    }
                }
            }
        }
    }
}
