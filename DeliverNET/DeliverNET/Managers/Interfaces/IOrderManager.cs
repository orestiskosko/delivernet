﻿using System;
using System.Collections.Generic;
using DeliverNET.Data;
using DeliverNET.Managers.Models;

namespace DeliverNET.Managers.Interfaces
{
    public interface IOrderManager
    {
        // Create
        bool Create(Order order);

        // Read
        List<Order> GetAll();
        Order Get(int id);
        Order Get(DateTime tStamp);
        List<Order> Get(Business business);
        List<Order> Get(Deliverer deliverer);
        
        // Update
        bool UpdateAddress(int id, string newAddress);
        bool UpdateReceiver(int id, OrderReceiverInfoModel newReceiverInfo);

        // Delete
        bool DeleteAll();
        bool Delete(int id);
    }
}
