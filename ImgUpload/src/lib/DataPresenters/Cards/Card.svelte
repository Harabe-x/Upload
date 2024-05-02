<script> 
    
    import CardSubTitle from "./CardSubTitle.svelte";
    export let title;
    export let containerStyle = null;

</script>

<div class="card w-full p-6 bg-base-100 shadow-xl mt-6 {containerStyle}" >

      <CardSubTitle>
        {title}        
      </CardSubTitle>
      
      <div class="divider mt-2"></div>
  
      <div class='h-full w-full pb-6 bg-base-100'>
          <slot />
      </div>
  </div>