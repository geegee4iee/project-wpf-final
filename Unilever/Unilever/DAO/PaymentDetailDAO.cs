using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unilever.DTO.Entity;

namespace Unilever.DAO
{
    class PaymentDetailDAO
    {
        public void Add(PaymentDetail pay)
        {
            using (UnileverEntities ent = new UnileverEntities())
            {
                ent.PaymentDetails.Add(pay);
                ent.SaveChanges();
            }
        }
    }
}
