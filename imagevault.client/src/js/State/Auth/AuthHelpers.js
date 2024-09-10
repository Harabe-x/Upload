import {getAuthStore} from "@/js/State/Auth/AuthStore.js";
import { get } from "svelte/store"

const checkIfUserIsLoggedIn = (callback) => {
    const authStore = getAuthStore();

    const authStoreObject = get(authStore);

    if(!authStoreObject.isLoggedIn) return false;

    if(typeof callback === "function") {
        callback();
    }

    return true;
}
export { checkIfUserIsLoggedIn }
