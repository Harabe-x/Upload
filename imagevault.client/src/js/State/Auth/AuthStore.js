import { writable } from "svelte/store";
import axios from "axios";
import { UserDataModel } from "@/js/Models/UserDataModel.js";
import { setAuthToken } from "@/js/Configuration/AxiosDefaultConfig.js";

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

    const HTTP_STATUS_OK = 200;
    const HTTP_STATUS_UNAUTHORIZED = 401;

    return {
        subscribe: store.subscribe,

        set(value) {
            store.set(value);
            const json = JSON.stringify(value);
            localStorage.setItem("userData", json);
        },

        async login(email, password) {
            try {
                const response = await axios.post("/auth/login", { email, password });

                if (response.status !== HTTP_STATUS_OK) return;

                setAuthToken(response.data.token);


                this.set({
                    userDataModel: new UserDataModel(response.data.token, response.data.name, response.data.name, response.data.email, "darl"),
                    isLoggedIn: true
                });

            } catch (error) {
                console.error("Error during login:", error);
                return false;
            }
        },

        logout() {
            this.set({
                userDataModel: null,
                isLoggedIn: false
            });
        },

        async register( firstName, lastName,email, password) {
            try {
                const response = await axios.post("/auth/register", { firstName, lastName, email, password });

                if (response.status !== HTTP_STATUS_OK) return;

                await this.login(email, password);
            } catch (error) {
                console.error("Error during registration:", error);
            }
        },

        async pingAuth() {
            const updateState = async (state) => {
                if (!state.isLoggedIn) return state;

                try {
                    const response = await axios.post("/auth/pingauth");

                    if (response.status === HTTP_STATUS_OK) {
                        return { ...state, isLoggedIn: true };
                    }

                    return { isLoggedIn: false, userDataModel: null };

                } catch (error) {
                    console.error("Ping auth failed:", error);
                    return { isLoggedIn: false, userDataModel: null };
                }
            };

            store.update(updateState);
        }
    };
}
