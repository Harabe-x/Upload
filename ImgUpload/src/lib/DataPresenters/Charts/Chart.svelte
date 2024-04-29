<script>
    import Chart from 'chart.js/auto'
    import { onMount } from 'svelte';
    
    export let data;
    export let chartType;

    onMount(() => {
        new Chart(element, {
    type: chartType,
    data: {
      labels: data.labels,
      datasets: data.datasets
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