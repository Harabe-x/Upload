<script>
    import { ArrowLeft, ArrowPath } from "svelte-hero-icons";
    import { getPhotoList } from "../../../../js/Temp/PhotoPlaceholderApi";
    import PageContent from "../../../Containers/PageContent.svelte";
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";
    import PageTopMenu from "../../../Controls/Shared/PageTopMenu.svelte";
    import DataFetchingInfo from "../../../Controls/Shared/DataFetchingInfo.svelte";
    import ImageFrame from "./ImageFrame.svelte";
    import ModalWindow from "../../../Controls/Shared/ModalWindow.svelte";
    import DataPaginator from "../../../Controls/Shared/DataPaginator.svelte";

    let currentPage = 5; 
    let imagesPerPage = 32;
    
    let promise =  getPhotoList(currentPage,imagesPerPage)
    let imageModalToggleFunction;
    let selectedImage;

    export let collection = null;

    function IncrementPage()
    {
        currentPage += 1;
        flipPage();
    }
    function selectImage(image)
    {
        selectedImage = image;
    }
    function decrementPage()
    {
        if(currentPage - 1 < 1 ) return; 
        currentPage -= 1;
        flipPage();
    }
    function flipPage()
    {
        promise = getPhotoList(currentPage,imagesPerPage)
    }

</script>

<PageContent>
     <div slot="leftSide">
            <IconButton icon={ArrowLeft} iconStyle="w-4 ml-1 ">Back</IconButton>
     </div>

     <div slot="rightSide">
        <IconButton icon={ArrowPath} iconStyle="w-4 ml-1 ">Refresh</IconButton>
     </div>
</PageContent>

<div class="grid grid-cols-4 md:grid-cols-2 sm:grid-cols-1 gap-10">
    {#await promise }
        <DataFetchingInfo></DataFetchingInfo>
    {:then result } 
        

      {#each result as item }

            <button on:click={() => { selectImage(item.download_url); imageModalToggleFunction(); }}>
                <ImageFrame imgSrc={item.download_url}></ImageFrame>
            </button>
      {/each}   

    {:catch error}
        Something went wrong 
    {/await}
<DataPaginator on:navigatedToNextPage={IncrementPage} on:navigatedToPreviousPage={decrementPage} ></DataPaginator>
</div>

<ModalWindow type="imageBrowserModal" bind:toggleModal={imageModalToggleFunction} param={selectedImage}> </ModalWindow>