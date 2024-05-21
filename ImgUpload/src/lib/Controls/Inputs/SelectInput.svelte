<script>
    export let title;
</script>

<div class="mr-1">
    <label>
        <span class="text-sm">{title}</span> <br>
        <select class="select select-info  w-36">
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