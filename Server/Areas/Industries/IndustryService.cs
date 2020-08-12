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

        public List<IndustryViewModel> Index()
        {
            return _mapper.Map<List<IndustryViewModel>>(_context.Industries.ToList());
        }

        public IndustryViewModel Get(string guid)
        {
            return _mapper.Map<IndustryViewModel>(_context.Industries.Where(x => x.Guid == guid).Select(x => x).Single());
        }

        public string Create(CreateIndustryDto dto)
        {
            var model = new Industry
            {
                Name = dto.Name
            };
            _context.Industries.Add(model);
            _context.SaveChanges();
            return model.Guid;
        }

        public void Update(UpdateIndustryDto dto)
        {
            var ind = (from x in _context.Industries where x.Guid == dto.Guid select x).First();
            ind.Name = dto.Name;
            _context.SaveChanges();
        }
    }
}
