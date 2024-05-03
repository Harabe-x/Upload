<script>
    import { getApiKeys } from "../../../../js/Temp/ApiKeysData";
    import { Pencil,Trash } from "svelte-hero-icons";
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";

    let data = getApiKeys(); 
    const tableItems = []
    let selectedElement;

    function action(elem)
    {
        tableItems.push(elem)
    }
    function tableRowOnClick(event)
    {
        selectedElement = event.currentTarget;

        tableItems.forEach((item) => {  
            item.classList.remove('bg-base-200')
        })

        selectedElement.classList.add('bg-base-200')
    }
    function openDialog(item)
    {
        // TODO
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
        {#each data as item }
        <tr use:action on:click={tableRowOnClick} on:dblclick={openDialog(item)}>
            <th>{data.indexOf(item) + 1}</th>
            <td>{item.Name}</td>
            <td>{item.Key}</td>
            <td>{item.Storage} GB</td>
            <td>
                <IconButton icon={Pencil} iconStyle="w-5">Edit</IconButton> 
            </td>
        </tr>
        {/each}
      </tbody>
    </table>
  </div>
