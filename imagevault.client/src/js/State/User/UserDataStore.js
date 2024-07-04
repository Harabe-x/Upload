import { writable, get } from "svelte/store";
import axios from "axios";
import { getAuthStore } from "@/js/State/Auth/AuthStore.js";
import { UserDataModel } from "@/js/Models/UserDataModel.js";

const storedData = localStorage.getItem("userData");
const store = writable({
    userData: null
});

if (storedData !== null) {
    const serializedData = JSON.parse(storedData);
    store.set(serializedData);
}

export function getUserDataStore() {
    const REQUEST_URL = "/account";
    const HTTP_STATUS_OK = 200;
    const HTTP_STATUS_UNAUTHORIZED = 401;

    return {
        subscribe: store.subscribe,

        set: function(value) {
            store.set(value);
            const json = JSON.stringify(value);
            localStorage.setItem("userData", json);
        },

        fetchUserData: async function() {
            try {
                const authStore = getAuthStore();
                const storeData = get(authStore);

                const response = await axios.get(REQUEST_URL, {
                    headers: { "Authorization": `Bearer ${storeData.token}` }
                });

                if (response.status === HTTP_STATUS_OK) {
                    this.set({
                        userData: new UserDataModel(
                            "",
                            response.data.firstName,
                            response.data.lastName,
                            response.data.email,
                            response.data.dataTheme
                        ),
                    });
                } else if (response.status === HTTP_STATUS_UNAUTHORIZED) {
                    // Handle unauthorized access, e.g., redirect to login
                    console.error("Unauthorized access - please log in again.");
                }
            } catch (error) {
                console.error("Error fetching user data:", error);
                if (error.response) {
                    // Server responded with a status other than 200 range
                    console.error("Response status:", error.response.status);
                    console.error("Response data:", error.response.data);
                } else if (error.request) {
                    // Request was made but no response received
                    console.error("No response received:", error.request);
                } else {
                    // Something else happened while setting up the request
                    console.error("Error setting up request:", error.message);
                }
            }
        }
    };
}
