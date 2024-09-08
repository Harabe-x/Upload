<script>
    import Chart from 'chart.js/auto'
    import { onMount } from 'svelte';
    
    export let chartType;
    export let labels ;
    export let values ;
    export let chartLabel; 
    
    onMount(() => {
        new Chart(element, {
    type: chartType,
    data: {
      labels: labels,
      datasets:[{
          label : chartLabel, 
          data : values
      }]

    },
    options: {
        responsive:true,
      scales: {
        y: {
          beginAtZero: false
        }
      }
    }
  });
    })

  let element;

</script>

<canvas class="w-[25%] h-50%" bind:this={element}></canvas>