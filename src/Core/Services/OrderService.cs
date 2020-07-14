using Core.Dtos;
using Core.Models;
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Auth;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository repository;
        private readonly ICurrentRequestDataProvider currentRequestDataProvider;

        public OrderService(IRepository repository, ICurrentRequestDataProvider currentRequestDataProvider)
        {
            this.repository = repository;
            this.currentRequestDataProvider = currentRequestDataProvider;
        }

        public async Task<List<Order>> GetMyOrders()
        {
            var currentCustomer = await currentRequestDataProvider.GetCurrentRequestCustomer();
            return await this.repository.GetQuery<Order>(_=>_.CustomerId == currentCustomer.Id).ToListAsync();
        }

        public async Task<long> PlaceOrder(OrderRequest orderRequest)
        {
            var currentCustomer = await currentRequestDataProvider.GetCurrentRequestCustomer();

            var products = await this.repository.GetQuery<Product>(_ => orderRequest.ProductIds.Contains(_.Id)).ToListAsync();

            List<OrderItem> orderItems = products.Select(_ => new OrderItem() { ProductId = _.Id, Price = _.Price }).ToList();
            var totalPrice = orderItems.Sum(_ => _.Price);
            var order = new Order()
            {
                CustomerId = currentCustomer.Id,
                OrderItems = orderItems,
                OrderStatusId = (short)OrderStatuses.Pending,
                TotalPrice = totalPrice,
                Discount = 0,
                TotalPayable = totalPrice
            };

            await this.repository.Insert<Order>(order);
            await this.repository.Save();
            return order.Id;
        }
    }
}
