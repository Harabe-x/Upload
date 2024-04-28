<script>
    export let image;
</script>

<div class="dropdown dropdown-end ml-4">
    <label tabIndex={0} class="btn btn-ghost btn-circle avatar">
        <div class="w-10 rounded-full">
        <img src={image} alt="profile" />
        </div>
    </label>
    <ul tabindex="0" class="dropdown-content z-[1] menu p-2 shadow bg-base-100 rounded-box w-52">
        <slot> 
            <li> Dummy </li>
            <li> Dummy </li>
            <li> Dummy </li>
        </slot>
      </ul>
</div>
