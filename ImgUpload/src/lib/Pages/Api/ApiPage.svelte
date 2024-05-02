<script>
    import Card from '../../DataPresenters/Cards/Card.svelte'
    import Chart from '../../DataPresenters/Charts/Chart.svelte';
    import { getChartData } from '../../../js/ApplicationData/ChartData';
    import { Icon,Plus } from 'svelte-hero-icons';
</script>
    

<div class="grid mt-1 grid-cols-1">
     
    <Card title="Api Keys">
        <!-- Title Button  -->
            <div class="flex flex-row ml-auto mr-2" slot="titleControl"> 
                <button class="btn btn-ghost btn-sm normal-case">
                     <Icon src={Plus} class="w-5 h-5 "></Icon>
                     Add
                </button> 
            </div>
             
    </Card>
</div> 
 
<div class="grid lg:grid-cols-2 mt-2 grid-cols-1 gap-10">
    <Card title="Api Keys Usage" > 
        <Chart chartType="line" data={getChartData()}   > </Chart>
    </Card>
    
    <Card title="Number of Api Keys" > 
        <Chart chartType="bar" data={getChartData()}   > </Chart>
    
    </Card>
     
</div>    
