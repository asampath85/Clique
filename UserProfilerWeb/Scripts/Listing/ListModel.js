
function ListModel() {

    this.address1 = ko.observable();
    this.address2 = ko.observable();
    this.city = ko.observable();
    this.state = ko.observable("");
    this.zip = ko.observable("");
    this.country = ko.observable();

    this.title = ko.observable();
    this.description = ko.observable();
    this.noofbedrooms = ko.observable();
    this.noofbeds = ko.observable("");
    this.noofpeople = ko.observable("");
    this.propertyType = ko.observable();


    this.nightlyCost = ko.observable();
    this.weeklyCost = ko.observable();


    this.isPetsAllowed = ko.observable();
    this.isWifiAvailable = ko.observable();
    this.isACAvailable = ko.observable();
    this.isLiftAvailable = ko.observable();
    this.isPrivatePool = ko.observable();
    this.isBuzzer = ko.observable();
    

}
