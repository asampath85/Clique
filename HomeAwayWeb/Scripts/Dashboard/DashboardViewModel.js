



var DashboardViewModel = function () {
    var self = this;

    self.propertyList = ko.observableArray([]);

    self.navigatetoDetails = function (item) {

        window.location.href = baseUrl + '/Home/Details/' + item.Id;
    }

    self.get = function () {
        debugger;



        $.ajax({
            type: "Get",
            contentType: "application/json",
            url: baseUrl + "api/HomeAwayAPI/GetPropertyList"
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

