<script>
    import AddApiKeyModal from "../../Pages/Api/Components/AddApiKeyModal.svelte";
    import ApiKeyEditModal from "../../Pages/Api/Components/ApiKeyEditModal.svelte";
    import ApiKeyDeleteModal from "../../Pages/Api/Components/ApiKeyDeleteModal.svelte";
    import AddImageModal from "../../Pages/Images/Components/AddImageModal.svelte";
    import PictureModal from "../../Pages/Images/Components/PictureModal.svelte";
    import { onMount } from "svelte";
    
    export let type; 
    export let param = null; 
    const componentDictionary = new Map();

    let isModalVisable = false;

    onMount(() => {
        componentDictionary.set('AddApiKeyModal',AddApiKeyModal)
        componentDictionary.set('ApiKeyEditModal',ApiKeyEditModal)
        componentDictionary.set('ApiKeyDeleteModal',ApiKeyDeleteModal)
        componentDictionary.set('AddImageModal',AddImageModal)
        componentDictionary.set('PictureModal',PictureModal)    
    })

    
    function toggleModal(event)
    {
        if(typeof event.detail == 'function')
        {
            const modalAction = event.detail;
            modalAction();
        }       

        isModalVisable = !isModalVisable; 
    }

</script>

<svelte:component on:modalCLosed={toggleModal} {param}   this={componentDictionary.get(type)} ></svelte:component>
