<script>
  import { ArrowPath, ArrowRight } from "svelte-hero-icons";
     import PageTopMenu from "../../Controls/Shared/PageTopMenu.svelte";
     import IconButton from "../../Controls/Buttons/IconButton.svelte";
     import Card from "../../DataPresenters/Cards/Card.svelte";
     import CollectionCard from "./Components/CollectionCard.svelte";
     import { getPhotoList } from "../../../js/Temp/PhotoPlaceholderApi";
     import ImageFrame from "./Components/ImageFrame.svelte";
     import PictureModal from "./Components/PictureModal.svelte";
     import { Result } from "postcss";
     
     let promise =  getPhotoList(2,32);
     let isPictureModalVisable = false; 
     let selectedPicture; 
 
     function openModal(item)
     {
          selectedPicture = item; 
          toggleModal();
     }
     function toggleModal()
     {
          isPictureModalVisable = !isPictureModalVisable;
     }

</script>


<PageTopMenu> 
     <IconButton iconStyle="w-4 mr-1" icon={ArrowPath}>
          Refresh
     </IconButton>            
</PageTopMenu>

<div class="grid grid-rows-1">
     <Card title="Your collections"> 

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
          {#await promise }
               Waiting
          {:then result} 
               <div class="grid lg:grid-cols-4 md:grid-cols-2 sm:grid-cols-1 gap-10"> 
               {#each result as photo }
                    
               <button on:click={() => { openModal(photo.download_url)}}> 
                    <ImageFrame imgDescription={photo.author} imgSrc={photo.download_url}></ImageFrame>
               </button>
                    
               {/each} 
               </div>
          {/await}
     </Card> 
</div>


<PictureModal imgSrc={selectedPicture} on:modalClosed={toggleModal} isModalVisable={isPictureModalVisable} ></PictureModal>