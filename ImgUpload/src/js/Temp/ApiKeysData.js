import { getRandomGuid,getRandomNumber } from "./DummyDataGenerator";
import {get, writable} from "svelte/store";
const apiKeysData = [
    { Id:0, Name:'Main' , Key: getRandomGuid() , Storage:getRandomNumber(1,200) },
    { Id:1,Name:'AI Generation App' , Key: getRandomGuid() , Storage:getRandomNumber(1,200).toString()  },
    { Id:2,Name:'Social Media App' , Key: getRandomGuid() , Storage: getRandomNumber(1,200).toString()  } ,
    { Id:3,Name:'Screenshots App' , Key: getRandomGuid() , Storage:getRandomNumber(1,200).toString()  },

 ]

    const  store = writable(
        {
            apiKeys :  [],
            selectedApiKey : {}
        }
    )


function fetchKeys ()
{

    store.update(state => {
            state.apiKeys = apiKeysData;

            if(state.selectedApiKey === {} ) {
                selectKey('Main')
            }
            return state;
        })
}

function selectKey (name) {
    const keyStore = get(store);
    const selectedKey = keyStore.apiKeys.find(key => key.Name === name)

    console.log(selectedKey);

    if(!selectedKey) return;
    store.update(state  => ({
        ...state,
        selectedApiKey: selectedKey,
    }))
}

export function getApiKeys()
{

    return {

        subscribe : store.subscribe,

        fetchKeys : fetchKeys,

        selectKey : selectKey
    }
}