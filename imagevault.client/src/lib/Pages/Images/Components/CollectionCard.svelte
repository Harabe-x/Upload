<script>
    import { Eye } from "svelte-hero-icons";
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";
    import {createEventDispatcher} from "svelte";

    export let collection;
    export let imgSrc;
    export let displayImage = false;

    const dispatcher = createEventDispatcher();

    function navigateToCollection() {
        // Logika nawigacji do kolekcji
    }

    function collectionImageLoadFailed(event) {
        displayImage = false;
    }
    function dispatchEvent(event)
    {
        console.log(event.type)
        dispatcher(event.type)
     }
</script>

<div class="carousel-item w-full h-full lg:w-1/3  rounded-xl sm:w-full  cursor-pointer relative">
    {#if displayImage}
        <button on:click={dispatchEvent}>
            <img
                    on:error={collectionImageLoadFailed}
                    src={imgSrc}
                    class="h-full w-full object-cover rounded-md"
                    alt="Collection Cover"
            />
        </button>
        <div class="absolute inset-0 bg-black opacity-0 hover:opacity-75 transition-opacity flex flex-col items-center justify-center">
            <span  class="text-white">{collection.CollectionName}</span>
        </div>
    {:else}
              <button class="carousel-item max-h-full h-full w-full"  on:click={dispatchEvent}>
                  <div class="flex flex-col  justify-center items-center max-h-full h-full w-full bg-base-300 rounded-md  max-w-[60%] lg:max-w-full md:max-w-full ">
                      <span class="font-semibold ">{collection.CollectionName}</span>
                  </div>
              </button>
    {/if}

</div>
