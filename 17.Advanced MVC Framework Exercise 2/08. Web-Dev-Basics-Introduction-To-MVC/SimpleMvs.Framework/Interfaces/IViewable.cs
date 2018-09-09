namespace SimpleMvs.Framework.Interfaces
{
    public interface IViewable : IActionResult
    {
        //has a view
        IRenderable View { get; set; }
    }
}
