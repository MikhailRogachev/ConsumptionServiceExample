using ConsumptionServiceIntegrationTest.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ConsumptionServiceIntegrationTest
{
    public class ConsumptionApiTests
    {
        private readonly HttpClient _client;

        public ConsumptionApiTests()
        {
            var factory = new WebApplicationFactory<Program>();
            _client = factory.CreateDefaultClient();
        }

        /// <summary>
        /// This integration test checks a response from the API and compares it with
        /// values expected.
        /// Expexted status code is 200.
        /// </summary>
        /// <param name="consumption">The value of the annual consumption.</param>
        /// <param name="expected">The expected annual cost segregated by products.</param>
        [Theory(DisplayName = "Get Annual Cost value"), MemberData(nameof(GetConsumptions))]
        public void CallApi_Response200(
            decimal consumption,
            Dictionary<string, decimal> expected)
        {
            // axt
            var response = _client.GetAsync($"/Consumption/{consumption}");
            response.Wait();

            // assert
            var dto = response.Result.GetResponseFromContext();

            foreach (var item in expected)
            {
                var expectedAmount = item.Value;
                var realAmount = dto.FirstOrDefault(p => p.TariffName == item.Key);

                realAmount?.AnualCost.Should().Be(expectedAmount);
            }

            dto.First().AnualCost.Should().BeLessThan(dto.Last().AnualCost);
        }

        public static TheoryData<decimal, Dictionary<string, decimal>> GetConsumptions()
        {
            return new TheoryData<decimal, Dictionary<string, decimal>>
            {
                {
                    3500,
                    new Dictionary<string, decimal>
                    {
                        {"Packaged", 800 },
                        {"Basic", 830 }
                    }
                },
                {
                    4500,
                    new Dictionary<string, decimal>
                    {
                        {"Packaged", 950 },
                        {"Basic", 1050 }
                    }
                },
                {
                    6000,
                    new Dictionary<string, decimal>
                    {
                        {"Packaged", 1400 },
                        {"Basic", 1380 }
                    }
                }
            };
        }
    }
}
