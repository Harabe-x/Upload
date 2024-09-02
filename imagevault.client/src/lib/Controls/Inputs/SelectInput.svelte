<script>
    import {createEventDispatcher} from "svelte";

    const dispatcher = createEventDispatcher();
    export let title;
    export let value = null;
    
    function dispatchEvent(event)
    {
        dispatcher(event.type,event)
    }
</script>

<div>
    <label>
        <span class="text-sm">{title}</span> <br>
        <select bind:value={value} on:change={dispatchEvent}  class="select w-full select-bordered"  {...$$restProps}>
            <slot>
                <option> Main </option>
                <option> Main </option>
                <option> Main </option>
                <option> Main </option>
                <option> Main </option>
                <option> Main </option>
            </slot>
        </select>
    </label>
</div>