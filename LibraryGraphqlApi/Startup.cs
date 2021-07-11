using LibraryGraphqlApi.Data;
using LibraryGraphqlApi.GraphQL.Authors;
using LibraryGraphqlApi.GraphQL.Books;
using LibraryGraphqlApi.GraphQL.DataLoader;
using LibraryGraphqlApi.GraphQL.Types;
using LibraryGraphqlApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace LibraryGraphqlApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<AppDbContext>(opt => opt.UseInMemoryDatabase("InMemory"));
            services.AddGraphQLServer()
                    .AddQueryType(d => d.Name("Query"))
                        .AddTypeExtension<BooksQueries>()
                        .AddTypeExtension<AuthorsQueries>()
                    .AddMutationType(d => d.Name("Mutation"))
                        .AddTypeExtension<BookMutations>()
                        .AddTypeExtension<AuthorMutations>()
                    .AddSubscriptionType(d => d.Name("Subscription"))
                        .AddTypeExtension<BookSubscriptions>()
                    .AddType<BookType>()
                    .AddType<AuthorType>()
                    .EnableRelaySupport()
                    .AddFiltering()
                    .AddSorting()
                    .AddInMemorySubscriptions()
                    .AddDataLoader<BookByIdDataLoader>()
                    .AddDataLoader<AuthorByIdDataLoader>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedData(app);
            }

            app.UseWebSockets();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapGraphQL();
            });
        }

        private static void SeedData(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            using (var context = serviceScope.ServiceProvider.GetService<IDbContextFactory<AppDbContext>>().CreateDbContext())
            {
                var author1 = new Author
                {
                    FullName = "Frank Herbert",
                    DOB = new DateTime(1920, 10, 8),
                };

                var book1 = new Book
                {
                    Title = "Dune",
                    Publisher = "Chilton Books",
                    Authors = new List<Author> { author1 }
                };

                var author2 = new Author
                {
                    FullName = "J.R.R. Tolkien",
                    DOB = new DateTime(1892, 2, 3)
                };

                var book2 = new Book
                {
                    Title = "The Lord of the Rings",
                    Publisher = "HarperCollins",
                    Authors = new List<Author> { author2 }
                };

                context.Books.Add(book1);
                context.Books.Add(book2);
                context.SaveChanges();
            }
        }
    }
}
