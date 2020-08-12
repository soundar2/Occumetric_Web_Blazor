using MediatR;
using System.Linq;
using System;
using System.Collections.Generic;
using Occumetric.Server.Areas.Shared;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.Snooks
{
    public class GetSnooksPercentagesDto : IRequest<SnooksPercentagesViewModel>
    {
        [Required]
        public int weight { get; set; }

        [Required]
        public string from_height { get; set; }

        [Required]
        public string to_height { get; set; }
    }

    public class SnooksPercentagesViewModel
    {
        public string male { get; set; }
        public string female { get; set; }
    }

    public class GetSnooksPercentagesDtoHandler : IRequestHandler<GetSnooksPercentagesDto, SnooksPercentagesViewModel>
    {
        private readonly ISnooksService _snooksService;

        public GetSnooksPercentagesDtoHandler(ISnooksService snooksService)
        {
            _snooksService = snooksService;
        }

        public Task<SnooksPercentagesViewModel> Handle(GetSnooksPercentagesDto request, CancellationToken cancellationToken)
        {
            var snooks = _snooksService.ComputeSnooks(
                PdaUtility.SanitizeString(request.from_height),
                PdaUtility.SanitizeString(request.to_height),
                request.weight);

            return Task.Run(() =>
            {
                return new SnooksPercentagesViewModel
                {
                    male = snooks.Item1,
                    female = snooks.Item2
                };
            });
        }
    }
}