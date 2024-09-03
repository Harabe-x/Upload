import {writable} from "svelte/store";


const collectionBrowserStore = writable(
    {
        collectionName : ""
    }
)


export function getCollectionBrowserStore()
{
    return collectionBrowserStore; 
}