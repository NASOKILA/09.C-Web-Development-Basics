namespace WebServer.Http.Response
{
    using Enums;
    using Exceptions;

    public class FileResponse : HttpResponse
    {
        private const string attachment = "attachment";
        public byte[] FileData { get; private set; }

        //this class shows the fileData taht we passes to it
        public FileResponse(HttpStatusCode statusCode, byte[] fileData)
        {   
            this.ValidateStatusCode(statusCode);

            this.FileData = fileData;
            this.StatusCode = statusCode;
            this.Headers.Add(HttpHeader.ContentLength, this.FileData.Length.ToString());
            this.Headers.Add(HttpHeader.ContentDisposition, attachment);
        }   
            
        private void ValidateStatusCode(HttpStatusCode statusCode)
        {
            var statusCodeNumber = (int)statusCode;

            if (299 > statusCodeNumber && statusCodeNumber < 400)
            {
                throw new InvalidResponseException("File responses need a status code above 300 and below 400.");
            }
        }
            
    }
}
