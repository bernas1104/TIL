# Today I Learned

On the first day of the GoStack Bootcamp (https://skylab.rocketseat.com.br/),
I reviewed some of the content from the OmniStack Week (also from Rocketseat).
Thins like:

- Node.JS basic concepts;
  - Engine which Node is based uppon;
  - How the requests are processed (Events and Call Stack);
- What are NPM and Yarn;
- Famous Node.JS frameworks
  - Express.JS (https://expressjs.com/pt-br/): Not opinionated;
  - Adonis.JS (https://adonisjs.com/)/ Nest.JS (https://nestjs.com/): Opinionated;
- How an API REST works;
  - Benefits:
    - Multiple clients (front-end), only one back-end;
    - Standardized communication protocol;
  - api.com/route/:params?query;
  - JSON Body;
  - Header for other information;
- Middlewares
  - Basis of the Express.JS framework;
  - Can completely interrupt the flow of the requisition.

## Middlewares

During the development of my Graduation Project (https://github.com/bernas1104/MASA-Webaligner), 
I stumbled upon the following problem: I wanted to check if an uploaded file
was a valid FASTA file.

With the Multer middleware, I was able to check the file extension, but that
was not enough, since anyone could create any text file and save it as a .fasta.

I was not aware of the possibility of creating my own middleware, so that, before
the controller started to process the request, I could decide if the file was
valid or not, and on the latter, I could delete the file and return a 400 status code.

With that in mind, developers can create any number of middlewares and use them
before the function that will actually process the request. All you need is:

- Create a function like: 

> function Foo(request, response, next){  
>   // some action  
>   next();  
> }  

- Add it to the route (or all the routes)
  - To some route: app.get('/foo', Foo, (request, response) => {});
  - To some routes: app.use('/foo/:param', Foo, (request, response) => {});
    - This will add the middle to all routes with the format '/foo/:param';
  - To all routes: app.use(Foo);
