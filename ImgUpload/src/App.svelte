<script>
  import NavbarItem from './lib/Navbar/NavbarItem.svelte';
  import Navbar from './lib/Navbar/Navbar.svelte'
  import TestComponent from './lib/Components/TestComponent.svelte';
  import getIconStore from './js/IconStore';
  import Dashboard from './lib/Pages/Dashboard.svelte';
  const icons = getIconStore();

</script>

<div class="flex flex-col">
  <Navbar></Navbar>
  <div  class="w-full h-screen bg-base-300">
     <Dashboard></Dashboard>
  </div>
</div>
<style>
  
</style>
