<script>
    import { createEventDispatcher } from "svelte";
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";  
    import { XMark,Trash } from "svelte-hero-icons";
    import { getApiKeys } from "../../../../js/Temp/ApiKeysData";
    import {onEscapeAction} from "../../../../js/UserInterface/Actions/ModalActions.js";
    import {getApiKeyStore} from "@/js/State/ApiKey/ApiKeyStore.js";
    const apiKeysStore = getApiKeyStore()
    const dispatcher = createEventDispatcher();
    export let param;
    export let isModalVisable = false;

    function closeDeleteModal()
    {
        dispatcher('modalClosed');
    }
    function deleteApiKey()
    {

        apiKeysStore.deleteKey(param.key)
        closeDeleteModal();
    }

</script>


{#if isModalVisable}
<div class="modal modal-open" use:onEscapeAction  on:escape={closeDeleteModal}>
    <div class="modal-box">
      <h3 class="font-bold text-lg">Delete key </h3>
      <p class="py-4">Are you sure you want to delete the key named "<span class="font-bold text-primary">{param.keyName}</span>" ?</p>
      <div class="modal-action">
        <IconButton iconStyle="w-4" icon={XMark} on:click={closeDeleteModal}> Cancle </IconButton>
        <IconButton iconStyle="w-4" buttonStyle="bg-error text-primary-content" on:click={deleteApiKey} icon={Trash}> Delete </IconButton>
      </div>
    </div>
  </div>
{/if}



