<script>
    import NavigationBar from "./lib/NavigationBar/NavigationBar.svelte";
    import PageContent from "./lib/Containers/PageContent.svelte";
    import DashboardPage from "./lib/Pages/Dashboard/DashboardPage.svelte";
    import { Router, Route } from "svelte-routing";
    import { getNavigationStore } from "./js/Temp/NavigationStore";
    import { getNavBarStateStore } from "./js/Temp/NavbarStateStore";
    import {onMount} from "svelte";
    import {themeChange} from "theme-change";
    import ApiPage from "@/lib/Pages/Api/ApiPage.svelte";
    import ImagesPage from "@/lib/Pages/Images/ImagesPage.svelte";
    import ProfilePage from "@/lib/Pages/Settings/Profile/ProfilePage.svelte";
    import BillingPage from "@/lib/Pages/Settings/Billing/BillingPage.svelte";
    import NotFoundPage from "@/lib/Pages/InfoPages/NotFoundPage.svelte";
    
    const navbarStateStore = getNavBarStateStore();
    const navigation = getNavigationStore();

    onMount(() => {
        themeChange(false)
    })

</script>
<div class="drawer  lg:drawer-open" class:drawer-open={$navbarStateStore}>
  <input id="left-sidebar-drawer" type="checkbox" class="drawer-toggle" />
  <PageContent>
      <Router>
          <Route path="/" component={DashboardPage}></Route>
          <Route path="/api" component={ApiPage}></Route>
          <Route path="/images" component={ImagesPage}></Route>
          <Route path="/settings/profile" component={ProfilePage}></Route>
          <Route path="/settings/billing" component={BillingPage}></Route>
          <Route path="*" component={NotFoundPage}></Route>

      </Router>
  </PageContent>
  <NavigationBar></NavigationBar>-->
</div>


<style>
  
</style>
