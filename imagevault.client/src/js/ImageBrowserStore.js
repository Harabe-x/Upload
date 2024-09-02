import PreviousMap from "postcss/lib/previous-map";
import { writable, get } from "svelte/store";
import {HTTP_STATUS_OK} from "@/js/Constants.js"; 
export default function createImageBrowserStore(apiKey) {
    const store = writable({
        images: [],
        nextPageCallback: () => { },
        previousPageCallback: () => { },
        currentImage: 0,
        currentPage: 1,
        isLoading: false,
    });

    async function fetchImages(collectionName,limit,currentImage = 0) {
        if (limit === undefined) return;

        try {
            const { currentPage } = get(store);

            store.update(state => ({ ...state, isLoading: true    }))
            const response = await fetch(`https://picsum.photos/v2/list?page=${currentPage}&limit=${limit}`);
            const data = await response.json();
            const imageUrls = data.map(item => item.download_url);

            store.update(state => ({
                ...state,
                images: imageUrls,
                currentImage: currentImage,
                isLoading: false
            }));
        } catch (error) {
            console.error("Error fetching images:", error);
        }
    }

    return {
        subscribe: store.subscribe,

        nextImage: function () {
            store.update(state => {

                if(state.isLoading) return ;
                const nextImage = state.currentImage + 1;
                if (nextImage >= state.images.length) {
                    state.currentPage += 1;
                    state.nextPageCallback();
                    fetchImages(32);
                    return state;
                }
                return { ...state, currentImage: nextImage };
            });
        },

        previousImage: function () {
            store.update(state => {

                if(state.isLoading)  return;

                const prevImage = state.currentImage - 1;
                if (prevImage < 0) {
                    if (state.currentPage > 1) {
                        state.currentPage -= 1;
                        state.previousPageCallback();
                        fetchImages(32,state.images.length - 1);
                        return state;
                    }
                    return state;``
                }
                return { ...state, currentImage: prevImage };
            });
        },

        getCurrentImage: function () {
            const state = get(store);
            if (state.images.length === 0) {
                fetchImages(32);
                return '';
            }
            return state.images[state.currentImage];
        },
        fetchImages: fetchImages,
        goToSelectedPage : function(pageNumber)
        {

            store.update(state => {

                if(state.isLoading)  return;

                return {...state,
                    images: [],
                    currentImage: 0,
                    currentPage: pageNumber}
            });
            fetchImages(32);
        },
        selectImage: function(imageUrl)
        {

            if(typeof imageUrl !== 'string') return;

            store.update(state => ({
                ...state ,
                currentImage: state.images.indexOf(imageUrl),
            }))
        },
        setNextPageCallback : function(func) {
            store.update(state => ({
                ...state ,
                nextPageCallback:func,
            }))
        },
        setPreviousPageCallback : function(func) {
            store.update(state => ({
                ...state,
                previousPageCallback : func
            }))
        }

    };
}

