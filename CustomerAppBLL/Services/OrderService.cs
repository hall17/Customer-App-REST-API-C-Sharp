using CustomerAppBLL.BusinessObjects;
using CustomerAppBLL.Converters;
using CustomerAppDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerAppBLL.Services
{
    public class OrderService : IOrderService
    {
        OrderConverter conv = new OrderConverter();
        private readonly DALFacade _facade;

        public OrderService(DALFacade facade)
        {
            _facade = facade;
        }
        public OrderBO Create(OrderBO order)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var orderEntity = uow.OrderRepository.Create(conv.Convert(order));
                uow.Complete();
                return conv.Convert(orderEntity);
            }
        }

        public OrderBO Delete(int Id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var orderEntity = uow.OrderRepository.Delete(Id);
                uow.Complete();
                return conv.Convert(orderEntity);
            }
        }

        public OrderBO Get(int Id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var orderEntity = uow.OrderRepository.Get(Id);
                orderEntity.Customer = uow.CustomerRepository.Get(orderEntity.CustomerId);
                return conv.Convert(orderEntity);
            }
        }

        public List<OrderBO> GetAll()
        {
            using (var uow = _facade.UnitOfWork)
            {
                return uow.OrderRepository.GetAll().Select(conv.Convert).ToList();
            }
        }

        public OrderBO Update(OrderBO order)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var orderEntity = uow.OrderRepository.Get(order.Id);
                if(orderEntity == null)
                {
                    throw new InvalidOperationException("Order not found");
                }
                orderEntity.DeliveryDate = order.DeliveryDate;
                orderEntity.OrderDate = order.OrderDate;
                orderEntity.CustomerId = order.CustomerId;
                uow.Complete();
                // BLL Choice
                orderEntity.Customer = uow.CustomerRepository.Get(orderEntity.CustomerId);
                return conv.Convert(orderEntity);
            }
        }
    }
}
