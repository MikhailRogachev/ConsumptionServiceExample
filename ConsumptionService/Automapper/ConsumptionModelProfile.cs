using AutoMapper;
using ConsumptionService.Dto;
using ConsumptionService.Models;

namespace ConsumptionService.Automapper
{
    /// <summary>
    /// This class maps the <see cref="ConsumptionCostModel">Consumption Model</see> entity into
    /// the <see cref="ResponseDto">Output DTO</see> entity.
    /// </summary>
    public class ConsumptionModelProfile : Profile
    {
        public ConsumptionModelProfile()
        {
            CreateMap<ConsumptionCostModel, ResponseDto>()
                .ForMember(d => d.TariffName, s => s.MapFrom(r => r.Name))
                .ForMember(d => d.AnualCost, s => s.MapFrom(r => r.AnualCost));                
        }
    }
}
