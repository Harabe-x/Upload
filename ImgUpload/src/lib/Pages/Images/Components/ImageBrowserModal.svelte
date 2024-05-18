<script> 
    import { createEventDispatcher } from 'svelte'
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";
    import {ArrowDownTray, ArrowUpOnSquare, ChevronLeft, ChevronRight, Icon, XMark} from "svelte-hero-icons";
    import Stat from '../../../DataPresenters/Cards/Stat.svelte';

    export let isModalVisable = false; 
    export let param; 

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
            <div class="flex flex-row w-full h-[15%]  ">
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
            <div class="h-[70%] w-full flex  flex-row">
                <div class="h-full w-[15%] flex flex-col items-center justify-center">
                    <IconButton icon={ChevronLeft} iconStyle="w-8"></IconButton>
                </div>

                <div class="h-full w-[70%] flex justify-center items-center">
                        <img src={param} alt="Requested" class="max-w-[80%] rounded-md">
                </div>


                <div class="h-full w-[15%] flex flex-col items-center justify-center">
                    <IconButton icon={ChevronRight} iconStyle="w-8"></IconButton>
                </div>

            </div>

        <!-- Info Section -->
            <div class="h-[15%] flex flex-row">

            </div>

        </div>
    </div>

{/if}