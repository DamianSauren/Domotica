var connection = new signalR.HubConnectionBuilder().withUrl("/feed").build();
var toggleSwitch = document.getElementById(`${lightID}-switch`);
var lightID;

window.onload = function () {
    toggleSwitch.addEventListener('change', function () {
        var checkbox = document.querySelector('input[type="checkbox"]');

        checkbox.addEventListener('change', function () {
            if (checkbox.checked) {
                connection.invoke("TurnOn", lightID);
            } else {
                connection.invoke("TurnOff", lightID);
            }
        });
    });

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
    lightID = lightId;
    document.getElementById(`${lightId}-hexColor`).innerHTML = hexColor;
    document.getElementById(`${lightId}-isOn`).innerHTML = isOn;
});