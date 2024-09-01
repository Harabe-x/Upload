<script>
   import TextInput from "../../../Controls/Inputs/TextInput.svelte";
   import IconButton from "../../../Controls/Buttons/IconButton.svelte";
   import { createEventDispatcher } from "svelte";
   import {XMark, Check, ArrowPath} from "svelte-hero-icons";
   import { getApiKeys } from "../../../../js/Temp/ApiKeysData";
   import { validateStorage ,validateName} from "../../../../js/Temp/DataValidator";
   import {onEscapeAction,onEnterAction} from "../../../../js/UserInterface/Actions/ModalActions.js";
   import { getApiKeyStore} from "@/js/State/ApiKey/ApiKeyStore.js";

   const dispatcher = createEventDispatcher();
   const apiKeysStore = getApiKeyStore();
   
   export let isModalVisable = false;
   export let param;
  function closeEditModal()
  { 
    dispatcher('modalClosed')
  }
  
  function saveEditedKey()
  {
    if(validateName(param.keyName))
    {
        apiKeysStore.editKey(param.key, param.keyName)
      closeEditModal();
    }
  }
</script> 


{#if isModalVisable}
<div class="modal modal-open" use:onEscapeAction  use:onEnterAction on:enter={saveEditedKey} on:escape={closeEditModal}>
    <div  class="modal-box">
      <h3 class="font-bold text-lg">Add key</h3>
  
      <div class="mt-2 p-2">
        <TextInput bind:value={param.keyName} isError={!validateName(param.keyName)} errorMessage="Name can't be empty or white space" label="Name"></TextInput>
        <TextInput bind:value={param.key} disabled={true} label="Key"></TextInput>
      </div>
  
      <div class="modal-action ">
          <IconButton buttonStyle="bg-accent text-secondary-content mr-auto" iconStyle="w-4"on:click={saveEditedKey} icon={ArrowPath}>Rotate</IconButton>
          <IconButton buttonStyle="" iconStyle="w-4"  icon={XMark} on:click={closeEditModal}>Close</IconButton>
          <IconButton buttonStyle="bg-success text-secondary-content" iconStyle="w-4"on:click={saveEditedKey} icon={Check}>Save</IconButton>
      </div>
    </div>
  </div>
{/if}