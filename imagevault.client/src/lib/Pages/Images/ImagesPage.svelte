<script>
     import PageTopMenu from "../../Controls/Shared/PageTopMenu.svelte";
     import IconButton from "../../Controls/Buttons/IconButton.svelte";
     import Card from "../../Controls/Cards/Card.svelte";
     import CollectionCard from "./Components/CollectionCard.svelte";
     import ImageFrame from "./Components/ImageFrame.svelte";
     import DataPaginator from "../../Controls/Shared/DataPaginator.svelte";
     import ModalWindow from "../../Controls/Shared/ModalWindow.svelte";
     import { ArrowPath, ArrowRight, Plus } from "svelte-hero-icons";
     import {onMount} from "svelte";
     import ApiKeySelector from "@/lib/Controls/Shared/ApiKeySelector.svelte";
     import {getImageManagerStore} from "@/js/State/Image/ImageStore.js";
     import {getApiKeyStore} from "@/js/State/ApiKey/ApiKeyStore.js";
     import {get, writable} from "svelte/store";
     import { getCollectionBrowserStore} from "@/js/State/Image/CollectionBrowserStore.js";
     import {navigate} from "svelte-routing";
     import PageHeaderNameStore from "@/js/Temp/PageHeaderNameStore.js";
     import NoImagesYet from "@/lib/Pages/Images/Components/NoImagesYet.svelte";
     
     const imageManagerStore = getImageManagerStore(); 
     const apiKeyStore = getApiKeyStore();
     const collectionBrowser = getCollectionBrowserStore()
     const pageHeaderStore = PageHeaderNameStore()
     let selectedImage = writable({ selectedIndex : 0, apiKey: "", collectionName:""  })
     let imgPages = 10; // Here will be method for fetching totalImgPages
     let imageModalToggleFunction;
     let addImageModalToggleFunction;
     let addCollectionModalToggleFunction;
     let addImageData;
     onMount(async () => {
          pageHeaderStore.set("Images")
          await fetchNecessaryData()
     })

     async function fetchNecessaryData() 
     {
          await apiKeyStore.fetchKeys()
          await imageManagerStore.fetchCollections($apiKeyStore.selectedKey.key);
          await imageManagerStore.fetchImages($apiKeyStore.selectedKey.key,"default",$imageManagerStore.limit,1)
     }
     
     async function nextPage()
     {
          if($imageManagerStore.images.length < $imageManagerStore.limit) return; 
          
          imageManagerStore.nextPage();
          await imageManagerStore.fetchImages($apiKeyStore.selectedKey.key,"default",$imageManagerStore.limit,$imageManagerStore.currentPage)
     }

     async function previousPage()
     {
          imageManagerStore.previousPage();
          await imageManagerStore.fetchImages($apiKeyStore.selectedKey.key,"default",$imageManagerStore.limit,$imageManagerStore.currentPage)
     }
     
     function openAddImageDialog()
     {
          const apiKeyData = get(apiKeyStore)
          const imageManagerData = get(imageManagerStore)
          addImageData = writable({
               collections:imageManagerData.collections,
               collection: "",
               key : apiKeyData.selectedKey.key
          })
          
          addImageModalToggleFunction(); 
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
                    <CollectionCard on:click={() => { collectionBrowser.set(collection); navigate("/app/collectionBrowser")}}  collection={{CollectionName: collection.collectionName}} imgSrc="https://img.daisyui.com/images/stock/photo-1606107557195-0e29a4b5b4aa.jpg"> </CollectionCard>
               {/each}
             </div>
          </Card>
</div>    

<div class="grid grid-cols-1 grid-rows-1"> 
     <Card title="All Images"> 
          <div slot="titleControl" class="ml-auto">
               <IconButton icon={Plus} iconStyle="w-4" on:click={openAddImageDialog}>  Add Image </IconButton>
          </div>


          {#if $imageManagerStore.images.length !== 0}
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
          {:else}
               <NoImagesYet> </NoImagesYet>
          {/if}


     </Card> 
</div>


 <ModalWindow bind:toggleModal={imageModalToggleFunction} param={selectedImage} type="ImageBrowserModal" ></ModalWindow>
<ModalWindow bind:toggleModal={addImageModalToggleFunction}  param={addImageData} type="AddImageModal" ></ModalWindow>
<ModalWindow bind:toggleModal={addCollectionModalToggleFunction} param={$apiKeyStore.selectedKey} type="AddCollectionModal"></ModalWindow>