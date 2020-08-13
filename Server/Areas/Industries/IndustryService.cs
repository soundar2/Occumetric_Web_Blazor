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

        public IndustryViewModel Get(int Id)
        {
            return _mapper.Map<IndustryViewModel>(_context.Industries.Find(Id));
        }

        public int Create(CreateIndustryDto dto)
        {
            var model = new Industry
            {
                Name = dto.Name
            };
            _context.Industries.Add(model);
            _context.SaveChanges();
            return model.Id;
        }

        public void Update(UpdateIndustryDto dto)
        {
            var ind = _context.Industries.Find(dto.Id);
            ind.Name = dto.Name;
            _context.SaveChanges();
        }
    }
}
