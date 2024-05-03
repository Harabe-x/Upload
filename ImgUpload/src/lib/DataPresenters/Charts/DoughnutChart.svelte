<script>
    import Chart from 'chart.js/auto'
    import { onMount } from 'svelte';

    export let labels = [];
    export let values = [];
    export let color = 'text-green-500'; 
    let canvas;
    onMount(() => {
        new Chart(canvas, {
    type: 'doughnut',
    data: {
      labels: labels,
      datasets: [ 
        { label:'Storage Used', data:values }
      ]

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
    
</script>


<canvas bind:this={canvas}></canvas>





