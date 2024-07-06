    <script>
    import {getNotificationsStore} from "@/js/State/UserInterface/ToastNotificationStore.js";
    import { CheckCircle, Icon, XCircle } from "svelte-hero-icons";

    const notificationStore = getNotificationsStore();
    const ALERT_DISPLAY_TIME = 3000;

</script>

<div class="toast toast-end">

    {#each $notificationStore as toast (toast.id)}

        {#if toast.type === "success"}
            <div class="alert alert-success" >
                <div class="flex flex-row gap-3">
                    <Icon class="w-6 h-6"  src={CheckCircle}  ></Icon>
                    <span> {toast.message} </span>
                </div>
            </div>

            {:else}
            <div class="alert alert-error" >
                <div class="flex flex-row gap-3">
                    <Icon class="w-6 h-6"  src={XCircle}  ></Icon>
                    <span> {toast.message} </span>
                </div>
            </div>
        {/if}

<span class="hidden" > {setTimeout(() => notificationStore.removeLastNotification(), ALERT_DISPLAY_TIME)} </span>
    {/each}
</div>
