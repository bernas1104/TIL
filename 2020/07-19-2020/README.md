# Today I Learned

To connect our Spring REST API to a SQL database, we need to add the Spring JPA
project to our dependencies. Also, we need the MySQL Driver to be able to interact
with the MySQL database.

After installing the dependencies, if we try to run our application, we'll see that
the application won't start. That's because it is expecting a database connection,
so we need to create one.

To create the connection, we'll use the "application.properties" file to inform
the datasource URL.

```
spring.datasource.url=jdbc:mysql://host1:3306/myDatabase
```

## Database Migrations

To use migrations, Spring supports two options: Flyway and Liquibase. As far as
I can tell, both of them work using plain SQL, which I really don't like.

With Flyway, you'll need to create a folder "db/migration" inside the resources
package. Then, all the migration files should follow the pattern
"V<version>__Description.sql".

After creating the first migration script, when you lauch the application, Flyway
will run the migrations.

## ORM

Spring Data JPA also provides the Hibernate ORM as a dependency. To map our entities
to the database, we simply need to add some decorators:

```java
@Entity(name = "users")
public class User {
  @Id
  @GeneratedValue(strategy = GenerationType.IDENTITY)
  private Long id;

  private String name;
  private String email;
  private String phone;

  // Rest of the User class code
```

The "@Entity(name = "users")" will map the "User" class to the "users" table on the
database. The @Id and @GeneratedValue tells that our "id" field is the auto incremented
ID field on the "users" table. The other fields don't need a decorator since they
have the same name as the table columns.

## Repository

The Spring Data JPA also provides a generic Repository to any of our entities and
is also capable of autogenerate some methods if we follow some patterns:

```java
@Repository
public interface UsersRepository extends JpaRepository<User, Long> {
  User findByName(String name);
}
```

The @Repository decorator defines the interface as a Repository. We must extend
our interface from the JpaRepository<T Entity, ID Type>, in our case T = User
and ID = Long, since we are dealing with the User class that has an "id" of type
Long. When we define the "findBy<field>" method, JPA understands that we want a
method to search by some field and implements that method for us. We can also
create our own methods by creating a class that implements from this interface.

To inject - Spring also has native Dependency Injection - the repository, simply
use the @Autowired decorator above the repository property in the class.

```java
@Autowired
private UsersRepository usersRepository;
```

## Group Routing

To create a set of end-points that have the same root, i.e. /users, we can use
the @RequestMapping("/root-route") decorator. With it, all the @GetMapping decorators
will only add to this root route.

## Responses

Spring has a "ResponseEntity" which allows us to return objects and status codes
as needed:

```java
@GetMapping("/{id}")
public ResponseEntity<User> show(@PathVariable Long id) {
  var user = usersRepository.findById(id);

  if (!user.isPresent())
    return ResponseEntity.notFound().build();

  return ResponseEntity.ok(user.get());
}
```

The above code will receive an "id" parameter from the URL path, binding it to
type Long, search for an User with that id. The method has an Optional<User>
return, which means that we may not find such an User. If the user is not found,
we will return a 404 code. If it is found, return a 200 status code with the user
object.

It is also possible to add a @ResponseStatus(HttpStatus.SOMESTATUS) decorator to
our routes to define a standard response code.

## Bean Validation

We can create validations to our routes by using the Hibernate Validator. If it
is not present by default, you can add it by searching for "validation" on the
Spring Starters.

After installation, simply add decorators to your View Model (the video uses them
on the Entity, but it is best to separate these and use the MVVM pattern). Also,
you'll need to add a @Valid decorator to your body binding on the route.

## Exception Handler

When the Bean Validation fails, we get an error response with the full trace, which
is not ideal. We can create our own error responses. To do this, we can create
exception classes:

```java
@ControllerAdvice
public class ApiExceptionHandler extends ResponseEntityExceptionHandler {
  @Autowired
  private MessageSource messageSource;

  @ExceptionHandler(ServiceException.class)
  public ResponseEntity<Object> handleService(
    ServiceException ex,
    WebRequest request
  ) {
    var status = HttpStatus.BAD_REQUEST;

    var problem = new Problem();
    problem.setStatus(status.value());
    problem.setTitle(ex.getMessage());
    problem.setDateTime(LocalDateTime.now());

    return handleExceptionInternal(
      ex,
      problem,
      new HttpHeaders(),
      status,
      request
    );
  }

  @Override
  protected ResponseEntity<Object> handleMethodArgumentNotValid(
    MethodArgumentNotValidException ex,
    HttpHeaders headers,
    HttpStatus status,
    WebRequest request
  ) {
    var fields = new ArrayList<Problem.Field>();

    for (ObjectError error : ex.getBindingResult().getAllErrors()){
      String name = ((FieldError) error).getField();
      String message = messageSource.getMessage(
        error, LocaleContextHolder.getLocale()
      );

      fields.add(new Problem.Field(name, message));
    }

    var problem = new Problem();

    problem.setStatus(status.value());
    problem.setTitle(
      "One or more fields are invalid. Fill the fields correctly and try again"
    );
    problem.setDateTime(LocalDateTime.now());
    problem.setFields(fields);

    return handleExceptionInternal(ex, problem, headers, status, request);
  }
}
```

The @ControllerAdvice tells to treat errors generated within the application
controllers. The "Problem" class is a ViewModel of our error body. The @ExceptionHandler
creates a new custom handler. The "handleMethodArgumentNotValid" treats our Bean
Validation exceptions. I need to study this last one since the implementation given
on the video tutorial doesn't seem to work. I'm still getting the full trace.

## Services

We can easily create Services to our application by defining classes with the
@Services decorator, like so:

```java
@Service
public class CreateUserService {
  @Autowired
  private UsersRepository usersRepository;

  public User execute(User user) {
    var exists = usersRepository.existsByEmail(user.getEmail());

    if (exists)
      throw new ServiceException("E-mail deve ser único");

    var savedUser = usersRepository.save(user);

    return savedUser;
  }
}
```

The above code defines a service for creating a User and, if the e-mail of said
user is not unique, the service throws a custom ServiceException.
