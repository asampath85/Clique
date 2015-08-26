var TwitterViewModel = function () {
    var self = this;
    
    self.userList = ko.observableArray();
   
    self.selectedItem = ko.observable();
    
    self.name = ko.observable();
    self.userName = ko.observable();

    self.edit = function (item) {
        self.selectedItem(item);
    };

    self.cancel = function () {
        self.selectedItem(null);
    };
    self.disable = function () {
        self.selectedItem(null);
    };

    self.add = function () {
        debugger;
        var newItem = new TwitterUser();
        newItem.name(self.name());
        newItem.userName(self.userName());

        var item = ko.toJSON(newItem);
        debugger;

        $.ajax({
            type: "POST",
            contentType: "application/json",
            data: item,
            url: "api/TwitterAPI"
        }).done(function (res) {
            debugger;
            alert("success");
            self.Get();


        }).error(function (ex) {
            debugger;
            alert("Error");
        });

        
    };

    self.Get = function () {


        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: "api/TwitterAPI"
        }).done(function (data) {

            alert("success");
            self.userList(data);


        }).error(function (ex) {

            alert("Error");
        });
    };
   
  
    self.save = function () {
        var item = ko.toJSON(self.selectedItem());
        debugger;

        $.ajax({
            type: "POST",
            contentType: "application/json",
            data: item,
            url: "api/TwitterAPI"
        }).done(function (res) {
            debugger;
            alert("success");
            self.userList(res);
            

        }).error(function (ex) {
            debugger;
            alert("Error");
        });


     

     
    };

    self.templateToUse = function (item) {
        return self.selectedItem() === item ? 'editTmpl' : 'itemsTmpl';
    };


    function TwitterUser(id, name, userName) {
        this.id = ko.observable(id);
        this.name = ko.observable(name);
        this.userName = ko.observable(userName);;

    }

    self.Get();

   
};

