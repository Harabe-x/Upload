import {get, writable} from "svelte/store";
import axios from "axios";      
import {
    HTTP_STATUS_OK,
    GET_API_KEY_LOGS_ENDPOINT,
    COLLECTION_LIST_ENDPOINT_URL,
    NOTIFICATION_TYPE_ERROR
} from "@/js/Constants.js"; 

const apiKeyLogStore = writable({ 
    apiKeyLogs: [],
    limit : 12
})
export function getApiKeyLogStore() {

    const fetchApiKeysLogs = async (apiKey,page) => {
        try {

            var storeData = get(apiKeyLogStore)

            const response = await axios.post(  GET_API_KEY_LOGS_ENDPOINT, {apiKey,limit:storeData.limit, page }, {});

        apiKeyLogStore.update((state) =>  ({ 
            ...state, 
            apiKeyLogs : response.data  
        }))

        }
        catch (error)
        {
            notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, "API key log fetching failed")
        }

    }
    
    const getAllLogs = async (apiKey) => 
    {
        try {

            var storeData = get(apiKeyLogStore)

            const response = await axios.post(  GET_API_KEY_LOGS_ENDPOINT, {apiKey,limit:99999, page:1  }, {});

            return response.data; 
        }
        catch (error)
        {
            notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, "API key log fetching failed")
        }

    }

    return {
        subscribe : apiKeyLogStore.subscribe,
        fetchApiKeysLogs,
        getAllLogs
    }
}