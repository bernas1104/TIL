# Today I Learned

## Domain Drive Design

So far, we separete files by their "type", i.e., there are folders for config,
database, errors, etc... Still, this is not the best way of organizing files for
a big project. A more interesting way of organizing, is by domain (Domain Driven Design
or DDD).

> This will be applied only on the back-end

On the class about DDD, we learned that "modules" should be equivalent to "sectors"
on our application, where every "sector" functions independently. We begin by creating
our modules (with their own entities, repositories and services), configurations,
types and shared files.

> We separate our business logic from the infrastructure, i.e., from the tools (ORM,
> Mailer, etc...) we chose to interact with our domain layer.

> The infrastructure layer contains all the technical stuff: Framework, ORM, Mailer,
> Queues, etc...

Within our modules, we need to separate all the files that depend on tools contained
in the infrastructure layer. As an example, our application on the GoStack Bootcamp
had its models dependent on TypeORM, so we need to separate them on the modules folder.

> modules -> appointments -> infra -> typeorm -> entities -> Appointment.ts

### Making paths easier

The "tsconfig.json" has a configuration that's very helpful. It allows to always begin
the search from the rootDir, making it difficult to break files when moving them
from folder to folder. Here's an example:

```json
"baseUrl": "./src",
"paths": {
  "@modules/*": ["modules/*"],
  "@config/*": ["config/*"],
  "@shared/*": ["shared/*"]
}
```

### Liskov Substitution Principle (LSP)

On our application at the GoStack Bootcamp, our repositories were completely dependent
on the TypeORM module. If we were to change the ORM on our application, all the application
would break, and we would need to refactor a lot of code. So we need to apply the
Liskov Substitution Principle from S.O.**L**.I.D.

> Liskov Substitution Principle: tells that derived classes must be substitutable for their
> base classes. Thus, we'll have repository classes that must implement a basic set of
> operations (by using Interfaces), so that on the event of a substitution, their beheaviour
> stays the same.

> If it looks like a duck, talks like a duck, swims like a duck, but it needs batteries, then
> you have a problematic abstraction.

### Dependecy Inversion Principle

Back at the GoStack application, whenever we call a service, we need, from within that service,
to call the repository. That makes our services dependent on something that they shouldn't
recognize. To remedy this situation, we need the Dependency Inversion Principle from
S.O.L.I.**D**.

> Dependency Inversion Principle: tells that we must depend on abstractions, not implementations.
> By doing so, we make it easier to change behaviour, by decoupling.

To make it simpler to apply the DIP, we can use a lib called ["tsyringe"](https://github.com/microsoft/tsyringe)
from Microsoft.

> With this lib, we can create "containers", that control the injection;
> We can register Singletons for the repositories, which receives the name and the repository itself.

```typescript
container<T>('name', class);
```
```typescript
container.resolve(class);
```

### Controllers

Until now, we've used our routes to select the actions we we're going to take, but as the "Single
Resposability Principle" tells us, that shouldn't be the role of the routes files. We
will create controllers to execute the actions received by the routes.

## Test Driven Development (TDD)

The idea behind TDD is that developers will create tests before they create the actual code.
This will (or at least try to) ensure that the software evolution will not break the system,
and if it does break, we will know where it did.

There are three basic types of tests:

- Unit Tests: will test specific functionalities (pure functions). In other words,
functions that do not depend on other functions, API's, and don't generate any side effects.

- Integration Tests: will test a complete functionality, going through multiple layers
of the application. It simulates the usage of a given functionality of the application.

- E2E (End-to-End): it simulates the actions of the user (UI), as he goes through the application's
functionalities. We don't do E2E testing on the back-end.

### Jest with Typescript

To make use of Jest with Typescript, its very similar to using it with Javascript. We simply need
to add the "ts-jest" lib and to the "jest.config.js" preset. Also, we need to add the "jest" enviroment
to the ".eslintrc.json".

> We also need to make Jest understand the easier paths we talked about before!

```javascript
moduleNameMapper: pathsToModuleNameMapper(compilerOptions.paths, {
  prefix: '<rootDir>/src/'
}),
```

To test our services, we need to make them independent of other parts of our application.
To do this, we must create fakes, mocks and stubs. For example, our repositories need to be
faked in order to test the service functionality.

Lastly, Jest has a way of spying on objects and checking if any given methods were called.
We simply need to use the "spyOn(object, 'nameOfMethod')" method. The result will be used
in the "expect" with ".toHaveBeenCalled" and ".toHaveBeenCalledWith".
