using DateRangeAccounting.DAL.Domain;
using DateRangeAccounting.DAL.Interfaces.UnitOfWork;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DateRangeAccounting.DAL.EF;
using DateRangeAccounting.DAL.UnitOfWork;

namespace DateRangeAccounting.API.Handlers
{
    public class LogHandler : DelegatingHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogHandler()
        {
            var dbContext = new DateRangeContext(ConfigurationManager.ConnectionStrings["dateRangeConnectionString"]
                .ConnectionString);
            _unitOfWork = new UnitOfWork(dbContext);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var logMetadata = BuildRequestMetadata(request);
            var response = await base.SendAsync(request, cancellationToken);
            logMetadata = BuildResponseMetadata(logMetadata, response);
            await SendToLog(logMetadata);

            return response;
        }
        private static LogMetadata BuildRequestMetadata(HttpRequestMessage request)
        {
            var log = new LogMetadata
            {
                RequestMethod = request.Method.Method,
                RequestTimestamp = DateTime.Now,
                RequestUri = request.RequestUri.ToString()
            };
            return log;
        }
        private static LogMetadata BuildResponseMetadata(LogMetadata logMetadata, HttpResponseMessage response)
        {
            logMetadata.ResponseStatusCode = response.StatusCode;
            logMetadata.ResponseTimestamp = DateTime.Now;
            logMetadata.ResponseContentType = response.Content.Headers.ContentType.MediaType;
            return logMetadata;
        }
        private async Task<bool> SendToLog(LogMetadata logMetadata)
        {
            //preferably use NLogger or something but it's much simpler this way to integrate w/o existing db, and task says to save logs to DB, not to file
            _unitOfWork.Logs.Add(logMetadata);
            await _unitOfWork.SaveAsync();

            return true;
        }
    }
}