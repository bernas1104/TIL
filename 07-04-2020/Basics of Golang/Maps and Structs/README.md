# Today I Learned

Moving on with the basic types of Golang, we'll take a look at Maps and Structs,
the last two types of the language. We'll cover:

- Maps
  - What are they?
  - Creating
  - Manipulation
- Structs
  - What are they?
  - Creating
  - Naming conventions
  - Embedding
  - Tags

## Maps

Maps are, basically, like JavaScript objects or C Maps. They are composed of a key
that returns a value. The key has to follow a given type and the same goes for the
value. Both types are defined at declaration time. The keys have a basic
constraint, which is: they have to be able to be test for equality. So, we can't
use slices, maps and functions as keys in a map.

Just like slices, we can declare maps directly or using the "make" function:

```go
a := make(map[string]int) // Creates a map with string keys and int values
b := map[string]int{key1: value1, ..., keyN: valueN}  // Creates a map with string keys and int values
```

Maps are variable in size. To add a new key/pair value, simply write "map[newKey] = value".
When we return the mapped values, the order is not guaranted. The memory is not
allocated in a contiguous way, like in slices or arrays.

To remove elements from the map, we can use the "delete(src, key)" function.

Maps have a curious behaviour when you try to access the value of a non-existing
key. If you run:

```go
a := map[string]int{
  "b": 1
}
fmt.Println(a["c"]) // > 0
```

But why does it return zero? Well, the compiler sees that as any other non-initialized
variable, which returns zero. There's a way, though, to check if the value is
valid within a map. We can use the "comma ok" syntax:

```go
a := map[string]int{
  "b": 1
}
c, ok = a["c"]
fmt.Println(c, ok) // > 0, false
```

What this tells us is that the map "a" doesn't have a "c" key mapped yet. Also,
you don't NEED to use the "ok" name, but it's convention.

Maps also have the "len(map)" function and, just like slices, maps are reference
values.

## Structs

Structs are very similar to C structs. They are a way to store data of variable
types in the same "place". To declare a struct, just do:

```go
type Doctor struct {
  number int
  actorName string
  companions []string
}
```

To assign values to the struct fields, we can do:

```go
aDoctor := Doctor {
  number: 3,
  actorName: "Jon Pertwee",
  companions: []string {
    "Liz Shaw",
    "Jo Grant",
    "Sarah Jane Smith",
  },
}

// > {3 Jon Pertwee [Liz Shaw Jo Grant Sara Jane Smith]}
```

To access a specific field, we just use the "dot operator", just like any other
language.

When assign values to the struct, we don't HAVE to write the name of the field we're
assign, assuming we follow the correct order, and, while this is valid go syntax,
it is not recommended since creates a maintenance problem (the order of the fields
can change, for example).

As expected, if we declare a struct with a capital first letter, that means it'll
be available to other packages. But, if the fields start with lower case, they
won't be available. So, if you want to make any fields available outside of the
package, you must capitalize them too.

Another interesting thing is that you don't have to declare a type of struct,
like the example above. You can also declare anonymous structs, like so:

```go
aDoctor := struct{name string}{name: "John Pertwee"}
```

Unlike maps, structs are value types.

Although Go doesn't support things like inheritance, we can use a concept called
"embedding" to achive similar results. For example, we can declare structs for
an Animal type and a Bird type. Now, birds are also animals and should have the
same fields as a any other animal, so:

```go
type Animal struct {
  Name string
  Origin string
}

type Bird struct {
  Animal  // embeds the Animal struct
  SpeedKPH float32
  CanFly bool
}
```

The embedding is different then declaring a field of type Animal. It literally
provides the Animal fields to the Bird struct. The declaration of a field of type
Animal is also possible and is just like in C. If we were to print the type,
Bird is still "just" a bird and not a type of animal, since we are not inheriting.

If we are modeling behaviour, embedding is probably not the best case for it.
Although it is possible to embed methods, the fact that you can't use the embeded
and embedding struct interchangeably, is a very severe limitation. To model
behaviour, it is best to use Interfaces.

Structs can also have a "tag" within its fields. The tag can be used to, for
example, create some validations to the fields of the struct. To achieve this,
we need the "reflect" package (Golang supports reflection):

```go
import (
  "fmt"
  "reflect"
)

type Animal struct {
  Name string `required max:"100"`  // tag for a required field with max length of 100 characters
  Origin string
}

func main() {
  t := reflect.TypeOf(Animal{})
  field, _ := t.FieldByName("Name")
  fmt.Println(field.Tag)  // > required max:"100"
}
```

The tags only provide a string of text. What the string will be used for, is up
the framework/developer.
