namespace SimpleMvs.Framework.Controllers
{
    using ActionResults;
    using Attributes.Property;
    using Interfaces;
    using Models;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System;
    using System.Reflection;
    using System.Linq;
    using Security;
    using Views;
    using WebServer.Http.Contracts;
    using WebServer.Http;

    public abstract class Controller
    {

        protected Controller()
        {
            this.Model = new ViewModel();

            //if we dont have a user we create it
            this.User = new Authentication();
        }


        protected ViewModel Model { get; }

        protected internal IHttpRequest Request { get; internal set; }

        protected Authentication User { get; set; }

        protected IViewable View([CallerMemberName] string caller = "")
        {
            this.InitializeViewModelData();

            string controllerName = this.GetType()
                .Name
                .Replace(MvcContext.Get.ControllersSuffix, string.Empty);

            string fullQualifiedName = string.Format(
                "{0}\\{1}\\{2}",
                MvcContext.Get.ViewsFolder,
                controllerName, //.Remove(controllerName.Length - 10),
                caller);

            IRenderable view = new View(fullQualifiedName, this.Model.Data);

            return new ViewResult(view);
        }

        protected IRedirectable RedirectToAction(string redirectUrl)
        {
            return new RedirectResult(redirectUrl);
        }

        protected bool IsValidModel(object bindingModel)
        {
            //we foreach its properties
            foreach (var property in bindingModel.GetType().GetProperties())
            {

                //we take only the attributes for each property
                IEnumerable<Attribute> attributes =
                    property.GetCustomAttributes()
                        .Where(a => a is PropertyAttribute);

                //if there arent any we ontinue to the next property
                if (!attributes.Any())
                {
                    continue;
                }

                foreach (PropertyAttribute attribute in attributes)
                {
                    if (!attribute.IsValid(property.GetValue(bindingModel)))
                    {
                        return false;
                    }
                }

            }

            return true;

        }

        //initialize the Session in the controller, each time the Controller is initialized.
        //its internal so we can use it only in this assembly
        protected internal void InitializeController()
        {
            var user = this.Request
                .Session
                .Get<string>(SessionStore.CurrentUserKey);

            if (user != null)
            {
                this.User = new Authentication();
            }

        }
        
        //methods to work with the session, login and logout user
        protected void SignIn(string name)
        {
            this.Request.Session.Add(SessionStore.CurrentUserKey, name);
        }

        protected void SignOut()
        {
            this.Request.Session.Clear();
        }

        private void InitializeViewModelData()
        {
            this.Model["displayType"] = this.User.IsAuthenticated ? "block" : "none";
        }

    }
}


