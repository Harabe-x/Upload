  <script>
    import {ArrowLeft, ArrowPath, Icon, Plus} from "svelte-hero-icons";
    import IconButton from "../../Controls/Buttons/IconButton.svelte";
    import PageTopMenu from "../../Controls/Shared/PageTopMenu.svelte";
    import ImageFrame from "./Components/ImageFrame.svelte";
    import ModalWindow from "../../Controls/Shared/ModalWindow.svelte";
    import DataPaginator from "../../Controls/Shared/DataPaginator.svelte";
    import Card from "../../Controls/Cards/Card.svelte";
    import {onMount} from "svelte";
    import pageHeaderNameStore from "../../../js/Temp/PageHeaderNameStore.js";
    import {getSelectedCollectionDataStore} from "../../../js/Temp/SelectedCollectionStore.js";
    import ImagesPage from "./ImagesPage.svelte";
    import {getNavigationStore} from "../../../js/Temp/NavigationStore.js";
    import createImageBrowserStore from "../../../js/ImageBrowserStore.js";
    import {get} from "svelte/store";

    const selectedPageStore = getSelectedCollectionDataStore();
    const pageHeaderStore = pageHeaderNameStore()
    const navigationStore = getNavigationStore()
    const imageBrowserStore = createImageBrowserStore();
    const imagesPerPage = 32;

    let imageModalToggleFunction;
    let addImageModalToggleFunction;
    let nextPageFunction;
    let previousPageFunction;

    onMount(() => {
        pageHeaderStore.update(_ => {
            return get(selectedPageStore).CollectionName + ' Collection';
        })

        imageBrowserStore.setNextPageCallback(nextPageFunction)
        imageBrowserStore.setPreviousPageCallback(previousPageFunction)
        imageBrowserStore.fetchImages(imagesPerPage)
    })

    function changePage(event)
    {
        imageBrowserStore.goToSelectedPage(event.detail)
    }

    function goBack()
    {
        pageHeaderStore.update(_ => { return "Images"})
        navigationStore.update(_ => { return ImagesPage });
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
            <div class="grid lg:grid-cols-4 md:grid-cols-4 sm:grid-cols-1 gap-10">
            {#each  $imageBrowserStore.images as image (image)}
                <button on:click={() => { imageBrowserStore.selectImage(image);  imageModalToggleFunction() }}>
                    <ImageFrame imgSrc={image}></ImageFrame>
                </button>
            {/each}
        </div>

        <div class="flex flex-row items-center justify-center mr-4  mt-5">
            <DataPaginator on:navigatedToNextPage={changePage} on:navigatedToPreviousPage={changePage} bind:nextPage={nextPageFunction} bind:previousPage={previousPageFunction}></DataPaginator>
        </div>
    </Card>
</div>

<ModalWindow type="AddImageModal" bind:toggleModal={addImageModalToggleFunction} ></ModalWindow>
<ModalWindow type="ImageBrowserModal" param={imageBrowserStore} bind:toggleModal={imageModalToggleFunction}> </ModalWindow>