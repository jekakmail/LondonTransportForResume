var points = [];

function subgurim_Points_Clean() {
        for (var i = 0; i < points.length; i++) {
            points[i].setMap(null);
        };
    points = [];
}

function getCoordinates() {
    //navigator.geolocation.getCurrentPosition(sendCoordinates);
    fakeCoordinates();
}

function fakeCoordinates() {
    __doPostBack("getCoordinate", "51.50211782162702" + ":" + "-0.15031635761260986" + ":" + metres);
}

function sendCoordinates(position) {
    if (position.coords.longitude < -11.05 || position.coords.longitude > 1.78) {
        alert("Longitude specified is out of UK bounds.");
        $("#getCoordinate").addClass("disabled");
    } else {
        var metres = document.getElementById("ContentPlaceHolder_numMetres").value;
        if (metres === 0) {
            __doPostBack("getCoordinate", position.coords.latitude + ":" + position.coords.longitude + ":" + metres);
        }
    }
}

function __doPostBack(eventTarget, eventArgument) {
    window.form1.__EVENTTARGET.value = eventTarget;
    window.form1.__EVENTARGUMENT.value = eventArgument;
    window.form1.submit();

}