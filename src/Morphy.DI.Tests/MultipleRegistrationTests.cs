using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Shouldly;
using Xunit;

namespace Morphy.Extensions.Microsoft.DependencyInjection.Tests
{
    public class MultipleRegistrationTests
    {
        [Fact]
        public void Can_register_multiple_times()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ILoggerFactory>(NullLoggerFactory.Instance);

            services.AddMorphy(cfg => { });
            services.AddMorphy(cfg => { });
            services.AddMorphy(cfg => { });

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<IMapper>().ShouldNotBeNull();
        }

        [Fact]
        public void Can_register_assembly_multiple_times()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ILoggerFactory>(NullLoggerFactory.Instance);

            services.AddMorphy(_ => { }, typeof(MultipleRegistrationTests));
            services.AddMorphy(_ => { }, typeof(MultipleRegistrationTests));
            services.AddMorphy(_ => { }, typeof(MultipleRegistrationTests));
            services.AddTransient<ISomeService, MutableService>();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<IMapper>().ShouldNotBeNull();
            serviceProvider.GetService<DependencyValueConverter>().ShouldNotBeNull();
            serviceProvider.GetServices<DependencyValueConverter>().Count().ShouldBe(1);
        }
    }
}