using AutoMapper;
using PrinterSolution.Repository.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Tests
{
    public class AutomapperConfig
    {
        public AutomapperConfig()
        {
            MapperConfiguration mappingConfig = new(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            Mapper = mappingConfig.CreateMapper();
        }

        public IMapper Mapper { get; }
    }
}
