<script>
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";
    import { ArrowDownTray } from "svelte-hero-icons";
    import ApiKeyLogList from "./ApiKeyLogList.svelte";
    import {onMount} from "svelte";
    import {getApiKeys} from "../../../../js/Temp/ApiKeysData.js";
    import SelectInput from "../../../Controls/Inputs/SelectInput.svelte";
    import ApiKeySelector from "@/lib/Controls/Shared/ApiKeySelector.svelte";
    import {getApiKeyLogStore} from "@/js/State/Logs/ApiKeyLogStore.js";
    import {getApiKeyStore} from "@/js/State/ApiKey/ApiKeyStore.js";
    
    const apiKeyLogStore = getApiKeyLogStore(); 
    const apiKeyStore = getApiKeyStore()
    
    async function downloadLogs()
    {
        const formatedLogs = await fetchAndCreateFormatedLogs();
        const blob = new Blob([formatedLogs], { type: 'text/plain' });
        const link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.download = `${$apiKeyStore.selectedKey.keyName} key ${getCurrentDate()} logs.txt`

        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    }

    async function fetchAndCreateFormatedLogs()
    {
        const logs = await apiKeyLogStore.getAllLogs($apiKeyStore.selectedKey.key);
        let formatedLogs = ""

        for (let i = 0; i < logs.length; i++)
        {
            formatedLogs += `[${logs[i].timeStamp}] ${logs[i].messaage}\n`
        }

        return formatedLogs;
    }

    function getCurrentDate() {
        
        let date = new Date()
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0'); 
        const day = String(date.getDate()).padStart(2, '0');

        const hours = String(date.getHours()).padStart(2, '0');
        const minutes = String(date.getMinutes()).padStart(2, '0');
        const seconds = String(date.getSeconds()).padStart(2, '0');

        return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
    }


</script>

<div class="flex flex-row items-center w-full">
    <ApiKeySelector> </ApiKeySelector>

    <div class="ml-auto">
        <IconButton iconStyle="w-6" icon={ArrowDownTray} on:click={downloadLogs}> Download </IconButton>
    </div>
</div>

<div class="divider mt-2"></div>

<div class="">
    {#key $apiKeyStore.selectedKey}
        <ApiKeyLogList></ApiKeyLogList> 
    {/key}
    
</div>


