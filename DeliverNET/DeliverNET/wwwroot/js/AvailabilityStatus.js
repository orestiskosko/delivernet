﻿"use strict";
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
    //   event.preventDefault();
}
//invoke through Signalr the method that removes a deliver from the group of available deliverers
function addMeToGroup() {
    console.log('eimai available')
    connection.invoke("AddToGroupAvailable").catch(function (err) {
        return console.error(err.toString());
    });
    connection.invoke("GetActiveOrders").catch(function (err) {
        return console.error(err.toString());
    });
    //  event.preventDefault();
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




//
//this js receives new orders ,adds and removes orders from the list of available orders in the indexIndi
//



//takes the order
connection.on("NewOrder", (orderId, coords, paymentType, Tstamp) => {

    console.log(orderId);
    console.log(coords);
    console.log(paymentType);
    console.log(Tstamp);
    //TODO: append to list of available orders the order summary
    //TODO: also append to list the button which will be pressed to accept an order
    notifyMe()


    console.log("eimai prin to promise")

    ETA(orderId, coords)
});




connection.on("OrderRemove", (orderId) => {

    //TODO: remove from the list of orders the order by order id
});

//
// Estimated time and distance
//

function ETA(orderId, busiCoords) {

    var orderETA;
    var orderDistance;

    navigator.geolocation.getCurrentPosition(function (pos) {
        var lat = pos.coords.latitude.toFixed(6),
            long = pos.coords.longitude.toFixed(6),
            coords = lat + ', ' + long;
        console.log(coords);
        console.log(busiCoords);
        calculateDistance(coords, busiCoords)
    });


    function calculateDistance(coords, busiCoords) {
        // alert(typeof mycoords)
        var origin = coords;
        console.log(coords);
        var destination = busiCoords;
        var service = new google.maps.DistanceMatrixService();
        service.getDistanceMatrix(
            {
                origins: [origin],
                destinations: [destination],
                travelMode: google.maps.TravelMode.DRIVING,
                // unitSystem: google.maps.UnitSystem.IMPERIAL, // miles and feet.
                unitSystem: google.maps.UnitSystem.metric, // kilometers and meters.
                avoidHighways: false,
                avoidTolls: false
            },
            callback);
    }

    // get distance results
    function callback(response, status) {
        if (status != google.maps.DistanceMatrixStatus.OK) {
            $('#result').html(err);
        } else {
            var origin = response.originAddresses[0];
            var destination = response.destinationAddresses[0];
            if (response.rows[0].elements[0].status === "ZERO_RESULTS") {
                $('#result').html("Better get on a plane. There are no roads between " +
                    origin +
                    " and " +
                    destination);
            } else {
                //  alert(JSON.stringify(response.rows[0].elements[0].distance.value));
                // connection.invoke("TakeDistance", JSON.stringify(response.rows[0].elements[0].distance.value + ',' + JSON.stringify(response.rows[0].elements[0].duration.value)));


                // connection.invoke("TakeDistance", JSON.stringify(response.rows[0].elements[0].duration.value));
                orderDistance = response.rows[0].elements[0].distance.text;
                orderETA = response.rows[0].elements[0].duration.text;

                console.log(orderDistance);
                console.log(orderETA);

                // TODO Put real values in here
                appendOrder(orderId, orderDistance, orderETA, "5:00");
            }
        }
    }

}

//
// Browser notification
//
function notifyMe() {
    // Let's check if the browser supports notifications
    if (!("Notification" in window)) {
        alert("This browser does not support desktop notification");
    }

    // Let's check whether notification permissions have already been granted
    else if (Notification.permission === "granted") {
        // If it's okay let's create a notification
        var notification = new Notification(`${userName} have a new order`);
    }

    // Otherwise, we need to ask the user for permission
    else if (Notification.permission !== "denied") {
        Notification.requestPermission().then(function (permission) {
            // If the user accepts, let's create a notification
            if (permission === "granted") {
                var notification = new Notification(`${userName} have a new order`);
            }
        });
    }
}


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
var orderItemHtml = function (orderId, distance, eta, timer) {
    return `<a id="${orderId}" class="list-group-item list-group-item-action flex-column align-items-start">
                    <div class="d-flex justify-content-between">
                        <div>
                            <span class="d-block"><strong>Id</strong></span>
                            <span class="d-block" id="orderId">${orderId}</span>
                        </div>
                        <div>
                            <span class="d-block"><strong>Distance</strong></span>
                            <span class="d-block" id="orderDistance">${distance}</span>
                        </div>
                        <div>
                            <span class="d-block"><strong>ETA</strong></span>
                            <span class="d-block" id="orderDistance">${eta}</span>
                        </div>
                        <div>
                            <span class="d-block"><strong>Timer</strong></span>
                            <span class="d-block" id="orderDistance">${timer}</span>
                        </div>
                    </div>
                </a>`
}


var removeOrderBtn = document.getElementById("removeOrderBtn");
removeOrderBtn.addEventListener("click", () => {
    var orderId = document.getElementById("orderToRemove").value;
    removeOrder(orderId);
});


//
// Open order details modal logic
//
// Find orderId based on click on specific order list item
connection.on("GetOrder", (business, order) => {

    console.log(business);
    console.log(order);

    document.getElementById("modal-restaurantTitle").innerText = business.title;
    document.getElementById("modal-timestamp").innerText = order.tstamp;
    document.getElementById("modal-restaurantAddress").innerText = business.address;
    document.getElementById("modal-price").innerText = order.price;
    if (order.paymentTypeId == "0") {
        document.getElementById("modal-paymentType").innerText = "Cash";
    } else {
        document.getElementById("modal-paymentType").innerText = "Credit Card";
    }

});

// Add event listener for all order items
document.getElementById("orderList").addEventListener("click", (e) => {

    var orderId = e.path.find((el) => {
        return el.nodeName == "A";
    }).id

    connection.invoke("GetOrderFromDb", orderId);

    $("#orderModal").modal({
        show: true,
        focus: true
    })

});


//
//Displays the countdown timer inside the diplay property
//
function startTimer(orderId) { //TODO: Take as input to the function the duration in seconds and place them to the duration property
     var duration = 5, //secs
        display = document.getElementById("time");
    var timer = duration, minutes, seconds;
    var myVar = setInterval(function () { // loops every 1 sec
        minutes = parseInt(timer / 60, 10)
        seconds = parseInt(timer % 60, 10);

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        display.textContent = minutes + ":" + seconds;

        if (--timer < 0) {
            console.log(`the order with orderId ${"orderId"} is timedout`)
            clearInterval(myVar) //TODO: Remove from list the order with order id
        }
    }, 1000);
}

//TODO:when the deliverer presses the accept button an event will occur and send back to the server with signalr the responce
//TODO when document ready
