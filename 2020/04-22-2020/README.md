# Today I Learned

Today we started to delve into React.JS specifics on the GoStack Bootcamp. The first
thing we learned was that in order to restrict the css styling only on a specific
level on the hierarchy, we use the '>' symbol followed by the element.

> \> div {...}  

## Isolating Components

A good strategy to organize code, is to isolate components - 'parts' of code
that will repeat and have no dependency with its surroundings. To achieve this, we
need to import \<component\>HTMLAttributes (where 'component' can be any of the
HTML tags) and create a new interface/type (in case there are no new attributes).
We can receive the props the same way as any other component. We can also pass an
entire component as an attribute, and if we do this, we need to use the ComponentType
in the interface.

We can use this, for instance, to pass an react-icon to an isolated component. The
prop will have lower case, so we need to change it to upper case, otherwise react
won't understand it as a component to render.

## useCallback

When we create components as React.FC, everytime a function is created inside this
component and there's a change of state, this function will be copied once again to
memory (the older copy won't be deleted).

useCallback is designed to avoid this behaviour. It takes a function and a list of
dependencies and will only create a new copy of the function if one of it's dependencies
suffer a change of state.

> useCallback(() => {...}, [dependencies])

## useRef

React has a way to identify specific elements on the DOM. useRef creates a unique reference
that you can easily access to handle the component.

> const someRef = useRef(null);  
> ...  
> <SomeComponent ref={someRef} ...rest />

## Context

As I understand, Contexts are a way to pass information to all the hierarchy of components
so that we can access this information in a simple way. It comes in handy when we are dealing,
for instance, with a login.

An example can be seen [here](https://github.com/bernas1104/gostack/blob/master/semana-iii/gobarber-frontend/src/hooks/AuthContext.tsx)

As seen in the example above, we can use this to create our own hooks that can be used to create
specific contexts on our application.

## Authentication

On the front-end there are some stratagies to make a persistent authentication.
A couple of examples are signed cookies and local storage.

There are pros and cons for both. On the bootcamp, we will be using local storage.
It's a simple way to access the information we need about authenticated users and,
to minimize security risks, our JWT only lasts a day.

As seen on the last section, we can create our own hook to make this process a lot
less painful.

The rest is just access the API via axios and saving the data on the local storage.

## Toasts

It's a simple way to notify the users about the results of his/her actions on the UI.
It's greate because it doesn't depend on the layout, since it's a fixed div on the
top of the page.

We can use useEffect function as way to set a time to remove the Toast of the UI
after a few seconds. But there's a catch: if the use removes the Toast himself,
we need to clear the setTimeout. To achieve this, useEffect can return a function
that is called if the component ceases to exist. On this function, we call the
clearTimeout() and pass the result of the setTimeout() function.

To see an example of how to create a simples Toast, check this [file](https://github.com/bernas1104/gostack/tree/master/semana-iii/gobarber-frontend/src/components/ToastContainer).

## Authenticated Routes

Last but no least, we learned how to check if the user is logged to access some route.
We need to create a new Route component (that extends the React Route). After this
we need to pass a new prop to this component and make some sort of validation. If it
passes, user can go to the page he's requesting, otherwise there will be a redirect.
An example of the new Route component can be seen [here](https://github.com/bernas1104/gostack/blob/master/semana-iii/gobarber-frontend/src/routes/Route.tsx).
