<script>
    import Card from '../../DataPresenters/Cards/Card.svelte'
    import Chart from '../../DataPresenters/Charts/Chart.svelte';
    import { getChartData } from '../../../js/ApplicationData/ChartData';
    import { ArrowPath,Plus } from 'svelte-hero-icons';
    import ApiKeysList from './Components/ApiKeysList.svelte';
    import IconButton from '../../Controls/Buttons/IconButton.svelte';
    import DoughnutChart from "../../DataPresenters/Charts/DoughnutChart.svelte";
    import { getApiKeys } from '../../../js/Temp/ApiKeysData';

    const apiKeys = getApiKeys();
</script>

<div class="text-right">
<IconButton icon={ArrowPath} iconStyle="w-4 mr-2"> Refresh Data </IconButton>
</div>


<div class="grid mt-1 grid-cols-1">
     
    <Card title="Api Keys">
        <!-- Title Button  -->
            <div class="flex flex-row ml-auto mr-2" slot="titleControl"> 
                <IconButton icon={Plus} iconStyle="w-4 h-4"> Add </IconButton> 
            </div>
        <ApiKeysList></ApiKeysList> 
    </Card>
</div> 
 
<div class="grid lg:grid-cols-2 mt-2 grid-cols-1 gap-10 mb-10">
    <Card title="Api Keys Usage" > 
        <DoughnutChart labels={apiKeys.map((item) => item.Name )} values={apiKeys.map((item) => item.Storage)}></DoughnutChart>
    </Card>
    
    <Card title="Number of Api Keys" > 
        <Chart chartType="bar" data={getChartData()}   > </Chart>
    </Card>
     
</div>    
