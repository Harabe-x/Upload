<script>
    export let icon; 
    export let title; 


    import '../../app.css';



</script>   

<!-- HTML  -->
    <div class="flex">
        <div class="navbar bg-primary text-primary-content text-primary-contet"  >
            <button class="btn btn-ghost text-xl  " >
                <img src="{icon}" class="inline-block w-5 h-5" alt="Dashboard">
                {title}
            </button>
          </div>
    </div>

<style>

</style>


