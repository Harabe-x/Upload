import {getAuthStore} from "@/js/State/Auth/AuthStore.js";
import {get} from "svelte/store";
import axios from 'axios'



axios.defaults.baseURL = 'https://localhost:7110/api';



const setAuthToken = (token) => {

    // const authStore = getAuthStore();
    //
    // authStore.pingAuth();
    //
    // const store = get(authStore);
    //
    // axios.interceptors.request.use(config => {
    //
    //     if(!store.token) return;
    //
    //     config.headers['Authorization'] = `Bearer ${token}`;
    //
    //     return config;
    // },
    //     error => {
    //         return Promise.reject(error);
    //     }
    // )
}




export { setAuthToken }
