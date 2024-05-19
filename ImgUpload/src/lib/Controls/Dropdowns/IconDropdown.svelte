<script>
    import {Icon,ArrowDownTray, EllipsisVertical, Envelope} from "svelte-hero-icons";
    export let icon;
</script>


<div class="dropdown dropdown-bottom dropdown-end">
    <label tabIndex={0} class="btn btn-ghost btn-sm normal-case btn-square "> <Icon src={icon} class="w-5"/> </label>
    <ul tabIndex={0} class="dropdown-content menu menu-compact  p-2 shadow bg-base-200 rounded-box w-52">
        <slot>
            <li> There should be list items </li>
        </slot>
    </ul>
</div>