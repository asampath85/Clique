
function ClaimRequestModel() {

    this.statusName = ko.observable();
    this.score = ko.observable();
    this.price = ko.observable();
    this.id = ko.observable();
    this.name = ko.observable();
    this.description = ko.observable();
    this.bedrooms = ko.observable();
    this.beds = ko.observable();
    this.accomodates = ko.observable();

    this.type = ko.observable();
    this.address1 = ko.observable();
    this.address2 = ko.observable();
    this.city = ko.observable();
    this.state = ko.observable();
    this.country = ko.observable();


    this.zip = ko.observable();
    this.nightPrice = ko.observable();


    this.weekPrice = ko.observable();
    this.locality = ko.observable();
    this.addedAt = ko.observable();
    this.ssn = ko.observable();
    this.isPetsAllowed = ko.observable();
    this.isWifiAvailable = ko.observable();
    this.isACAvailable = ko.observable();
    this.isLiftAvailable = ko.observable();
    this.isPrivatePoolAvailable = ko.observable();
    this.isBuzzerAvailable = ko.observable();
    this.fromDate = ko.observable();
    this.toDate = ko.observable();


}
