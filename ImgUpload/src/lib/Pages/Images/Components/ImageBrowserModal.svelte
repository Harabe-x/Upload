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
    export let isModalVisable = false;
    export let param;

    const dispatcher = createEventDispatcher();

    function closeModal() {
        dispatcher('modalClosed');
    }
</script>

{#if isModalVisable }
    <div class="modal modal-open flex flex-col justify-center items-center">
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
                            <li><a><Icon src={ArrowDownTray} class="w-4" />Download</a></li>
                            <li><a><Icon src={ArrowUpOnSquare} class="w-4" />Share</a></li>
                            <li><a><Icon src={Trash} class="w-4" />Delete</a></li>
                            <li><a><Icon src={InformationCircle} class="w-4" />File info</a></li>
                        </IconDropdown>
                    </div>

                    <!-- Buttons and dropdown for larger screens (tablet and desktop) -->
                    <div class="hidden sm:flex items-center gap-1">
                        <IconButton icon={ArrowUpOnSquare} iconStyle="w-5" flipIcons={true}></IconButton>
                        <IconButton icon={ArrowDownTray} iconStyle="w-5 sm:w-3" flipIcons={true} buttonStyle="bg-success text-primary-content">Download</IconButton>
                        <IconDropdown icon={EllipsisVertical}>
                            <li><a><Icon src={Trash} class="w-4" />Delete</a></li>
                            <li><a><Icon src={InformationCircle} class="w-4" />File info</a></li>
                        </IconDropdown>
                    </div>
                </div>
            </div>

            <div class="h-[70%] w-full flex flex-row">
                <div class="h-full w-[15%] flex flex-col items-center justify-center">
                    <IconButton on:click={() => { param.previousImage(); }} icon={ChevronLeft} iconStyle="w-8"></IconButton>
                </div>

                <div class="w-[70%] flex justify-center items-center overflow-hidden">
                    {#key $param.currentImage}
                        <img src={param.getCurrentImage()} alt="There should be an img here but something went wrong" class="max-w-full max-h-full h-full w-full object-contain rounded-xl overflow-auto">
                    {/key}
                </div>


                <div class="h-full w-[15%] flex flex-col items-center justify-center">
                    <IconButton on:click={() => { param.nextImage() }} icon={ChevronRight} iconStyle="w-8"></IconButton>
                </div>
            </div>

            <!-- Info Section -->
            <div class="h-[15%] flex flex-row">

            </div>
        </div>
    </div>
{/if}
