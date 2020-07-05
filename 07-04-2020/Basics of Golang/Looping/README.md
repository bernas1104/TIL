# Today I Learned

Now we're on looping. Although the loops are very similar to any other language
there's a few catches. If we want to initialize multiple counters on a FOR loop,
we need to to the following:

```go
for i, j := 0, 0; i < 2; i, j = i + 1, j + 1 {
  // some code
}
```

If we want to increment (++) one of the variables, we can only use the increment
operator and nothing else, otherwise, we need to use "+ 1" for each variable.

As expected, we can ommit the initializer and the increment of a FOR loop. If we
do so, we can also ommit the ";" that separates the initializer from the condition
and the ";" that separates the condition from the increment.

```go
i := 0
for i < 5 {
  fmt.Println(i)
  i++
}

// > 0, 1, 2, 3, 4
```

We can also ommit every information of the FOR loop to determine, inside it, when
to exit the loop (if we don't have a way to determine initially). To this, we can
use the "break" keyword.

```go
i := 0
for {
  fmt.Println(i)
  i++
  if i == 5 {
    break
  }
}

// > 0, 1, 2, 3, 4
```

There's also the "continue" keyword, that works as expected. Interestingly, Go
also has the concept of labels. So, suppose you have a nested loop and, for any
reason, you want to break out of them. You could add some logic inside the inner
loop, add break, add some logic inside the outter loop and another break, or:

```go
Loop:                         // Declares the label "Loop"
  for i := 1; i <= 3; i++ {
    for j := 1; j <= 3; j++ {
      fmt.Println(i * j)
      if i * j >= 3 {
        break Loop            // Breaks out of both loops inside the label
      }
    }
  }

// > 1, 2, 3
```

Finally, consider a slice. We can loop through it using a special case of the for
loop, called the "for range":

```go
s := []int{1, 2, 3}     // Declares a slice of integers
for k, v := range s {   // Defines a (k)ey and a (v)alue variables for the range of the slice s
  fmt.Println(k, v)
}

/*
 * > 0, 1
 * > 1, 2
 * > 2, 3
 */
```

This works for any type of collection: slices, arrays, maps, strings, channels...
