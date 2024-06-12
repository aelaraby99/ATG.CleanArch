using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.ATG.Application.Features.ProductFeatures.Commands
{
    public record DeleteProductCommand(int Id) : IRequest;
}
