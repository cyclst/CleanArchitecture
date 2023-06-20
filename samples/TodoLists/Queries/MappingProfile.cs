using AutoMapper;
using CleanArchitecture.Application.Mappings;
using System.Reflection;

namespace TodoLists.Queries;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        Assembly.GetExecutingAssembly().ApplyMappingsFromAssembly(this);
    }
}
