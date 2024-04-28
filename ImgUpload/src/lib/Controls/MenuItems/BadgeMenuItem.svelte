<script>
    export let badgeText; 
    export let badgeColor;
    export let url = ''
</script>

<li>
    <a href={url}>
        <div class="badge badge-{badgeColor}">{badgeText}</div>
            <slot> item </slot>
    </a>
</li>