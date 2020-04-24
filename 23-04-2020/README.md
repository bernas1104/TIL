# Today I Learned

So, today we started going a little bit deeper on React Native. We started simples, by adding
router components, our own components automatically get a header bar. We can disable this
by simply using the "headerShown" props. Also, we can use an "cardStyle" prop to, you know, style
the card.

## React Native Styled Components

On React Native, we can also use styled components to isolate and make it easier to style all the
components on our application. But there's a catch on Native: we can't use style within styles.
For example;

> View {  
> // some css  
> Text {  
> // some css  
> }  
> }  

React Native won't allow us to style a Text component within the View component. We need to separate
the styling.

Also, when dealing with icons, instead of passing a entire component as a props, we only need to send
a string. React Native treats icons in a different way than Reac.JS.

## Form Submission

To deal with form submissions, React Native gets a little trick. The people from Rocketseat created a 
package to help deal with forms: Unform (@unform/core & @unform/mobile). By using this package, dealing
with the form becomes easier. We also need to use the "useRef" Hook to create a reference to the form.

With the reference, we can create a function for the button "onPress" that will trigger the form to be
sent. An example can be seen [here](https://github.com/bernas1104/gostack/blob/master/semana-iii/gobarbermobile/src/pages/SignIn/index.tsx).

## Form Input Data

Handling the input data is also different on React Native. Once again, to access the value of the field,
we can "useRef" to it, updating as needed with the "onChangeText" (and onChange also?). We also need a
reference to the element of the field to make visual changes. Unform has a "setValue" to deal with this.
An example can be seen here [here](https://github.com/bernas1104/gostack/blob/master/semana-iii/gobarbermobile/src/components/Input/index.tsx).

## Define Focus (and possibilly more things)

So, to make a more pleasent UX, we need to make the input fields have 'focus', so that the user can hit
'return' and move on to the next field. The usual way to access a component and check this kind of stuff 
is using 'useRef'. But there's a few problems: we need to do this on the parent component of the input and
we need to be able to pass the useRef - like a props - and access the actual input field 'focus()' function.

To do this, we need to change our component from React.FC to React.RefForwardingComponent, which allows us
to send a second argument (ref) along side the props. Also, we need the "useImparativeHandle" (because our
component already has a 'ref') and the "forwardRef". By using these two things, we can:

- Pass information from child to parent component;
  - Up until now, we could only pass from parent to child;
- "useImperativeHandle" will take the ref passed from the parent and associate it with an object (I think
we can do anything really) with the data we want to pass it back to the parent;
- "forwardRef" will make it possible to export the component with the data, so it will be available to the
parent;

Also, since the ref being passed is a variable, we can give any name we want.

## Alerts

Back on React.JS, to create alerts to the user, we created "Toasts". In React Native we can simply use the
Alert component (from the "react-native" package) to generate alerts to the user (as a pop-up).

## Storage

Also in React.JS, we have the "Local Storage" to save user data to access some functionalities on the application.
Since "Local Storage" is available only on browsers, we need an alternative. React Native gives us the Async Store
(@react-native-community/async-store). It's just like "Local Storage"! It has the same functions even!

## Authentication

Since React.JS and React Native are very similar, the authentication part is pretty much the same. You can see
an example [here](https://github.com/bernas1104/gostack/blob/master/semana-iii/gobarbermobile/src/hooks/AuthContext.tsx).

### Disclaimers

On the mobile app [developed](https://github.com/bernas1104/gostack/tree/master/semana-iii/gobarbermobile) so far,
checking if the user is authenticated gives a little 'flash' of the SignIn screen. To void that, we use the "ActivityIndicator"
and a "loading" state.