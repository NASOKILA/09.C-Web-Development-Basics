namespace SimpleMvs.Framework.Interfaces
{
    public interface IRedirectable : IActionResult
    {
        string RedirectUrl { get; }
    }
}
