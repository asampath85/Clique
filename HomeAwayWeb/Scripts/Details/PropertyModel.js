
function PropertyModel() {

    
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
}
