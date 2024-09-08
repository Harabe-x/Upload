<script>
    import Card from "../../Controls/Cards/Card.svelte";
    import Chart from "../../Controls/Charts/Chart.svelte";
    import Stat from "../../Controls/Cards/Stat.svelte";
    import { Icon,ServerStack,Photo, Banknotes,CloudArrowUp,ArrowPath,Envelope,EllipsisVertical,ArrowDownTray} from "svelte-hero-icons";
    import { getChartData } from "../../../js/ApplicationData/ChartData"
    import IconButton from "../../Controls/Buttons/IconButton.svelte";
    import PageTopMenu from "../../Controls/Shared/PageTopMenu.svelte";
    import IconDropdown from "../../Controls/Dropdowns/IconDropdown.svelte";
    import SelectInput from "../../Controls/Inputs/SelectInput.svelte";
    import {getApiKeys} from "../../../js/Temp/ApiKeysData.js";
    import {onMount} from "svelte";
    import DataFetchingPage from "@/lib/Pages/InfoPages/DataFetchingPage.svelte";
    import {get} from "svelte/store";
    import ApiKeySelector from "@/lib/Controls/Shared/ApiKeySelector.svelte";
    import { getUsageStore} from "@/js/State/Metrics/UsageStore.js";
    import {formatBytes} from "@/js/Converters/ByteConverter.js";


    const apiKeyStore = getApiKeys();
    const usageStore = getUsageStore() 
    
    onMount(async () => {
        await fetchData()
    })

    let formatedBytes = "";
    
    async function fetchData()
    { 
         await usageStore.fetchDailyUsage(365)
         await usageStore.fetchUsageMetrics()
         formatedBytes = formatBytes($usageStore.UsageMetrics.totalStorageUsed);
    }
    
</script>


{#key $usageStore}


<!-- Card section -->
<PageTopMenu>
    <div slot="leftSide" class="hidden">
        <ApiKeySelector></ApiKeySelector>
    </div>
    <div slot="rightSide">
        <IconButton iconStyle="w-4 mr-1" icon={ArrowPath}>
            Refresh
        </IconButton>
    </div>
</PageTopMenu>

<div class="grid lg:grid-cols-4 mt-2 md:grid-cols-2 grid-cols-1 gap-6" >
    <Stat titleTextColor="base-content" 
     icon={ServerStack} 
       title="Storage used"
          value={formatedBytes}
          description="Your value increased by  220%" >
         </Stat>
    <Stat icon={Photo} 
    title="Uploaded Images"
     value="{$usageStore.UsageMetrics.totalImageUploaded}"
      description="Your value increased by  220%" > 
    </Stat>
    <Stat icon={CloudArrowUp} 
      title="API requests"
      value="{$usageStore.UsageMetrics.totalRequests}" 
      description="Api calls during selected time period">
     </Stat>
     <Stat icon={Banknotes} 
      title="Costs"
      value="Unknown" 
      description="Costs for using the API during this time period">
     </Stat>

</div>

<!-- Charts -->
<div class="grid lg:grid-cols-2 mt-1 md:grid-cols-2 grid-cols-1 gap-6">
        <Card title="Uploaded Images">
            <Chart chartType="line" chartLabel="Uploaded images" labels={$usageStore.DailyUsageMetrics.map((x) =>  x.date.slice(0,10))} values={$usageStore.DailyUsageMetrics.map((x) => x.totalImageUploaded)}   ></Chart>
        </Card>

        <Card title="Number of requests">
            <Chart chartType="line" chartLabel="Total Requests" labels={$usageStore.DailyUsageMetrics.map((x) => x.date.slice(0,10))} values={$usageStore.DailyUsageMetrics.map((x) => x.totalRequests)}   ></Chart>
        </Card>
</div>
    {/key}
    