<script>
    import TextInput from "@/lib/Controls/Inputs/TextInput.svelte";
    import {LottiePlayer} from "@lottiefiles/svelte-lottie-player";
</script>

<div class="min-h-screen bg-base-200 flex items-center">
    <div class="card mx-auto w-full max-w-5xl shadow-xl">
        <div class="grid md:grid-cols-2 grid-cols-1">
            <div class="py-24 px-10">
                <p class="text-center text-3xl font-semibold mb-4">Register</p>
                <form class="form-control gap-1">
                    <TextInput label="First Name" placeholder="name"></TextInput>
                    <TextInput label="Last Name" placeholder="email"></TextInput>
                    <TextInput label="Email" placeholder="email"></TextInput>
                    <TextInput label="Password" placeholder="password"></TextInput>


                    <button class="btn btn-primary mt-10">Register</button>
                </form>
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
                                    style="width: 32rem; margin-right: 1rem"
                                    src="https://lottie.host/a01f59c1-5f8a-49b1-b519-2bf3ea6653b2/9xoIpWGXxT.json">
                            </LottiePlayer>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
