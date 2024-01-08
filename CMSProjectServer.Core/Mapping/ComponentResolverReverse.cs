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

internal class ComponentResolverReverse : IValueResolver<GridDto, Grid, List<BaseComponent>>
{
    public List<BaseComponent> Resolve(GridDto source, Grid destination, List<BaseComponent> destMember, ResolutionContext context)
    {
        return source.Components.Select(x => ResolveComponentet(x, context.Mapper)).ToList();
    }

    public BaseComponent ResolveComponentet(BaseComponentDto component, IRuntimeMapper mapper)
    {
        switch (component?.Type)
        {
            case "Row":
                return mapper.Map<Row>(component as RowDto);

            case "Grid":
                return mapper.Map<Grid>(component as GridDto);

            case "Block":
                return mapper.Map<BlockComponent>(component as BlockComponentDto);

            case "Image":
                return mapper.Map<ImageComponent>(component as ImageComponentDto);

            default:
                return mapper.Map<BaseComponent>(component);
        }
    }
}