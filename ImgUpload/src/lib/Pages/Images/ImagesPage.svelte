<script>
     import PageTopMenu from "../../Controls/Shared/PageTopMenu.svelte";
     import IconButton from "../../Controls/Buttons/IconButton.svelte";
     import Card from "../../DataPresenters/Cards/Card.svelte";
     import CollectionCard from "./Components/CollectionCard.svelte";
     import ImageFrame from "./Components/ImageFrame.svelte";
     import DataPaginator from "../../Controls/Shared/DataPaginator.svelte";
     import ModalWindow from "../../Controls/Shared/ModalWindow.svelte";
     import { ArrowPath, ArrowRight, Plus } from "svelte-hero-icons";
     import { getPhotoList } from "../../../js/Temp/PhotoPlaceholderApi";

     let promise =  getPhotoList(1,256);
     let selectedImage;
     let imgPages = 10; // Here will be method for fetching totalImgPages 

     let imageModalToggleFunction;
     let addImageModalToggleFunction;
     let addCollectionModalToggleFunction;

     function changePage(event)
     {
       console.log(event)
       promise = getPhotoList(event.detail,256)
     }

     


</script>

<div>
<PageTopMenu> 
     <IconButton iconStyle="w-4 mr-1" icon={ArrowPath}>
          Refresh
     </IconButton>            
</PageTopMenu>

</div>

<div class="grid grid-rows-1">
     <Card title="Your collections"> 
          <div slot="titleControl" class="ml-auto">
               <IconButton icon={Plus} iconStyle="w-4">  Add Collection </IconButton>
        </div>
          <div class="carousel-with-scroll w-full  max-w p-4 space-x-4 bg-ghost-100 rounded-box  carusel-scroll ">

               {#each [1,1,1,1,1,1] as  item}
                    <CollectionCard collection={{Name:"Shoes"}} imgSrc="https://img.daisyui.com/images/stock/photo-1606107557195-0e29a4b5b4aa.jpg"> </CollectionCard>   
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
               Waiting
          {:then result} 
               <div class="grid lg:grid-cols-4 md:grid-cols-2 sm:grid-cols-1 gap-10"> 
               {#each result as photo }
                    
               <button on:click={() => { selectedImage = photo.download_url; imageModalToggleFunction(); console.log(imageModalToggleFunction) }}>
                    <ImageFrame imgTitle={photo.author} imgSrc={photo.download_url}></ImageFrame>
               </button>
                    
               {/each} 
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