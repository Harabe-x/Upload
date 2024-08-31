<script>
    import { getApiKeys } from "../../../../js/Temp/ApiKeysData";
    import { Pencil,Trash,XMark,Check } from "svelte-hero-icons";
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";
    import ApiKeyEditModal from "./ApiKeyEditModal.svelte";
    import ApiKeyDeleteModal from './ApiKeyDeleteModal.svelte'
    import ModalWindow from "../../../Controls/Shared/ModalWindow.svelte";
    import {onMount} from "svelte";
    import {getApiKeyStore} from "../../../../js/State/ApiKey/ApiKeyStore.js";
    import DataFetchingPage from "@/lib/Pages/InfoPages/DataFetchingPage.svelte";
    
    const apiKeysStore = getApiKeys();
    const apiKeys = $apiKeysStore;
    const tableRows = []
    const apiKeyStore = getApiKeyStore()
    let selectedTableRow;
    let selectedKey;
    let editModalToggleFunction;
    let deleteModalToggleFucntion;
   


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

<div class="overflow-x-auto bg-base-200">
    {#await apiKeyStore.fetchKeys()}
        <DataFetchingPage></DataFetchingPage>
        {:then  apiKeys}
        <table class="table bg-base-200">
            <!-- head -->
            <thead>
            <tr>
                <th>No.</th>
                <th>Name</th>
                <th>Key</th>
                <th>Storage used</th>
            </tr>
            </thead>
            <tbody>
            {#each  apiKeys as key,index (key.key)}
                <tr class="bg-base-200" use:action on:click={tableRowOnClick}  on:dblclick={() => { selectKey(key); editModalToggleFunction();  }}>
                    <th class="bg-base-200">{index + 1}</th>
                    <td >{key.keyName}</td>
                    <td>{key.key}</td>
                    <td>{key.storageUsed} Bytes</td>
                    <td>
                        <div class="flex flex-row gap-2">
                            <IconButton on:click={() => { selectKey(key); editModalToggleFunction(); }}  icon={Pencil} iconStyle="w-5">Edit</IconButton>
                            <IconButton on:click={() => { selectKey(key); deleteModalToggleFucntion();  }}  icon={Trash} iconStyle="w-5">Delete</IconButton>
                        </div>
                    </td>
                </tr>
            {/each}
            </tbody>
        </table>

        {/await}
  </div>

<ModalWindow bind:toggleModal={editModalToggleFunction} param={selectedKey} type="ApiKeyEditModal" ></ModalWindow>
<ModalWindow bind:toggleModal={deleteModalToggleFucntion} param={selectedKey} type="ApiKeyDeleteModal" ></ModalWindow>
