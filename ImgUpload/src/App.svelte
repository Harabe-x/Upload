<script>
    import NavigationBar from "./lib/NavigationBar/NavigationBar.svelte";
    import PageContent from "./lib/Containers/PageContent.svelte";
    import DashboardPage from "./lib/Pages/Dashboard/DashboardPage.svelte";
    import { getNavigationStore } from "./js/Temp/NavigationStore";
    
    const navigation = getNavigationStore();


</script>
<div class="drawer  lg:drawer-open">
  <input id="left-sidebar-drawer" type="checkbox" class="drawer-toggle" />
  <PageContent>
      <svelte:component this={$navigation} />
  </PageContent>
  <NavigationBar></NavigationBar>
</div>


<style>
  
</style>
