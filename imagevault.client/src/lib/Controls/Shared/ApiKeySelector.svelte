<script>
import SelectInput from "@/lib/Controls/Inputs/SelectInput.svelte";
import {getApiKeyStore} from "@/js/State/ApiKey/ApiKeyStore.js";
import {onMount} from "svelte";
import {get} from "svelte/store";

const apiKeyStore = getApiKeyStore(); 


onMount( async () => { 
    await fetchKeys();
})


async function fetchKeys()
{
    await apiKeyStore.fetchKeys();
}

function selectKey(event)
{
    apiKeyStore.selectKey(event.detail.target.value);
}

</script>

<SelectInput on:change={selectKey} title="Selected key:">
    {#each $apiKeyStore.apiKeys as key (key.key)}
        <option  selected={key.key === $apiKeyStore.selectedKey} value={key.key}> {key.keyName} </option>
    {/each}
</SelectInput>