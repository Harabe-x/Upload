<script> 
    import { createEventDispatcher } from 'svelte'


    export let isModalVisable = false; 
    export let param; 
    export let title = null; 
    export let description = null; 
    
    const dipsatcher = createEventDispatcher()

    function closeModal()
    {
        dipsatcher('modalClosed')  
   } 

</script>

{#if isModalVisable }
    <div class="modal modal-open">
        <div class="card card-side bg-base-100 shadow-xl w-3/4">
            <figure><img class=" max-w-screen-sm max-h-96 object-cover rounded-xl" src={param} alt="Album"/></figure>
            <div class="card-body">
                <h2 class="card-title">{title === null ? 'No title' :  title}</h2>
              <p>{description === null ? 'No description' : description}</p>
                <div class="card-actions justify-end ">
                    <button on:click={closeModal} class="btn">Close</button>
                </div> 
            </div>
          </div>
    </div>

{/if}