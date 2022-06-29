using AutoFixture.Xunit2;
using AutoMapper;
using ConsumptionService.Automapper;
using ConsumptionService.Dto;
using ConsumptionService.Models;
using FluentAssertions;

namespace ConsumptionServiceTest.AutoMapper
{
    public class AutoMapperTests
    {
        /// <summary>
        /// This test checks the profile configuration validity.
        /// </summary>
        [Fact(DisplayName = "Check the automapper configuration asserting")]
        public void AutoMappert_ConfigurationAsserting()
        {
            // arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ConsumptionModelProfile>());

            // assert
            config.AssertConfigurationIsValid();
        }

        /// <summary>
        /// This test compares properties of the <see cref="IEnumerable{ConsumptionModelProfile}">Models</see>
        /// and <see cref="IEnumerable{ResponseDto}">Output collection</see> mapped.
        /// </summary>
        /// <param name="models"></param>
        [Theory(DisplayName = "Check objects mapping"), AutoData]
        public void AutoMapper_Mapping(IEnumerable<ConsumptionCostModel> models)
        {
            // arrange
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ConsumptionModelProfile>())
                .CreateMapper();

            // act
            var dto = mapper.Map<IEnumerable<ResponseDto>>(models);

            // assert
            dto.Should().HaveCount(models.Count());
            foreach(var model in models)
            {
                var dt = dto.FirstOrDefault(p => p.TariffName == model.Name && p.AnualCost == model.AnualCost);
                dt.Should().NotBeNull();
            }
        }
    }
}
