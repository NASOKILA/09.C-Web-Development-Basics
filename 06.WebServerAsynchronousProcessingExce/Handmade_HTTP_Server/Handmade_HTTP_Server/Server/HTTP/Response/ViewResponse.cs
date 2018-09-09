namespace Handmade_HTTP_Server.Server.HTTP.Response
{
    using Server.Enums;
    using Server.Contracts;
    using Server.Exceptions;

    public class ViewResponse : HttpResponse
    {
        private const string VIEW_RESPOSE_ERROR_MESSAGE = "View responses need status code below 300 and above 400!";

        private readonly IView view;
        
        public ViewResponse(HttpStatusCode statusCode, IView view)
        {
            this.ValidateStatusCode(statusCode);
            this.view = view;
            this.StatusCode = statusCode;
        }

        private void ValidateStatusCode(HttpStatusCode statusCode)
        {
            var statusCodeNumber = (int)statusCode;

            if (299 < statusCodeNumber && statusCodeNumber < 400)
                throw new InvalidResponseException(VIEW_RESPOSE_ERROR_MESSAGE);
        }

        public override string ToString()
        {
            return $"{base.ToString()} {this.view.View()}";
        }
    }
}
