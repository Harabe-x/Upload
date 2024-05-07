<script>
    import { getApiKeys } from "../../../../js/Temp/ApiKeysData";
    import { Pencil,Trash,XMark,Check } from "svelte-hero-icons";
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";
    import ApiKeyEditModal from "./ApiKeyEditModal.svelte";
    import ApiKeyDeleteModal from './ApiKeyDeleteModal.svelte'
    import TextInput from "../../../Controls/Inputs/TextInput.svelte";
    
    const apiKeysStore = getApiKeys();
    const apiKeys = $apiKeysStore;
    const tableRows = []
    let selectedTableRow;
    let isEditModalVisable = false;
    let isDeleteModalVisable = false;
    let selectedKey;
   
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
    function toggleEditModal()
    {
      isEditModalVisable = !isEditModalVisable;
    }
    function toggleDeleteModal()
    {
      isDeleteModalVisable = !isDeleteModalVisable;
    }
    
    function openEditModal(item)
    { 
      selectedKey = item;
      toggleEditModal();
    }
    function openDeleteModal(item)
    {
      selectedKey = item; 
      toggleDeleteModal()
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
          {#key $apiKeysStore}
          {#each  $apiKeysStore as item (item.Id) }
          <tr use:action on:click={tableRowOnClick}  on:dblclick={() => { openEditModal(item)  }}>
              <th>{apiKeys.indexOf(item) + 1}</th>
              <td>{item.Name}</td>
              <td>{item.Key}</td>
              <td>{item.Storage} GB</td>
              <td>
                  <IconButton on:click={() => { openEditModal(item) }}  icon={Pencil} iconStyle="w-5">Edit</IconButton>
                  <IconButton on:click={() => { openDeleteModal(item) }}  icon={Trash} iconStyle="w-5">Delete</IconButton> 

              </td>
          </tr>
          {/each}
          {/key}
      </tbody>
    </table>
  </div>

  <ApiKeyDeleteModal currentKey={selectedKey}  isVisable={isDeleteModalVisable} on:modalClosed={toggleDeleteModal}></ApiKeyDeleteModal>
  <ApiKeyEditModal currentKey={selectedKey}  isVisable={isEditModalVisable} on:modalClosed={toggleEditModal} > </ApiKeyEditModal>