using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seller.DTO.Entity;

namespace Seller.DAO
{
    class PaymentDetailDAO
    {
        public void Add(PaymentDetail pay)
        {
            using (SellerEntities ent = new SellerEntities())
            {
                ent.PaymentDetails.Add(pay);

                var ord = ent.Orders.Where(c => c.Id == pay.OrderId).FirstOrDefault();
                ord.Remainder = pay.Remainder;
                ord.Payment = pay.Paid;

                ent.SaveChanges();
            }
        }
    }
}
