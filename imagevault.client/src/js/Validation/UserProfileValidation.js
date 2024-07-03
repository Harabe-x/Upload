
export function validateEmail(email)
{
    const emailValidationRegex = /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;

    return emailValidationRegex.test(email);
}
export function validatePassword(password)
{
    const passwordValidationRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@.#$!%*?&^])[A-Za-z\d@.#$!%*?&]{8,15}$/;

    return passwordValidationRegex.test(password);
}
export function validateName(name)
{
    const nameValidationRegex = /^[a-zA-Z]*$/

    return nameValidationRegex.test(name) && name.length > 0;

}
export function validateColorScheme(colorSchema)
{
    const colorSchemas = ["dark","light","aqua","cupcake","black","nord","dracula","lemonade"]

    return colorSchemas.includes(colorSchema);
}
