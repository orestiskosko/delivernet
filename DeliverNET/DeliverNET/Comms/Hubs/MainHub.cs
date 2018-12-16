﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverNET.Data;
using DeliverNET.Managers;
using DeliverNET.Managers.Interfaces;
using DeliverNET.Models;
using DeliverNET.Models.ProfileBusiViewModels;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace DeliverNET.Comms.Hubs
{

    public class MainHub : Hub
    {
        private IOrderManager _mngOrder;
        private IBusinessManager _mngBussiness;
        private IMasterManager _mngMaster;
        private IDelivererManager _mngDeliverer;
        private string GroupAvailableName = "AvailableDeliverers";


        public MainHub(
            IMasterManager mng)
        {
            _mngMaster = mng;
            _mngOrder = mng.GetOrderManager();
            _mngBussiness = mng.GetBusinessManager();
            _mngDeliverer = mng.GetDelivererManager();

        }
        // Invoke from client to add a deliverer to the group of the available deliverers.
        public async Task AddToGroupAvailable()
        {
            // Add to group
            _mngMaster.GetDelivererManager().SetWorkingStatus(Context.UserIdentifier, true);
            await Groups.AddToGroupAsync(Context.ConnectionId, GroupAvailableName);

        }

        //Invoke from client to remove a deliverer from the group of the available deliverers.
        public async Task RemoveFromGroupAvailable()
        {
            // Remove from group   
            _mngMaster.GetDelivererManager().SetWorkingStatus(Context.UserIdentifier, false);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, GroupAvailableName);
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
            
            // Get from the db the order id
            orderId = _mngOrder.Get(tstamp).Id;

            // create a signalr group and add this bussiness
            await Groups.AddToGroupAsync(Context.ConnectionId, orderId.ToString());

            //send the whole order back to the client invoked this method

            await Clients.Caller.SendAsync("GetOrderIdForAppend", orderId);
            //await Clients.Group(orderId.ToString()).SendAsync("AppendThisOrder", _mngOrder.Get(orderId));

            // broadcast to deliverers group the order and the order id in method newOrderAnnounce the name of the method that will be invoked in the client will be "NewOrder"
            await Clients.Group(GroupAvailableName).SendAsync("NewOrder", orderId, geolocation, order.PaymentTypeId, tstamp);

        }




        // Get specific order details from db (for deliverer)
        public async Task GetOrderFromDb(string orderId)
        {
            Order order = _mngOrder.Get(int.Parse(orderId));
            Business business = _mngBussiness.Get(order.BusinessId);
            await Clients.Caller.SendAsync("GetOrder", business, order);
        }

        // get specific order object (for business cashier)
        public async Task GetOrderFromDbBusi(string orderId)
        {
            Order order = _mngOrder.Get(int.Parse(orderId));
            await Clients.Caller.SendAsync("AppendNewOrder", order);
        }

        // Get all orders that are NOT timed out
        public async Task GetActiveOrders()
        {
            List<Order> orders = _mngOrder.GetActive();
            await Clients.Caller.SendAsync("GetActiveOrders", orders);
        }


        // Get deliverer working status
        public async Task GetDelivererWorkingStatus()
        {
            bool isWorking = _mngMaster.GetDelivererManager().GetWorkingStatus(Context.UserIdentifier);
            await Clients.Caller.SendAsync("GetWorkingStatus", isWorking);
        }


        //
        //accept pickedup delivered functions
        //

        // invokes from client site when a deliverer acceptes the order
        public void OrderAccepted(string orderId)
        {

            _mngOrder.SetIsAccepted(int.Parse(orderId), true);  //change status to db to IsAccepted=true
            _mngOrder.SetAcceptedTime(int.Parse(orderId), DateTime.Now); //add the current time to the property acceptedtime in the db for this order
            _mngDeliverer.SetWorkingStatus(Context.ConnectionId, false); // is working to db change to false
            _mngDeliverer.SetDeliveringStatus(Context.ConnectionId, true); //is delivering db change to true
            Groups.RemoveFromGroupAsync(Context.ConnectionId, "AvailableDeliverers");  //remove from group availabledeliverers the context.useridentifier deliverer 
            Groups.AddToGroupAsync(Context.ConnectionId, orderId); // add to group with name the order id the deliverer that accepted the order
            Clients.Group(GroupAvailableName).SendAsync("AnOrderIsAccepted", orderId); //Broadcast to available deliverers the order is taken=>invoke function AnOrderIsAccepted
            Clients.Group(Convert.ToString(orderId)).SendAsync("OrderAcceptedStatus", Context.UserIdentifier, orderId);// invoke to grouped business to tell that the order is accepted

        }

        // invokes from client site when a deliverer PickesUp the order
        public void OrderPickedUp(string orderId)
        {
            _mngOrder.SetPickupTime(int.Parse(orderId), DateTime.Now);//add the current time to the property Pickeduptime in the db for this order
            _mngOrder.SetIsPickedUp(int.Parse(orderId), true); //change status to db to IsPickedUp=true
            Clients.Group(Convert.ToString(orderId)).SendAsync("OrderPickedUpStatus", Context.UserIdentifier, orderId);// invoke OrderPickedUpStatus with order id to grouped businness
        }

        // invokes from client site when a deliverer Finally delivers the order
        public void OrderDelivered(string orderId)
        {
            _mngOrder.SetDeliveredTime(int.Parse(orderId), DateTime.Now); //add the current time to the property isdelivered in the db for this 
            Clients.Group(Convert.ToString(orderId)).SendAsync("OrderDeliveredStatus", Context.UserIdentifier, orderId);// invoke OrderPickedUpStatus with order id to grouped businness
            _mngOrder.SetIsDelivered(int.Parse(orderId), true); //change status to db to IsDelivered=true
            _mngDeliverer.SetDeliveringStatus(Context.ConnectionId, false); //Change deliverer status IsDelivering to false
            Groups.RemoveFromGroupAsync(Context.ConnectionId, orderId); //remove from group the deliverer
            //TODO: remove cashier from group name of orderId

            //TODO: decide if you want to add the deliverer again to group with available deliverers

        }

        
        // This is called only by Cashier users
        public void RemoveFromOrderGroup(string orderId)
        {
            Groups.RemoveFromGroupAsync(Context.ConnectionId, orderId);
        }


        //
        // Order checks
        //
        // broadcasts if order is timed out
        public async Task CheckOrderTimeout(string orderId)
        {
            Order order = _mngOrder.Get(int.Parse(orderId));
            await Clients.Caller.SendAsync("CheckOrderTimeout", orderId, order.Tstamp, order.IsTimedOut);
        }

        // broadcasts if order is accepted
        public async Task CheckOrderAccepted(string orderId)
        {
            Order order = _mngOrder.Get(int.Parse(orderId));
            await Clients.Caller.SendAsync("CheckOrderAccepted", orderId, order.IsAccepted);
        }

        // broadcasts if order is pickedup
        public async Task CheckOrderPickedup(string orderId)
        {
            Order order = _mngOrder.Get(int.Parse(orderId));
            await Clients.Caller.SendAsync("CheckOrderPickedup", orderId, order.IsPickedup);
        }

        // broadcasts if order is delivered
        public async Task CheckOrderDelivered(string orderId)
        {
            Order order = _mngOrder.Get(int.Parse(orderId));
            await Clients.Caller.SendAsync("CheckOrderDelivered", orderId, order.IsDelivered);
        }
    }
}
