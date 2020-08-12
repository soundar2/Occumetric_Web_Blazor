using AutoMapper;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.Shared.Mappings
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}