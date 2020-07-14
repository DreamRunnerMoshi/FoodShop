using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IDiscountCalculator
    {
        Task<decimal> GetDiscountPercentage(long customerId);
    }
}
