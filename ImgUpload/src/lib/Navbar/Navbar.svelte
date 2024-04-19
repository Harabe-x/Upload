<script>
    import UserProfile from './UserProfile.svelte'
</script>

<!-- HTML -->
    <div class="bg-primary nav">
        <div class="user-profile nav" >
            <slot /> 
        </div>    
    </div>

<style>
  
 .nav { 
    width: 10%;
    height: 100%;   
    position: fixed;
    display: flex;
    flex-direction: column;
    align-items: center;
    left: 0;
 }      
  .user-profile { 
     margin-top: auto; 
     margin-bottom: 10%;
  }  
  .line { 
     color: #3c3c3c;
     
     height: 0.5px;
  }


</style>