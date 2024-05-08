<script>
    import viewport from "../../../../js/Temp/Viewport";
    import { getLogPage } from "../../../../js/Temp/DummyLogEntry";
    import { writable } from "svelte/store";
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";
    import { ChevronLeft,ChevronRight } from "svelte-hero-icons";
    import { onMount } from "svelte";
    
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
    <table class="table">
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
    <div class="flex flex-row  items-center justify-center gap-1">
        <IconButton iconStyle="w-4" on:click={goToPreviousPage} icon={ChevronLeft}>Previous</IconButton>
        <span class="font-semibold">{currentPage}</span>
        <IconButton flipIcons={true} iconStyle="w-4" on:click={goToNextPage} icon={ChevronRight}>Next</IconButton>
      </div>
    {/if}
</div>      
