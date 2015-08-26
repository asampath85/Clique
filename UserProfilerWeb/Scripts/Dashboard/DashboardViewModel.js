



var DashboardViewModel = function () {
    var self = this;

    self.requestList = ko.observableArray([]);

    self.navigatetoDetails = function (item) {
        
        window.location.href = baseUrl +  '/Home/Details/' + item.Id;
    }

    self.get = function () {
        debugger;



        $.ajax({
            type: "Get",
            contentType: "application/json",
            url: baseUrl + "/api/RequestAPI/GetRequest"
        }).done(function (res) {
            debugger;
            
            // knockout mapping JSON data to view model
            
            self.requestList(res);

            $("#example").DataTable({ responsive: true });
            


        }).error(function (ex) {
            debugger;
            alert("Error");
        });
       
    };

  
    debugger;

    self.get();

};

