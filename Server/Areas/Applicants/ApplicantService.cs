using AutoMapper;
using Occumetric.Server.Areas.Helpers;
using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Data;
using Occumetric.Shared;
using System.Collections.Generic;
using System.Linq;

namespace Occumetric.Server.Areas.Jobs
{
    public class ApplicantService : OccumetricServiceBase
    {
        private IHelperService _helperService { get; set; }

        public ApplicantService(ApplicationDbContext context, IMapper mapper, IHelperService helperService) : base(context, mapper)
        {
            _helperService = helperService;
        }

        public List<ApplicantViewModel> Index(int tenantId)
        {
            List<ApplicantViewModel> applicants = (from app in _context.Applicants where app.TenantId == tenantId select _mapper.Map<ApplicantViewModel>(app)).ToList();

            return applicants;
        }

        public ApplicantViewModel ViewGet(int applicantId)
        {
            var applicant = (from app in _context.Applicants where app.Id == applicantId select _mapper.Map<ApplicantViewModel>(app)).Single();

            return applicant;
        }
    }
}
