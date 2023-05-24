using MassTransit;
using NewAvalon.Messaging.Contracts;
using System.Threading.Tasks;

namespace NewAvalon.Catalog.Business
{
    public sealed class ExampleConsumer : IConsumer<IExampleRequest>
    {
        public ExampleConsumer() { }

        public async Task Consume(ConsumeContext<IExampleRequest> context)
        {
            var response = new ExampleResponse
            {
                WorkingMessage = "Radi"
            };

            await context.RespondAsync<IExampleResponse>(response);
        }
    }

    public sealed class ExampleResponse : IExampleResponse
    {
        public string WorkingMessage { get; set; }
    }
}
