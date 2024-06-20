import {writable} from "svelte/store";
import axios from "axios";
import { UserDataModel } from "@/js/Models/UserDataModel.js";



const store = writable(
    {
        userDataModel: null,
        isLoggedIn: false
    }
)

export function getAuthStore()
{

    return {
        subscribe: store.subscribe,
        login : async function (email,password)
        {
         axios.post("https://localhost:7110/api/auth/login", {email: email, password: password})
             .then(function (response) {
                if(response.status !== 200) return;

                console.log("logged in")
                store.update( value => ({
                    userDataModel: new UserDataModel(response.data.token,response.data.name,response.data.name,response.data.email,"darl"),
                    isLoggedIn: true
                }))


             })
        },
        logout : function ()
        {

        },
        register : function()
        {

        }

    }
}