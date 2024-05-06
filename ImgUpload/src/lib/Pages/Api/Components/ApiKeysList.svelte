<script>
    import { getApiKeys } from "../../../../js/Temp/ApiKeysData";
    import { Pencil,Trash,XMark,Check } from "svelte-hero-icons";
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";
    import ApiKeyEditModal from "./ApiKeyEditModal.svelte";
    import TextInput from "../../../Controls/Inputs/TextInput.svelte";
    
    const apiKeysStore = getApiKeys();
    const apiKeys = $apiKeysStore;
    const tableItems = []
    let selectedTableRow;

      // This value have to be assigned 
     let selectedApiKey =apiKeys[0];

    var isModalOpen = false;
    function action(elem)
    {
        tableItems.push(elem)
    }
    function tableRowOnClick(event)
    {
       selectedTableRow = event.currentTarget;

        tableItems.forEach((item) => {  
            item.classList.remove('bg-base-200')
        })

        selectedTableRow.classList.add('bg-base-200')
    }
    function openDialog(item)
    {
      selectedApiKey = item;
      isModalOpen = true;
    }
    function closeDialog()
    {
      isModalOpen = false;
    }
    function updateApiKey()
    {
      $apiKeysStore.splice(0,1)
      closeDialog();
      alert(JSON.stringify($apiKeysStore))
    }   
</script>


<div class="overflow-x-auto">
    <table class="table">
      <!-- head -->
      <thead>
        <tr>
          <th>No.</th>
          <th>Name</th>
          <th>Key</th>
          <th>Space Available</th>
        </tr>
      </thead>
      <tbody>
        {#each  $apiKeysStore as item (item.Id) }
        <tr use:action on:click={tableRowOnClick}  on:dblclick={() => { openDialog(item)  }}>
            <th>{apiKeys.indexOf(item) + 1}</th>
            <td>{item.Name}</td>
            <td>{item.Key}</td>
            <td>{item.Storage} GB</td>
            <td>
                <IconButton on:click={() => { openDialog(item) }}  icon={Pencil} iconStyle="w-5">Edit</IconButton> 
            </td>
        </tr>
        {/each}
      </tbody>
    </table>
  </div>

  <!-- Edit Api Key Modal Window  -->

  
  <div class="modal" class:modal-open={isModalOpen}>
    <div class="modal-box">
      <h3 class="font-bold text-lg">Edit Key</h3>
  
      <div class="mt-2 p-2">
        <TextInput label="Name" bind:value={selectedApiKey.Name}></TextInput>
        <TextInput label="Key" bind:value={selectedApiKey.Key}></TextInput>
        <TextInput label="Storage" bind:value={selectedApiKey.Storage}></TextInput>
      </div>
  
      <div class="modal-action">
          <IconButton buttonStyle="" iconStyle="w-4" icon={XMark} on:click={closeDialog}>Close</IconButton>
          <IconButton buttonStyle="bg-success text-secondary-content" iconStyle="w-4"on:click={updateApiKey} icon={Check}>Save</IconButton>
      </div>
    </div>
  </div>