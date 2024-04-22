<script>
    import Chart from 'chart.js/auto'
    import { onMount } from 'svelte';

    onMount(() => { 
        new Chart(element, {
    type: 'bar',
    data: {
      labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
      datasets: [{
        label: '# of Votes',
        data: [12, 19, 3, 5, 2, 3,2],
        borderWidth: 1
      }]
    },
    options: {
        responsive:true,
      scales: {
        y: {
          beginAtZero: true
        }
      }
    }
  });
    })

    let element;

</script>

<canvas class="w-[25%] h-50%" bind:this={element}></canvas>