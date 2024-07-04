<script>
    import TextInput from "@/lib/Controls/Inputs/TextInput.svelte";
    import {link, navigate} from 'svelte-routing'
    import {LottiePlayer} from "@lottiefiles/svelte-lottie-player";
    import {onMount} from "svelte";
    import { checkIfUserIsLoggedIn } from "@/js/State/Auth/AuthHelpers.js";
    import {ArrowPath, Icon} from "svelte-hero-icons";
    import {getAuthStore} from "@/js/State/Auth/AuthStore.js";
    import {get} from "svelte/store";
    import {validateColorScheme,validateEmail, validatePassword, validateName} from "@/js/Validation/UserProfileValidation.js";

    let dataValidationStatus = {
         isFirstNameValid : true,
        isLastNameValid : true,
        isEmailValid : true ,
        isPasswordValid : true,

    }


    let isRegistering,isError = false;
    let firstName, lastName,email,password

    onMount(() => {
        checkIfUserIsLoggedIn(() =>
        {
            navigate("/app")
        })
    })

    async function register() {


        if(!checkData()) return;

        isRegistering = true;

        const authStore = getAuthStore();

        await authStore.register(firstName, lastName, email, password)

        const store = get(authStore)

        if (!store.IsLoggedIn) {
            isRegistering = false;
            return;
        }

        navigate("/app")

    }

    function checkData()
    {
        dataValidationStatus.isPasswordValid = validatePassword(password);
        dataValidationStatus.isEmailValid = validateEmail(email);
        dataValidationStatus.isLastNameValid = validateName(lastName);
        dataValidationStatus.isFirstNameValid = validateName(firstName);

        return  dataValidationStatus.isFirstNameValid && dataValidationStatus.isLastNameValid && dataValidationStatus.isEmailValid &&  dataValidationStatus.isPasswordValid;
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
                <p class="text-center text-3xl font-semibold mb-4">Registers</p>
                <div class="form-control gap-1">
                    <TextInput on:focus={cancelError}   isError={ ! dataValidationStatus.isFirstNameValid } bind:value={firstName} label="First Name" errorMessage="First name can't have number or special characters" placeholder="First name"></TextInput>
                    <TextInput on:focus={cancelError}  isError={ ! dataValidationStatus.isLastNameValid } bind:value={lastName}   label="Last Name" errorMessage="Last name can't have number or special characters" placeholder="Last name"></TextInput>
                    <TextInput on:focus={cancelError}  isError={ ! dataValidationStatus.isEmailValid }  bind:value={email}      label="Email" errorMessage="Invalid email or email already exist"  placeholder="Email" type="email"></TextInput>
                    <TextInput on:focus={cancelError}  isError={ ! dataValidationStatus.isPasswordValid } errorMessage="The provided data is incorrect or a user with this email address already exists." bind:value={password}  label="Password" placeholder="Password" type="password"></TextInput>
                    <button on:click={register} class="btn btn-primary mt-10">
                            {#if isRegistering}
                                  <Icon src={ArrowPath} class="w-6 animate-spin"></Icon>
                                {:else}
                                 Register
                            {/if}
                    </button>
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
                                    style="width: 32rem; margin-right: 1rem"
                                    src="https://lottie.host/a01f59c1-5f8a-49b1-b519-2bf3ea6653b2/9xoIpWGXxT.json">
                            </LottiePlayer>
                            <p>Already have account? <a class="link" href="/login" use:link> Login </a> </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
