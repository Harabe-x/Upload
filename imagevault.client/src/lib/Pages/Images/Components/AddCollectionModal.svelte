<script>
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";
    import TextInput from "../../../Controls/Inputs/TextInput.svelte";
    import FIleInput from "../../../Controls/Inputs/FIleInput.svelte";
    import {createEventDispatcher} from "svelte";
    import {Check, XMark} from "svelte-hero-icons";
    import {onEscapeAction,onEnterAction} from "../../../../js/UserInterface/Actions/ModalActions.js";

    const dispatcher = createEventDispatcher();
    export let isModalVisable = false;
    let title;
    let description;


    function closeModal()
    {
        dispatcher('modalClosed')
    }
    function addCollection()
    {
        //TODO
        closeModal()
    }

</script>


{#if isModalVisable}

    <div class="modal modal-open" use:onEnterAction use:onEscapeAction  on:enter={addCollection} on:escape={closeModal} >
        <div class="modal-box flex flex-col gap-3 "  >
            <TextInput label="Collection title" bind:value={title}></TextInput>
            <TextInput label="Collection description" bind:value={description}></TextInput>
            <FIleInput label="Collection Cover"></FIleInput>
            <div class="modal-action  ">
                <IconButton on:click={closeModal} icon={XMark} iconStyle="w-4 ml-1" >Close</IconButton>
                <IconButton on:click={addCollection}  icon={Check} iconStyle="w-4 ml-1" buttonStyle="bg-success text-primary-content"> Save </IconButton>
            </div>
        </div>
    </div>

{/if}
