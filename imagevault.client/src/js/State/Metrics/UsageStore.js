import {get, writable} from "svelte/store";
import axios from "axios";
import {getNotificationsStore} from "@/js/State/UserInterface/ToastNotificationStore.js";    
import {
    APIKEY_EDIT_ENDPOINT_URL, DAILY_RESOURCE_USAGE_METRICS_ENDPOINT,
    HTTP_STATUS_OK,
    HTTP_STATUS_UNAUTHORIZED, NOTIFICATION_TYPE_ERROR,
    NOTIFICATION_TYPE_SUCCESS, TOTAL_RESOURCE_USAGE_METRICS_ENDPOINT
} from "@/js/Constants.js";
import {getAuthStore} from "@/js/State/Auth/AuthStore.js";


const usageStore = writable(
    {
     UsageMetrics: {},
     DailyUsageMetrics : []   
    }) 



export function getUsageStore()
{
    const notificationStore = getNotificationsStore();
    
    const fetchDailyUsage = async (dayRange) => {
        try {
            const authStore = getAuthStore();
            const storeData = get(authStore);

            const response = await axios.post(DAILY_RESOURCE_USAGE_METRICS_ENDPOINT, { dayRange }, {
                headers: { "Authorization": `Bearer ${storeData.token}` }
            });
            

            if (response.status === HTTP_STATUS_OK) {
                usageStore.update((state) => ({ 
                     ...state, 
                     DailyUsageMetrics: response.data   
                }))
            } 
            else {
                notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, "Failed to fetch daily usage data");
            }
        } catch (error) {
            notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, error.message);
        }

    }
    
    const fetchUsageMetrics = async () => {

        try {
            const authStore = getAuthStore();
            const storeData = get(authStore);

            const response = await axios.post(TOTAL_RESOURCE_USAGE_METRICS_ENDPOINT, { }, {
                headers: { "Authorization": `Bearer ${storeData.token}` }
            });


            if (response.status === HTTP_STATUS_OK) {
                usageStore.update((state) => ({
                    ...state,
                    UsageMetrics: response.data
                }))
            }
            else {
                notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, "Failed to fetch daily usage data");
            }
        } catch (error) {
            notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, error.message);
        }
    }   

    return {
        subscribe : usageStore.subscribe,
        fetchUsageMetrics,
        fetchDailyUsage
    }
    
}