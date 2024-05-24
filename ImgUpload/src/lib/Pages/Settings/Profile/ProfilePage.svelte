
<script>
    import Card from "../../../Controls/Cards/Card.svelte";
    import TextInput  from "../../../Controls/Inputs/TextInput.svelte";
    import FIleInput from "../../../Controls/Inputs/FIleInput.svelte";
    import SelectInput from "../../../Controls/Inputs/SelectInput.svelte";

</script>

<Card title="Profile Settings">

    <div  class="grid grid-cols-1 md:grid-cols-2 gap-6">
        <TextInput label="First Name" placeholder="Your first name"  />
        <TextInput label="Last Name" placeholder="Your last name"  />
        <TextInput label="Email" placeholder="email@example.com"  />
        <FIleInput label="Profile Picture"> </FIleInput>
  </div>
    <div  class="divider" ></div>

    <div  class="grid grid-cols-1 md:grid-cols-2 gap-6">
        <TextInput label="Language" placeholder="English"  />
    </div>

    <div class="mt-16"> <button class="btn btn-primary float-right w-36">Update</button></div>
</Card>
<Card title="Preferences">

    <div  class="grid grid-cols-1 md:grid-cols-2 gap-6">
        <SelectInput title="Application Theme">
            <option> Dark</option>
            <option selected> Light </option>
            <option> Cupcake </option>
        </SelectInput>

        <SelectInput title="Application Language">
            <option selected> English </option>
        </SelectInput>
    </div>

    <div class="mt-16"> <button class="btn btn-primary float-right w-36">Save</button></div>
</Card>