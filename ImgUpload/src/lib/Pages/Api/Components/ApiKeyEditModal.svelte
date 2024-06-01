<script>
   import TextInput from "../../../Controls/Inputs/TextInput.svelte";
   import IconButton from "../../../Controls/Buttons/IconButton.svelte";
   import { createEventDispatcher } from "svelte";
   import { XMark, Check } from "svelte-hero-icons";
   import { getApiKeys } from "../../../../js/Temp/ApiKeysData";
   import { validateStorage ,validateName} from "../../../../js/Temp/DataValidator";
   const dispatcher = createEventDispatcher();
   const apiKeysStore = getApiKeys();

   export let isModalVisable = false;
   export let param;
  function closeEditModal()
  { 
    dispatcher('modalClosed')
  }
  
  function saveEditedKey()
  {
    if(validateName(param.Name) && validateStorage(param.Storage))
    {
      apiKeysStore.updateKey(param,param.Name,param.Storage)
      closeEditModal();
    }
  }


</script> 


{#if isModalVisable}
<div class="modal modal-open">
    <div  class="modal-box">
      <h3 class="font-bold text-lg">Add key</h3>
  
      <div class="mt-2 p-2">
        <TextInput bind:value={param.Name} isError={!validateName(param.Name)} errorMessage="Name can't be empty or white space" label="Name"></TextInput>
        <TextInput bind:value={param.Key} disabled={true} label="Key"></TextInput>
        <TextInput bind:value={param.Storage} isError={!validateStorage(param.Storage)} errorMessage="Storage have to be a number" label="Storage"></TextInput>

      </div>
  
      <div class="modal-action"> 
          <IconButton buttonStyle="" iconStyle="w-4" icon={XMark} on:click={closeEditModal}>Close</IconButton>
          <IconButton buttonStyle="bg-success text-secondary-content" iconStyle="w-4"on:click={saveEditedKey} icon={Check}>Save</IconButton>
      </div>
    </div>
  </div>
{/if}