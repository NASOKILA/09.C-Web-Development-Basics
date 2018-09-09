namespace SimpleMvs.Framework.Interfaces.Generic
{

    //same as IRenderable
    public interface IRenderable<T> : IRenderable
    {
        //generate dynamic content to be displayed on the page
        T Model { get; set; }
    }
}
