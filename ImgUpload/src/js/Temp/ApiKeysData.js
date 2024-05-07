import { getRandomGuid,getRandomNumber } from "./DummyDataGenerator";
import { writable } from "svelte/store";
const apiKeysData = [
    { Id:0, Name:'Main' , Key: getRandomGuid() , Storage:getRandomNumber(1,200) },
    { Id:1,Name:'AI Generation App' , Key: getRandomGuid() , Storage:getRandomNumber(1,200).toString()  },
    { Id:2,Name:'Social Media App' , Key: getRandomGuid() , Storage: getRandomNumber(1,200).toString()  } ,
    { Id:3,Name:'Screenshots App' , Key: getRandomGuid() , Storage:getRandomNumber(1,200).toString()  },

 ]

 const store = writable(apiKeysData)


export function getApiKeys()
{
    return store;
}

