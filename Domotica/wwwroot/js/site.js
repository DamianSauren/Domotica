var connection = new signalR.HubConnectionBuilder().withUrl("/feed").build();

window.onload = function () {
    connection.start().then(function () {
        console.log("Connected to feedhub");
    }).catch(function (err) {
        return console.error(err.toString());
    });
};

connection.on("newTemperatureData", function (tempId, temperature) {
    document.getElementById(`${tempId}-temperature`).innerHTML = temperature;
});

connection.on("newMotionData", function (motionId, isTriggered, timeOfTrigger) {
    document.getElementById(`${motionId}-isTriggered`).innerHTML = isTriggered;
    document.getElementById(`${motionId}-timeOfTrigger`).innerHTML = timeOfTrigger;
});

connection.on("newLightData", function (lightId, hexColor, isOn) {
    document.getElementById(`${lightId}-hexColor`).innerHTML = hexColor;
    document.getElementById(`${lightId}-isOn`).innerHTML = isOn;

    var toggleSwitch = document.getElementById(`${lightID}-switch`);

    toggleSwitch.addEventListener('change', function () {
        var checkbox = document.querySelector('input[type="checkbox"]');
        var sendColor = document.getElementById(`${lightID}-button`)

        sendColor.addEventListener("click", function () {
            connection.send("ChangeColor", lightID, hexColor)
        });

        checkbox.addEventListener('change', function () {
            if (checkbox.checked) {
                connection.send("TurnOn", lightID);
            } else {
                connection.send("TurnOff", lightID);
            }
        });
    });
});