<script>
    import { getUserProfileInfo } from "../../js/UserInfoStore";
    import getIconStore from "../../js/IconStore";
    import ProfilePictureDropdown from "../Dropdowns/ProfilePictureDropdown.svelte"
    const userProfileInfo = getUserProfileInfo();
    const iconStore = getIconStore(); 
    const userDropdownItems = [ 
        {text:"Account",url:"#",icon:$iconStore.User},
        {text:"Billing",url:"#",icon:$iconStore.CreditCard},
        {text:"Docs",url:"#",icon:$iconStore.Collection},
        {text:"API",url:"#",icon:$iconStore.Key},
        {text:"Logout",url:"#",icon:$iconStore.Logout}        
    ]

</script>

<!-- HTML 94a0a9 -->
<div class="navbar bg-base-100">
  <div class="flex-1">
    <a class="btn btn-ghost text-xl">ImageVault</a>
  </div>
  <div class="flex-none gap-2">
    <div class="form-control">
      <input type="text" placeholder="Search" class="input input-bordered w-24 md:w-auto" />
    </div>
        <ProfilePictureDropdown items={userDropdownItems}> </ProfilePictureDropdown>
  </div>
</div>
<style>
  


</style>