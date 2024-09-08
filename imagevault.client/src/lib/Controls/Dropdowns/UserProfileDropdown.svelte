<script>
    import {getUserDataStore} from "@/js/State/User/UserDataStore.js";
    import {onMount} from "svelte";

    const userProfie = getUserDataStore()
    
    onMount(async() => {
        await fetchUserProfile()
    })
    
    async function fetchUserProfile()
    {
        await userProfie.fetchUserData()
    }
</script>

<div class="dropdown dropdown-end ml-4">
    <label tabIndex={0} class="btn btn-ghost btn-circle avatar">
        <div class="w-10 rounded-full bg-accent" >
            <div class="flex flex-row items-center justify-center">
                {#await userProfie.fetchUserData()}
                            
                    {:then s}
                    <p class=" text-white  text-2xl" > {$userProfie.userData.firstName[0]}  </p>
                {/await}
            </div>
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
