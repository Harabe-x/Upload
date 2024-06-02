

export function onEnterAction(element)
{

    document.body.addEventListener("keydown", keyDownEventHandler);

    function keyDownEventHandler(event)
    {
        if(event.key === "Enter") {
            element.dispatchEvent(new CustomEvent("enter"));
        }
    }

    return {
        destroy()
        {
            document.body.removeEventListener("keydown", keyDownEventHandler)
        }
    }
}

export function onEscapeAction(element)
{
    document.body.addEventListener("keydown",keyDownEventHandler)

    function keyDownEventHandler(event)
    {
        if(event.code === "Escape") {
            element.dispatchEvent(new CustomEvent("escape"));
        }
    }

    return {
        destroy()
        {
            document.body.removeEventListener("keydown", keyDownEventHandler)
        }
    }
}

