namespace SimpleMvs.Framework.Interfaces
{
    public interface IRedirectable : IActionResult
    {
        //this one is for redirecting
        string RedirectUrl { get; }
    }
}
