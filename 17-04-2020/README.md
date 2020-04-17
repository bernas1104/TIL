# Today I Learned

Today things got a little more interesting on the GoStack Bootcamp. We started
working on user registration, authentication, file uploads and exception handling.

I won't focus on the file uploads part because we only saw the basics, which I
was already familiar. We can use the [Multer](https://github.com/expressjs/multer)
package, which is very simples to use.

## Registration

So, we began by creating the model of the Entity we want to register on the database,
like "User", for example.

- Repository: it's not necessary to create a "EntityRepository", if all we need
are the basic functions, like: create, find, update, delete, etc...
- There are decorators for a lot of functions, such as: CreatedDateColumn(), 
UpdateDateColumn(), relations decorators (OneToOne, ManyToOne, ManyToMany) and
operations decorators, like Join.

One thing we need to be carefull about it, is user information. In Brazil (and other
countries as well), we have the LGPDP (Lei Geral de Proteção de Dados Pessoais / 
Personal Data Protection General Law), and so we need to apply some sort of cryptography
to sensible data - like passwords -, so we will be using the [BCryptJS](https://github.com/dcodeIO/bcrypt.js)
to apply criptography to user passwords.

## Authentication

There are several methods a developer can use to authenticate users. We'll be using
the JWT method (JSON Web Token).

First, we need some sort of route on the API. So, we create a route like: http://api.com/sessions,
which will direct the user to the sessions controller, the controller will access the
database and verify the user's data, if everything is ok, the server will generate
a JWT and send it to the user.

The JWT is a string that can be broken in three parts:

- Headers: is the first part of the JWT and it contains the HTTP Headers info;
- Payload: is the second part of the JWT. It contains any specific information the
user will need.
- Signature: It ensures that the JWT was not corrupted in any way.

Since our users' passwords will be encrypted on the database, we need a way to check
if the password sent by the route matches the one on the database. BCryptJS offers
a function "compare", which checks an unencrypted password againts an encrypted one.

To create the JWT, the [jsonwebtoken](https://github.com/auth0/node-jsonwebtoken)
has the "sign({payload}, 'secret', {options})". The basic options are: subject,
which identifies what the token is identifying; and expiresIn, which tells when
you'll be needing a new JWT. It's important that the JWT has a expire date for
security reasons.

To make the payload availabe throughout all the routes, we need to add the information
to the requests. To do that, we can code "request.anything = payload" ('anything' will
be any name you want). But there's a problem: ESLint will state an error, since the
ExpressJS Request type doesn't know what this 'anything' is. To correct that, we need
to create a folder "@types" and use *.d.ts files. Inside theses, we need to declare
a new type for the ExpressJS Request type, like so:

> declare namespace Express {  
> export interface Request {  
> user {  
> id: string;  
> }  
> }  
> }

We can do this anytime we'd like to add some new types to the libraries we use on
our applications.

## Exception Handling

This last topic was the one I found it more interesting. I knew that we could use
Try/Catch and Throw to 'create' and handle exceptions on our code. What I didn't
know was that we can centralize all the exceptions of our API and avoid the usage
of Try/Catch in almost all the common places.

We can create Error classes, specific to our needs, and then, we can create a 
middleware that will handle all the applications exceptions. This middleware will
be declared AFTER (this is very important!) the declaration of the applications
routes. We also need the "express-async-errors", so that our middleware can handle
async routes.

Inside the middleware, we can create conditionals based on the applications Errors
classes, and if a not predicted exception occours, we can simply throw a generic
500 error.

### Disclaimers

#### Migrations

While developing the GoStack Bootcamp challenges, I found out that the migrations
are dependent on the Models. So, if you create a migration and run it without the
model ready, you can run into some kind of error. I'm not sure if this will happen
100% of the time, but it definitely will happen some of the time.

#### Async/Await Loops

Also during the challenges, we needed to create a route that would read a .csv
file, and register 3 instances of a model to the database. My main problem was
"How to loop asynchronously?". Upon some Google/Medium reading, I found that using
forEach is a "no-no". It creates concurrency, and this can create problems (it did
create problems on the challenge).

So, the solution is to use the Promise.all. This will take a async function, and
ensure that the order will be respected. So we can create a code like this:

> async Foo() {
> const promises = await Promise.all(
>     array.map(async item => {
>       const response = await Bar(item);
>       return response;
>     })
>   )
> }

This will loop through the array, execute the async function and return the results
without generating concurrency.