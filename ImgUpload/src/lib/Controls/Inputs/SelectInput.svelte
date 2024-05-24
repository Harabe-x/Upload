<script>
    export let title;
</script>

<div>
    <label>
        <span class="text-sm">{title}</span> <br>
        <select class="select w-full ">
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