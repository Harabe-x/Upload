<script> 
    import { createEventDispatcher } from 'svelte'


    export let isModalVisable = false; 
    export let imgSrc; 
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
        <div class="card lg:card-side bg-base-100 shadow-xl">
            <figure><img class="object-cover" src={imgSrc} alt="Album"/></figure>
            <div class="card-body">
                <h2 class="card-title">{title === null ? 'lorem ipsum title' :  title}</h2>
              <p>{description === null ? 'Lorem Ipsum description' : description}</p>
                <div class="card-actions justify-end ">
                    <button on:click={closeModal} class="btn">Close</button>
                </div>  
            </div>
          </div>
    </div>

{/if}