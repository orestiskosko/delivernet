using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverNET.Data;
using DeliverNET.Managers.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace DeliverNET.Comms.Hubs
{
    public class MainHub : Hub
    {

        // TODO Remove that shit
        public Task Kati()
        {
            string id = Context.UserIdentifier;
            return Task.CompletedTask;
        }
    }

}
