using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Morphy.Extensions.Microsoft.DependencyInjection.Tests
{
	public class ServiceLifetimeTests
	{
		//Implicitly Transient
		[Fact]
		public void AddMorphyExtensionDefaultWithAssemblySingleDelegateArgCollection()
		{
			//arrange
			var serviceCollection = new ServiceCollection();

			//act
            serviceCollection.AddMorphy(_ =>
            {
            });
			var serviceDescriptor = serviceCollection.FirstOrDefault(sd => sd.ServiceType == typeof(IMapper));

			//assert
			serviceDescriptor.ShouldNotBeNull();
			serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Transient);
		}

		[Fact]
		public void AddMorphyExtensionDefaultWithServiceLifetime()
		{
			//arrange
			var serviceCollection = new ServiceCollection();

			//act
            serviceCollection.AddMorphy(_ =>
            {
            }, new List<Assembly>(), ServiceLifetime.Singleton);
			var serviceDescriptor = serviceCollection.FirstOrDefault(sd => sd.ServiceType == typeof(IMapper));

			//assert
			serviceDescriptor.ShouldNotBeNull();
			serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Singleton);
		}
	}
}