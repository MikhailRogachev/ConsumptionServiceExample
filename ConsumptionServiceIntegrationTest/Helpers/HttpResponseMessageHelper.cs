using ConsumptionService.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumptionServiceIntegrationTest.Helpers
{
    internal static class HttpResponseMessageHelper
    {
        internal static IEnumerable<ResponseDto> GetResponseFromContext(this HttpResponseMessage message)
        {
            var task = message.Content.ReadAsStringAsync();
            task.Wait();

            return JsonConvert.DeserializeObject<IEnumerable<ResponseDto>>(task.Result);
        }
    }
}
