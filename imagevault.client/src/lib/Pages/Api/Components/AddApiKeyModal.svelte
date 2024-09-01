<script>
     import IconButton from "../../../Controls/Buttons/IconButton.svelte";
     import TextInput from "../../../Controls/Inputs/TextInput.svelte";
     import { XMark , Plus } from "svelte-hero-icons";
     import { createEventDispatcher } from "svelte";
     import { getApiKeys } from "../../../../js/Temp/ApiKeysData";
     import { validateName,validateStorage } from "../../../../js/Temp/DataValidator";
     import { onEscapeAction,onEnterAction} from "../../../../js/UserInterface/Actions/ModalActions.js";
     import { focusAction } from "../../../../js/UserInterface/Actions/ElementStateAction.js";
     import {getApiKeyStore} from "@/js/State/ApiKey/ApiKeyStore.js";

     const apiKeyStore = getApiKeyStore() 
     const dispatcher = createEventDispatcher();
     
    export let isModalVisable = false;

     let name = ''; 

    function closeModal()
    {
        dispatcher('modalClosed');
    }
    async function addApiKeyToTheStore()
    {
      if(validateName(name) )
      {
          await apiKeyStore.addKey(name)
          
          closeModal();
      }
     clearTextBoxes();

    }
    function clearTextBoxes()
    {
        name = ''; 
    }

</script>

{#if isModalVisable }
<div use:onEscapeAction on:enter={addApiKeyToTheStore} on:escape={closeModal} use:onEnterAction  class="modal modal-open">
    <div class="modal-box">
      <h3 class="font-bold text-lg">Add key</h3>
  
      <div class="mt-2 p-2">
        <TextInput  bind:value={name} isError={!validateName(name)} errorMessage="Name can't be empty or white space" label="Name"></TextInput>
      </div>
  
      <div class="modal-action">
          <IconButton buttonStyle="" iconStyle="w-4" icon={XMark} on:click={closeModal}>Close</IconButton>
          <IconButton buttonStyle="bg-success text-secondary-content"  iconStyle="w-4"on:click={addApiKeyToTheStore} icon={Plus}>Add</IconButton>
      </div>
    </div>
  </div>
{/if}