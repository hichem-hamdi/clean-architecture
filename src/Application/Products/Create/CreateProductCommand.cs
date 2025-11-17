using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Application.Abstractions.Messaging;

namespace Application.Products.Create;
public sealed class CreateProductCommand : ICommand<Guid>
{ 
    public Guid Id { get; set; }
    public string Label { get; set; }
    public decimal Price { get; set; }
}
