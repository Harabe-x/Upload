<script>
    import LoadingPage from "../Pages/LoadingPage.svelte";
    import PageHeader from "./PageHeader.svelte";
    import PageHeaderNameStoreStore from '../../js/Temp/PageHeaderNameStore'
    const pageHeaderStore = PageHeaderNameStoreStore();
</script>


<div class="drawer-content flex flex-col">
    <PageHeader pageTitle={$pageHeaderStore}></PageHeader>
    <main class="flex-1 overflow-y-auto md:pt-4 pt-4 px-6  bg-base-200">
        <div class="w-full h-screen" >
            <slot> 
                <LoadingPage> </LoadingPage>
            </slot>
        </div>
    </main>

</div>