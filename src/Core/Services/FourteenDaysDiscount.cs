using Core.Dtos;
using Core.Models;
using Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class FourteenDaysDiscount : IDiscountService
    {
        private readonly IRepository repository;

        public FourteenDaysDiscount(IRepository repository)
        {
            this.repository = repository;
        }
        public async Task<decimal> GetDiscount(long customerId)
        {
            DateTime start = DateTime.UtcNow.AddDays(-14);
            DateTime end = DateTime.UtcNow;

            var totalPurchaseWithinFourteenDays = await this.repository.GetQuery<Order>(_ => _.PaidTime >= start
            && _.OrderStatusId == (short)OrderStatuses.Paid).ToListAsync();
            var totalAmount = totalPurchaseWithinFourteenDays.Sum(_ => _.TotalPayable);
            return totalPurchaseWithinFourteenDays.Count < 2 && totalAmount >= 10000m ? 7.0M : 0.0M;
        }
    }
}
