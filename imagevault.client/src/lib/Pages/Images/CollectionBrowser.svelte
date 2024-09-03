<script>
    import PageTopMenu from "../../Controls/Shared/PageTopMenu.svelte";
    import IconButton from "../../Controls/Buttons/IconButton.svelte";
    import Card from "../../Controls/Cards/Card.svelte";
    import ImageFrame from "./Components/ImageFrame.svelte";
    import DataPaginator from "../../Controls/Shared/DataPaginator.svelte";
    import CollectionBrowser from "./CollectionBrowser.svelte";
    import ModalWindow from "../../Controls/Shared/ModalWindow.svelte";
    import {ArrowLeft, ArrowPath, ArrowRight, Plus} from "svelte-hero-icons";
    import {onMount} from "svelte";
    import {getImageManagerStore} from "@/js/State/Image/ImageStore.js";
    import {getApiKeyStore} from "@/js/State/ApiKey/ApiKeyStore.js";
    import {get, writable} from "svelte/store";
    import {getCollectionBrowserStore} from "@/js/State/Image/CollectionBrowserStore.js";
    import PageHeaderNameStore from "@/js/Temp/PageHeaderNameStore.js";
    import {navigate} from "svelte-routing";

    const imageManagerStore = getImageManagerStore();
    const apiKeyStore = getApiKeyStore();
    const collectionBrowserStore = getCollectionBrowserStore()
    const pageHeaderStore = PageHeaderNameStore()
        
    let selectedImage = writable({ selectedIndex : 0, apiKey: "", collectionName:""  })
    let imageModalToggleFunction;
    let addImageModalToggleFunction;
    let addCollectionModalToggleFunction;
    let addImageData;
    
    onMount(async () => {
        pageHeaderStore.set( $collectionBrowserStore.collectionName + " collection")
        await fetchNecessaryData()
    })

    async function fetchNecessaryData()
    {
        const apiStoreValue =   get(apiKeyStore)
        await imageManagerStore.fetchCollections(apiStoreValue.selectedKey.key);
        await imageManagerStore.fetchImages(apiStoreValue.selectedKey.key,$collectionBrowserStore.collectionName,$imageManagerStore.limit,1)
    }

    async function nextPage()
    {
        if($imageManagerStore.images.length < $imageManagerStore.limit) return;

        const apiStoreValue =   get(apiKeyStore)
        imageManagerStore.nextPage();
        await imageManagerStore.fetchImages(apiStoreValue.selectedKey.key,$collectionBrowserStore.collectionName,$imageManagerStore.limit,$imageManagerStore.currentPage)
    }

    async function previousPage()
    {
        const apiStoreValue =   get(apiKeyStore)
        imageManagerStore.previousPage();
        await imageManagerStore.fetchImages(apiStoreValue.selectedKey.key,$collectionBrowserStore.collectionName,$imageManagerStore.limit,$imageManagerStore.currentPage)
    }

    function openAddImageDialog()
    {
        const apiKeyData = get(apiKeyStore)
        const imageManagerData = get(imageManagerStore)
        addImageData = writable({
            collections:imageManagerData.collections,
            collection: $collectionBrowserStore.collectionName,
            key : apiKeyData.selectedKey.key,
        })

        addImageModalToggleFunction();
    }
    function backToImagePage()
    {
        navigate("/app/images")
    }

</script>

<div>
    <PageTopMenu>
        <div slot="leftSide">
            <IconButton on:click={backToImagePage} iconStyle="w-4 mr-1" icon={ArrowLeft}>
                Back
            </IconButton>
        </div>
        <div slot="rightSide">
            <IconButton iconStyle="w-4 mr-1" icon={ArrowPath}>
                Refresh
            </IconButton>
        </div>

    </PageTopMenu>

</div>


<div class="grid grid-cols-1 grid-rows-1">
    <Card title="All Images">
        <div slot="titleControl" class="ml-auto">
            <IconButton icon={Plus} iconStyle="w-4" on:click={openAddImageDialog}>  Add Image </IconButton>
        </div>
        <div class="grid lg:grid-cols-4 md:grid-cols-2 sm:grid-cols-1 gap-10">
            {#each $imageManagerStore.images as image (image.key)}
                <button on:click={() => { selectedImage.update((state) => ({ ...state, selectedIndex: $imageManagerStore.images.indexOf(image) , apiKey: $apiKeyStore.selectedKey, collectionName: image.collectionName  }));imageModalToggleFunction(); }}>
                    <ImageFrame imgTitle={image.title} imgSrc={image.imageUrl}></ImageFrame>
                </button>

            {/each}
        </div>
            <div class="flex flex-row items-center justify-center mr-4  mt-5">
                <DataPaginator currentPage={$imageManagerStore.currentPage} on:navigatedToNextPage={nextPage} on:navigatedToPreviousPage={previousPage} ></DataPaginator>
            </div>
    </Card>
</div>


<ModalWindow bind:toggleModal={imageModalToggleFunction} param={selectedImage} type="ImageBrowserModal" ></ModalWindow>
<ModalWindow bind:toggleModal={addImageModalToggleFunction}  param={addImageData} type="AddImageModal" ></ModalWindow>
<ModalWindow bind:toggleModal={addCollectionModalToggleFunction} param={$apiKeyStore.selectedKey} type="AddCollectionModal"></ModalWindow>