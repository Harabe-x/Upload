

const temporaryDisableButton = function (button, timeout)
{
    button.disabled = true;
    setTimeout(() => { button.disabled = false; }, timeout)
}

export { temporaryDisableButton }