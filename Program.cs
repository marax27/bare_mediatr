using System;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace bare_mediatr
{
    public class HelloQuery : IRequest<string> { }
    public class HelloQueryHandler : RequestHandler<HelloQuery, string>
    {
        protected override string Handle(HelloQuery request)
            => "Hello world!";
    }
    
    public class SampleCommand : IRequest { }
    public class SampleCommandHandler : RequestHandler<SampleCommand>
    {
        protected override void Handle(SampleCommand request)
        {
            Console.WriteLine("Command handled.");
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var mediator = BuildMediator();
            Run(mediator).Wait();
        }

        static async Task Run(IMediator mediator)
        {
            var result = await mediator.Send(new HelloQuery());
            Console.WriteLine(result);

            await mediator.Send(new SampleCommand());
        }

        static IMediator BuildMediator()
        {
            var services = new ServiceCollection()
                .AddMediatR(Assembly.GetEntryAssembly())
                .BuildServiceProvider();

            return services.GetService<IMediator>();
        }
    }
}
