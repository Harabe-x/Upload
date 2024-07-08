import { writable } from "svelte/store";
import {getUserDataStore} from "@/js/State/User/UserDataStore.js";
import {getNotificationsStore} from "@/js/State/UserInterface/ToastNotificationStore.js";
import axios from "axios";
import {
    HTTP_STATUS_OK,
    LOGIN_ENDPOINT_URL,
    NOTIFICATION_TYPE_ERROR,
    PING_AUTH_ENDPOINT_URL,
    REGISTER_ENDPOINT_URL
} from "@/js/Constants.js";

const store = writable({
    token: null,
    isLoggedIn: false
});

const storedData = localStorage.getItem("loginData");

if (storedData !== null) {
    const serializedData = JSON.parse(storedData);
    store.set(serializedData);
}

export function getAuthStore() {
    const notificationStore = getNotificationsStore();

    return {
        subscribe: store.subscribe,

        set(value) {
            store.set(value);
            const json = JSON.stringify(value);
            localStorage.setItem("loginData", json);
        },

        async login(email, password) {
            try {
                const response = await axios.post(LOGIN_ENDPOINT_URL, { email, password });

                if (response.status !== 200) return;

               this.set({token: response.data.token, isLoggedIn: true});

               const userDataStore = getUserDataStore();

               await  userDataStore.fetchUserData();


            } catch (error) {
                console.error("Error during login:", error);
                return false;
            }
        },

        logout() {

            this.set({
                token: null,
                isLoggedIn: false
            });
            localStorage.clear();
            location.reload();
        },

        async register( firstName, lastName,email, password) {
            try {
                const response = await axios.post(REGISTER_ENDPOINT_URL, { firstName, lastName, email, password });

                if (response.status !== HTTP_STATUS_OK) return;

                    await this.login(email, password);
                } catch (error) {
                notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, error.message);
            }
        },

        async pingAuth() {
            const updateState = async (state) => {
                if (!state.isLoggedIn) return state;

                try {
                    const response = await axios.post(PING_AUTH_ENDPOINT_URL);

                    if (response.status === HTTP_STATUS_OK) {
                        setAuthToken(state.token);
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
