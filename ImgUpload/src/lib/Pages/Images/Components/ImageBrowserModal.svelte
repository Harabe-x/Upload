<script> 
    import { createEventDispatcher } from 'svelte'
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";
    import {ArrowDownTray, ArrowUpOnSquare, XMark} from "svelte-hero-icons";

    export let isModalVisable = false; 
    export let param; 
    export let title = null; 
    export let description = null; 
    
    const dipsatcher = createEventDispatcher()

    function closeModal()
    {
        dipsatcher('modalClosed')  
   } 

</script>

{#if isModalVisable }
    <div class="modal modal-open">
        <div class="w-3/4 h-3/4 bg-base-300 rounded-xl">

        <!-- Top Panel  -->
            <div class="flex flex-row w-full h-[10%]  ">
                <div class="flex w-1/3  items-center ml-3 " >
                    <IconButton on:click={closeModal} icon={XMark} iconStyle="w-5 "></IconButton>
                </div>
                <div class="flex justify-center items-center w-1/3 " >
                    <span class="text-xl"> Lorem Picusm </span>
                </div>
                <div class="flex items-center w-1/3 justify-end mr-3 gap-3 " >
                    <IconButton  icon={ArrowUpOnSquare} iconStyle="w-5" flipIcons={true} ></IconButton>
                    <IconButton icon={ArrowDownTray} iconStyle="w-5 sm:3"  flipIcons={true} buttonStyle="lg:bg-success lg:text-primary-content  ">Download</IconButton>
                </div>
              </div>
            <div class="h-[80%] w-full flex flex-row">

            </div>

        <!-- Info Section -->
            <div class="h-[10%] flex flex-row">

            </div>

        </div>
    </div>

{/if}