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
        
        public static IServiceProvider ConfigureServices(IConfigurationRoot configuration)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new ConfigurationModule(configuration.GetSection("unitOfWorkFactory")));
            return new AutofacServiceProvider(containerBuilder.Build());
        }
        
        static void PrintType()
        {
            System.Diagnostics.Debug.WriteLine(typeof(AES.Infrastructure.EntityFrameworkCore.PostgreSql.UnitOfWorkFactory).AssemblyQualifiedName);
        }

        static void RenderCurriculum(Person person)
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
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();

            var serviceProvider = ConfigureServices(configuration);
            var factory = serviceProvider.GetService(typeof(IUnitOfWorkFactory)) as IUnitOfWorkFactory;


            AES.Init.Initializer.InitDictionary(factory);
            using (var unitOfWork = factory.Create())
            {
                IUserFinder userFinder = new UserFinder(unitOfWork);
                var pushkin = userFinder.findByLogin("pushkin");
                if (pushkin != null)
                {
                    Console.WriteLine(pushkin.Name);
                    RenderCurriculum(pushkin);
                }
                //ShowSubjects(unitOfWork);
                
                unitOfWork.Commit();
            }
        }
    }
}