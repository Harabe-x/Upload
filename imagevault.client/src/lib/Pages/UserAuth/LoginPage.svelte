<script>
    import TextInput from "@/lib/Controls/Inputs/TextInput.svelte";
    import {LottiePlayer} from "@lottiefiles/svelte-lottie-player";
    import {getAuthStore} from "@/js/State/Auth/AuthStore.js";
    import {Link, navigate} from "svelte-routing";
    import { get } from "svelte/store";
    import {onMount} from "svelte";
    import { link } from 'svelte-routing'
    import {ArrowPath, Icon} from "svelte-hero-icons";
    import {checkIfUserIsLoggedIn} from "@/js/State/Auth/AuthHelpers.js";
    import { DotLottieSvelte } from '@lottiefiles/dotlottie-svelte';


    let email;
    let password;
    let isLoggingIn = false;
    let isError = false;


    onMount(() => {

        checkIfUserIsLoggedIn(() => {
            navigate("/app")
        })
    })

   async function login()
    {
         isLoggingIn = true;

        const authStore = getAuthStore();

        await  authStore.login(email,password)
        
        const store = get(authStore)

        if(!store.isLoggedIn)
        {
         isError = true;
         isLoggingIn = false;
         return;
        }

        navigate("/app/")
    }
    function cancelError()
    {
        isError = false;
    }



</script>

<div class="min-h-screen bg-base-200 flex items-center">
    <div class="card mx-auto w-full max-w-5xl shadow-xl">
        <div class="grid md:grid-cols-2 grid-cols-1">
            <div class="py-24 px-10">
                <p class="text-center text-3xl font-semibold mb-4">Login</p>
                <div class="form-control gap-3">
                    <TextInput on:focus={cancelError}  {isError} bind:value={email} label="Email" placeholder="Email"></TextInput>
                    <TextInput on:focus={cancelError}   {isError} errorMessage="Invalid email or password" type="password" bind:value={password}   label="Password" placeholder="Password"></TextInput>
                    <p class="text-xs link">Forgot password?</p>
                    <button on:click={login} class="btn btn-primary mt-10">
                        {#if isLoggingIn}
                                <Icon src={ArrowPath} class="w-6 animate-spin" ></Icon>
                            {:else }
                                Login
                            {/if}
                    </button>
                </div>
            </div>

            <div class="hero min-h-full rounded-l-xl bg-base-200 hidden sm:block">
                <div class="hero-content w-full py-12 bg-base-300">
                    <div class="text-center">
                        <h1 class="text-2xl font-semibold">ImageVault</h1>
                        <div class="w-[56rem] h-full flex flex-col items-center justify-center">
                            <DotLottieSvelte
                                    loop={true}
                                    autoplay={true}
                                    src="https://lottie.host/708d3e2c-2c70-4d28-8eae-7cff3c174380/UmvDwCf4xw.json">
                            </DotLottieSvelte>
                        </div>
                        <p>Don’t have an account yet? <a class="link" href="/register" use:link> Register </a> </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
