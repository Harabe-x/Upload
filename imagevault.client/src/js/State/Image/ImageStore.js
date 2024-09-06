import {get, writable} from "svelte/store";
import {
    HTTP_STATUS_OK,
    IMAGE_DELETE_ENDPOINT_URL,
    IMAGE_EDIT_ENDPOINT_URL,
    IMAGE_GET_PAGED_ENDPOINT_URL,
    COLLECTION_CREATE_ENDPOINT_URL,
    COLLECTION_LIST_ENDPOINT_URL,
    APIKEY_ADD_ENDPOINT_URL,
    APIKEY_EDIT_ENDPOINT_URL,
    NOTIFICATION_TYPE_ERROR,
    NOTIFICATION_TYPE_SUCCESS,
    UPLOAD_ENDPOINT_URL, HTTP_NO_CONTENT
} from "@/js/Constants.js";
import axios from "axios";  
import { getAuthStore} from "@/js/State/Auth/AuthStore.js";
import {getNotificationsStore} from "@/js/State/UserInterface/ToastNotificationStore.js";
;



const imageStore = writable({
      collections : [], 
      images : [], 
      currentPage: 1,
      limit : 16, 
      nextPageCallback : () => { }, 
      previousPageCallback : () => { },
    })
export function getImageManagerStore()
{
    const notificationStore = getNotificationsStore()
    const authStore = getAuthStore(); 
    
    function handleUnauthorized()
    {
    }
    
     const fetchImages = async (key,collection,limit, page ) => {
         try
         {
             const response = await axios.post(IMAGE_GET_PAGED_ENDPOINT_URL, { apiKey: key, collectionName: collection , page , limit }, {} );

             if (response.status !== HTTP_STATUS_OK)
             {
                 notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, response.data)
             }

             imageStore.update(state => ({
                 ...state,
                 images : response.data
             }))
         }
         catch (error)
         {
             notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, "Collection fetching failed")
         }
     }
     
     const deleteImage = async (key,collectionName, imageKey ) => {

         try
         {
             const response = await axios.delete(IMAGE_DELETE_ENDPOINT_URL,{ data: { apiKey:key,collectionName, imageKey }} );

             if (response.status !== HTTP_NO_CONTENT)
             {
                 notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, response.data)
                 return; 
             }
             
             const currentStoreData = get(imageStore)
             await fetchImages(key,  collectionName, currentStoreData.limit, currentStoreData.currentPage)
             
             notificationStore.sendNotification(NOTIFICATION_TYPE_SUCCESS, "Image deleted successfully")
         }
         catch (error)
         {
             notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, "Image deleting failed")
         }
    }

     
     const fetchCollections = async (key) => { 
         try 
         {

             const response = await axios.post(COLLECTION_LIST_ENDPOINT_URL, { key }, {} );
             
             if (response.status !== HTTP_STATUS_OK)
             {
                 notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, response.data)
             }
             
             imageStore.update(state => ({ 
                 ...state, 
                 collections: response.data 
             }))
         }
         catch (error)
         { 
             notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, "Collection fetching failed")
         }
     }
    
     const addCollection = async (key,collectionName , collectionDescription) => {
         try
         {

             const response = await axios.post(COLLECTION_CREATE_ENDPOINT_URL, { apiKey:key,collectionName,collectionDescription  }, {} );

             if (response.status !== HTTP_STATUS_OK)
             {
                 notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, response.data)
             }
             
             await fetchCollections(key);
             
             notificationStore.sendNotification(NOTIFICATION_TYPE_SUCCESS, "Collection created.")
         }
         catch (error)
         {
             notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, "Collection creating failed")
         }
     }
     const clearFetchedData = () => {
         imageStore.update((state) => ({ 
             ...state, 
             images: [],
             collections: [],
         }))
     }
     
     const uploadImage = async (image, key,collection,imageName,imageDescription, useCompression) => { 
         const formData = new FormData();
         formData.append("Image", image[0])
         formData.append("ApiKey", key )
         formData.append("CollectionName",collection)
         formData.append("Title" , imageName)
         formData.append("Description", imageDescription)
         formData.append("useCompression" , useCompression)
         
         try {
             const response = await axios.post(UPLOAD_ENDPOINT_URL,formData);

             if (response.status !== HTTP_STATUS_OK)
             {
                 notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, response.data)
             }

             const currentStoreData = get(imageStore)
             
             
             await fetchImages(key, collection,currentStoreData.limit,currentStoreData.currentPage)
             
             notificationStore.sendNotification(NOTIFICATION_TYPE_SUCCESS, "Uploaded image successfully")
             
         }
         catch(error)
         {
             notificationStore.sendNotification(NOTIFICATION_TYPE_ERROR, "Image upload failed")
         }
     }
     const nextPage = () => { 
        imageStore.update((state) =>  ({
            ...state,
            currentPage: state.currentPage + 1 
        })) 
     }
     const previousPage = () => {
         imageStore.update((state) => {
             if (state.currentPage === 1) return state;

             return {...state, currentPage: state.currentPage - 1}
         });

      }

    return { 
        subscribe : imageStore.subscribe, 
        fetchImages,
        fetchCollections, 
        addCollection, 
        deleteImage, 
        clearFetchedData,
        uploadImage,
        nextPage,
        previousPage,
    }
}

        