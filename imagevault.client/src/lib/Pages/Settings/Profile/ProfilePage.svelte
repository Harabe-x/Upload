
<script>
    import Card from "../../../Controls/Cards/Card.svelte";
    import TextInput  from "../../../Controls/Inputs/TextInput.svelte";
    import FIleInput from "../../../Controls/Inputs/FIleInput.svelte";
    import SelectInput from "../../../Controls/Inputs/SelectInput.svelte";
    import {onMount} from "svelte";
    import {themeChange} from "theme-change";
    import {getThemeStore} from "../../../../js/State/UserInterface/ThemeStore.js";
    import {get} from "svelte/store";
    import {getUserDataStore} from "@/js/State/User/UserDataStore.js";
    import {temporaryDisableButton} from "@/js/UserInterface/Utilities/TemporaryDisableButton.js";
    import {validateName,validateColorScheme} from "@/js/Validation/UserProfileValidation.js";

    const USER_PROFILE_UPDATE_TIMEOUT = 1000;

    const themeStore = getThemeStore();
    const userData = getUserDataStore();
    const themes = [];
    const dataValidationStatus = {
        isFirstNameValid: true,
        isLastNameValid: true,
        isPreferedColorSchemaValid: true
    };

    let email;
    let firstName;
    let lastName;
    let preferedColorSchema = "";

    onMount(async () => {
        themeChange(false);
        await setUserData();
    });

    async function setUserData() {
        let data = get(userData);

        if (data.userData === null) {
            await userData.fetchUserData();
            data = get(userData);
        }

        if (data.userData) {
            email = data.userData.email;
            firstName = data.userData.firstName;
            lastName = data.userData.lastName;
            preferedColorSchema = data.userData.preferedColorSchema;
        }
    }

    function updateTheme(event) {
        const newValue = event.detail.target.value;

        themeStore.update(_ => newValue);

        themes.forEach((element) => { element.removeAttribute('selected'); });

        const selectedTheme = themes.find(theme => theme.value === newValue);
        if (selectedTheme) {
            selectedTheme.setAttribute('selected', '');
        }
    }

    async function updateUserData(event) {
        temporaryDisableButton(event.target, USER_PROFILE_UPDATE_TIMEOUT);

        if (!checkIfUserDataChanged()) return;
        if (!validateUserData()) return;

        await userData.updateUserData(firstName, lastName, preferedColorSchema);
        await setUserData();
    }

    function validateUserData() {
        dataValidationStatus.isFirstNameValid = validateName(firstName);
        dataValidationStatus.isLastNameValid = validateName(lastName);
        dataValidationStatus.isPreferedColorSchemaValid = validateColorScheme(preferedColorSchema);

        return dataValidationStatus.isFirstNameValid && dataValidationStatus.isLastNameValid && dataValidationStatus.isPreferedColorSchemaValid;
    }

    function checkIfUserDataChanged() {
        const data = get(userData);

        return !(data.userData.firstName === firstName && data.userData.lastName === lastName && data.userData.preferedColorSchema === preferedColorSchema);
    }

    function addThemeAction(element) {
        themes.push(element);
    }


</script>

<Card title="Profile Settings">

    <div  class="grid grid-cols-1 md:grid-cols-2 gap-6">
        <TextInput bind:value={firstName} label="First Name" placeholder="Your first name" isError={!dataValidationStatus.isFirstNameValid} errorMessage="First name is invalid"   />
        <TextInput bind:value={lastName}  label="Last Name" placeholder="Your last name" isError={!dataValidationStatus.isLastNameValid} errorMessage="Last name is invalid"   />
        <TextInput bind:value={email} disabled="true" label="Email" placeholder="email@example.com"  />
        <FIleInput label="Profile Picture"> </FIleInput>
  </div>
    <div  class="divider" ></div>

    <div  class="grid grid-cols-1 md:grid-cols-2 gap-6">
        <TextInput label="Language" placeholder="English"  />
    </div>

    <div class="mt-16"> <button on:click={updateUserData} class="btn btn-primary float-right w-36">Update</button></div>
</Card>




<Card title="Preferences">

    <div  class="grid grid-cols-1 md:grid-cols-2 gap-6">
        <SelectInput bind:value={preferedColorSchema} on:change={updateTheme} title="Application Theme" data-choose-theme>
            <option use:addThemeAction  value="light"> Light </option>
            <option use:addThemeAction  value="dark"> Dark </option>
            <option use:addThemeAction  value="cupcake"> Cupcake </option>
            <option use:addThemeAction  value="dracula"> Dracula </option>
            <option use:addThemeAction  value="black"> Black </option>
            <option use:addThemeAction  value="nord"> Nord </option>
            <option use:addThemeAction  value="aqua"> Aqua </option>
            <option use:addThemeAction value="lemonade"> Lemonade </option>
        </SelectInput>

        <SelectInput title="Application Language">
            <option selected={true}> English </option>
        </SelectInput>
    </div>

    <div class="mt-16"> <button class="btn btn-primary float-right w-36">Save</button></div>
</Card>