<script>
     import IconButton from "../../../Controls/Buttons/IconButton.svelte";
     import TextInput from "../../../Controls/Inputs/TextInput.svelte";
     import { XMark , Plus } from "svelte-hero-icons";
     import { createEventDispatcher } from "svelte";
     import { getApiKeys } from "../../../../js/Temp/ApiKeysData";
     import { getRandomGuid } from "../../../../js/Temp/DummyDataGenerator";
     import { validateName,validateStorage } from "../../../../js/Temp/DataValidator";
     const apiKeyStore = getApiKeys();
     const dispatcher = createEventDispatcher();
     
    export let    isModalVisable = false;

     let name = ''; 
     let storage = '';

    function closeModal()
    {
        dispatcher('modalClosed');
    }
    function addApiKeyToTheStore()
    {
      if(validateName(name) && validateStorage(storage))
      {
        const newApiKey = { Id: $apiKeyStore.length, Name: name, Key: getRandomGuid(), Storage: storage };
        $apiKeyStore.push(newApiKey); 
        // IDK why this is wokring 
        apiKeyStore.update(() => { return $apiKeyStore })

      closeModal();
      }
     clearTextBoxes();

    }
    function clearTextBoxes()
    {
        name = ''; 
        storage = '';
    }

</script>

{#if isModalVisable }
<div class="modal modal-open">
    <div class="modal-box">
      <h3 class="font-bold text-lg">Add key</h3>
  
      <div class="mt-2 p-2">
        <TextInput bind:value={name} isError={!validateName(name)} errorMessage="Name can't be empty or white space" label="Name"></TextInput>
        <TextInput bind:value={storage} isError={!validateStorage(storage)} errorMessage="Storage have to be a number" label="Storage"></TextInput>
      </div>
  
      <div class="modal-action">
          <IconButton buttonStyle="" iconStyle="w-4" icon={XMark} on:click={closeModal}>Close</IconButton>
          <IconButton buttonStyle="bg-success text-secondary-content"  iconStyle="w-4"on:click={addApiKeyToTheStore} icon={Plus}>Add</IconButton>
      </div>
    </div>
  </div>
{/if}