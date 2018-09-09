namespace SimpleMvs.Framework.Interfaces.Generic
{

    public interface IActionResult<T> : IInvocable
    {
        //it is generic and its property Action is of type IRenderable<T>.
        IRenderable<T> Action { get; set; }
    }
}
