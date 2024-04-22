<script>   
   import StatsFrame from "../DataPresenters/Stats/StatsFrame.svelte";    
    import Stat from "../DataPresenters/Stats/Stat.svelte";
</script>
 
<StatsFrame>
    <Stat title="Revanue" centered={true} value="23,300$"         description="Lorem ipsum dolor sit amet">
    </Stat>

    <Stat title="Revanue" color="text-secondary" centered={true} value="23,300$"         description="Lorem ipsum dolor sit amet">
    </Stat>

    <Stat title="Revanue" centered={true} value="23,300$"         description="Lorem ipsum dolor sit amet">
    </Stat>
    
</StatsFrame>







