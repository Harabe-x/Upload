<script>
     import PageTopMenu from "../../Controls/Shared/PageTopMenu.svelte";
     import IconButton from "../../Controls/Buttons/IconButton.svelte";
     import Card from "../../Controls/Cards/Card.svelte";
     import CollectionCard from "./Components/CollectionCard.svelte";
     import ImageFrame from "./Components/ImageFrame.svelte";
     import DataPaginator from "../../Controls/Shared/DataPaginator.svelte";
     import DataFetchingInfo from "../../Controls/Shared/DataFetchingInfo.svelte";
     import SelectInput from "../../Controls/Inputs/SelectInput.svelte";
     import CollectionBrowser from "./Components/CollectionBrowser.svelte";
     import ModalWindow from "../../Controls/Shared/ModalWindow.svelte";;
     import { ArrowPath, ArrowRight, Plus } from "svelte-hero-icons";
     import { getPhotoList } from "../../../js/Temp/PhotoPlaceholderApi";
     import { getNavigationStore } from "../../../js/Temp/NavigationStore";
     import {getSelectedCollectionDataStore} from "../../../js/Temp/SelectedCollectionStore.js";

     const selectedCollectionDataStore = getSelectedCollectionDataStore()
     const navigationStore = getNavigationStore();
     let promise =  getPhotoList(1,32);
     let selectedImage;
     let imgPages = 10; // Here will be method for fetching totalImgPages 

     let imageModalToggleFunction;
     let addImageModalToggleFunction;
     let addCollectionModalToggleFunction;


     // Sample data
     var collectionData = [ {CollectionName:'Shoes', someFutureProperty: 'foo' },{CollectionName:'Almond Blossom', someFutureProperty: 'foo' },{CollectionName:'Forerst', someFutureProperty: 'Coffee' },{CollectionName:'Laptop', someFutureProperty: 'foo' }  ]

     function changePage(event)
     {
       promise = getPhotoList(event.detail,32)
     }
     function openCollection(collectionData)
     {
        $selectedCollectionDataStore = collectionData;
        $navigationStore =  CollectionBrowser;
     }
        


</script>

<div>
<PageTopMenu>
     <div slot="leftSide">
          <SelectInput title="Selected key:"></SelectInput>
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
               <IconButton on:click={() => { addCollectionModalToggleFunction(); }} icon={Plus} iconStyle="w-4">  Add Collection </IconButton>
        </div>
          <div class="carousel-with-scroll w-full  h-64 lg:h-56 xl:h-64 sm:h max-w  space-x-4 bg-ghost-100 rounded-box gap-3   carusel-scroll ">

               {#each collectionData as item}
                    <CollectionCard on:click={() => { openCollection(item) }}  collection={{CollectionName: item.CollectionName}} imgSrc="https://img.daisyui.com/images/stock/photo-1606107557195-0e29a4b5b4aa.jpg"> </CollectionCard>
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
               <IconButton icon={Plus} iconStyle="w-4" on:click={() =>{ addImageModalToggleFunction() }}>  Add Image </IconButton>
          </div>

          {#await promise }
               <DataFetchingInfo> </DataFetchingInfo>
          {:then result}
               <div class="grid lg:grid-cols-4 md:grid-cols-2 sm:grid-cols-1 gap-10"> 
               {#each result as photo }
                    
               <button on:click={() => { selectedImage = photo.download_url; imageModalToggleFunction(); }}>
                    <ImageFrame imgTitle={photo.author} imgSrc={photo.download_url}></ImageFrame>
               </button>
                    
               {/each} 
               </div>

               {:catch error}
               <div>
                    <span> Error occured : {error} </span>
               </div>
          {/await}
          {#if imgPages > 1}
          <div class="flex flex-row items-center justify-center mr-4  mt-5">
          <DataPaginator on:navigatedToNextPage={changePage} on:navigatedToPreviousPage={changePage} ></DataPaginator>
          </div>
     {/if}
     </Card> 
</div>


 <ModalWindow bind:toggleModal={imageModalToggleFunction} param={selectedImage} type="ImageBrowserModal" ></ModalWindow>
<ModalWindow bind:toggleModal={addImageModalToggleFunction} type="AddImageModal" ></ModalWindow>
<ModalWindow bind:toggleModal={addCollectionModalToggleFunction} type="AddCollectionModal"></ModalWindow>