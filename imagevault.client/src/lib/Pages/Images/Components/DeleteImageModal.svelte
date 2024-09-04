<script>
    import { createEventDispatcher } from "svelte";
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";
    import { XMark,Trash } from "svelte-hero-icons";
    import { getApiKeys } from "../../../../js/Temp/ApiKeysData";
    import {onEscapeAction} from "../../../../js/UserInterface/Actions/ModalActions.js";
    import {getApiKeyStore} from "@/js/State/ApiKey/ApiKeyStore.js";
    import {getImageManagerStore} from "@/js/State/Image/ImageStore.js";
    import ModalWindow from "@/lib/Controls/Shared/ModalWindow.svelte";
    const imageManager = getImageManagerStore()
    
    const dispatcher = createEventDispatcher();
    
    export let param;
    export let isModalVisable = false;

    
    
    function closeDeleteModal()
    {
        dispatcher('modalClosed');
    }
    async function deleteImage()
    {
        await imageManager.deleteImage($param.apiKey,  $param.collection, $param.imageKey)
        closeDeleteModal();
    }


</script>


{#if isModalVisable}
    <div class="modal modal-open" use:onEscapeAction  on:escape={closeDeleteModal}>
        <div class="modal-box">
            <h3 class="font-bold text-lg">Delete key </h3>
            <p class="py-4">Are you sure you want to delete image with key "<span class="font-bold text-primary">{$param.imageKey}</span>" ?</p>
            <div class="modal-action">
                <IconButton iconStyle="w-4" icon={XMark} on:click={closeDeleteModal}> Cancle </IconButton>
                <IconButton iconStyle="w-4" buttonStyle="bg-error text-primary-content" on:click={deleteImage} icon={Trash}> Delete </IconButton>
            </div>
        </div>
    </div>
{/if}



