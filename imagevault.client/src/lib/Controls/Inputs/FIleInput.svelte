<script>
 import {createEventDispatcher} from "svelte";

 export let value = null;
 export let label = '';


 const dispatcher = createEventDispatcher();
 function dispatchEvent(event)
 {
     dispatcher(event.type,event)
 }
 
</script>


<div class="form-control w-full ">
    <label>
        <span class="label-text text-base-content"> {label} </span>
    </label>

<input type="file" bind:files={value} class="file-input file-input-bordered file-input-primary w-full max-w-xs" />
</div>


