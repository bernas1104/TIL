# Today I Learned

So, today I started taking some lessons on Vue.js, which is a "Progressive JavaScript Framework". The first thing we learn, is about the "Vue instance". Different from, say, React.js, Vue.js, apparently, keeps every thing pretty much separeted. What I mean is, HTML has it's own files, CSS has it's own files and JavaScript has it own files.

## The Vue Instance

To access and act over the HTML elements, we first need to create a Vue instance. We first import the JavaScript script with:

```html
<script src="https://cdn.jsdelivr.net/npm/vue"></script>
```

Then, all we need to do is write the following JavaScript line:

```js
var app = new Vue({
  el: '#app',
  data: {
    // some data to put into the HTML with {{ data }}
  }
});
```

For the above code to work, we need a HTML element with the ID "app" on our page. All our Vue work will be executed within that  root HTML element.

It's important to notice that Vue.js is a reactive framework, which means that if you change the value of a variable that appears on the HTML page, it'll change its value on all places that variable appears.

## V-Bind

In Vue, V-Bind is a way to dynamically bind an attribute to an expression. For example:

```html
<img v-bind:src="image" />
```

```js
var app = new Vue({
  el: '#app',
  data: {
    image: './assets/image.png',
  },
});
```

The code above binds the "image" word to the "image" expression insade the Vue instance 'data' property. V-Binding in Vue is so common that you can use a short hand for it, which is ":some-attribute".

## Conditional Rendering

We can also apply conditional rendering to our web page. We can do this by using the "v-if", "v-else" and "v-else-if". We can write boolean values or expressions, that evaluate as booleans, to achive conditional rendering.

By using "v-if" syntax, we are actually adding or removing some element off our page. If we are doing this very frequently, it might be more efficient to keep the element on the page and only change its "display" value. To do this, we can use "v-show". If the expression of the "v-show" is truthy, the element will be visible, but if it's falsy, it'll have a "display: none".

## List Rendering

To loop through a list and print all the elements, we can use a "v-for". It'll have the same syntax as JavaScript, like so:

```html
<ul>
  <li v-for="item in collection">{{ item }}</li>
<ul>
```

Similarly to React.js, it's recommended that you add a "key" attribute, with a unique value, to all the elements of the list, so that Vue can keep track of each element:

```html
<div v-for="item in collection" :key="item.uniqueValue">
  <p>{{ item.Information }}</p>
</div>
```

## Event Handling

Suppose you have a button that, when clicked, increments a counter displayed on the page. Vue has the "v-on" to handle events and update information on the page.

```html
<!-- Counter increments on each click! -->
<p>{{ counter }}</p>
<button v-on:click="counter += 1">Click me</button>
```

We can also create functions inside our Vue instance to make the code more organized:

```html
<!-- Counter increments on each click! -->
<p>{{ counter }}</p>
<button v-on:click="addToCart">Click me</button>
```

```js
var app = new Vue({
  el: '#app',
  data: {
    counter: 0,
  },
  methods: {
    addToCart: function() {
      this.cart += 1;
    },
  },
});
```

Now, when we click our button, the method "addToCart" will be called and the value of the counter will be incremeted and updated on the page.

The same that "v-bind" has a short had, "v-on" also has one, which is the "@" sign followed by the action you'll be listening.

## Class & Style Binding

We acn also play with styles dinamically using the "v-bind:style", or ":style". On the "Vue Mastery - Intro to Vue" course, we build a simple product page. On the example, we can hover the mouse over to colored boxes to change the image of the product. So, we have 2 possible colors and the boxes. We can achive this by doing:

```css
.color-box {
  width: 40px;
  height: 40px;
  margin-top: 5px;
}
```

```html
<div v-for="variant in variantColors"
     :key="varient.variantId"
     class="color-box"
     :style="{ backgroundColor: variant.variantColor }"
>
...
</div>
```

```js
var app = new Vue({
  el: '#app',
  data: {
    variant: [
      {
        id: 1,
        color: "green",
      },
      {
        id: 2,
        color: "blue",
      },
    ],
  },
});
```

This will create the two colored boxes dinamically! We can, to make cleaner, bind to a style object, which is declared inside de "data" property of the Vue instance.

```js
var app = new Vue({
  el: '#app',
  data: {
    styleObject: {
      color: 'red',
      fontSize: '13px',
    },
  },
});
```

What if we need to disable a button for a given circunstance? We use the binding ":disabled". We can also bind a class to be associated with the button if some condition evaluates to true:

```css
.disabledButton {
  /* some css */
}
```

```html
<button v-on:click="addToCart"
        :disabled
        :class="{ disabledButton: !onStock }"
>
  Add to cart
</button>
```

The same way we can add objects or arrays of binded styles, we can add them with binded class as well.

## Computed Properties

Just like methods, we can add a "computed" property to the Vue instance. There, we can create functions that will return some computed data to be displayed on the page. It's similar to the "useMemo" hook on React.js. If we change the values of the computed variables, the page gets updated automatically.

It is best to use a computed property, instead of a method, of an expansive operation that you don't want to re-run everytime you access it.

## Components

Components, just like in React.js, is a way to break your application down into blocks of repeatable code.

To create a component, we can write:

```js
Vue.component('nameOfComponent', {  // options object
  template: `
    <div>
      ...
    </div>
  `,  // Like React.js, we must return only 1 root component!!
  data() {    // Instead of an object, a function
    return {  // Component data!
      ...
    },
  },
});
```

To pass data from a parent component to a child component, we can use Props (just like React.js).

We define the Props a component expects to receive using the "props" property.

```js
Vue.component('nameOfComponent', {
  props: [propName],
  ...
});
```

We can go further and declare the expected type, if it is required and default values.

```js
Vue.component('nameOfComponent', {
  props: {
    message: {
      type: String,
      required: true,
      default: 'Hello, Component!',
    },
  },
});
```

## Communicating Events

If we have a child component that fires an event that is supposed to change some information on a parent component, how can we do this? We can emit an event:

```js
Vue.component('component', {
  ...
  addToCart() {
    this.$emit('add-to-cart');
  },
});
```

```html
<component @add-to-cart="updateCart">...</component>
```

This will trigger the "updateCart" method inside the Vue instance.

## Forms

When using v-binding, it works only as one way binding: from the data to the template. But, if we want an input from the user to be saved on the data, i.e. binding from the template to the data and back, we need v-model.

```html
<input v-model="dataName" />
```

We can add a modifier, like "v-model.number" to cast the data as a specific type, in this case, a number.

We can also add modifiers to event listeners. An example of this the ".prevent", which is equal to the "preventDefault()" method.

Adding the "required" attribute will enforce a non-empty validation to an input field. But, this is not the best option for every situation. You can also write your own validations.
