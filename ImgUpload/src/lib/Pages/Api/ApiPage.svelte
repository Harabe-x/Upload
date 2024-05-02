<script>
    import Card from '../../DataPresenters/Cards/Card.svelte'
    import Chart from '../../DataPresenters/Charts/Chart.svelte';
    import { getChartData } from '../../../js/ApplicationData/ChartData';
</script>
    

<div class="grid mt-1 grid-cols-1">
     
    <Card title="Api Keys"> </Card>
</div> 
 
<div class="grid lg:grid-cols-2 mt-2 grid-cols-1 gap-10">
    <Card title="Api Keys Usage" > 
        <Chart chartType="line" data={getChartData()}   > </Chart>
    </Card>
    
    <Card title="Number of Api Keys" > 
        <Chart chartType="bar" data={getChartData()}   > </Chart>
    
    </Card>
     
</div>    
