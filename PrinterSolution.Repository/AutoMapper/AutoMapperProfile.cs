using AutoMapper;
using PrinterSolution.Common.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Repository.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Printer

            CreateMap<CreatePrinterModel, Printer>()
                .ForMember(e => e.HasHeatedBed, m => m.MapFrom(d => d.HeatBed)).ReverseMap();

            #endregion

            #region PriceRule

            CreateMap<CreatePriceRuleModel, PriceRule>()
                .ForMember(e => e.Operation, m => m.MapFrom(d => d.Type)).ReverseMap();

            #endregion
        }
    }
}
