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
    import LoginPage from "@/lib/Pages/UserAuth/LoginPage.svelte";
    import {getContext} from "svelte";
    import CollectionBrowser from "@/lib/Pages/Images/CollectionBrowser.svelte";
    import NotFoundPage from "@/lib/Pages/InfoPages/NotFoundPage.svelte";
    import PageUnderConstruction from "@/lib/Controls/Shared/PageUnderConstruction.svelte";

    const navbarStateStore = getNavBarStateStore();
    const navigation = getNavigationStore();

    let currentLocation;

    onMount(() => {
        currentLocation =  window.location.pathname.toString();
        console.log(currentLocation != '/login');
    })


</script>
<Router>
    <div class="drawer  lg:drawer-open" class:drawer-open={$navbarStateStore}>
        <input id="left-sidebar-drawer" type="checkbox" class="drawer-toggle" />
        <PageContent>
            <Route path="/" component={DashboardPage}></Route>
            <Route path="/api" component={ApiPage}></Route>
            <Route path="/images" component={ImagesPage}></Route>
            <Route path="/collectionBrowser"  component={CollectionBrowser}></Route>
            <Route path="/settings/profile" component={ProfilePage}></Route>
            <Route path="/settings/billing"  component={PageUnderConstruction}></Route>
            <Route path="/login" component={LoginPage}></Route>
            <Route path="*" component={NotFoundPage}></Route>

        </PageContent>
        <NavigationBar></NavigationBar>
    </div>
</Router>s

<style>

</style>
