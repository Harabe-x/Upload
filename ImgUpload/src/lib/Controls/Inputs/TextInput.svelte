<script>
    export let label; 
    export let placeholder = undefined;
    export let value = ''; 
    export let type = '';
    
    function setInputTypeAction(node)
    {
        node.type = type.length === 0 ? 'text' : type ;
    }
</script>


<div class="form-control w-full ">
        <label>
            <span class="label-text text-base-content"> {label} </span>
        </label>
    <input use:setInputTypeAction {placeholder}  bind:value={value} class="input  input-bordered w-ful" /> 
</div>  