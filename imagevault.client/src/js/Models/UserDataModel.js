export class UserDataModel {
    constructor(token = "", firstName = "", lastName = "", email = "", preferedColorSchema = "") {
        this.token = token;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.preferedColorSchema = preferedColorSchema;
    }
}