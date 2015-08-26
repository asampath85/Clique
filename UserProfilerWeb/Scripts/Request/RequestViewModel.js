var RequestViewModel = function () {
    var self = this;
     
    self.model = ko.observable(new RequestModel());

   
    self.add = function () {
        debugger;
     

        var item = ko.toJSON(self.model());
        debugger;

        $.ajax({
            type: "POST",
            contentType: "application/json",
            data: item,
            url: baseUrl + "/api/RequestAPI/AddRequest"
        }).done(function (res) {
            debugger;
            
            window.location.href = baseUrl;


        }).error(function (ex) {
            debugger;
            alert("Error");
        });


    };

    self.GetLocation = function () {


        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: "api/RequestAPI/GetLocation"
        }).done(function (data) {

            alert("success");
            self.userList(data);


        }).error(function (ex) {

            alert("Error");
        });
    };




};

