import { writable } from "svelte/store";
import axios from "axios";
import { UserDataModel } from "@/js/Models/UserDataModel.js";

const store = writable({
    userDataModel: null,
    isLoggedIn: false
});

const storedData = localStorage.getItem("userData");

if (storedData !== null) {
    const serializedData = JSON.parse(storedData);
    store.set(serializedData);
}

export function getAuthStore() {
    return {
        subscribe: store.subscribe,

        set(value) {
            store.set(value);
            const json = JSON.stringify(value);
            localStorage.setItem("userData", json);
        },

        login(email, password) {
            axios.post("https://localhost:7110/api/auth/login", { email: email, password: password })
                .then(function (response) {
                    if (response.status !== 200) return;

                    this.set({
                        userDataModel: new UserDataModel(response.data.token, response.data.name, response.data.name, response.data.email, "darl"),
                        isLoggedIn: true
                    });
                }.bind(this))
                .catch(function (error) {
                    console.error("Error during login:", error);
                });
        },

        logout() {
            this.set({
                userDataModel: null,
                isLoggedIn: false
            });
        },

        register(email, password, firstName, lastName) {
            axios.post("https://localhost:7110/api/auth/register", { firstName: firstName, lastName: lastName, email: email, password: password })
                .then(function (response) {
                    if (response.status !== 200) return;

                    this.login(email, password);
                }.bind(this))
                .catch(function (error) {
                    console.error("Error during registration:", error);
                });
        }
    };
}
