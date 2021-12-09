using OrderSheet.Application.Abstractions.Messaging.Interfaces;

namespace OrderSheet.Application.Commands
{
    public class CreateProductCommand : ICommand<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
