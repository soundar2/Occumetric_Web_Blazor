using AutoMapper;
using MediatR;
using Occumetric.Server.Areas.Niosh;
using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Areas.Snooks;
using Occumetric.Server.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.MasterTasks
{
    public class CreateMasterTaskDto : IRequest<int>
    {
        public string task_name { get; set; }

        public string effort_type { get; set; }
        public double weight_lb { get; set; }
        public string from_height { get; set; }
        public string to_height { get; set; }

        public string carry_distance { get; set; }
        public string short_description { get; set; }
        public string snooks_male { get; set; }
        public string snooks_female { get; set; }

        public string lift_duration_type { get; set; }
        public string lift_frequency_type { get; set; }
    }

    public class CreateMasterTaskDtoHandler : IRequestHandler<CreateMasterTaskDto, int>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISnooksService _snooksService;
        private readonly INioshService _nioshService;

        public CreateMasterTaskDtoHandler(ApplicationDbContext context,
            IMapper mapper,
            ISnooksService snooksService,
            INioshService nioshService)
        {
            _context = context;
            _mapper = mapper;
            _snooksService = snooksService;
            _nioshService = nioshService;
        }

        public async Task<int> Handle(CreateMasterTaskDto request, CancellationToken cancellationToken)
        {
            var masterTask = _mapper.Map<MasterTask>(request);
            masterTask.int_from_height = PdaUtility.SanitizeString(masterTask.from_height);
            masterTask.int_to_height = PdaUtility.SanitizeString(masterTask.to_height);
            if (masterTask.effort_type.Contains("Lift"))
            {
                var snooks = _snooksService.ComputeSnooks(masterTask.int_from_height, masterTask.int_to_height, Convert.ToInt32(masterTask.weight_lb ?? 0));
                masterTask.snooks_male = snooks.Item1;
                masterTask.snooks_female = snooks.Item2;
                masterTask.lifting_index = _nioshService.LiftingIndex(masterTask);
            }
            _context.MasterTasks.Add(masterTask);
            await _context.SaveChangesAsync(cancellationToken);
            return masterTask.Id;
        }
    }
}