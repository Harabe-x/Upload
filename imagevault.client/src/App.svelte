<link rel="stylesheet" href="app.css">
<script>
    import NavigationBar from "./lib/NavigationBar/NavigationBar.svelte";
    import PageContent from "./lib/Containers/PageContent.svelte";
    import DashboardPage from "./lib/Pages/Dashboard/DashboardPage.svelte";
    import { Router, Route  , useLocation} from "svelte-routing";
    import { getNavigationStore } from "./js/Temp/NavigationStore";
    import { getNavBarStateStore } from "./js/Temp/NavbarStateStore";
    import {onMount} from "svelte";
    import {themeChange} from "theme-change";
    import ApiPage from "@/lib/Pages/Api/ApiPage.svelte";
    import ImagesPage from "@/lib/Pages/Images/ImagesPage.svelte";
    import ProfilePage from "@/lib/Pages/Settings/Profile/ProfilePage.svelte";
    import BillingPage from "@/lib/Pages/Settings/Billing/BillingPage.svelte";
    import NotFoundPage from "@/lib/Pages/InfoPages/NotFoundPage.svelte";
    import LoginPage from "@/lib/Pages/UserAuth/LoginPage.svelte";
    import RegisterPage from "@/lib/Pages/UserAuth/RegisterPage.svelte";
    import {getContext} from "svelte";
    import ApplicationMenu from "@/ApplicationUI.svelte";
    import {getAuthStore} from "@/js/State/Auth/AuthStore.js";
    import ToastNotification from "@/lib/Controls/Shared/ToastNotification.svelte";

    const navbarStateStore = getNavBarStateStore();
    const navigation = getNavigationStore();
    const authStore = getAuthStore();
    let currentLocation;

    onMount(() => {
        currentLocation =  window.location.pathname.toString();
        console.log(currentLocation != '/login');
    })


</script>


<Router>
    <Route path="/login" component={LoginPage}></Route>
    <Route path="/register" component={RegisterPage}></Route>
    <Route path="*" component={LoginPage}></Route>

    {#if $authStore.isLoggedIn}
        <Route path="/app/*"  component={ApplicationMenu}></Route>
        {/if}
</Router>

<ToastNotification> </ToastNotification>

<style>

</style>
