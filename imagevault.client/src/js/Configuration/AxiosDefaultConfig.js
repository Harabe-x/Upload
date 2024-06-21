import {getAuthStore} from "@/js/State/Auth/AuthStore.js";
import {get} from "svelte/store";
import axios from 'axios'



axios.defaults.baseURL = 'https://localhost:7110/api';


const setAuthToken = (token) => {

    const authStore = getAuthStore();

    authStore.pingAuth();

    const store = get(authStore);

    if(!store.isLoggedIn) return;

    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
}

export { setAuthToken }
