<script>
    import { createEventDispatcher } from "svelte";
    import TextInput from "../../../Controls/Inputs/TextInput.svelte";
    import FIleInput from "../../../Controls/Inputs/FIleInput.svelte";
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";
    import { Check, Icon, XMark } from "svelte-hero-icons";
    import { onEnterAction,onEscapeAction} from "../../../../js/Actions/ModalActions.js";

    let title = '';
    let description = '';

    const dispatcher = createEventDispatcher()

    function closeModal()
    {
        dispatcher('modalClosed')
    }
    function savePhoto()
    {
        // TODO 
        closeModal();
    }
 export let isModalVisable  = false;
</script>

{#if  isModalVisable}
    <div class="modal modal-open" use:onEnterAction use:onEscapeAction on:enter={savePhoto} on:escape={closeModal}>
        <div class="modal-box flex flex-col gap-3 "  >
            <TextInput label="Title" bind:value={title}></TextInput>
            <TextInput label="Description" bind:value={description}></TextInput>
            <FIleInput label="Image"></FIleInput>
            <div class="modal-action  ">
                <IconButton on:click={closeModal} icon={XMark} iconStyle="w-4 ml-1" >Close</IconButton>
                <IconButton on:click={savePhoto}  icon={Check} iconStyle="w-4 ml-1" buttonStyle="bg-success text-primary-content"> Save </IconButton>
            </div>
        </div>
    </div>
{/if}