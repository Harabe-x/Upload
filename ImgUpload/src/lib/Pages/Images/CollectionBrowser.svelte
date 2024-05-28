  <script>
    import {ArrowLeft, ArrowPath, Icon, Plus} from "svelte-hero-icons";
    import { getPhotoList } from "../../../js/Temp/PhotoPlaceholderApi.js";
    import IconButton from "../../Controls/Buttons/IconButton.svelte";
    import PageTopMenu from "../../Controls/Shared/PageTopMenu.svelte";
    import DataFetchingInfo from "../../Controls/Shared/DataFetchingInfo.svelte";
    import ImageFrame from "./Components/ImageFrame.svelte";
    import ModalWindow from "../../Controls/Shared/ModalWindow.svelte";
    import DataPaginator from "../../Controls/Shared/DataPaginator.svelte";
    import Card from "../../Controls/Cards/Card.svelte";
    import {onMount} from "svelte";
    import PageHeaderNameStore from "../../../js/Temp/PageHeaderNameStore.js";
    import pageHeaderNameStore from "../../../js/Temp/PageHeaderNameStore.js";
    import {getSelectedCollectionDataStore} from "../../../js/Temp/SelectedCollectionStore.js";
    import ImagesPage from "./ImagesPage.svelte";
    import {getNavigationStore} from "../../../js/Temp/NavigationStore.js";

    const selectedPageStore = getSelectedCollectionDataStore();
    const pageHeaderStore = pageHeaderNameStore()
    const navigationStore = getNavigationStore()
    const imagesPerPage = 32;

    let promise =   getPhotoList(1,imagesPerPage);;
    let imageModalToggleFunction;
    let addImageModalToggleFunction;
    let selectedImage;


    onMount(() => {
        $pageHeaderStore = $selectedPageStore.CollectionName + ' Collection';
    })

    function goBack()
    {
        $pageHeaderStore = "Images"
        $navigationStore = ImagesPage;
    }

    function selectImage(image)
    {
        selectedImage = image;
    }

    function changePage(event)
    {
       promise = getPhotoList(event.detail,imagesPerPage)
    }

</script>

<PageTopMenu>
    <div slot="leftSide">
        <IconButton icon={ArrowLeft} on:click={goBack} iconStyle="w-4 ml-1 ">Back</IconButton>
    </div>

    <div slot="rightSide">
        <IconButton icon={ArrowPath} iconStyle="w-4 ml-1 ">Refresh</IconButton>
    </div>
</PageTopMenu>

<div class="grid grid-rows-1">
    <Card title="Collection Images" >
        <div  slot="titleControl" class="ml-auto">
            <IconButton icon={Plus} iconStyle="w-4" on:click={addImageModalToggleFunction} > Add Image  </IconButton>
        </div>
            {#await promise }
                <DataFetchingInfo></DataFetchingInfo>
            {:then result }

                <div class="grid lg:grid-cols-4 md:grid-cols-4 sm:grid-cols-1 gap-10">

                {#each result as item }
                    <button on:click={() => { selectImage(item.download_url); imageModalToggleFunction(); }}>
                        <ImageFrame imgSrc={item.download_url}></ImageFrame>
                    </button>
                {/each}
                </div>
            {:catch error}
                Something went wrong
            {/await}
        <div class="flex flex-row items-center justify-center mr-4  mt-5">
            <DataPaginator on:navigatedToNextPage={changePage} on:navigatedToPreviousPage={changePage} ></DataPaginator>
        </div>
    </Card>
</div>

<ModalWindow type="AddImageModal" bind:toggleModal={addImageModalToggleFunction} ></ModalWindow>
<ModalWindow type="ImageBrowserModal" bind:toggleModal={imageModalToggleFunction} param={selectedImage}> </ModalWindow>