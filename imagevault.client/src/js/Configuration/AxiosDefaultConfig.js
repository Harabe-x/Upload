import {getAuthStore} from "@/js/State/Auth/AuthStore.js";
import {get} from "svelte/store";
import axios from 'axios'

const authStore = getAuthStore();
const applicationData = get(authStore);


axios.defaults.baseURL = 'http://localhost:7110';

if(applicationData.isLoggedIn)
{
    const token = applicationData.userDataModel.token;

    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
}

const setAuthToken = (token) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
}

export { setAuthToken }
