<script>
  import NavbarItem from './lib/Navbar/NavbarItem.svelte';
  import Navbar from './lib/Navbar/Navbar.svelte'
  import TestComponent from './lib/Components/TestComponent.svelte';
  import getIconStore from './js/IconStore';

  const icons = getIconStore();

</script>

  <Navbar></Navbar>
  
  <TestComponent> </TestComponent>  
<style>
  
</style>
