<script>
    import { getLogPage } from "../../../../js/Temp/DummyLogEntry";
    import { writable } from "svelte/store";
    import { onMount } from "svelte";
    import DataPaginator from "../../../Controls/Shared/DataPaginator.svelte";

    const logStore = writable([])
    const chunkSize = 15;
    let currentPage = 1;

    
    function fetchLogs()
    {
        $logStore = getLogPage(currentPage,chunkSize)
        logStore.set($logStore);
    }
    function goToNextPage()
    {
        currentPage += 1; 
        fetchLogs();
    }
    function goToPreviousPage()
    {
        if(currentPage - 1 < 1 ) return;
        currentPage -= 1; 
        fetchLogs();
    }
    
    onMount(() => { 
        fetchLogs();
    })
   
</script>


<div class="overflow-auto">
    <table class="table bg-base-200">
        <thead>
            <tr>
              <th>Date</th>
              <th>Log Entry</th>
            </tr>
          </thead>
          <tbody>
            

           {#each $logStore as log (log.Date)}
           <tr>
               <td class="font-bold">{log.Date}</td>
               <td>{log.Message}</td>
           </tr>
        {/each}
          </tbody>
    </table> 
    {#if chunkSize > 5}
        <DataPaginator on:navigatedToNextPage={goToNextPage} on:navigatedToPreviousPage={goToPreviousPage}></DataPaginator>
    {/if}
</div>      
