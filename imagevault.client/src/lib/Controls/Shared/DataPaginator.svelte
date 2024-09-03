<script>
    import IconButton from "../Buttons/IconButton.svelte";
    import { createEventDispatcher } from "svelte";
    import { ChevronLeft,ChevronRight } from "svelte-hero-icons";
    
    export let currentPage = 1;

    

    const dispatcher = createEventDispatcher();

    // Returns pageNumber as event.detail
    export function nextPage()
    {   
       dispatcher('navigatedToNextPage',currentPage)
    }
     export function previousPage()
    {
        dispatcher('navigatedToPreviousPage',currentPage)
     }

</script>

        
<div class="flex flex-row  items-center justify-center gap-1">
    <IconButton iconStyle="w-4" on:click={previousPage} icon={ChevronLeft}>Previous</IconButton>
    <span class="font-semibold"> {currentPage} </span> 
    <IconButton flipIcons={true} iconStyle="w-4" on:click={nextPage} icon={ChevronRight}>Next</IconButton>
  </div>


