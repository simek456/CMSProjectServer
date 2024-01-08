using AutoMapper;
using AutoMapper.Execution;
using CMSProjectServer.Domain.Dto.SiteContents;
using CMSProjectServer.Domain.Entities.SiteContents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSProjectServer.Core.Mapping;

internal class ComponentResolver : IValueResolver<Grid, GridDto, List<BaseComponentDto>>
{
    public List<BaseComponentDto> Resolve(Grid source, GridDto destination, List<BaseComponentDto> destMember, ResolutionContext context)
    {
        return source.Components.Select(x => ResolveComponentet(x, context.Mapper)).ToList();
    }

    public BaseComponentDto ResolveComponentet(BaseComponent component, IRuntimeMapper mapper)
    {
        switch (component?.Type)
        {
            case "Row":
                return mapper.Map<RowDto>(component as Row);

            case "Grid":
                return mapper.Map<GridDto>(component as Grid);

            case "Block":
                return mapper.Map<BlockComponentDto>(component as BlockComponent);

            case "Image":
                return mapper.Map<ImageComponentDto>(component as ImageComponent);

            default:
                return mapper.Map<BaseComponentDto>(component);
        }
    }
}