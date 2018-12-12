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

namespace DeliverNET.Comms.Hubs
{
  
    public class MainHub : Hub
    {
        //private IOrderManager _dbOrder;
        //private IBusinessCashierManager _dbCashier
        //public MainHub(IOrderManager db)
        //{
        //    _dbOrder = db;
        //}
 
        //Invoke from client to add a deliverer to the group of the available deliverers.
        public void AddToGroupAvailable()
        {
            //TODO: Add to group
            
        }

        //Invoke from client to remove a deliverer from the group of the available deliverers.
        public void RemoveFromGroupAvailable()
        {
            //TODO: Remove from group   
        }


        //Invoke from Client busi in order to send a new order

        public void PlaceNewOrder(OrderBusiViewModel order)
        {
            Order NewOrder = new Order();
            order.Address = NewOrder.Address;
            order.DoorName = NewOrder.DoorName;
            order.FirstName = NewOrder.FirstName;
            order.FloorNo = NewOrder.FloorNo;
            order.PaymentTypeId = NewOrder.PaymentTypeId;
           // NewOrder.Business = _dbOrder.Get(_dbOrder.get);
            

            //TODO: broadcast to delivers group the order and the order id in method newOrderAnnounce the name of the method that will be invoked in the client will be
            //TODO: complete the order and  add to db the new order(date time now)
            //TODO: Get from the db the order id
            //TODO: create a signalr group and add this bussiness


        }


        // invoke by client deliverer when accepted
        public void Accepted(string orderid)
        {
            //Add to the group that containes the bussiness with the order id the deliverer that accepted the order
            //TODO:invoke to all available 
        }

    }

}
