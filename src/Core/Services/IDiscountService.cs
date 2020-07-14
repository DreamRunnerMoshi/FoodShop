using Core.Models;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IDiscountService
    {
        Task<decimal> GetDiscount(long customerId);
    }
}
