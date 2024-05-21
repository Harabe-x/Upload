<script>
    export let label; 
    export let placeholder = undefined;
    export let value = ''; 
    export let type = '';
    export let disabled = false;
    export let isError = false;
    export let errorMessage = '';
    function setInputTypeAction(node)
    {
        node.type = type.length === 0 ? 'text' : type ;
    }
</script>


<div class="form-control w-full ">
        <label>
            <span class="label-text text-base-content"> {label} </span>
        </label>
    <input  use:setInputTypeAction {placeholder} {disabled} bind:value={value} class="input  input-bordered w-ful" class:border-error={isError} />
        {#if isError}
        <span class="text-error text-xs">{errorMessage}</span>
        {/if}

</div>  