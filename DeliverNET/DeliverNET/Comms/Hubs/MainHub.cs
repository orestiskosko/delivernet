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
        private IMasterManager _mngMasterManager;

        public MainHub(IMasterManager mng)
        {
            _mngMasterManager = mng;
            _mngOrder = mng.GetOrderManager();
            _mngBussiness = mng.GetBusinessManager();

        }
        //Invoke from client to add a deliverer to the group of the available deliverers.
        public void AddToGroupAvailable()
        {
            //TODO: Add to group

            Groups.AddToGroupAsync(Context.ConnectionId, "AvailableDeliverers");

        }

        //Invoke from client to remove a deliverer from the group of the available deliverers.
        public void RemoveFromGroupAvailable()
        {
            //TODO: Remove from group   
             Groups.RemoveFromGroupAsync(Context.ConnectionId, "AvailableDeliverers");
        }


        //Invoke from Client busi in order to send a new order

        public void PlaceNewOrder(OrderBusiViewModel order)
        {
            DateTime Tstamp = DateTime.Now;
            int orderId;
            string Geolocation= _mngBussiness.Get(_mngMasterManager.GetBusinessCashierManager().Get(Context.UserIdentifier).BusinessId).Geolocation;
            
            Order NewOrder = new Order();
            NewOrder.Address = order.Address;
            NewOrder.DoorName = order.DoorName;
            NewOrder.FirstName = order.FirstName;
            NewOrder.FloorNo = order.FloorNo;
            NewOrder.LastName = order.LastName;
            NewOrder.PhoneNumber = order.PhoneNumber;
            NewOrder.PaymentTypeId = order.PaymentTypeId;
            NewOrder.Price = order.Price;
            NewOrder.Comments = order.Comments;

            NewOrder.Business = _mngBussiness.Get(_mngMasterManager.GetBusinessCashierManager().Get(Context.UserIdentifier).BusinessId);
            NewOrder.Cashier = _mngMasterManager.GetBusinessCashierManager().Get(Context.UserIdentifier);
            NewOrder.Tstamp =Tstamp;
            NewOrder.Geolocation= Geolocation;
            NewOrder.Tariff = 1;
            NewOrder.AcceptedTime = null;
            NewOrder.PickupTime = null;
            NewOrder.DeliveredTime = null;
            NewOrder.IsAccepted = false;
            NewOrder.IsPickedup = false;
            NewOrder.IsDelivered = false;
            NewOrder.IsTimedOut = false;

            _mngOrder.Create(NewOrder);
            //TODO: Get from the db the order id
            orderId = _mngOrder.Get(Tstamp).Id;

            //TODO: create a signalr group and add this bussiness
            Groups.AddToGroupAsync(Context.ConnectionId,orderId.ToString());



            //TODO: broadcast to delivers group the order and the order id in method newOrderAnnounce the name of the method that will be invoked in the client will be "NewOrder"
             Clients.Group("AvailableDeliverers").SendAsync("NewOrder", orderId, Geolocation,order.PaymentTypeId,Tstamp);


        }


        // invoke by client deliverer when accepted
        public void Accepted(string orderid)
        {
            //Add to the group that containes the bussiness with the order id the deliverer that accepted the order
            //TODO:invoke to all available 
        }

    }

}
