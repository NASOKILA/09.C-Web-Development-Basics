namespace SimpleMvs.Framework.Interfaces
{

    public interface IActionResult : IInvocable
    {
        //1 property called Action of type IRenderable. Our view engine will implement that interface
        IRenderable Action { get; set; }
    }
}
