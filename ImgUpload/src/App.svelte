<script>
    import NavigationBar from "./lib/NavigationBar/NavigationBar.svelte";
    import PageContent from "./lib/Containers/PageContent.svelte";
    import DashboardPage from "./lib/Pages/Dashboard/DashboardPage.svelte";
</script>
<div class="drawer  lg:drawer-open">
  <input id="left-sidebar-drawer" type="checkbox" class="drawer-toggle" />
  <PageContent>
      <DashboardPage>

      </DashboardPage>
  </PageContent>
  <NavigationBar></NavigationBar>
</div>


<style>
  
</style>
