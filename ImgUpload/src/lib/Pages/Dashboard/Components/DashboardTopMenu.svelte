<script>
    import { Icon,ArrowPath,Share,Envelope ,ArrowDownTray,EllipsisVertical} from "svelte-hero-icons";
    import IconButton from "../../../Controls/Buttons/IconButton.svelte";
</script>

<div class="grid grid-cols-1 sm:grid-cols-2 gap-4">
    <div class="">
    
    
    </div>
    <div class="text-right ">
        <IconButton icon={ArrowPath} iconStyle="w-4 mr-2"> Refresh data</IconButton>
        <div class="dropdown dropdown-bottom dropdown-end  ml-2">
            <label tabIndex={0} class="btn btn-ghost btn-sm normal-case btn-square "><Icon src={EllipsisVertical} class="w-5"/></label>
            <ul tabIndex={0} class="dropdown-content menu menu-compact  p-2 shadow bg-base-100 rounded-box w-52">
                <li><a><Icon src={Envelope} class="w-4"/>Email Digests</a></li>
                <li><a><Icon src={ArrowDownTray} class="w-4"/>Download</a></li>
            </ul>
        </div>
    </div>
</div>