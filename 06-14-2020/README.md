# Today I Learned

On my last entry, I was starting the [Codenation](http://codenation.dev) program
with C#. In the first week, the video classes were all about the basics (conditionals,
loops, list, etc...).

I decided to start studying the basics on how to build a REST API with ASP.NET.
I saw a video on [youtube](https://www.youtube.com/watch?v=but7jqjopKM), where the
teacher build a simple example of a REST API in 15 minutes.

While doing that, he also talked - very little, but still - about Dependecy Injection,
which, apparently, is a included feature on ASP.NET basic template. Nice.

## Folders Structure

From the looks of the video, ASP.NET gives us a very basic folder structure and
it does not seems to mind if we add our own folders and organization.

## Database

To start on persisting data to Databases, ASP.NET uses DataAnnotations, which are
similar to the Decorators pattern on Java and JavaScript/TypeScript. So, for example,
if you want to define a key for your model, you can achieve this by doing something like:

```csharp
[Key]
public int Id { get; set; }
```

and now your API understands that the "Id" attribute/column is the primary key of
your table. It's very similar to the TypeORM method on TypeScript APIs.

To interact with the Database, we a DataContext. The basic template doesn't seem
to provide us with the necessary tools. On the video, the teacher ran the command:

> dotnet add package Microsoft.EntityFrameWorkCore.InMemory

which gives us support to store information in memory (duh). So, this gave me two
questions:

- There's "InMemory". What else is there?
- So, .NET uses package management. Where do I search for more packages?

The first answer is: apperantly SQL Server, Azure SQL Database, SQLite, Azure Cosmos DB,
MySQL, PostgreSQL, and more.

The second answer is: [here](https://www.nuget.org/packages).

## Services

For every service added to you API, you need to make sure that the application knows
they are there. So, we need to add the services on the main file. Also, on the video,
this is where the teacher added the dependency injection stuff. He achieved this by
using the [AddScoped](https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1)
method.

## Controllers

Here is where the magic happens. On the Controllers, we define the route and the
methods that will be executed once a particular route is accessed. All of these
things are done by using Annotations, like:

```csharp
namespace dotnet_api.Controllers {
  [ApiController]
  [Route("v1/categories")]

  public class CategoriesController : ControllerBase {
    [HttpGet]
    [Route("")]
    public async Task<ActionResult<List<Category>>> GetAction([FromServices] DataContext context) {
      var categories = await context.Categories.ToListAsync();
      return categories;
    }

    [...]
  }
}
```

On the example above, we define a "CategoriesController" with the basic route
"myapi.com/v1/categories" and a GET route that returns all the Categories on the
database.

I'm still a bit confused about the "Task<ActionResult<List<Category>>>" non-sense,
but I'm going to read the ASP.NET documentation to understand this.
