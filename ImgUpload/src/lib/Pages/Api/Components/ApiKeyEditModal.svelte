<script>
   import TextInput from "../../../Controls/Inputs/TextInput.svelte";
   import IconButton from "../../../Controls/Buttons/IconButton.svelte";
   import { createEventDispatcher } from "svelte";
   import { XMark, Check } from "svelte-hero-icons";
   import { getApiKeys } from "../../../../js/Temp/ApiKeysData";
   const dispatcher = createEventDispatcher();
   const apiKeysStore = getApiKeys();
   export let isVisable = false; 
   export let currentKey;
  function closeEditModal()
  { 
    dispatcher('modalClosed')
  }
  
  function saveEditedKey()
  {
    apiKeysStore.update(() => { return $apiKeysStore })
    closeEditModal();
  }


</script> 


{#if isVisable}
<div class="modal modal-open">
    <div class="modal-box">
      <h3 class="font-bold text-lg">Add key</h3>
  
      <div class="mt-2 p-2">
        <TextInput bind:value={currentKey.Name}  label="Name"></TextInput>
        <TextInput bind:value={currentKey.Key} disabled={true} label="Key"></TextInput>
        <TextInput bind:value={currentKey.Storage}  label="Storage"></TextInput>

      </div>
  
      <div class="modal-action"> 
          <IconButton buttonStyle="" iconStyle="w-4" icon={XMark} on:click={closeEditModal}>Close</IconButton>
          <IconButton buttonStyle="bg-success text-secondary-content" iconStyle="w-4"on:click={saveEditedKey} icon={Check}>Save</IconButton>
      </div>
    </div>
  </div>
{/if}