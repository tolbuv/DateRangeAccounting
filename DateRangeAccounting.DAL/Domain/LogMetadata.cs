using System;
using System.Net;

namespace DateRangeAccounting.DAL.Domain
{
    public class LogMetadata
    {
        public int Id { get; set; }

        public string RequestMethod { get; set; }
        public DateTime RequestTimestamp { get; set; }
        public string RequestUri { get; set; }

        public HttpStatusCode ResponseStatusCode { get; set; }
        public DateTime ResponseTimestamp { get; set; }
        public string ResponseContentType { get; set; }
    }
}