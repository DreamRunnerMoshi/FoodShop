using Core.Dtos;
using Core.Models;
using Core.Repository;
using System.Threading.Tasks;

namespace Core.Services
{
    public class FirstPurchaseDiscount : IDiscountService
    {
        private readonly IRepository repository;

        public FirstPurchaseDiscount(IRepository repository)
        {
            this.repository = repository;
        }
        public async Task<decimal> GetDiscount(long customerId)
        {
            var isFirstPurchase = await this.repository.Any<Order>(_ => _.CustomerId == customerId
            && _.OrderStatusId == (short)OrderStatuses.Paid);
            return !isFirstPurchase ? 5.0M : 0.0M;
        }
    }
}
