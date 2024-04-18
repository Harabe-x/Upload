<script>
    export let icon; 
    export let title; 

    function navigate(item)
    {

    } 
     
</script>   

<!-- HTML  -->
    <div class="navItem">
        <img src={icon} alt="navigation icon"/>
        <button on:click={navigate}> 
            <span> {title} </span>
        </button>
    </div>

<style>
    .navItem { 
        display: flex; 
        justify-content: center;
        align-items:c center;
    }
    .navItem > button { 
        all:unset;
    }
    .navItem > button:focus,button:hover {
        outline : revert;
        color: #ffffff;
    }
    .navItem > span { 
        color: #ececec;
    }

</style>


