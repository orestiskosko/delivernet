"use strict";
// This Script is binded to the available button 'btnReady' on the deliverer indexindi in order to add or remove from the group of available deliverers
//window.addEventListener("load", () => {
var connection = new signalR.HubConnectionBuilder().withUrl("/Comms/Hubs/MainHub").build();

connection.start().catch(function (err) {
    return console.error(err.toString());
});


//invoke through Signalr the method that removes a deliver from the group of available deliverers
function removeMeFromGroup() {
    console.log('eimai unavailable')
    connection.invoke("RemoveFromGroupAvailable").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
}
//invoke through Signalr the method that removes a deliver from the group of available deliverers
function addMeToGroup() {
    console.log('eimai available')
    connection.invoke("AddToGroupAvailable").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
}

//
//this js receives new orders ,adds and removes orders from the list of available orders in the indexIndi
//

//create an new connection to the hub
var connection = new signalR.HubConnectionBuilder().withUrl("/Comms/Hubs/MainHub").build();

//start the connection
connection.start().catch(function (err) {
    return console.error(err.toString());
});

//takes
connection.on("NewOrder", (orderId, coords, paymentType, Tstampm) => {

    console.log(orderId);
    console.log(coords);
    console.log(paymentType);
    console.log(Tstampm);
    //TODO: append to list of available orders the order summary
    //TODO: also append to list the button which will be pressed to accept an order
    notifyMe()

});


connection.on("OrderRemove", (orderId) => {

    //TODO: remove from the list of order the order by order id
});

//TODO: an ginei refresh xanonati ola ta stoixeia tis listas.. kane kati

//TODO:when the deliverer presses the accept button an event will occur and send back to the server with signalr the responce





//});
function notifyMe() {
    // Let's check if the browser supports notifications
    if (!("Notification" in window)) {
        alert("This browser does not support desktop notification");
    }

    // Let's check whether notification permissions have already been granted
    else if (Notification.permission === "granted") {
        // If it's okay let's create a notification
        var notification = new Notification("You have a new order");
    }

    // Otherwise, we need to ask the user for permission
    else if (Notification.permission !== "denied") {
        Notification.requestPermission().then(function (permission) {
            // If the user accepts, let's create a notification
            if (permission === "granted") {
                var notification = new Notification("You have a new order");
            }
        });
    }

    // At last, if the user has denied notifications, and you 
    // want to be respectful there is no need to bother them any more.
}