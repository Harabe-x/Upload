<script>
    export let width = 10;
    export let items = [];
    export let picture = 'https://daisyui.com/images/stock/photo-1534528741775-53994a69daeb.jpg';
</script>


<div class="dropdown dropdown-end">
    <div tabindex="0" role="button" class="btn btn-ghost btn-circle avatar">
      <div class="w-{width} rounded-full">
        <img alt="Tailwind CSS Navbar component" src="https://daisyui.com/images/stock/photo-1534528741775-53994a69daeb.jpg" />
      </div>
    </div>

    <!-- svelte-ignore a11y-no-noninteractive-tabindex -->
    <ul tabindex="0" class="mt-3 z-[1] p-2 shadow menu menu-sm dropdown-content bg-base-100 rounded-box w-52">
        {#each items as item }        
            <li >
                <div>
                     <div class="badge badge-primary badge-sm"> <img class="w-5" src={item.icon} /> </div>
                     <a href={item.url}> {item.text} </a>
                </div>
            </li>
        {/each}
    </ul>
  </div>