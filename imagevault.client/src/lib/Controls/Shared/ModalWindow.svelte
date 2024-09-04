<script>
    import AddApiKeyModal from "../../Pages/Api/Components/AddApiKeyModal.svelte";
    import ApiKeyEditModal from "../../Pages/Api/Components/ApiKeyEditModal.svelte";
    import ApiKeyDeleteModal from "../../Pages/Api/Components/ApiKeyDeleteModal.svelte";
    import AddImageModal from "../../Pages/Images/Components/AddImageModal.svelte";
    import ImageBrowserModal from "../../Pages/Images/Components/ImageBrowserModal.svelte";
    import AddCollectionModal from "../../Pages/Images/Components/AddCollectionModal.svelte";
    import { onMount } from "svelte";
    import DeleteImageModal from "@/lib/Pages/Images/Components/DeleteImageModal.svelte";
    import ImageInfoModal from "@/lib/Pages/Images/Components/ImageInfoModal.svelte";
    
    export let type; 
    export let param = null; 
    const componentDictionary = new Map();

    let isModalVisable = false;

    onMount(() => {
        componentDictionary.set('AddApiKeyModal',AddApiKeyModal)
        componentDictionary.set('ApiKeyEditModal',ApiKeyEditModal)
        componentDictionary.set('ApiKeyDeleteModal',ApiKeyDeleteModal)
        componentDictionary.set('AddImageModal',AddImageModal)
        componentDictionary.set('ImageBrowserModal',ImageBrowserModal)
        componentDictionary.set('AddCollectionModal',AddCollectionModal)
        componentDictionary.set('DeleteImageModal',DeleteImageModal)
        componentDictionary.set('ImageInfoModal',ImageInfoModal)
        
    })

    
   export function toggleModal(event)
    {
        // Event detail is function passed by child as event param;
        if(event !== undefined && typeof event.detail == 'function')
        {
            const modalAction = event.detail;
            modalAction();
        }       

        isModalVisable = !isModalVisable; 
    }

</script>

{#if isModalVisable}
<svelte:component on:modalClosed={toggleModal} {param}  {isModalVisable}  this={componentDictionary.get(type)} ></svelte:component>
{/if}