export class UserDataModel {
    constructor( firstName = "", lastName = "", email = "", preferedColorSchema = "") {
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.preferedColorSchema = preferedColorSchema;
    }
}