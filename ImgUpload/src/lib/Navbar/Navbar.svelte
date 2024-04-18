<script>

</script>


<!-- HTML -->
    <div class="nav">
        <slot /> 
  
        <slot class="profile" name="profile" /> 
    </div>

<style>
  
 .nav { 
    width: 15%;
    height: 100%;   
    position: fixed;
    display: flex;
    flex-direction: column;
    align-items: center;
    background-color: #151515;
    left: 0;
 }      
 .profile { 
    margin: 90%;
 }

</style>