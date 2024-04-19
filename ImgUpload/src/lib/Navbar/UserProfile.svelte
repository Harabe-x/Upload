<script>
	import UserProfile from './UserProfile.svelte';
    import { getUserProfileInfo} from "../../js/Userprofile";
    import horizontalDots from '../../assets/horizontal dots.svg' 
    import cog from '../../assets/cog.svg'
    
    const userProfile = getUserProfileInfo();
</script>


<div> 
     
    <div class="user-profile" > 
         <img src={$userProfile.ProfilePic} alt="Profile of {$userProfile.profileName}" />
        <p> {$userProfile.FirstName} </p> 
        <img src={horizontalDots} alt="menuIcons" class="icon"/> 
    </div>
     
</div>

<style> 
.user-profile { 
    width: 100%;
    display: flex; 
    margin-left: auto;
    font-size: 75%; 
}
.user-profile > img { 
    border-radius: 50%;
    margin: 5px
} 
.icon { 
    width: 42px;
    margin-right: auto;
}
</style>