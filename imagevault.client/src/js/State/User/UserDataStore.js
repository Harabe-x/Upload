import { writable, get } from "svelte/store";
import axios from "axios";
import { getAuthStore } from "@/js/State/Auth/AuthStore.js";
import { UserDataModel } from "@/js/Models/UserDataModel.js";
import {
    HTTP_STATUS_OK,
    HTTP_STATUS_UNAUTHORIZED,
    NOTIFICATION_TYPE_ERROR,
    NOTIFICATION_TYPE_SUCCESS,
    USER_ENDPOINT_URL
} from "@/js/Constants.js";
import {getNotificationsStore} from "@/js/State/UserInterface/ToastNotificationStore.js";
import {getThemeStore} from "@/js/State/UserInterface/ThemeStore.js";

const storedData = localStorage.getItem("userData");
const store = writable({
    userData: null
});

if (storedData !== null) {
    const serializedData = JSON.parse(storedData);
    store.set(serializedData);
}

export function getUserDataStore() {

    const toastNotificationStore = getNotificationsStore();

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

                const response = await axios.get(USER_ENDPOINT_URL, {
                    headers: { "Authorization": `Bearer ${storeData.token}` }
                });

                if(response.status === HTTP_STATUS_UNAUTHORIZED) {
                    authStore.logout();
                    return;
                }

                if (response.status === HTTP_STATUS_OK) {

                    const themeStore = getThemeStore();

                    themeStore.set(response.data.dataTheme)

                    this.set({
                        userData: new UserDataModel(
                            response.data.firstName,
                            response.data.lastName,
                            response.data.email,
                            response.data.dataTheme
                        ),
                    });
                } else if (response.status === HTTP_STATUS_UNAUTHORIZED) {
                    console.error("Unauthorized access - please log in again.");
                }
            } catch (error) {
                toastNotificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, "User data fetching failed.");
            }
        },
        updateUserData : async function(firstName,lastName,preferedColorSchema,email)
        {

            const authStore = getAuthStore();
            const storeData = get(authStore);


            try {
                const response = await  axios.patch(USER_ENDPOINT_URL, {
                    "firstName": firstName,
                    "lastName": lastName,
                    "dataTheme": preferedColorSchema,
                    "email": email,
                    "profilePicture": "string"
                },
                    { headers: { "Authorization": `Bearer ${storeData.token}` } }  )


                if(response.status === HTTP_STATUS_UNAUTHORIZED)
                {
                    authStore.logout();
                }

                if(response.status !== HTTP_STATUS_OK) return;

                toastNotificationStore.sendNotification(NOTIFICATION_TYPE_SUCCESS, "User data updated successfully.");

                this.set({
                    userData: new UserDataModel(
                        response.data.firstName,
                        response.data.lastName,
                        response.data.email,
                        response.data.dataTheme
                    ),
                });

            }
            catch (error)
            {
                toastNotificationStore.sendNotification(NOTIFICATION_TYPE_ERROR,"Updating user data failed.")
            }

        }
    };
}
