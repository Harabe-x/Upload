<script>
    import { createEventDispatcher } from "svelte";
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";  
    import { XMark,Trash } from "svelte-hero-icons";
    import { getApiKeys } from "../../../../js/Temp/ApiKeysData";

    const apiKeysStore = getApiKeys();
    const dispatcher = createEventDispatcher();
    export let currentKey;
    export let isVisable = false; 

    function closeDeleteModal()
    {
        dispatcher('modalClosed');
    }
    function deleteApiKey()
    {
        $apiKeysStore.splice($apiKeysStore.indexOf(currentKey),1)
        apiKeysStore.update(() => { return $apiKeysStore } )
    
        closeDeleteModal();
    }

</script>


{#if isVisable}
<div class="modal modal-open">
    <div class="modal-box">
      <h3 class="font-bold text-lg">Delete key </h3>
      <p class="py-4">Are you sure you want to delete the key named "<span class="font-bold text-primary">{currentKey.Name}</span>" ?</p>
      <div class="modal-action">
        <IconButton iconStyle="w-4" icon={XMark} on:click={closeDeleteModal}> Cancle </IconButton>
        <IconButton iconStyle="w-4" buttonStyle="bg-error text-primary-content" on:click={deleteApiKey} icon={Trash}> Delete </IconButton>
      </div>
    </div>
  </div>
{/if}



