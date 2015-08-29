



var DashboardViewModel = function () {
    var self = this;

    self.propertyList = ko.observableArray([]);

    self.get = function () {
        debugger;



        $.ajax({
            type: "Get",
            contentType: "application/json",
            url: baseUrl + "api/HomeAwayAPI/GetProperty"
        }).done(function (res) {
            debugger;
            
            // knockout mapping JSON data to view model
            
            self.propertyList(res);

            $("#example").DataTable({ responsive: true });
            


        }).error(function (ex) {
            debugger;
            alert("Error");
        });
       
    };

  
    debugger;

    self.get();

};

