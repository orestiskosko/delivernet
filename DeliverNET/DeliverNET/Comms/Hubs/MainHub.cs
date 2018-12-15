using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverNET.Data;
using DeliverNET.Managers;
using DeliverNET.Managers.Interfaces;
using DeliverNET.Models;
using DeliverNET.Models.ProfileBusiViewModels;
using Microsoft.AspNetCore.SignalR;

namespace DeliverNET.Comms.Hubs
{

    public class MainHub : Hub
    {
        private IOrderManager _mngOrder;
        private IBusinessManager _mngBussiness;
        private IMasterManager _mngMaster;

        public MainHub(IMasterManager mng)
        {
            _mngMaster = mng;
            _mngOrder = mng.GetOrderManager();
            _mngBussiness = mng.GetBusinessManager();

        }
        // Invoke from client to add a deliverer to the group of the available deliverers.
        public async Task AddToGroupAvailable()
        {
            // Add to group
            _mngMaster.GetDelivererManager().SetWorkingStatus(Context.UserIdentifier, true);
            await Groups.AddToGroupAsync(Context.ConnectionId, "AvailableDeliverers");

        }

        //Invoke from client to remove a deliverer from the group of the available deliverers.
        public async Task RemoveFromGroupAvailable()
        {
            // Remove from group   
            _mngMaster.GetDelivererManager().SetWorkingStatus(Context.UserIdentifier, false);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "AvailableDeliverers");
        }


        // Invoke from Client busi in order to send a new order

        public async Task PlaceNewOrder(OrderBusiViewModel order)
        {
            DateTime tstamp = DateTime.Now;
            int orderId;
            string geolocation = _mngBussiness.Get(_mngMaster.GetBusinessCashierManager().Get(Context.UserIdentifier).BusinessId).Geolocation;

            Order NewOrder = new Order
            {
                Address = order.Address,
                DoorName = order.DoorName,
                FirstName = order.FirstName,
                FloorNo = order.FloorNo,
                LastName = order.LastName,
                PhoneNumber = order.PhoneNumber,
                PaymentTypeId = order.PaymentTypeId,
                Price = order.Price,
                Comments = order.Comments,

                Business = _mngBussiness.Get(_mngMaster.GetBusinessCashierManager().Get(Context.UserIdentifier).BusinessId),
                Cashier = _mngMaster.GetBusinessCashierManager().Get(Context.UserIdentifier),
                Tstamp = tstamp,
                Geolocation = geolocation,
                Tariff = 1,
                AcceptedTime = null,
                PickupTime = null,
                DeliveredTime = null,
                IsAccepted = false,
                IsPickedup = false,
                IsDelivered = false,
                IsTimedOut = false
            };

            _mngOrder.Create(NewOrder);

            //TODO: Get from the db the order id
            orderId = _mngOrder.Get(tstamp).Id;

            //TODO: create a signalr group and add this bussiness
            await Groups.AddToGroupAsync(Context.ConnectionId, orderId.ToString());


            //TODO: broadcast to delivers group the order and the order id in method newOrderAnnounce the name of the method that will be invoked in the client will be "NewOrder"
            await Clients.Group("AvailableDeliverers").SendAsync("NewOrder", orderId, geolocation, order.PaymentTypeId, tstamp);


        }

        //
        //accept pickedup delivered status change to db
        //


        // invoke by client deliverer when accepted
        public void OderAccepted(int orderid)
        {
            
        }

        // invoke by client deliverer when PickedUp
        public void OrderPickedUo(string orderid)
        {
           
        }

        // invoke by client deliverer when Delivered
        public void OrderDelivered(string orderid)
        {
          
        }



        // Get order details from db
        public async Task GetOrderFromDb(string orderId)
        {
            Order order = _mngOrder.Get(int.Parse(orderId));
            Business business = _mngBussiness.Get(order.BusinessId);
            await Clients.Group("AvailableDeliverers").SendAsync("GetOrder", business, order);
        }

        // Get all orders that are NOT timedout
        public async Task GetActiveOrders()
        {
            // TODO Write query to get all active orders
            List<Order> orders = _mngOrder.GetAll();
            await Clients.Group("AvailableDeliverers").SendAsync("GetActiveOrders", orders);
        }



    }

}
