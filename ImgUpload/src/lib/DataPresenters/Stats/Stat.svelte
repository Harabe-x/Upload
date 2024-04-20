<script>
    export let title;
    export let value;
    export let description;
    export let color ='';
    export let centered = false;
    
</script>
<div class="stat {centered ? 'place-items-center' : ''}">
    <div class="stat-title">{title}</div>
    <div class="stat-value {color}">{value}</div>
    <div class="stat-desc">{description}</div>
  </div>