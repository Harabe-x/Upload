<script>
    import UserProfile from './UserProfile.svelte'
</script>

<!-- HTML -->
    <div class="nav">
        <slot /> 
        <div class="user-profile" >
            <hr class="line"> 
            <UserProfile></UserProfile>
        </div>    
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
  .user-profile { 
     margin-top: auto; 
     margin-bottom: 10%;
  }  
  .line { 
     color: #3c3c3c;
     
     height: 0.5px;
  }


</style>