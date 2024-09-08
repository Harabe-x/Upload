<script>
    import {getLogPage} from "../../../../js/Temp/DummyLogEntry";
    import {writable} from "svelte/store";
    import {onMount} from "svelte";
    import DataPaginator from "../../../Controls/Shared/DataPaginator.svelte";
    import {getApiKeyLogStore} from "@/js/State/Logs/ApiKeyLogStore.js";
    import {getApiKeyStore} from "@/js/State/ApiKey/ApiKeyStore.js";
        
    const apiKeyLogStore = getApiKeyLogStore()
    const apiKeyStore = getApiKeyStore()
    
    let currentPage = 1; 
    
    onMount(async () => { 
         await fetchData()
    })
    
    
    async function fetchData()
    {
        await apiKeyLogStore.fetchApiKeysLogs($apiKeyStore.selectedKey.key,currentPage) 
    }
    
    async function nextPage()
    {
        if($apiKeyLogStore.apiKeyLogs.length <  $apiKeyLogStore.limit) return 
        currentPage += 1; 
        await fetchData()
    }
    
    async function previousPage()
    {
        if(currentPage - 1 < 1) return; 
        currentPage -= 1; 
        await fetchData();
    }
    
    function processDate(date)
    {
        let newDate = ""; 
        
        for (let i = 0; i < 19 ; i++)
        {
            if (date[i] === "T")
            {
             newDate += " "; 
             continue;
            }
            
            newDate += date[i]; 
        }
        
        return newDate;
    }
    
   
    
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
        {#each $apiKeyLogStore.apiKeyLogs as log (log.timeStamp)}
            <tr>
                <td class="font-bold text-sm">{processDate(log.timeStamp)}</td>
                <td>{log.messaage}</td>
            </tr>
        {/each}
        </tbody>
    </table>
        <DataPaginator on:navigatedToNextPage={nextPage} on:navigatedToPreviousPage={previousPage}  bind:currentPage></DataPaginator>
</div>      
