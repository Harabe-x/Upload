<script>
    export let imgSrc;
    export let imgTitle = null;
</script>


<div class="w-full relative pb-[74%] cursor-pointer">
    <div class="skeleton rounded-xl object-cover absolute  h-full w-full transition-transform duration-300 transform hover:scale-100"></div>
    <img class="rounded-xl object-cover absolute  h-full w-full transition-transform duration-300 transform hover:scale-100"src={ "https://" + imgSrc} alt=""  />
        <div class="rounded-xl absolute inset-0 bg-black opacity-0 hover:opacity-75 transition-opacity flex flex-col items-center justify-center">
           <span class="text-white">  {imgTitle === null ? '' : imgTitle} </span>
        </div>
</div>