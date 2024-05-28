import PreviousMap from "postcss/lib/previous-map";
import { writable, get } from "svelte/store";

export default function createImageBrowserStore(apiKey) {
    const store = writable({
        images: [],
        nextPageCallback: () => { },
        previousPageCallback: () => { },
        currentImage: 0,
        currentPage: 1,
    });

    async function fetchImages(limit) {
        if (limit === undefined) return;
        try {
            const { currentPage } = get(store);
            const response = await fetch(`https://picsum.photos/v2/list?page=${currentPage}&limit=${limit}`);
            const data = await response.json();
            const imageUrls = data.map(item => item.download_url);

            store.update(state => ({
                ...state,
                currentImage: 0,
                images: imageUrls
               ,
            }));
        } catch (error) {
            console.error("Error fetching images:", error);
        }
    }

    return {
        subscribe: store.subscribe,

        nextImage: function () {
            store.update(state => {
                const nextImage = state.currentImage + 1;
                if (nextImage >= state.images.length) {
                    state.currentPage += 1;
                    const _store = get(store);
                    _store.nextPageCallback();
                    fetchImages(32);
                    return state;
                }
                return { ...state, currentImage: nextImage };
            });
        },

        previousImage: function () {
            store.update(state => {
                const prevImage = state.currentImage - 1;
                if (prevImage < 0) {
                    if (state.currentPage > 1) {
                        state.currentPage -= 1;
                        const _store = get(store);
                        _store.previousPageCallback();
                        fetchImages(32);
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
            store.update(state => ({
                ...state,
                images: [],
                currentImage: 0,
                currentPage: pageNumber
            }));
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

