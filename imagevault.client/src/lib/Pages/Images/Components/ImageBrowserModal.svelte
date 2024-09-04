<script>
    import { createEventDispatcher } from 'svelte';
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";
    import {
        ArrowDownTray,
        ArrowUpOnSquare,
        ChevronLeft,
        ChevronRight,
        EllipsisVertical,
        Icon, InformationCircle, Trash,
        XMark
    } from "svelte-hero-icons";
    import IconDropdown from "../../../Controls/Dropdowns/IconDropdown.svelte";
    import {onEscapeAction,onArrowLeftAction,onArrowRight} from "../../../../js/UserInterface/Actions/ModalActions.js";
    import {getImageManagerStore} from "@/js/State/Image/ImageStore.js";
    import {getNotificationsStore} from "@/js/State/UserInterface/ToastNotificationStore.js";
    import {NOTIFICATION_TYPE_SUCCESS} from "@/js/Constants.js";
    import {getApiKeyStore} from "@/js/State/ApiKey/ApiKeyStore.js";
    import ModalWindow from "@/lib/Controls/Shared/ModalWindow.svelte";
    import {writable} from "svelte/store";

    export let isModalVisable = false;
    export let param;
    const imageManager = getImageManagerStore(); 
    const dispatcher = createEventDispatcher();
    const notificationStore = getNotificationsStore();
    const imageDeleteStore = writable({ 
        apiKey : $param.apiKey.key,
        collection : $param.collectionName,
        imageKey : "", 
    })
    let fileInfoStore = writable({});
    
    let apiKeyStore = getApiKeyStore();
    let currentIndex = $param.selectedIndex;
    let currentImage;
    let openDeleteModalFunction;
    let openFileInfoModalFunction;
    
    function closeModal() {
        dispatcher('modalClosed');
    }
   async function nextImage()
    {
        if(currentIndex + 1 >= $imageManager.images.length )
        {
            if( $imageManager.images.length < $imageManager.limit) return;

            currentIndex = 0;
            imageManager.nextPage();
            await imageManager.fetchImages($param.apiKey.key, $param.collectionName,$imageManager.limit,$imageManager.currentPage)
            return;
        }

        currentIndex += 1 ;
    }
    async function previousImage()
    {
        if(currentIndex - 1 < 0 )
        {
            imageManager.previousPage();
            await imageManager.fetchImages($param.apiKey.key, $param.collectionName,$imageManager.limit,$imageManager.currentPage)
            currentIndex =  $imageManager.images.length - 1;
            return;
        }

        currentIndex -= 1;
    }
    
    function sendNotification(message)
    {
        notificationStore.sendNotification(NOTIFICATION_TYPE_SUCCESS,message)
    }


</script>

{#if isModalVisable }
    <div class="modal modal-open flex flex-col justify-center items-center"  use:onEscapeAction on:escape={closeModal}  >
        <div class="w-3/4 h-3/4 bg-base-300 rounded-xl">
            <!-- Top Panel  -->
            <div class="flex flex-row w-full h-[15%]">
                <div class="flex w-1/3 items-center ml-3">
                    <IconButton on:click={closeModal} icon={XMark} iconStyle="w-5"></IconButton>
                </div>
                <div class="flex justify-center items-center w-1/3">
                    <span class="text-xl">ImageVault</span>
                </div>
                <div class="flex items-center w-1/3 justify-end mr-3 gap-3 sm:gap-1">
                    <div class="block sm:hidden">
                        <IconDropdown icon={EllipsisVertical}>
                            <li><button><Icon src={ArrowDownTray} class="w-4" />Download</button></li>
                            <li ><button><Icon src={ArrowUpOnSquare} class="w-4" />Share</button></li>
                            <li><button  on:click={() => { imageDeleteStore.update(state => { return {...state , imageKey:$imageManager.images[currentIndex].key }}) ; openDeleteModalFunction();  }} ><Icon src={Trash} class="w-4" />Delete</button></li>
                            <li><button><Icon src={InformationCircle} class="w-4" />File info</button></li>
                        </IconDropdown>
                    </div>

                    <!-- Buttons and dropdown for larger screens (tablet and desktop) -->
                    <div class="hidden sm:flex items-center gap-1">
                        <IconButton icon={ArrowUpOnSquare}   iconStyle="w-5" flipIcons={true}></IconButton>
                        <IconButton icon={ArrowDownTray} iconStyle="w-5 sm:w-3" flipIcons={true} buttonStyle="bg-success text-primary-content">Download</IconButton>
                        <IconDropdown icon={EllipsisVertical}>
                            <li><button  on:click={() => { imageDeleteStore.update(state => { return {...state , imageKey:$imageManager.images[currentIndex].key }}) ; openDeleteModalFunction();  }} ><Icon src={Trash} class="w-4" />Delete</button></li>
                            <li><button on:click={() => { fileInfoStore.set($imageManager.images[currentIndex]); openFileInfoModalFunction(); }}><Icon src={InformationCircle} class="w-4" />File info</button></li>
                        </IconDropdown> 
                    </div>
                </div>
            </div>

            <div class="h-[70%] w-full flex flex-row">
                <div class="h-full w-[15%] flex flex-col items-center justify-center" use:onArrowLeftAction on:arrowLeft={previousImage}  >
                    <IconButton on:click={previousImage} icon={ChevronLeft} iconStyle="w-8"></IconButton>
                </div>

                <div class="w-[70%] flex justify-center items-center overflow-hidden">
                    {#key currentImage}
                        {#if  $imageManager.images.length !== 0}
                        <img src={ "https://"+$imageManager.images[currentIndex].imageUrl}   class="max-w-full max-h-full h-full w-full object-contain rounded-xl overflow-auto">
                            {:else}
                              <span class="text-2xl"> You reached end of collection imagaes. </span>
                        {/if}
                    {/key}
                </div>


                <div class="h-full w-[15%] flex flex-col items-center justify-center" use:onArrowRight on:arrowRight={nextImage}  >
                    <IconButton on:click={nextImage} icon={ChevronRight} iconStyle="w-8"></IconButton>
                </div>
            </div>

            <!-- Info Section -->
            <div class="h-[15%] flex flex-row">

            </div>
        </div>
    </div>
{/if}

<ModalWindow bind:toggleModal={openDeleteModalFunction} param={imageDeleteStore} type="DeleteImageModal"></ModalWindow>
<ModalWindow bind:toggleModal={openFileInfoModalFunction} param={fileInfoStore} type="ImageInfoModal"></ModalWindow>
