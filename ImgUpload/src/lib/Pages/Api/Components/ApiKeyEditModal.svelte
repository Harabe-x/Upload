<script>
  import { createEventDispatcher } from "svelte";
  import TextInput from "../../../Controls/Inputs/TextInput.svelte";
  import IconButton from "../../../Controls/Buttons/IconButton.svelte";
  import { XMark,Check } from "svelte-hero-icons";

  const dispatcher = createEventDispatcher();

  export let isModalOpen ;

  let apiKeyName  = 'Social Media App'
  let apiKey = '61990008-c5da-54a6-ad7e-0cb5536ae398'
  let storage = '142GB'
  function closeModal()
  {
    isModalOpen = false;
    dispatcher('modalClosed')

  }
  
</script>
 
 
<dialog id="my_modal_1" class="modal" class:modal-open={isModalOpen}>
  <div class="modal-box">
    <h3 class="font-bold text-lg">Edit Key</h3>

    <div class="mt-2 p-2">
      <TextInput label="Name" value={apiKeyName}></TextInput>
      <TextInput label="Key" value={apiKey}></TextInput>
      <TextInput label="Storage" value={storage}></TextInput>
    </div>

    <div class="modal-action">
        <IconButton buttonStyle="" iconStyle="w-4" icon={XMark} on:click={closeModal}>Close</IconButton>
        <IconButton buttonStyle="bg-success text-secondary-content" iconStyle="w-4"on:click={closeModal} icon={Check}>Save</IconButton>

    </div>
  </div>
</dialog>