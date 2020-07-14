using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class DiscountCalculator : IDiscountCalculator
    {
        private readonly List<IDiscountService> discountServices;
        public DiscountCalculator(FirstPurchaseDiscount firstPurchaseDiscount, FourteenDaysDiscount fourteenDaysDiscount)
        {
            discountServices = new List<IDiscountService>() { firstPurchaseDiscount, fourteenDaysDiscount };
        }
        public async Task<decimal> GetDiscountPercentage(long customerId)
        {
            decimal totalPercentage = 0.0m;
            foreach (var item in this.discountServices)
            {
                totalPercentage += await item.GetDiscount(customerId);
            }
            return totalPercentage;
        }
    }
}
