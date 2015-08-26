var DetailsViewModel = function () {
    var self = this;

    self.eventList = ko.observableArray([]);
    self.locationTweetList = ko.observableArray([]);
    self.userTweetList = ko.observableArray([]);

  
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

    self.getUserTweet = function () {
        debugger;



        $.ajax({
            type: "Get",
            contentType: "application/json",
            url: baseUrl + "/api/RequestAPI/GetUserTweet/" + requestId
        }).done(function (res) {
            debugger;
            
            self.userTweetList(res);
            $("#userTable").DataTable({ responsive: true });


        }).error(function (ex) {
            debugger;
            alert("Error");
        });

    };


    debugger;

    self.getEvent();
    self.getLocationTweet();
    self.getUserTweet();

};

