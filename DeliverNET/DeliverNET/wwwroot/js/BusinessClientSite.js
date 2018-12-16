"use strict";
// this js is for placing a new order in busi index the button 'submit order is bounded with this js'

//when document ready
//window.addEventListener("load", () => {
//create an new connection to the hub
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/Comms/Hubs/MainHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

//start the connection
connection.start()
    .then(() => {
        //fuction here starts after connection establish
    })
    .catch(function (err) {
    return console.error(err.toString());
    });

// declare 
const newOrderElement = document.getElementById("orderForm")

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
    //TODO:append the new order
    //TODO:take from server the orderid

    placeANewOrder(order);

})

 
//the server sends back the order in an object in order for the client to append it to the html
connection.on("AppendThisOrder" ,(orderFor) => {
    console.log("girise to "+orderFor);
    //TODO:call append function
});

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


//
// Order list manipulation
//
// append order item
var appendOrder = function (orderId, distance, eta, timer) {
    var orderList = document.getElementById("orderList");
    orderList.innerHTML += orderItemHtml(orderId, distance, eta, timer);
}

// remove order item
var removeOrder = function (orderId) {
    var orderToRemove = document.getElementById(orderId);
    orderToRemove.parentNode.removeChild(orderToRemove);
}

// remove all orders form list
var clearOrderList = function () {
    var orderList = document.getElementById("orderList");
    var t = true;
    while (t) {
        if (orderList.lastChild.nodeName == "A") {
            orderList.removeChild(orderList.lastChild);
        } else {
            t = false;
        }
    }
}

// order item html
var orderItemHtml = function (orderId, adrress, timer) {
    return `<a id="${orderId}" class="list-group-item list-group-item-action flex-column align-items-start">
                    <div class="d-flex justify-content-between">
                        <div>
                            <span class="d-block"><strong>Id</strong></span>
                            <span class="d-block" id="orderId">${orderId}</span>
                        </div>
                        <div>
                            <span class="d-block"><strong>Distance</strong></span>
                            <span class="d-block" id="orderDistance">${adrress}</span>
                        </div>
                        <div>
                            <span class="d-block"><strong>Timer</strong></span>
                            <span class="d-block" id="orderDistance">${timer}</span>
                        </div>
                    </div>
                </a>`
}

//
// Get all NON TIMEDOUT orders
//
connection.on("GetActiveOrders",
    (orders) => {
        orders.forEach((o) => {
            appendOrder(o.id, "--", "--", "5:00");
        })
    });

//})

