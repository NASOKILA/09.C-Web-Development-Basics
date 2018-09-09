
namespace SimpleMvs.Framework.ViewEngine.Generic
{
    using Interfaces.Generic;
    using System;

    public class ActionResult<T> : IActionResult<T>
    {

        public ActionResult(string viewFullQualifiedName, T model)
        {
            //we set the action to be an instance of the passed class name
            this.Action =
                (IRenderable<T>)Activator
                .CreateInstance(Type.GetType(viewFullQualifiedName));

            //we set the model of hte action
            this.Action.Model = model;
        }

        public IRenderable<T> Action { get; set; }

        public string Invoke()
        {
            //wecall its render method
            return this.Action.Render();
        }
    }
}
