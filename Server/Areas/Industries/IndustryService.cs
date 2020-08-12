using AutoMapper;
using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Data;
using Occumetric.Shared;
using System.Collections.Generic;
using System.Linq;

namespace Occumetric.Server.Areas.Industries
{
    public class IndustryService : OccumetricServiceBase, IIndustryService
    {
        public IndustryService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public List<IndustryViewModel> GetIndustries()
        {
            return _mapper.Map<List<IndustryViewModel>>(_context.Industries.ToList());
        }

        public string CreateIndustry(CreateIndustryDto dto)
        {
            var model = new Industry
            {
                Name = dto.Name
            };
            _context.Industries.Add(model);
            _context.SaveChanges();
            return model.Guid;
        }
    }
}
