<script>
    import { createEventDispatcher } from "svelte";
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";
    import {
        XMark,
        Trash,
        Icon,
        Key,
        DocumentText,
        Calendar,
        ClipboardDocumentList,
        Pencil,
        ChatBubbleLeft, ChatBubbleLeftRight, ArrowsPointingOut, Server, Folder
    } from "svelte-hero-icons";
    import { getApiKeys } from "../../../../js/Temp/ApiKeysData";
    import {onEscapeAction} from "../../../../js/UserInterface/Actions/ModalActions.js";
    import {getApiKeyStore} from "@/js/State/ApiKey/ApiKeyStore.js";
    import {getImageManagerStore} from "@/js/State/Image/ImageStore.js";
    import ModalWindow from "@/lib/Controls/Shared/ModalWindow.svelte";
    import FileStat from "@/lib/Controls/Cards/FileStat.svelte";
    import {formatBytes} from "@/js/Converters/ByteConverter.js";

    const imageManager = getImageManagerStore()

    const dispatcher = createEventDispatcher();

    export let param;
    export let isModalVisable = false;
    
    function closeInfoModal()
    {
        dispatcher('modalClosed');
    }
    async function deleteImage()
    {
        await imageManager.deleteImage($param.apiKey,  $param.collection, $param.imageKey)
        closeInfoModal();
    }
    
</script>

{#if isModalVisable}
    <div class="modal modal-open " use:onEscapeAction  on:escape={closeInfoModal}>
        <div class="bg-base-300  lg:w-1/2   lg:h-3/4 md:w-1/2 md:h-1/2 h-full w-full rounded-xl ">
            <div class="flex flex-col w-full h-full">
                <div  class="w-full h-auto flex flex-row">
                    <div class="w-1/3 mt-2 ml-2"> <IconButton icon={XMark} on:click={closeInfoModal} iconStyle="w-4"></IconButton> </div>
                </div>
                <div class="w-full h-full flex flex-col justify-center items-center ">
                    <div class=" flex flex-col w-full h-full  ">
                        <FileStat icon={Key} description="" title="Image Key" value={$param.key}></FileStat>
                        <FileStat icon={DocumentText}  description=""  title="Image Format" value={$param.imageFormat}></FileStat>
                        <FileStat icon={Calendar}  description="" title="Created At" value={$param.createdAt}></FileStat>
                        <FileStat icon={Folder} description="" title="Collection Name" value={$param.collectionName}></FileStat>
                        <FileStat icon={Pencil} description="" title="Image Title" value={$param.title}></FileStat>
                        <FileStat icon={Pencil}  description="" title="Description" value={$param.description}></FileStat>
                        <FileStat icon={Server}  description="" title="File Size" value={formatBytes($param.fileSize)}></FileStat>
                    </div>
                </div>
            </div>
        </div>
    </div>
{/if}



