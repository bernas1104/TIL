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