<script> 
    
    import CardSubTitle from "./CardSubTitle.svelte";
    export let title;
    export let containerStyle = null;

</script>

<div class="card  w-full p-6 bg-base-200 shadow-xl mt-6 {containerStyle}" >
    
    <div class="flex flex-row w-full items-center">
        <CardSubTitle>
          {title}        
        </CardSubTitle>
      
        <slot name="titleControl" />
    </div>

      <div class="divider mt-2"></div>
  
      <div class='h-full w-full pb-6 bg-base-200'>
          <slot />
      </div>
  </div>