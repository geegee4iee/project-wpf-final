using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Distributor.DTO.Entity;

namespace Distributor.DAO
{
    class IssueDetailDAO
    {
        public List<IssueDetail> GetAll(int sellerId, DateTime dateOfIssue)
        {
            using (DistributorEntities ent = new DistributorEntities())
            {
                return ent.IssueDetails.Include("Product").Where(c => c.SellerId == sellerId && c.DateOfIssue == dateOfIssue).ToList();
            }
        }

        public void UpdateRemainder(IssueDetail issDetail, int remain)
        {
            using (DistributorEntities ent = new DistributorEntities())
            {
                var issCurDetail = ent.IssueDetails.Where(c => c.SellerId == issDetail.SellerId && c.ProId == issDetail.ProId && c.DateOfIssue == issDetail.DateOfIssue).FirstOrDefault();
                issCurDetail.Remainder -= remain;
                var issCur = ent.Issues.Where(c => c.SellerId == issCurDetail.SellerId && c.DateOfIssue == issCurDetail.DateOfIssue).FirstOrDefault();
                issCur.Debt -= remain * issCurDetail.Product.Price.Value;

                ent.SaveChanges();

                SellerSale sale = new SellerSale
                {
                    SellerId = issDetail.SellerId,
                    ProId = issDetail.ProId,
                    Month = issDetail.DateOfIssue.Month,
                    Year = issDetail.DateOfIssue.Year,
                    Quantity = remain,
                    Sales = remain * issCurDetail.Product.Price.Value
                };

                new SellerSaleDAO().Add(sale);
            }
        }

        public void ReturnToStock(IssueDetail issDetail)
        {
            using (DistributorEntities ent = new DistributorEntities())
            {
                var issCurDetail = ent.IssueDetails.Where(c => c.SellerId == issDetail.SellerId && c.ProId == issDetail.ProId && c.DateOfIssue == issDetail.DateOfIssue).FirstOrDefault();
                var product = ent.Products.Where(c => c.Id == issCurDetail.ProId).FirstOrDefault();
                var issCur = ent.Issues.Where(c => c.SellerId == issCurDetail.SellerId && c.DateOfIssue == issCurDetail.DateOfIssue).FirstOrDefault();
                
                issCur.Debt -= issCurDetail.Remainder.Value * issCurDetail.Product.Price.Value;
                product.Quantity += issCurDetail.Remainder.Value;
                issCurDetail.Remainder = 0;
                
                ent.SaveChanges();
            }
        }
    }
}
