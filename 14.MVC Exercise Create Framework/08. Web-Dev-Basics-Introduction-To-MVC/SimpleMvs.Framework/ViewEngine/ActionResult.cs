namespace SimpleMvs.Framework.ViewEngine
{
    using SimpleMvs.Framework.Interfaces;
    using System;

    //responsible for generating view by creating an instance of the desired view class and calling its Render() method
    public class ActionResult : IActionResult
    {
        public ActionResult(string viewFullQualifiedName)
        {
            Type type = Type.GetType(viewFullQualifiedName);

            this.Action = (IRenderable)Activator
                .CreateInstance(type);
        }

        public IRenderable Action { get; set; }

        public string Invoke()
        {
            //we call the Render method of that action
            return this.Action.Render();
        }
    }
}
