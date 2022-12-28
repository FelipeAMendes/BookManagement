using AutoMapper;
using BookManagement.Core.Infra.Mappings.Extensions;

namespace BookManagement.Core.Infra.Mappings;

public class MappingsProfile : Profile
{
    public MappingsProfile()
    {
        this.ConfigureOptions();
        this.ConfigureMappings();
    }
}