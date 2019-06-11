namespace Exam.App
{
    using Exam.Data;
    using Exam.Services;
    using SIS.MvcFramework;
    using SIS.MvcFramework.DependencyContainer;
    using SIS.MvcFramework.Routing;
    using System.Collections.Generic;
    using System.Text;

    public class Startup : IMvcApplication
    {
        public void Configure(IServerRoutingTable serverRoutingTable)
        {
            // Once on start
            using (var db = new ExamDbContext())
            {
                db.Database.EnsureCreated();
            }
        }

        public void ConfigureServices(IServiceProvider serviceProvider)
        {
            serviceProvider.Add<IUsersService, UsersService>();
            //serviceProvider.Add<IPackagesService, PackagesService>();
            //serviceProvider.Add<IReceiptsService, ReceiptsService>();
        }
    }
}
