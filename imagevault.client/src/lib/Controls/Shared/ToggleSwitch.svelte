<script>
 
 export let value; 
 export let title; 
 
</script>


<div class="form-control flex-row">
    <label class="cursor-pointer label">
        <input type="checkbox"  bind:checked={value}  class="checkbox checkbox-success ml-auto mr-4" />
        <span class="label-text">{title}</span>
    </label>
</div>