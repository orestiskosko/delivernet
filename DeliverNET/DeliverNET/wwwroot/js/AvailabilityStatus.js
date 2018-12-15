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
    //   event.preventDefault();
}
//invoke through Signalr the method that removes a deliver from the group of available deliverers
function addMeToGroup() {
    console.log('eimai available')
    connection.invoke("AddToGroupAvailable").catch(function (err) {
        return console.error(err.toString());
    });
    //  event.preventDefault();
}



//
//this js receives new orders ,adds and removes orders from the list of available orders in the indexIndi
//



//takes the order
connection.on("NewOrder", (orderId, coords, paymentType, Tstampm) => {

    console.log(orderId);
    console.log(coords);
    console.log(paymentType);
    console.log(Tstampm);
    //TODO: append to list of available orders the order summary
    //TODO: also append to list the button which will be pressed to accept an order
    notifyMe()


    console.log("eimai prin to promise")

    ETA(coords)

});




connection.on("OrderRemove", (orderId) => {

    //TODO: remove from the list of orders the order by order id
});

//
// Estimated time and distance
//

function ETA(busiCoords) {
    navigator.geolocation.getCurrentPosition(function (pos) {
        var lat = pos.coords.latitude.toFixed(6),
            long = pos.coords.longitude.toFixed(6),
            coords = lat + ', ' + long;
        console.log(coords);
        console.log(busiCoords);
        calculateDistance(coords, busiCoords)
    });
}

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
        }, callback);
}
// get distance results
function callback(response, status) {
    if (status != google.maps.DistanceMatrixStatus.OK) {
        $('#result').html(err);
    } else {
        var origin = response.originAddresses[0];
        var destination = response.destinationAddresses[0];
        if (response.rows[0].elements[0].status === "ZERO_RESULTS") {
            $('#result').html("Better get on a plane. There are no roads between " + origin + " and " + destination);
        } else {
            //  alert(JSON.stringify(response.rows[0].elements[0].distance.value));
            // connection.invoke("TakeDistance", JSON.stringify(response.rows[0].elements[0].distance.value + ',' + JSON.stringify(response.rows[0].elements[0].duration.value)));


            // connection.invoke("TakeDistance", JSON.stringify(response.rows[0].elements[0].duration.value));
            console.log(response.rows[0].elements[0].distance.text);
            console.log(response.rows[0].elements[0].duration.text);

        }
    }
}

//
//Notifications
//

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

}

//TODO: an ginei refresh xanonati ola ta stoixeia tis listas.. kane kati

//TODO:when the deliverer presses the accept button an event will occur and send back to the server with signalr the responce
//TODO when document ready
