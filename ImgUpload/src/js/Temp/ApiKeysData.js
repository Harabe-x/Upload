import { getRandomGuid,getRandomNumber } from "./DummyDataGenerator";
     
const apiKeysData = [
    { Name:'Main' , Key: getRandomGuid() , Storage:getRandomNumber(1,200) },
    { Name:'AI Generation App' , Key: getRandomGuid() , Storage:getRandomNumber(1,200) },
    { Name:'Social Media App' , Key: getRandomGuid() , Storage: getRandomNumber(1,200) } ,
    { Name:'Screenshots App' , Key: getRandomGuid() , Storage:getRandomNumber(1,200)  },

 ]


export function getApiKeys()
{
    return apiKeysData; 
}