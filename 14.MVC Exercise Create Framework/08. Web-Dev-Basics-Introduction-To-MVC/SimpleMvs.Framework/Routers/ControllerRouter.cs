namespace SimpleMvs.Framework.Routers
{
    using WebServer.Contracts;
    using Attributes.Methods;
    using Controllers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using WebServer.Http.Contracts;
    using WebServer.Http.Response;
    using WebServer.Enums;
    using SimpleMvs.Framework.Interfaces;

    //The main purpose of this class would be to transform the incoming request to a response
    public class ControllerRouter : WebServer.Contracts.IHandleable
    {

        private IDictionary<string, string> getParams = new Dictionary<string, string>();

        private IDictionary<string, string> postParams = new Dictionary<string, string>();

        private string requestMethod;

        private string controllerName;
            
        private string actionName;

        private object[] methodParams;


        public IHttpResponse Handle(IHttpRequest request)
        {

            var getTokens =
                    request
                    .UrlParameters;
            
            foreach (var item in getTokens)
            {
                string key = item.Key;
                string value = item.Value;

                this.getParams[key] =  value ;
            }


            var postTokens = request.FormData;

            foreach (var item in postTokens)
            {
                string key = item.Key;
                string value = item.Value;

                this.postParams[key] = value;
            }

            

            this.requestMethod = request.Method.ToString().ToUpper();


            string getControllerAndActionTokens =
                    request
                    .Url
                    .Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries)[0];

            if (getControllerAndActionTokens.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries).Length < 2)
            {
                return new NotFoundResponse();
            }

            string controllerName = getControllerAndActionTokens
                    .Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[0] + "Controller";
            controllerName = controllerName.First().ToString().ToUpper() + controllerName.Substring(1);
            this.controllerName = controllerName;


            string actionName = getControllerAndActionTokens
                    .Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries).Last();
            actionName = actionName.First().ToString().ToUpper() + actionName.Substring(1);
            this.actionName = actionName;


            if (actionName == controllerName)
            {
                //return "Invalid URL";
                return new NotFoundResponse();
            }
            

            //obtain the method of the controller
            MethodInfo method = this.GetMethod();
            
            if(method == null){
                return new NotFoundResponse();
            }


            //convert the string parameters from getParams or postParams dictionaries to their appropriate type that the method in the controller expects
            IEnumerable<ParameterInfo> parameters = method.GetParameters();

            this.methodParams = new object[parameters.Count()];

            int index = 0;

            foreach (ParameterInfo param in parameters)
            {
                if (param.ParameterType.IsPrimitive ||
                    param.ParameterType == typeof(string))
                {

                    object value = this.getParams[param.Name];
                    this.methodParams[index] = Convert.ChangeType(
                        value,
                        param.ParameterType
                        );

                    index++;
                }
                else
                {
                    Type bindingModelType = param.ParameterType;

                    object bindingModel =
                        Activator.CreateInstance(bindingModelType);

                    IEnumerable<PropertyInfo> properties
                        = bindingModelType.GetProperties();

                    foreach (PropertyInfo property in properties)
                    {
                        property.SetValue(bindingModel,
                            Convert.ChangeType(
                                postParams[property.Name],
                                property.PropertyType
                                )
                            );
                    }

                    this.methodParams[index] = Convert.ChangeType(
                        bindingModel,
                        bindingModelType
                        );
                    index++;
                }
            }

            IInvocable actionResult = (IInvocable)this.GetMethod()
                .Invoke(this.GetController(), this.methodParams);
            
            string content = actionResult.Invoke();

            IHttpResponse response =
                new ContentResponse(HttpStatusCode.Ok, content);

            return response;
        }

        //If the method is not annotated with any HttpMethodAttribute and the request method is GET we should return it
        private MethodInfo GetMethod()
        {
            MethodInfo method = null;

            foreach (MethodInfo methodInfo in this.GetSuitableMethods())
            {

                IEnumerable<Attribute> attributes = methodInfo
                    .GetCustomAttributes()
                    .Where(a => a is HttpMethodAttribute);

                if (!attributes.Any())
                {
                    if (this.requestMethod == "GET")
                        return methodInfo;
                }

                foreach (HttpMethodAttribute attribute in attributes)
                {
                    if (attribute.IsValid(this.requestMethod)) {
                        return methodInfo;
                    }
                }
            }

            return method;
        }


        //get all methods of the requested controller
        private IEnumerable<MethodInfo> GetSuitableMethods()
        {
            var controller = this.GetController();

            if (controller == null) {
                return new MethodInfo[0];
            }

            return
                this.GetController()
                    .GetType()
                    .GetMethods()
                    .Where(m => m.Name == this.actionName);
        }


        //creates an instance of the requested controller 
        private Controller GetController()
        {
            //vzimame infoto za kontrolera
            var controllerFullQualifieldName = string.Format(
                "{0}.{1}.{2}, {0}",      
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ControllersFolder,
                this.controllerName);

            //vzimame tiput
            Type type = Type.GetType(controllerFullQualifieldName);

            //proverqvame dali ne e null
            if (type == null) {
                return null;
            }

            //suzdvame instanciq
            var controller = (Controller)Activator.CreateInstance(type);

            return controller;
        }
    }
}
