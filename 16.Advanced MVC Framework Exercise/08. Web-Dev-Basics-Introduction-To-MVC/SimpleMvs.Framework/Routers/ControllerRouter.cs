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
    using Interfaces;

    //The main purpose of this class would be to transform the incoming request to a response
    public class ControllerRouter : IHandleable
    {
        private Controller GetController(string controllerName, IHttpRequest request)
        {        
            var controllerFullQualifieldName = string.Format(
                "{0}.{1}.{2}, {0}",
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ControllersFolder,
                controllerName);
            
            Type type = Type.GetType(controllerFullQualifieldName);
            if (type == null)
            {
                return null;
            }
            
            var controller = (Controller)Activator.CreateInstance(type);
            return controller;
        }
        
        private IEnumerable<MethodInfo> GetSuitableMethods(Controller controller, string actionName)
        {
            if (controller == null)
            {
                return new MethodInfo[0];
            }

            return
                controller
                    .GetType()
                    .GetMethods()
                    .Where(m => m.Name == actionName);
        }

        private MethodInfo GetMethod(Controller controller, string requestMethod, string actionName)
        {
            MethodInfo method = null;

            foreach (MethodInfo methodInfo in this.GetSuitableMethods(controller, actionName))
            {

                IEnumerable<Attribute> attributes = methodInfo
                    .GetCustomAttributes()
                    .Where(a => a is HttpMethodAttribute);

                if (!attributes.Any())
                {
                    if (requestMethod == "GET")
                        return methodInfo;
                }

                foreach (HttpMethodAttribute attribute in attributes)
                {
                    if (attribute.IsValid(requestMethod))
                    {
                        return methodInfo;
                    }
                }
            }

            return method;
        }
        
        public IHttpResponse Handle(IHttpRequest request)
        {

            IDictionary<string, string> getParams = request.UrlParameters;

            IDictionary<string, string> postParams = request.FormData;
            
            string requestMethod = request.Method.ToString();

            string[] invocationParameters = request.Path.Split('/', StringSplitOptions.RemoveEmptyEntries);

            if (invocationParameters.Length != 2)
            {
                throw new InvalidOperationException("Invalid URL");
            }

            //try uppercase to the first letter
            string controllerName = invocationParameters[0] + 
                MvcContext.Get.ControllersSuffix;
            controllerName = Char.ToUpper(controllerName.First()) + controllerName.Substring(1);
            
            string actionName = invocationParameters[1];
            actionName = Char.ToUpper(actionName.First()) + actionName.Substring(1);
            
            Controller controller = this.GetController(controllerName, request);

            if (controller != null)
            {
                controller.Request = request;
                controller.InitializeController();
            }

            MethodInfo method = this.GetMethod(controller, requestMethod, actionName);

            if (method == null)
            {
                return new NotFoundResponse();
            }
            
            IEnumerable<ParameterInfo> parameters
                = method.GetParameters();

            object[] methodParams = this.AddParameters(parameters, getParams, postParams);
            
            try
            {
                IHttpResponse response = this.GetResponse(method, controller, methodParams);
                return response;
            }
            catch (Exception e)
            {
                return new InternalServerErrorResponse(e);
            }

        }

        private IHttpResponse GetResponse(MethodInfo method, Controller controller, object[] methodParams)
        {
            object actionResult = method
                .Invoke(controller, methodParams);

            IHttpResponse response = null;

            if (actionResult is IViewable)
            {
                string content = ((IViewable)actionResult).Invoke();
                response = new ContentResponse(HttpStatusCode.Ok, content);
            }
            else if (actionResult is IRedirectable)
            {
                string redirectUrl = ((IRedirectable)actionResult).Invoke();
                response =new RedirectResponse(redirectUrl);
            }
            else
            {
                throw new InvalidOperationException("The View is not supported !");
            }

            return response;
        }

        private object[] AddParameters(IEnumerable<ParameterInfo> parameters, IDictionary<string, string> getParams, IDictionary<string, string> postParams)
        {
            object[] methodParams = new object[parameters.Count()];

            int index = 0;

            foreach (ParameterInfo parameter in parameters)
            {
                if (parameter.ParameterType.IsPrimitive ||
                    parameter.ParameterType == typeof(string))
                {
                    methodParams[index] = this.ProcessPrimitiveParameter(parameter, getParams);
                    index++;
                }
                else
                {
                    methodParams[index] = this.ProcessComplexParameter(parameter, postParams);
                    index++;
                }
            }

            return methodParams;

        }

        private object ProcessComplexParameter(ParameterInfo parameter, IDictionary<string, string> postParams)
        {
            Type bindingModelType = parameter.ParameterType;

            object bindingModel =
                Activator.CreateInstance(bindingModelType);

            IEnumerable<PropertyInfo> properties
                = bindingModelType.GetProperties();


            foreach (PropertyInfo property in properties)
            {
                property.SetValue(bindingModel,
                    Convert.ChangeType(postParams[property.Name], property.PropertyType));
            }

            return Convert.ChangeType(bindingModel, bindingModelType);
        }

        private object ProcessPrimitiveParameter(ParameterInfo parameter, IDictionary<string, string> getParams)
        {
            object value = getParams[parameter.Name];
            return Convert.ChangeType(value, parameter.ParameterType);
        }
    }
}
