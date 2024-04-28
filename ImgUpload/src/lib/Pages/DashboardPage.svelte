<script>
    import Card from "../DataPresenters/Cards/Card.svelte";
    import LinearChart from "../DataPresenters/Charts/LinearChart.svelte";
    import Stat from "../DataPresenters/Cards/Stat.svelte";
    import StatIcon from "../DataPresenters/Cards/StatIcon.svelte";
    import { Server,ServerStack,Photo ,CurrencyDollar } from "svelte-hero-icons";


</script>



    <!-- Card section -->
<div class="grid lg:grid-cols-3 mt-2 md:grid-cols-2 grid-cols-1 gap-6" >
    <Stat icon={ServerStack} title="Request Ammount" value="221,222" description="Your value increased by  220%" > </Stat>
    <Stat icon={Photo} title="Uploaded Images" value="823,174" description="Your value increased by  220%" > </Stat>
    <Stat icon={CurrencyDollar} title="Money Spent" value="23,300$" description="Your value increased by  220%"> </Stat>

</div>

<!-- Charts -->
<div class="grid lg:grid-cols-2 mt-1 md:grid-cols-2 grid-cols-1 gap-6">
        <Card title="Sample Chart">
            <LinearChart></LinearChart>
            
        </Card>
        <Card title="Sample Chart">
            <LinearChart></LinearChart>
        </Card>
</div>
    