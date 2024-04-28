<script>
    import LoadingPage from "../Pages/LoadingPage.svelte";
    import PageHeader from "./PageHeader.svelte";
</script>


<div class="drawer-content flex flex-col">
    <PageHeader pageTitle="Dashboard"></PageHeader>
    <main class="flex-1 overflow-y-auto md:pt-4 pt-4 px-6  bg-base-100">
        <slot> <LoadingPage> </LoadingPage> </slot>
    </main>

</div>