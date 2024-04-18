<script>
	import UserProfile from './UserProfile.svelte';
    import { getUserProfileInfo} from "../../js/Userprofile";
    import cog from '../../assets/cog.svg'
    
    const userProfile = getUserProfileInfo();
</script>


<div> 
     
    <div class="user-profile" > 
         <img src={$userProfile.profilePic} alt="Profile of {$userProfile.profileName}" />
         <div class="ptofile-name"> 
            <p>{$userProfile.profileName}</p>
                <div class="profile-item"> 
                    <img src={cog} alt="{$userProfile} profile picture"  />
                </div>
            </div > 
    </div>
     
</div>

<style> 
.user-profile { 
    display: flex; 
    font-size: 75%; 
}
.user-profile > img { 
    border-radius: 50%;
    margin: 5px
} 
.profile-name { 
    display: flex;
    flex-direction: column;
}
.profile-item { 
  display:flex;
}


</style>