<script>
    import { createEventDispatcher } from "svelte";
    import TextInput from "../../../Controls/Inputs/TextInput.svelte";
    import FIleInput from "../../../Controls/Inputs/FIleInput.svelte";
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";
    import {Check, Icon, Key, XMark} from "svelte-hero-icons";
    import { onEnterAction,onEscapeAction} from "../../../../js/UserInterface/Actions/ModalActions.js";
    import SelectInput from "@/lib/Controls/Inputs/SelectInput.svelte";
    import ToggleSwitch from "@/lib/Controls/Shared/ToggleSwitch.svelte";
    import {getImageManagerStore} from "@/js/State/Image/ImageStore.js";

    let title = '';
    let description = '';
    let collectionName = ''; 
    let image; 
    let useCompression = true;
    let isAddImageButtonDisabled = false; 
  
  
    export let param;
    const dispatcher = createEventDispatcher()
    const imageManager = getImageManagerStore()
    function closeModal()
    {
        dispatcher('modalClosed')
    }
    async function savePhoto()
    {
        isAddImageButtonDisabled = true; 
        
        if ($param.collection === "" ) await imageManager.uploadImage(image, $param.key, collectionName, title, description, useCompression)
        else await imageManager.uploadImage(image, $param.key, $param.collection, title, description, useCompression)

        closeModal()
    }
    
 export let isModalVisable  = false;
</script>

{#if  isModalVisable}
    <div class="modal modal-open" use:onEnterAction use:onEscapeAction on:enter={savePhoto} on:escape={closeModal}>
        <div class="modal-box flex flex-col gap-3 "  >
            <TextInput disabled={true} label="Key"  bind:value={$param.key}></TextInput>
            <TextInput label="Title" bind:value={title}></TextInput>
            <TextInput label="Description" bind:value={description}></TextInput>
            
            {#if $param.collection === ""}
                <SelectInput bind:value={collectionName}  title="Collection:">
                    {#each $param.collections as collection}
                        <option  >{collection.collectionName}</option>
                    {/each}
                </SelectInput>
                {:else }
                <TextInput disabled={true} label="Collection"  bind:value={$param.collection}></TextInput>
                 {/if}
            <ToggleSwitch title="Use compression ?" bind:value={useCompression}></ToggleSwitch>
            <FIleInput bind:value={image} label="Image"></FIleInput>
            <div class="modal-action  ">
                <IconButton on:click={closeModal} icon={XMark} iconStyle="w-4 ml-1" >Close</IconButton>
                <IconButton on:click={savePhoto} disabled={isAddImageButtonDisabled} icon={Check} iconStyle="w-4 ml-1" buttonStyle="bg-success text-primary-content"> Save </IconButton>
            </div>
        </div>
    </div>
{/if}