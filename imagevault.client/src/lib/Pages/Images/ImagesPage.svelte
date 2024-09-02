<script>
     import PageTopMenu from "../../Controls/Shared/PageTopMenu.svelte";
     import IconButton from "../../Controls/Buttons/IconButton.svelte";
     import Card from "../../Controls/Cards/Card.svelte";
     import CollectionCard from "./Components/CollectionCard.svelte";
     import ImageFrame from "./Components/ImageFrame.svelte";
     import DataPaginator from "../../Controls/Shared/DataPaginator.svelte";
     import SelectInput from "../../Controls/Inputs/SelectInput.svelte";
     import CollectionBrowser from "./CollectionBrowser.svelte";
     import ModalWindow from "../../Controls/Shared/ModalWindow.svelte";
     import { ArrowPath, ArrowRight, Plus } from "svelte-hero-icons";
     import { getPhotoList } from "../../../js/Temp/PhotoPlaceholderApi";
     import { getNavigationStore } from "../../../js/Temp/NavigationStore";
     import {getSelectedCollectionDataStore} from "../../../js/Temp/SelectedCollectionStore.js";
     import createImageBrowserStore from "../../../js/ImageBrowserStore.js";
     import {onMount} from "svelte";
     import {getApiKeys} from "../../../js/Temp/ApiKeysData.js";
     import ApiKeySelector from "@/lib/Controls/Shared/ApiKeySelector.svelte";
     import {getImageManagerStore} from "@/js/State/Image/ImageStore.js";
     import {getApiKeyStore} from "@/js/State/ApiKey/ApiKeyStore.js";
     import {get, writable} from "svelte/store";

     const imageBrowserStore = createImageBrowserStore('')
     const selectedCollectionDataStore = getSelectedCollectionDataStore()
     const navigationStore = getNavigationStore();
     const collectionData = [ {CollectionName:'Shoes', someFutureProperty: 'foo' },{CollectionName:'Almond Blossom', someFutureProperty: 'foo' },{CollectionName:'Forerst', someFutureProperty: 'Coffee' },{CollectionName:'Laptop', someFutureProperty: 'foo' }  ]
     const imageManagerStore = getImageManagerStore(); 
     const apiKeyStore = getApiKeyStore();
     let nextPageFunction; 
     let previousPageFunction;
     let selectedImage = writable(0)
     let imgPages = 10; // Here will be method for fetching totalImgPages
     let imageModalToggleFunction;
     let addImageModalToggleFunction;
     let addCollectionModalToggleFunction;
     let addImageData;
     onMount(async () => {
          await fetchNecessaryData()
     })

     async function fetchNecessaryData() 
     {
          const apiStoreValue =   get(apiKeyStore)
          await imageManagerStore.fetchCollections(apiStoreValue.selectedKey.key);
          await imageManagerStore.fetchImages(apiStoreValue.selectedKey.key,"default",30,1)
     }
     
     function changePage(event)
     {
          imageBrowserStore.goToSelectedPage(event.detail)
     }
     function openCollection(collectionData)
     {
        $selectedCollectionDataStore = collectionData;
        $navigationStore =  CollectionBrowser;
     }
     
     function openAddImageDialog()
     {
          const apiKeyData = get(apiKeyStore)
          const imageManagerData = get(imageManagerStore)
          addImageData = writable({
               collections:imageManagerData.collections,
               key : apiKeyData.selectedKey.key
          })
          
          addImageModalToggleFunction(); 
     }
     function openImageBrowserWindow(event)
     {
          alert(event.detail)
          alert($imageManagerStore.imagaes.indexOf(event.detail)) ; imageModalToggleFunction();
     }
     
</script>

<div>
<PageTopMenu>
     <div slot="leftSide">
          <ApiKeySelector on:change={fetchNecessaryData}></ApiKeySelector>
     </div>
<div slot="rightSide">
     <IconButton iconStyle="w-4 mr-1" icon={ArrowPath}>
          Refresh
     </IconButton>
</div>

</PageTopMenu>

</div>

<div class="grid grid-rows-1">
     <Card title="Your collections"> 
          <div slot="titleControl" class="ml-auto">
               <IconButton on:click={() => { addCollectionModalToggleFunction() }} icon={Plus}  iconStyle="w-4">  Add Collection </IconButton>
        </div>
          <div class="carousel-with-scroll w-full  h-64 lg:h-56 xl:h-64 sm:h max-w  space-x-4 bg-ghost-100 rounded-box gap-3 ">

               {#each $imageManagerStore.collections as collection (collection.id)}
                    <CollectionCard on:click={() => { openCollection(collection) }}  collection={{CollectionName: collection.collectionName}} imgSrc="https://img.daisyui.com/images/stock/photo-1606107557195-0e29a4b5b4aa.jpg"> </CollectionCard>
               {/each}

               <div class="flex items-center">
                     <IconButton  buttonStyle="ml-auto" flipIcons={true} icon={ArrowRight} iconStyle="w-4"> See all collections </IconButton>     
               </div>
               
             </div>
          </Card>
</div>    

<div class="grid grid-cols-1 grid-rows-1"> 
     <Card title="All Images"> 
          <div slot="titleControl" class="ml-auto">
               <IconButton icon={Plus} iconStyle="w-4" on:click={openAddImageDialog}>  Add Image </IconButton>
          </div>
          <div class="grid lg:grid-cols-4 md:grid-cols-2 sm:grid-cols-1 gap-10">
               {#each $imageManagerStore.images as image (image.key)}
                    <button on:click={() => { selectedImage.set($imageManagerStore.images.indexOf(image));imageModalToggleFunction(); }}>
                         <ImageFrame imgTitle={image.title} imgSrc={image.imageUrl}></ImageFrame>
                    </button>
                    
               {/each}
          </div>
          {#if imgPages > 1}
          <div class="flex flex-row items-center justify-center mr-4  mt-5">
          <DataPaginator bind:nextPage={nextPageFunction} bind:previousPage={previousPageFunction} on:navigatedToNextPage={changePage} on:navigatedToPreviousPage={changePage} ></DataPaginator>
          </div>
     {/if}
     </Card> 
</div>


 <ModalWindow bind:toggleModal={imageModalToggleFunction} param={selectedImage} type="ImageBrowserModal" ></ModalWindow>
<ModalWindow bind:toggleModal={addImageModalToggleFunction}  param={addImageData} type="AddImageModal" ></ModalWindow>
<ModalWindow bind:toggleModal={addCollectionModalToggleFunction} param={$apiKeyStore.selectedKey} type="AddCollectionModal"></ModalWindow>