import {writable} from "svelte/store";
import { Chance } from "chance";

const notificationStore = writable([])

export function getNotificationsStore()
{
    const chance = new Chance();

    return {
        subscribe : notificationStore.subscribe,

        sendNotification : function(type,message)
        {
            notificationStore.update(value => {
                return [...value,   {id: chance.guid(),  type: type, message: message}];
            })
        },

        removeLastNotification : function()
        {
            notificationStore.update(value => {
                return value.slice(0,-1);
            });
        }
    }
}


