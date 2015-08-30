ko.bindingHandlers.showModal = {
    init: function (element, valueAccessor) {
        $(element).modal({ backdrop: 'static', keyboard: true, show: false });
    },
    update: function (element, valueAccessor) {

        var value = valueAccessor();
        if (ko.utils.unwrapObservable(value)) {
            $(element).modal('show');
            $("input", element).focus();
        }
        else {

            $(element).modal('hide');
        }
    }
};



var DashboardViewModel = function () {
    var self = this;

    self.propertyList = ko.observableArray([]);

    self.displaySubmitModal = ko.observable(false);

    self.model = ko.observable(new UserFeedbackModel());

    self.showModal = function (item) {
        debugger;
        self.model().propertyId(item.Id);
        self.displaySubmitModal(true);
        //TODO: need to send property details like pincode and property name
        
    };

    self.HideModal = function () {
        self.displaySubmitModal(false);
        //TODO: should clear the contents before closing the model for the next launch

        //self.model.id("");
        //self.model.name("");
        //self.model.emailid("");
        //self.model.feedback("");
        
    };


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

    debugger;
  
   
    self.addfeedback = function () {
        debugger;


        var item = ko.toJSON(self.model());
        

        debugger;

        $.ajax({
            type: "POST",
            contentType: "application/json",
            data: item,
            url: baseUrl + "api/HomeAwayAPI/AddFeedback"
        }).done(function (res) {
            debugger;

            //$('#myModel').model('hide');

            self.HideModal();


        }).error(function (ex) {
            debugger;
            alert("Error");
        });
    };

};

