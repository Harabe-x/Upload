<script>
 export let value = null;
 export let label = '';
</script>


<div class="form-control w-full ">
    <label>
        <span class="label-text text-base-content"> {label} </span>
    </label>

<input type="file" bind:value={value} class="file-input file-input-bordered file-input-primary w-full max-w-xs" />
</div>


