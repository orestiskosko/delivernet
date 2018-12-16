﻿"use strict";
// this js is for placing a new order in busi index the button 'submit order is bounded with this js'

//when document ready
//window.addEventListener("load", () => {
//create an new connection to the hub
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/Comms/Hubs/MainHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();


//start the connection
connection.start().catch(function (err) {
    return console.error(err.toString());
});
// declare 
const newOrderElement = document.getElementById("aaaa")

newOrderElement.addEventListener("submit", (e) => {
    e.preventDefault();
    console.log("mpika sto event submit");
    console.log(document.getElementById("comments").value);
    var order = {
        FirstName: document.getElementById("firstName").value,
        LastName: document.getElementById("lastName").value,
        Address: document.getElementById("address").value,
        FloorNo: document.getElementById("floor").value,
        DoorName: document.getElementById("doorName").value,
        PaymentTypeId: document.getElementById("paymentType").value,
        PhoneNumber: document.getElementById("phoneNumber").value,
        Price: document.getElementById("price").value,
        Comments: document.getElementById("comments").value
    }
    //console.log("kai edo");
    //console.log(order);
    console.log("order submitted");

    placeANewOrder(order);

})

// sends the order back to the server
function placeANewOrder(order) {
    console.log('order goes to server');
    connection.invoke("PlaceNewOrder", order).catch(function (err) {
        return console.error(err.toString());
    });
    //event.preventDefault();
}


connection.on("AppendThisOrder",
    order => {
        console.log("FAAAAAAAAAAAAAAAAAAAK");
        console.log(order);
    });

//
//functions that called when a deliverer accepts picks up or delivers the order
//

connection.on("OrderAccepted", (deliverer, orderId) => {
    //TODO:Change status of order with OrderId in the table to accepted
});

connection.on("OrderPickedUp", (deliverer, orderId) => {
    //TODO:Change status of order with OrderId in the table to PickedUp
});

connection.on("OrderDelivered", (deliverer, orderId) => {
    //TODO:Change status of order with OrderId in the table to accepted
});


//})

