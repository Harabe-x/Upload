import { get, writable } from "svelte/store";
import axios from "axios";
import {
    NOTIFICATION_TYPE_ERROR,
    NOTIFICATION_TYPE_SUCCESS,
    HTTP_STATUS_UNAUTHORIZED,
    HTTP_STATUS_OK,
    HTTP_NO_CONTENT,
    APIKEY_ADD_ENDPOINT_URL,
    APIKEY_GET_ENDPOINT_URL,
    APIKEY_DELETE_ENDPOINT_URL,
    APIKEY_LIST_ENDPOINT_URL,
    APIKEY_EDIT_ENDPOINT_URL,
    GET_USER_ENDPOINT_URL
} from "@/js/Constants.js";
import { getNotificationsStore } from "@/js/State/UserInterface/ToastNotificationStore.js";
import { getAuthStore } from "@/js/State/Auth/AuthStore.js";
import {Key} from "svelte-hero-icons";

const apiKeyStore = writable({
    apiKeys: [],
    selectedKey: null
});

export function getApiKeyStore() {
    const notificationStore = getNotificationsStore();

    const handleUnauthorized = () => {
        notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, "Please log in again");
        const authStore = getAuthStore();
        authStore.logout();
    };

    const fetchKeys = async () => {
        try {
            const authStore = getAuthStore();
            const storeData = get(authStore);

            const response = await axios.post(APIKEY_LIST_ENDPOINT_URL, null, {
                headers: { "Authorization": `Bearer ${storeData.token}` }
            });

            if (response.status === HTTP_STATUS_UNAUTHORIZED) {
                handleUnauthorized();
                return;
            }

            if (response.status === HTTP_STATUS_OK) {
                apiKeyStore.update(state => ({
                    ...state,
                    apiKeys: response.data
                }));
            } else {
                notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, "Failed to fetch API keys");
            }

            return response.data;
        } catch (error) {
            notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, error.message);
        }
    };

    const editKey = async (key, newKeyName) => {
        try {
            const authStore = getAuthStore();
            const storeData = get(authStore);

            const response = await axios.patch(APIKEY_EDIT_ENDPOINT_URL, { keyName: newKeyName, key }, {
                headers: { "Authorization": `Bearer ${storeData.token}` }
            });

            if (response.status === HTTP_STATUS_UNAUTHORIZED) {
                handleUnauthorized();
                return;
            }

            if (response.status === HTTP_STATUS_OK) {
                notificationStore.sendNotification(NOTIFICATION_TYPE_SUCCESS, "API key updated");
                await fetchKeys();
            } else {
                notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, "Failed to edit API key");
            }
        } catch (error) {
            notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, error.message);
        }
    };

    const deleteKey = async (key) => {
        try {
            const authStore = getAuthStore();
            const storeData = get(authStore);

            var parsedKey = key.toString();
            
             var response = await axios.delete(APIKEY_DELETE_ENDPOINT_URL, { headers: {"Authorization": `Bearer ${storeData.token}`}, data: {key} } );         
             
            if (response.status === HTTP_STATUS_UNAUTHORIZED) {
                handleUnauthorized();
                return;
            }

            if (response.status === HTTP_NO_CONTENT) {
                notificationStore.sendNotification(NOTIFICATION_TYPE_SUCCESS, "API key deleted");
                await fetchKeys();
            } else {
                notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, "Failed to delete API key");
            }
        } catch (error) {
            notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, error.message);
        }
    };

    const addKey = async(key) => {
        try {
            const authStore = getAuthStore();
            const storeData = get(authStore);

            const response = await axios.post(APIKEY_ADD_ENDPOINT_URL, { keyName: key }, {
                headers: { "Authorization": `Bearer ${storeData.token}` }
            });

            if (response.status === HTTP_STATUS_UNAUTHORIZED) {
                handleUnauthorized();
                return;
            }

            if (response.status === HTTP_STATUS_OK) {
                notificationStore.sendNotification(NOTIFICATION_TYPE_SUCCESS, "API key added");
                await fetchKeys();
            } else {
                notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, "Failed to add API key");
            }
        } catch (error) {
            notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, error.response.data.message);
        }
    }
    
    
    const selectKey = (key) => {
        apiKeyStore.update(state => ({
            ...state,
            selectedKey: state.apiKeys.find(apiKey => key === apiKey) || null
        }));
    };

    return {
        subscribe: apiKeyStore.subscribe,
        fetchKeys,
        editKey,
        deleteKey,
        selectKey,
        addKey,
        rotateKey: async (key) => {
            // TODO: Implement rotateKey logic
        }
    };
}
