var PropertyViewModel = function () {
    var self = this;
    debugger;
    self.model = ko.observable(new PropertyModel());


    self.add = function () {
        debugger;


        var item = ko.toJSON(self.model());
        debugger;

        $.ajax({
            type: "POST",
            contentType: "application/json",
            data: item,
            url: baseUrl + "api/HomeAwayAPI/AddProperty"
        }).done(function (res) {
            debugger;

            window.location.href = baseUrl;


        }).error(function (ex) {
            debugger;
            alert("Error");
        });


    };

 




};

