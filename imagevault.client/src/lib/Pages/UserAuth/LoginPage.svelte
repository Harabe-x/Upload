<script>
    import TextInput from "@/lib/Controls/Inputs/TextInput.svelte";
    import {LottiePlayer} from "@lottiefiles/svelte-lottie-player";
    import {getAuthStore} from "@/js/State/Auth/AuthStore.js";
    import { navigate} from "svelte-routing";
    import { get } from "svelte/store";

    const authStore = getAuthStore();

    let email;
    let password;

    function logIn()
    {
        authStore.login(email,password)

        var store = get(authStore)

        if(!store.isLoggedIn) return;

        navigate("/app/")
    }
</script>

<div class="min-h-screen bg-base-200 flex items-center">
    <div class="card mx-auto w-full max-w-5xl shadow-xl">
        <div class="grid md:grid-cols-2 grid-cols-1">
            <div class="py-24 px-10">
                <p class="text-center text-3xl font-semibold mb-4">Login</p>
                <div class="form-control gap-3">
                    <TextInput bind:value={email} label="Email" placeholder="email"></TextInput>
                    <TextInput bind:value={password}   label="Password" placeholder="password"></TextInput>
                    <p class="text-xs link">Forgot password?</p>
                    <button on:click={logIn} class="btn btn-primary mt-10">Login</button>
                </div>
            </div>

            <div class="hero min-h-full rounded-l-xl bg-base-200 hidden sm:block">
                <div class="hero-content w-full py-12 bg-base-300">
                    <div class="text-center">
                        <h1 class="text-2xl font-semibold">ImageVault</h1>
                        <div class="w-full h-full flex flex-col items-center justify-center">
                            <LottiePlayer
                                    loop={true}
                                    autoplay={true}
                                    background="transparent"
                                    style="width: 24rem; margin-right: 1rem"
                                    src="https://lottie.host/708d3e2c-2c70-4d28-8eae-7cff3c174380/UmvDwCf4xw.json">
                            </LottiePlayer>
                        </div>
                        <p>Don’t have an account yet? <a class="link" href="#">Register</a></p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
