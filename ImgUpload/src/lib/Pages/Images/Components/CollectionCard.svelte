<script>
    import { Eye } from "svelte-hero-icons";
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";

    export let collection;
    export let imgSrc;
    export let displayImage = true;

    function navigateToCollection() {
        // Logika nawigacji do kolekcji
    }

    function collectionImageLoadFailed(event) {
        displayImage = false;
    }
</script>

<div class="carousel-item w-full h-full lg:w-1/3  rounded-xl sm:w-full  cursor-pointer relative">
    {#if displayImage}
        <img
                on:error={collectionImageLoadFailed}
                src={imgSrc}
                class="h-full w-full object-cover rounded-md"
                alt="Collection Cover"
        />
    {:else}
        <div class="carousel-item  flex flex-col  justify-center items-center max-h-full h-full w-full bg-base-300 rounded-md  max-w-[60%] lg:max-w-full md:max-w-full ">
            <span class="font-semibold">{collection.Name}</span>
        </div>
    {/if}
</div>
