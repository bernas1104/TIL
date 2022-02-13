# Today I Learned

So, it's been some time since the last commit. Work, COVID... Things got away from me,
but it's time to focus.

Today a I wanna talk about two topics:
- [Entity Framework - Inheritance](#entity-framework---inheritance); and
- [Value Objects - ValueOf lib](#value-objects---valueof-lib).

I didn't learn about these topics today, but I decided to start working on my TIL repository
today, so, you know, it is what it is.

## Entity Framework - Inheritance

It all began with a problem at work: I needed to refactor the API I'm responsible for.
I had to work with two different calculations for loan simulations, let's call them
"SimulationA" and "SimulationB". The both of them have the same properties to be stored at
our database, so I had a pretty simple idea: create a "AbstractSimulation" from which
"SimuluationA" and "SimulationB" would inherit from.

The "AbstractSimulation" class was the one mapped on EF and, to my surprise, I got an
exception when I tried saving either simulation to the database. The exception went on
to describe a problem with the "Discriminator" column, and I was like "WTH is this
'Discriminator' column?

I turned to the almighty Google for some answers. First, I discovered that, for my
idea to work, I'd need a new column 'Discriminator' on my database. Then, I found the
[Microsoft docs](https://docs.microsoft.com/en-us/ef/core/modeling/inheritance) on the subject.

So, EF needs that all sub-types must be included explicitly. If you have, for example, the following:

```csharp
internal class MyContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<RssBlog> RssBlogs { get; set; }
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
}

public class RssBlog : Blog
{
    public string RssUrl { get; set; }
}
```

EF will map the 'Blog' and 'RssBlog' classes, but any other 'Blog' subclass won't be mapped.

By default, EF uses the "Type-per-hierarchy" (TPH) pattern. This pattern uses a single table to
store data for all types in the hierarchy, and a 'Discriminator' column to identify which type
each row represents. It's possible to configure the name and type of the discriminator column,
just edit the map configuration for your hierarchy.

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Blog>()
        .HasDiscriminator<string>("blog_type")
        .HasValue<Blog>("blog_base")
        .HasValue<RssBlog>("blog_rss");
}
```

The basic idea of the discriminator is to tell EF how to materialize the rows in your database
into your specific data model. If you try to materialize data without the discriminator information,
EF will throw an exception, since it won't know how to materialize that data.

Also, if two types in the hierarchy have a property with the same name, this property will be mapped
to different columns by default. If, however, they share the same type, you can map this property to
the same column.

```csharp
public class MyContext : DbContext
{
    public DbSet<BlogBase> Blogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
            .Property(b => b.Url)
            .HasColumnName("Url");

        modelBuilder.Entity<RssBlog>()
            .Property(b => b.Url)
            .HasColumnName("Url");
    }
}

public abstract class BlogBase
{
    public int BlogId { get; set; }
}

public class Blog : BlogBase
{
    public string Url { get; set; }
}

public class RssBlog : BlogBase
{
    public string Url { get; set; }
}
```

EF can also work with the 'Type-per-type' (TPT) pattern, but I won't go over it in detail.
Suffice to say that stores each type, of the hierarchy, on it's own table.

## Value Objects - ValueOf lib

Lately, my interest on Domain Driven Design (DDD) has been growing. One of the concepts that
DDD offers is 'Value Objects'. I'm not quite sure how to explain value objects, but so far
I understand that this concept is linked with another, which is 'Primitive Obsession'.
Yesterday (02-12-2022), I watched a [video from Nick Chapsas](https://www.youtube.com/watch?v=h4uldNA1JUE&list=LL&index=1) on YouTube. He goes over how to treat primitive obsessions by
using value objects.

The idea is that primitives - such as int, float, double, string, etc - can represent a wider
range of values than what we need. Nick gives an example based on the Web API default solution
from ASP.NET. The example Web API is the 'Weather Forecast' and it uses temperature on the
business logic. By default, the example uses a 'double' primitive to handle the temperature
values, but this is a far greater range than we need.

Temperatures have an absolute zero - which is -273.15C or -459.67F - and, if we're talking about
weather forecast, we can probably set a maximum - around 60.00C or 140.00F. We could check the
values every time a temperature appears on our code, but this would create unnecessary code repetition,
increase the chance of bugs, etc...

So, how can we handle this? We can treat our temperatures as value objects. To do this, we need
to create a new class:

```csharp
public class Celsius
{
  // ...
}
```

Our 'Celsius' class will store our temperatures and we can validate them inside this class. That way,
we centralize all the business logic, regarding temperatures, in one place. After creating the class,
we could start writing all the validation code and other basic methods - such as equality methods.
But, to make this job easier, we can use - on ASP.NET - a library called 'ValueOf'.

The ValueOf library implements the basic logic for our value objects! So, we can edit our 'Celsius'
class like so:

```csharp
public class Celsius : ValueOf<double, Celsius>
{
  // ...
}
```

With this simple line of code, we now have a value object based on the 'double' primitive. We
can add validations by overriding the 'Validate' method:

```csharp
public class Celsius : ValueOf<double, Celsius>
{
  // ...

  public override void Validate()
  {
    // Some validation here!
  }
}
```

And we can instantiate our value object in a more readable way:

```csharp
Celsius temperature = Celsius.From(32.5);
```

I need to start reading more about DDD. It's a very interesting and useful subject and I'll definitely be using on my day-to-day work and study projects.
