import {writable} from "svelte/store";


const collectionStoreData = { CollectionName: "", someFutureProperty: 'xd'  }

const collectionDataStore = writable(collectionStoreData);

export function getSelectedCollectionDataStore()
{
    return collectionDataStore;
}