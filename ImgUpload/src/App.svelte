<script>
    import NavigationBar from "./lib/NavigationBar/NavigationBar.svelte";
    import PageContent from "./lib/Containers/PageContent.svelte";
    import DashboardPage from "./lib/Pages/Dashboard/DashboardPage.svelte";
    import { getNavigationStore } from "./js/Temp/NavigationStore";
    import { getNavBarStateStore } from "./js/Temp/NavbarStateStore";
    import {onMount} from "svelte";
    import {themeChange} from "theme-change";
    const navbarStateStore = getNavBarStateStore();
    const navigation = getNavigationStore();

    onMount(() => {
        themeChange(false)
    })

</script>
<div class="drawer  lg:drawer-open" class:drawer-open={$navbarStateStore}>
  <input id="left-sidebar-drawer" type="checkbox" class="drawer-toggle" />
  <PageContent>
      <svelte:component this={$navigation} />
  </PageContent>
  <NavigationBar></NavigationBar>
</div>


<style>
  
</style>
