namespace SimpleMvs.Framework.ViewEngine
{
    using SimpleMvs.Framework.Interfaces;
    using System;

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
            return this.Action.Render();
        }
    }
}
