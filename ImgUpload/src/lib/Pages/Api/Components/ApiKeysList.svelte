<script>
    import { getApiKeys } from "../../../../js/Temp/ApiKeysData";
    import { Pencil,Trash,XMark,Check } from "svelte-hero-icons";
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";
    import ModalWindowManager from "../../../Controls/Shared/ModalWindowManager.svelte";
    import ApiKeyEditModal from "./ApiKeyEditModal.svelte";
    import ApiKeyDeleteModal from './ApiKeyDeleteModal.svelte'
    
    const apiKeysStore = getApiKeys();
    const apiKeys = $apiKeysStore;
    const tableRows = []
   
    let selectedTableRow;
    let selectedKey;
   
   
    let editApiKeyModalToggleFunction;
    let deleteApiKeyModalToggleFunction;
   
    function action(elem)
    {
      tableRows.push(elem)
    }
    function tableRowOnClick(event)
    {
       selectedTableRow = event.currentTarget;

       tableRows.forEach((item) => {  
            item.classList.remove('bg-base-200')
        })

        selectedTableRow.classList.add('bg-base-200')
    }
    
    function selectKey(key)
    {
      selectedKey = key;
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
          <tr use:action on:click={tableRowOnClick}  on:dblclick={() => { selectKey(item)  }}>
              <th>{apiKeys.indexOf(item) + 1}</th>
              <td>{item.Name}</td>
              <td>{item.Key}</td>
              <td>{item.Storage} GB</td>
              <td>
                <div class="flex flex-row gap-2">
                  <IconButton on:click={() => { selectKey(item); editApiKeyModalToggleFunction() }}  icon={Pencil} iconStyle="w-5">Edit</IconButton>
                  <IconButton on:click={() => { selectKey(item); deleteApiKeyModalToggleFunction();}}  icon={Trash} iconStyle="w-5">Delete</IconButton> 
                </div>
              </td>
          </tr>
          {/each}
      </tbody>
    </table>
  </div>
  <ModalWindowManager bind:toggleModal={editApiKeyModalToggleFunction} param={selectedKey} type="ApiKeyEditModal"></ModalWindowManager>
  <ModalWindowManager bind:toggleModal={deleteApiKeyModalToggleFunction} param={selectedKey} type="ApiKeyDeleteModal"></ModalWindowManager>
  