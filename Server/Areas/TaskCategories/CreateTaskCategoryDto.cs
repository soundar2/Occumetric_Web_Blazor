using AutoMapper;
using MediatR;
using Occumetric.Server.Areas.MasterTasks;
using Occumetric.Server.Data;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.Categories
{
    public class CreateTaskCategoryDto : IRequest
    {
        [Required]
        public string category_name { get; set; }
    }

    public class CreateCategoryDtoHandler : IRequestHandler<CreateTaskCategoryDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCategoryDtoHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateTaskCategoryDto request, CancellationToken cancellationToken)
        {
            //
            //create new category
            //

            _context.TaskCategories.Add(new TaskCategory
            {
                category_name = request.category_name
            });
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        //
        //
        //
    }
}