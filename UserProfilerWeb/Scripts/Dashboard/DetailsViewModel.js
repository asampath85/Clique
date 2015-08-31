var DetailsViewModel = function () {
    var self = this;

    self.eventList = ko.observableArray([]);
    self.locationTweetList = ko.observableArray([]);    
    self.userFeedbackList = ko.observableArray([]);
    self.locationWeatherList = ko.observableArray([]);

  
    self.getEvent = function () {
        debugger;



        $.ajax({
            type: "Get",
            contentType: "application/json",
            url: baseUrl + "/api/RequestAPI/GetEvent/" + requestId
        }).done(function (res) {
            debugger;
            
            self.eventList(res);

            $("#eventsTable").DataTable({ responsive: true });
        }).error(function (ex) {
            debugger;
            alert("Error");
        });

    };

    self.getLocationTweet = function () {
        debugger;



        $.ajax({
            type: "Get",
            contentType: "application/json",
            url: baseUrl + "/api/RequestAPI/GetLocationTweet/" + requestId
        }).done(function (res) {
            debugger;
            
            self.locationTweetList(res);
            $("#locationTable").DataTable({ responsive: true });

        }).error(function (ex) {
            debugger;
            alert("Error");
        });

    };

    self.getUserFeedback = function () {
        debugger;



        $.ajax({
            type: "Get",
            contentType: "application/json",
            url: baseUrl + "/api/RequestAPI/GetUserFeedback/" + requestId
        }).done(function (res) {
            debugger;
            
            self.userFeedbackList(res);
            $("#feedbackTable").DataTable({ responsive: true });


        }).error(function (ex) {
            debugger;
            alert("Error");
        });

    };

    self.getLocationWeather = function () {

        debugger;

        $.ajax({
            type: "Get",
            contentType: "application/json",
            url: baseUrl + "/api/RequestAPI/GetLocationWeather/" + requestId
        }).done(function (res) {
            debugger;

            self.locationWeatherList(res);
            $("#weatherTable").DataTable({ responsive: true });


        }).error(function (ex) {
            debugger;
            alert("Error");
        });

    };


    debugger;

    self.getEvent();
    self.getLocationTweet();
    self.getUserFeedback();
    self.getLocationWeather();

};

