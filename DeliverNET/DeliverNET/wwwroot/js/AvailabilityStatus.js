"use strict";
// This Script is binded to the available button 'btnReady' on the deliverer indexindi in order to add or remove from the group of available deliverers
window.addEventListener("load", ()=> {
var connection = new signalR.HubConnectionBuilder().withUrl("/Comms/Hubs/MainHub").build();

connection.start().catch(function (err) {
    return console.error(err.toString());
});


//invoke through Signalr the method that removes a deliver from the group of available deliverers
function removeMeFromGroup() { 
    console.log('eimai unavailable')
    connection.invoke("AddToGroupAvailable").catch(function (err) {
        return console.error(err.toString());
    });
        event.preventDefault();
}
//invoke through Signalr the method that removes a deliver from the group of available deliverers
function addMeToGroup() {
    console.log('eimai available')
    connection.invoke("RemoveFromGroupAvailable").catch(function (err) {
        return console.error(err.toString());
    });
       event.preventDefault();
    }
    //this js receives new orders ,adds and removes orders from the list of available orders in the indexIndi

    //create an new connection to the hub
    var connection = new signalR.HubConnectionBuilder().withUrl("/Comms/Hubs/MainHub").build();

    //start the connection
    connection.start().catch(function (err) {
        return console.error(err.toString());
    });


    connection.on("NewOrder", (orderId, coords, paymentType, Tstampm) => {

        //TODO: append to list of available orders the order summary
        //TODO: also append to list the button which will be pressed to accept an order
    })


    connection.on("OrderRemove", (orderId) => {

        //TODO: remove from the list of order the order by order id
    })

    //TODO: an ginei refresh xanonati ola ta stoixeia tis listas.. kane kati

    //TODO:when the deliverer presses the accept button an event will occur and send back to the server with signalr the responce





})
