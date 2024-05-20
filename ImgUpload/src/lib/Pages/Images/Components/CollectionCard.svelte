<script>
  import { Eye } from "svelte-hero-icons";
  import IconButton from "../../../Controls/Buttons/IconButton.svelte";

  export let collection;
  export let imgSrc;

  function navigateToCollection()
  {
    
  }
</script> 


<div class="carousel-item  hover:cursor-pointer">

    <img class="rounded-xl h-full w-full transition-transform duration-300 transform object-cover hover:scale-105" src={imgSrc} alt="Collection item" />
    <div class="absolute inset-0 bg-black opacity-0 hover:opacity-75 transition-opacity flex flex-col items-center justify-center">
        <h1 class="text-2xl">{collection.Name}</h1>
    </div>
  </div>  