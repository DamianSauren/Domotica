"use strict";

const connection = new signalR.HubConnectionBuilder().withUrl("/feed").build();

window.onload = function () {
    connection.start().then(function () {
        console.log("Connected to feedhub");
    }).catch(function (err) {
        return console.error(err.toString());
    });
};

connection.on("newTemperatureData", function (tempId, temperature) {
    console.log("received newTemperatureData");
    document.getElementById(`${tempId}-temperature`).innerText = temperature + " C";    
});

connection.on("newMotionData", function (motionId, isTriggered, timeOfTrigger) {
    document.getElementById(`${motionId}-isTriggered`).innerText = isTriggered;
    document.getElementById(`${motionId}-timeOfTrigger`).innerText = timeOfTrigger;
});

connection.on("newLightData", function (lightId, hexColor, isOn) {
    document.getElementById(`${lightId}-hexColor`).innerText = hexColor;
    document.getElementById(`${lightId}-isOn`).innerText = isOn;

    const toggleSwitch = document.getElementById(`${lightId}-switch`);

    toggleSwitch.addEventListener('change', function () {
        const checkbox = document.querySelector('input[type="checkbox"]');
        const sendColor = document.getElementById(`${lightId}-button`)

        sendColor.addEventListener("click", function () {
            connection.send("ChangeColor", lightId, hexColor)
        });

        checkbox.addEventListener('change', function () {
            if (checkbox.checked) {
                connection.send("TurnOn", lightId);
            } else {
                connection.send("TurnOff", lightId);
            }
        });
    });
});