namespace Handmade_HTTP_Server.Server.Handlers
{
    using Contracts;
    using System;
    using HTTP.Contracts;
    using HTTP;

    public abstract class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> handlingFunc;

        protected RequestHandler(Func<IHttpRequest, IHttpResponse> handlingFunc)
        {
            this.handlingFunc = handlingFunc;
        }

        public IHttpResponse Handle(IHttpContext context)
        {
            var response = this.handlingFunc(context.Request);

            response.Headers.Add(new HttpHeader("Content-Type", "text-plane"));

            return response;
        }
    }
}
