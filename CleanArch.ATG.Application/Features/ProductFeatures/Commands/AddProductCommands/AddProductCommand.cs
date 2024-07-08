using CleanArch.ATG.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.ATG.Application.Features.ProductFeatures.Commands.AddProductCommands
{
    public record AddProductCommand(string name, double price) : IRequest<Product>;
}
