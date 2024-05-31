<script>
    import {createEventDispatcher} from "svelte";

    const dispatcher = createEventDispatcher();

    export let title;

    function dispatchEvent(event)
    {
        dispatcher(event.type,event)
    }
</script>

<div>
    <label>
        <span class="text-sm">{title}</span> <br>
        <select on:change={dispatchEvent}  class="select w-full"  {...$$restProps}>
            <slot>
                <option selected> Main </option>
                <option> Main </option>
                <option> Main </option>
                <option> Main </option>
                <option> Main </option>
                <option> Main </option>
            </slot>
        </select>
    </label>
</div>