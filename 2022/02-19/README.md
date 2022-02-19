# Today I Learned

Similarly to my last entry, both subjects, that I want log about, weren't learned today.
So, today I'll write about:

- [Special Case Pattern](#special-case-pattern)
- [C# Language Extensions - Option](#c-language-extensions---options#)
- [C# MediatR](#c-mediatr)

## Special Case Pattern
For the last few months, I've been reading "Clean Code - A Handbook of Agile Software Craftsmanship", one of the books by the famous Uncle Bob (Robert C. Martin).

In it, Uncle Bob talks about how we shouldn't return null on our methods. He, than, goes over how we could avoid returning null by either applying a "Special Case Pattern" or, simply, throwing exceptions. Personally, I don't like the idea of throwing exceptions just to avoid a null return - because of the [performance costs](https://www.youtube.com/watch?v=2f2elFRmeLE) -, so I took an interest on the "Special Case Pattern".

The idea behind this pattern is to create a special class that would represent the null value, but without the risks of a NullReferenceException. So, for example, imagine that you have a "User" class:

```csharp
public class User
{
  public string Name { get; set; }

  public User(string name)
  {
    Name = name;
  }

  public override string ToString()
  {
    return $"User's name is: {Name}";
  }
}
```

Now, imagine that you have some service that goes to your applications's database and tries to retrieve some user. After receiving the results, the application would print the user's name on the console, like so:

```csharp
public class UserService
{
  private readonly IUserRepository _repository;

  public UserService(IUserRepository repository)
  {
    _repository = repository;
  }

  public void GetUserAndPrintName(int id)
  {
    var user = _repository.GetUserById(id);
    if (user is null)
    {
      Console.WriteLine("User not found");
    }

    Console.WriteLine(user.ToString());
  }
}
```

So, in order to ensure that our application will not throw an exception, if the user is not found, we need to check for a null return. This is exactly what we're trying to avoid.

What if, instead of possibly returning a null, our repository returned a special type of user? How this special type would look like? Like this:

```csharp
public class NullUser
{
  public NullUser() : base("")
  {}

  public override string ToString()
  {
    return "User not found";
  }
}
```

Now, we can updated our repository logic in such a way that we only need to check of a null once - inside the repository method - and, if no user was found, we should return a "NullUser". Now, our service can look like this:

```csharp
public class UserService
{
  private readonly IUserRepository _repository;

  public UserService(IUserRepository repository)
  {
    _repository = repository;
  }

  public void GetUserAndPrintName(int id)
  {
    var user = _repository.GetUserById(id);
    Console.WriteLine(user.ToString());
  }
}
```

Much more simple, right? If our repository finds a user, the service will print something like "User's name is: SOME-NAME", and if it doesn't, it will print "User not found". We don't need to check for a null return and there's no chance of a NullReferenceException - only if the name was stored as a null value on the database, but that's a whole other issue.

Though, There's a problem with this pattern: we need to create and maintain a bunch of special case classes, which increase the complexity of our code base. Is there a better way?

## C# Language Extensions - Option
One solution that **could** be better, is to use the language extensions library. With this library, we can use "Option".

With "Option", we don't need to create our special case classes. All we need is to define the method's return as an "Option\<SOME-TYPE\>" and apply some matching logic to treat the results. So, let's assume that our repository returns an "Option<User>". How would our service look like now?

```csharp
public class UserService
{
  private readonly IUserRepository _repository;

  public UserService(IUserRepository repository)
  {
    _repository = repository;
  }

  public void GetUserAndPrintName(int id)
  {
    var user = _repository.GetUserById(id);
    user.Match(Console.WriteLine(user.ToString()), "User not found");
  }
}
```

Of course, this a very simple example. We could apply some logic on either case. When we apply the "Match" method, it expects two delegates: one in case we have a valid User and another if the User is null (or None in the library's language).

Also, since this is a very simple example, I used a shorthand notation. We could also make things more explicit:

```csharp
public class UserService
{
  private readonly IUserRepository _repository;

  public UserService(IUserRepository repository)
  {
    _repository = repository;
  }

  public void GetUserAndPrintName(int id)
  {
    var user = _repository.GetUserById(id);
    user.Match(user => Console.WriteLine(user.ToString()), () => "User not found");
  }
}
```

By using "Option" we can treat nulls in a very effective way and with no extra classes to maintain. Obviously, there's a "cost" of using a third-party library, but it is a very interesting alternative.

Finally, the library adds a whole set of other things that move C# in a more functional direction, which is a interesting way to work. I should study this library further.

## C# MediatR
The last topic of today's entry is the MediatR library for C#. As described on the [Refactoring Guru's](https://refactoring.guru/design-patterns/mediator) website:

*__Mediator__ is a behavioral design pattern that lets you reduce chaotic dependencies between objects. The pattern restricts direct communications between the objects and forces them to collaborate only via a mediator object.*

How can the MediatR library help our code? I started looking at MediatR because I was working with a Web API that had services with 7 or more classes being injected to it. Also, some of the service classes were 2k lines long. They were a nightmare to maintain, since they were obviously not respecting the "Single Responsibility Principle". I decided that things needed to change.

So, the MediatR library helped me to break my big services' classes in multiple very small classes. I changed the "service" name to "handler", but that's just a detail.

Now, instead of having a 2k long service class, the biggest handler class is around 200 lines. Also, my handlers don't need multiple injections anymore. I simply inject the "mediator" object and it handles all the communication between the handlers. Lastly, I don't need to declare every new handler class on my "Startup.cs" file. The MediatR library is declared on the "Startup.cs" file and it takes care of all my handlers automatically.

It's not all sunshine and rainbows, though. The MediatR handlers can be a little tricky to test - specially if you're dealing with synchronous methods, which are "protected". Also, there is a [performance hit](https://www.youtube.com/watch?v=baiH3f_TFfY) to your application.

To wrap things up, here is a simple example of a mediator handler:

```csharp
public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, User>
{
  private readonly IUserRepository _repository;

  public GetUserByIdHandler(IUserRepository repository)
  {
    _repository = repository;
  }

  public async Task<User> Handle(
    GetUserByIdRequest request,
    CancellationToken cancellationToken
  )
  {
    return await _repository.GetUserByIdAsync(request.Id);
  }
}
```
