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

function deleteKey(key)
{
    // This function will delete keys from the cloud and fetch data

    store.update(state =>  {
        state.apiKeys.splice(state.apiKeys.indexOf(name), 1);
        return state;
    })

}

function addApiKey(name,storage)
{
       store.update(state => {

          state.apiKeys.push({Id:state.apiKeys.length,Name:name,Key:chance.guid(),Storage:storage})


           return state;
       })
}
function updateKey(key, newName, newStorageValue) {
    store.update(state => {

        const index = state.apiKeys.indexOf(key)


        if (index !== -1) {
            state.apiKeys[index] = {
                ...state.apiKeys[index],
                Name: newName,
                Storage: newStorageValue
            };
        }

        return state;
    });
}

export function getApiKeys()
{

    return {

        subscribe : store.subscribe,

        updateKey:updateKey,

        deleteKey:deleteKey,

        addApiKey:addApiKey,

        fetchKeys : fetchKeys,

        selectKey : selectKey
    }
}