<script> 
    
    import CardSubTitle from "./CardSubTitle.svelte";
    export let title;


</script>

<div class="card w-full p-6 bg-base-100 shadow-xl  mt-6">

      <CardSubTitle>
        {title}        
      </CardSubTitle>
      
      <div class="divider mt-2"></div>
  
      <div class='h-full w-full pb-6 bg-base-100'>
          <slot />
      </div>
  </div>