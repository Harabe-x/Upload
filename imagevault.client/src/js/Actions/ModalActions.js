

export function onEnterAction(element)
{
    return onKeyDown(element,"Enter","enter")
}

export function onEscapeAction(element)
{
    return onKeyDown(element,"Escape","escape")
}
export function onArrowLeftAction(element)
{
    return onKeyDown(element,"ArrowLeft","arrowLeft")
}
export function onArrowRight(element)
{
  return  onKeyDown(element,"ArrowRight","arrowRight")
}


function onKeyDown(element,key,eventName)
{
    document.body.addEventListener("keydown", keyDownEventHandler)

    function  keyDownEventHandler(event)
    {

        if(event.key === key )
        {
            element.dispatchEvent(new CustomEvent(eventName));
        }
    }

    return {
        destroy() {
            document.body.removeEventListener("keydown", keyDownEventHandler)
        }
    }
}
