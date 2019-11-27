using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphiQl;
using GraphQL;
using GraphQL.EntityFramework;
using GraphQL.Http;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProjectGraphQlTest.API.Schema;
using ProjectGraphQlTest.Infrastructure.Data;

namespace ProjectGraphQlTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<AppDbContext>(
                bd =>
                {
                    bd.UseMySql("fake");
                }
            );

            /**
             * GraphQL schema 
             */
            // nahradit neskor tymto https://github.com/SimonCropp/GraphQL.EntityFramework/blob/master/doco/configuration.md#connection-types
            EfGraphQLConventions.RegisterConnectionTypesInContainer(services);

            services.AddSingleton<Query>();

            services.AddSingleton<IDocumentExecuter, EfDocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddSingleton<ISchema, GraphQLSchema>();

            services.AddSingleton<IDependencyResolver>(x =>
                new FuncDependencyResolver(type => x.GetRequiredService(type)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseWebSockets();
            app.UseGraphQL<ISchema>();
            app.UseGraphiQl("/graphiql", "/api/graphql");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
