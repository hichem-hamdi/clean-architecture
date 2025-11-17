using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.Products;
public sealed class Product : Entity
{
    public Guid Id { get; set; }
    public string Label { get; set; }
    public decimal Price { get; set; }
}
