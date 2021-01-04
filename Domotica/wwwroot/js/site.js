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
    document.getElementById(`${tempId}-temperature`).innerText = temperature;    
});

connection.on("newMotionData", function (motionId, isTriggered, timeOfTrigger) {
    document.getElementById(`${motionId}-isTriggered`).innerText = isTriggered;
    document.getElementById(`${motionId}-timeOfTrigger`).innerText = timeOfTrigger;
});

connection.on("newLightData", function (lightId, hexColor, isOn) {
    document.getElementById(`${lightId}-isOn`).innerText = isOn;

    const colorPicker = document.getElementById(`${lightId}-color`);

    colorPicker.value = hexColor;
    colorPicker.addEventListener("change", updateColor, false);

    function updateColor(event) {
        console.log(event.target.value);
        connection.send("ChangeColor", lightId, event.target.value);
    };

    const toggleSwitch = document.getElementById(`${lightId}-switch`);

    toggleSwitch.addEventListener('change', function () {
        const checkbox = document.querySelector('input[type="checkbox"]');
        const sendColor = document.getElementById(`${lightId}-button`);

        checkbox.addEventListener('change', function () {
            if (checkbox.checked) {
                connection.send("TurnOn", lightId);
            } else {
                connection.send("TurnOff", lightId);
            }
        });
    });
});