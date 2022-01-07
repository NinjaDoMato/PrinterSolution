using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using PrinterSolution.Common.DTOs.Responses.OctoPrint;

namespace PrinterSolution.Common.Utils.Helper
{
    public class OctoPrintHelper
    {
        private string _address { get; set; }
        private string _apiKey { get; set; }

        public OctoPrintHelper(string address, string apiKey)
        {
            _address = address;
            _apiKey = apiKey;
        }

        public async Task<PrinterStateResponse> GetPrinterData(bool history = true, int limit = 5)
        {
            var request = _address
                .AppendPathSegment("api/printer")
                .SetQueryParams(new { history, limit })
                .WithHeader("X-Api-Key", _apiKey);

            var response = await request.GetJsonAsync<PrinterStateResponse>();

            return response;
        }

        public async Task<CurrentJobResponse> GetCurrentJobData()
        {
            var request = _address
                .AppendPathSegment("api/job")
                .WithHeader("X-Api-Key", _apiKey);

            var response = await request.GetJsonAsync<CurrentJobResponse>();

            return response;
        }

        public async Task<UploadFileResponse> UploadFile(string fileName, string filePath, string destinationPath)
        {
            var body = new
            {
                foldername = destinationPath
            };

            var request = _address
                .AppendPathSegment($"api/files/local")
                .AppendPathSegments(destinationPath)
                .WithHeader("X-Api-Key", _apiKey);

            var result = await request.PostMultipartAsync(mp =>
               mp.AddFile(fileName, filePath));

            return JsonConvert.DeserializeObject<UploadFileResponse>(result.ToString());
        }
    }
}
