using System;
using AES.BusinessLogic;
using AES.BusinessLogic.Implementation;
using AES.Domain;
using AES.Infrastructure;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace AES.Init
{
    class Program
    {
        private static IServiceProvider ConfigureServices(IConfigurationRoot configuration)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new ConfigurationModule(configuration.GetSection("unitOfWorkFactory")));
            containerBuilder.Register(c => c.Resolve<IUnitOfWorkFactory>().Create()).SingleInstance();
            return new AutofacServiceProvider(containerBuilder.Build());
        }

        private static void PrintType()
        {
            System.Diagnostics.Debug.WriteLine(typeof(AES.Infrastructure.EntityFrameworkCore.PostgreSql.UnitOfWorkFactory).AssemblyQualifiedName);
        }

        private static void RenderCurriculum(Person person)
        {
            foreach (var module in person.Student.Curriculum.Modules)
            {
                Console.WriteLine(module.Title);
                foreach (var item in module.Items)
                {
                    Console.WriteLine($"   {item.Subject.Name}, {item.Semester} sem, {item.TypeTesting.Name}");
                }
            }
        }

        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Current directory: " + Environment.CurrentDirectory);
            Console.WriteLine("Start");
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).AddUserSecrets<Program>().Build();

            var serviceProvider = ConfigureServices(configuration);
            Console.WriteLine("ServiceProvider created");
            var factory = serviceProvider.GetService(typeof(IUnitOfWorkFactory)) as IUnitOfWorkFactory 
                          ?? throw new ApplicationException("Не удалось создать фабрику");


            Initializer.InitDictionary(factory);
            using var unitOfWork = serviceProvider.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
            var userFinder = serviceProvider.GetService(typeof(IUserFinder)) as IUserFinder;
            var pushkin = userFinder.findByLogin("1577631715");
            if (pushkin != null)
            {
                Console.WriteLine(pushkin.Name);
                RenderCurriculum(pushkin);
                pushkin.Name = pushkin.Name;
            }
                
            unitOfWork.Commit();
        }
    }
}