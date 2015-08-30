
function ClaimRequestModel() {

    var self = this;

    self.statusName = ko.observable();
    self.score = ko.observable();
    self.price = ko.observable();
    self.id = ko.observable();
    self.name = ko.observable();
    self.description = ko.observable();
    self.bedrooms = ko.observable();
    self.beds = ko.observable();
    self.accomodates = ko.observable();

    self.type = ko.observable();
    self.address1 = ko.observable();
    self.address2 = ko.observable();
    self.city = ko.observable();
    self.state = ko.observable();
    self.country = ko.observable();


    self.zip = ko.observable();
    self.nightPrice = ko.observable();


    self.weekPrice = ko.observable();
    self.locality = ko.observable();

    self.locality.subscribe(function () {

        if (self.locality().trim() == "") {
            self.latitude("");
            self.longitude("");
            return;
        }
        self.GetGeoLocation();
    });

    self.GetGeoLocation = function () {
        debugger;
        var geocoder = new google.maps.Geocoder();
        var address = self.locality().trim() + ',' + self.city().trim();

        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                self.latitude(results[0].geometry.location.lat());
                self.longitude(results[0].geometry.location.lng());
            } else {
                alert("Geolocation api failed.");
            }
        });
    };

    self.addedAt = ko.observable();
    self.ssn = ko.observable();
    self.isPetsAllowed = ko.observable();
    self.isWifiAvailable = ko.observable();
    self.isACAvailable = ko.observable();
    self.isLiftAvailable = ko.observable();
    self.isPrivatePoolAvailable = ko.observable();
    self.isBuzzerAvailable = ko.observable();
    self.fromDate = ko.observable();
    self.toDate = ko.observable();
    self.latitude = ko.observable();
    self.longitude = ko.observable();


}
