# Today I Learned

So far on my the few React.JS projects I started, I used the "normal" template,
which uses JavaScript. This week on the GoStack Bootcamp, we learned about mixing
React.JS with TypeScript. By using the "--template=typescript" flag, React.JS
is created using TypeScript, instead of regular JavaScript.

## React.JS with TypeScript

There are a few changes on how to develop with TypeScript React.JS. First of all,
since all functions in React.JS need a type of return, we'll change the way we
export stuff of files. Instead of using:

> export default function Foo() {...}  
or  
> function Foo(): Type {...}  
> export default Foo;  

For exporting components, we'll use:

> export const Foo: Type = () => {}  

This will make it simpler to define the function's type and export it. Also, since
these are components, the type required is "React.FC", which is available inside the
React default import.

The second change is for how to import CSS to our components. Until now, we simply
imported the .css file. The problem with this approach is that all styles become
global. So, this can become messy on big applications. However, there's a way to
create scoped styles.

To do that, we need to use the 'styled components' lib. This will allow us to create
styles that are specific to each of our components. Also, we can reuse the same idea
with React Native apps!

## Props & Styled Components

With the help of interfaces, we can pass "props" down to the styled components files
the same way we would with normal components. This can be useful to create styles
that are specific to a given situation/state. As an example, in this [project](https://github.com/bernas1104/gostack/tree/master/semana-iii/frontend)
on the "src > pages > Dashboard > styles.ts" we take a props "hasError" to change
the input border color, only if the data input is wrong for some reason!

## Async/Await & useEffect

Up until now, I thought we couldn't use async/await calls inside the useEffect
function. I was wrong. To do so, we can create a function inside the useEffect
(man... JavaScript is weird haha) and call that function.

An interesting thing to know is that we can optimize api calls on the frontend.
Suppose you need to do two or more api calls when the useEffect is executed. There
two possible scenarios:

- Each call depends on the completion of the previous
  - On this case, you'll need to use the async/await way. You can write each of the
calls, saving their response. This works because every api call will execute only
after the previous is done.

- There is no dependency between the calls
  - Here, you can use .then(...) or use Promise.all to iterate through all the calls
while saving them on an array. By doing this, the application won't wait for the response
of all the calls. It will execute them "simultaneously", which will probably make the
application feels a little bit snapier.

### Disclaimers

We also learned two interesting bits:

- We always need to use "a" or "button" for navigation on our applications. This is
because they're needed to people with disabilities, who depend on programs to
understand the HTML;
- On functions that will take an "HTML Event", we need to define their type. To do
this, we'll need the Event (on the project, we used FormEvent, but I believe there
are other specific types) with the HTML...Event (same story as the Event).

## Basics of Spring Boot

So, yesterday I started a new online course that will last this entire week. It's
a series of 4 videos that will talk about creating a Spring Boot REST API for
beginners.

On the first video, I learned how to create Spring Boot projects using the Spring
Tools Suite (an Eclipse based IDE), creating my first route and my first model.

It's all very basic, but I'm seeing a lot of similarities with Node.JS, specially
with TypeScript.

The creation of classes follows no particular structure. Every class is just a simple
Java class, but to make Spring Boot understand that a given class is a Route to a
resource, we need to use the Decorators Pattern.

For instance, suppose you need a "/clients" GET route. All you need to do is:

> @RestController  
> public class ClientsController {  
>	@GetMapping("/clients")  
>	public List<Client> index() {  
>		var client1 = new Client();  
>		var client2 = new Client();  
>		  
>		client1.setName("Bernardo Costa");  
>		  
>		return Arrays.asList(client1, client2);  
>	 }  
> }  

The "@RestController" tells that the "ClientsController" class will receive requests.
The "@GetMapping("/clients")" tells that the "index" function will answer to the
"/clients" route with the GET method.

Spring Boot transforms the answer in a JSON automatically, and we can change the type
of response by adding new project dependecies.

I found interesting that building a REST API is similar between two distinct languages.
I hope to learn more about connecting to a DB, creating a CRUD and frontend developing
with Spring. Seems like an interesting tool.