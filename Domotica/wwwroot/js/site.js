`use strict`;
const connection = new signalR.HubConnectionBuilder().withUrl(`/feed`).build();

window.onload = function () {
    connection.start().then(function () {
        console.log(`Connected to feedhub`);
    }).catch(function (err) {
        return console.error(err.toString());
    });
};

connection.on("newTemperatureData", function (tempId, temperature) {
    document.getElementById(`${tempId}-temperature`).innerText = temperature;    
});

connection.on("newMotionData", function (motionId, isTriggered, timeOfTrigger) {
    document.getElementById(`${motionId}-isTriggered`).innerText = isTriggered;
    document.getElementById(`${motionId}-timeOfTrigger`).innerText = timeOfTrigger;
});

connection.on("newLightData", function (lightId, hexColor, isOn) {
    const colorPicker = document.getElementById(`${lightId}-color`);
    const toggleSwitch = document.getElementById(`${lightId}-switch`);

    colorPicker.value = hexColor;
    colorPicker.addEventListener(`change`, updateColor, false);

    toggleSwitch.checked = isOn;

    function updateColor(event) {
        connection.send(`ChangeColor`, lightId, event.target.value);
    };

    toggleSwitch.addEventListener(`change`, function () {
        if (this.checked) {
            connection.send("TurnOn", lightId);
        } else {
            connection.send("TurnOff", lightId);
        }
    });
});