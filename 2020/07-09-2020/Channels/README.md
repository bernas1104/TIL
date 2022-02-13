# Today I Learned

The last topic of the tutorial video is: channels. Most popular languages (such
as Java, C, C++, C#, etc...) were developed back when multicore desktops didn't
exist. So, as time went by and multicore became a reality, developers began
introducing patches and libraries to make concurrency and parallelism available
to thoses languages.

This is different on Golang. Golang was developed with concurrency and parallelism
in mind, so it has mechanisms for dealing with it as part of its core.

So, the "channels" mechanism is a way that threads can pass information between
each in a safe way and avoiding problems, such as race conditions and memory share
problems.

- Channel Basics
- Restrict data flow
- Buffered channels
- Closing channels
- For...range loops with channels
- Select statements

## Channel Basics

First, we need to create a WaitGroup to ensure that our application won't exit
before the goroutines finish executing. Next, we need to create our channel. To
do this, we can use the "make" statement and there's no other syntax for this:

```go
var wg = sync.WaitGroup{}

func main() {
  ch := make(chan int)    // Creates a channel for integers to flow through it
  wg.Add(2)               // Prepares to wait for 2 goroutines
  go func() {
    i := <- ch            // Receives data through the channel
    fmt.Println(i)        // Prints the received data
    wg.Done()             // Finishes the execution of the goroutine
  }()
  go func() {
    ch <- 42              // Sends the data "42" through the channel
    wg.Done()             // Finishes the execution of the goroutine
  }()
  wg.Wait()
}
```

Channels can be useful when you're dealing with processing information asynchronously,
i.e., when you have a thread that generates information very quickly, but it takes
time to process that information, or you have multiple threads that take time to
generate information and a thread that process the information very quickly.

```go
var wg = sync.WaitGroup{}

func main() {
  ch := make(chan int)      // Creates a channel for integers to flow through it
  for j := 0; j < 5; j++ {
    wg.Add(2)
    go func() {
      i := <- ch            // Receives data through the channel
      fmt.Println(i)        // Prints the received data
      wg.Done()             // Finishes the execution of the goroutine
    }()
    go func() {
      ch <- 42              // Sends the data "42" through the channel
      wg.Done()             // Finishes the execution of the goroutine
    }()
  }
  wg.Wait()
}
```

In the above example, things work as expected. Five messages are sent and five
messages are received. But, if we were to move the receiver outside of the loop,
this would generate a runtime error - deadlock - because we would be sending
messages that would never be received.

Also, both examples can have only one message in the channel at any given time,
since we are not working with buffered channels.

## Restrict Data Flow

We can create goroutines that are both readers and writers to a single channel,
but that's not recommended. It is best to use a goroutine just for reading things
from the channel and another just to write things into the channel. To make it
easier to see which goroutine is the reader and which is the writer, we can use
parameters:

```go
var wg = sync.WaitGroup{}

func main() {
  ch := make(chan int)      // Creates a channel for integers to flow through it
  for j := 0; j < 5; j++ {
    wg.Add(2)
    go func(ch <-chan int) {// RECEIVE ONLY CHANNEL
      i := <- ch            // Receives data through the channel
      fmt.Println(i)        // Prints the received data
      wg.Done()             // Finishes the execution of the goroutine
    }(ch)
    go func(ch chan<- int) {// SEND ONLY CHANNEL
      ch <- 42              // Sends the data "42" through the channel
      wg.Done()             // Finishes the execution of the goroutine
    }(ch)
  }
  wg.Wait()
}
```

## Buffered Channels

As mentioned, if we were to write the receiver channel outside of the loop, the
compiler would throw an error - deadlock. We could - although it might not be always
the best way to do it - use buffered channels to solve this problem.

Buffered channels are a way to allow channels to receive multiple messages and
store them. They will remain stored until a receiver pulls them from the channel
or they will be lost if, by the end of execution, no goroutine pulls them from
the channel.

Buffered channels are designed for when the sender or the receiver operates in a
different frequency than the other side. Suppose that you have a system data, for
any reason, sends data every hour. Your receiver will have to deal with that burst
of information. The buffered channel is a way to store all that data that arrives
every hour, while the receiver process it, without blocking the sender.

## for...range loops with channels

Now, the way you normally execute some action multiple times is with a loop. So,
the "for...range" loops is used when you are using buffered channels. It tells
the loop to execute while the channel is open, so we also need to close the channel,
otherwise our application will deadlock.

```go
var wg = sync.WaitGroup{}

func main() {
  ch := make(chan int, 50)  // Creates a buffered channel
  for j := 0; j < 5; j++ {
    wg.Add(2)
    go func(ch <-chan int) {// RECEIVE ONLY CHANNEL
      for i := range ch {   // Loops through the data on the channel
        fmt.Println(i)      // Prints the received data
      }
      wg.Done()             // Finishes the execution of the goroutine
    }(ch)
    go func(ch chan<- int) {// SEND ONLY CHANNEL
      ch <- 42              // Sends the data "42" through the channel
      ch <- 27              // Sends the data "27" through the channel
      close(ch)             // Closes the channel
      wg.Done()             // Finishes the execution of the goroutine
    }(ch)
  }
  wg.Wait()
}
```

If we close the channel, we need to be sure that no goroutine will try to send a
new message through the closed channel. If that happens, the application will panic.
There's no alternative here. If there's any chance that a goroutine will try to
send a message after the channel was closed, you'll need to use a defered recover.

On the receving end, things work a little different. While the sender can't tell
before hand if the channel is actually open, the receiver can check automatically
with for...range loop or manually with the "comma ok" syntax.

## Select Statements

In some cases, we need more control over our channels, especially to close channels
in a proper way. We can use "select statements" to achieve this. Also, with this
approach, we might need to signalize to our goroutine when to wrap thigs up. We
might me tempted to create a second channel that receives a boolean, but this actually
needs to allocate memory for the boolean value. A better approach would be to create
a signal only channel using an empty struct, since it won't need any memory allocation.

```go
const (
  logInfo    = "INFO"
  logWarning = "WARNING"
  logError   = "Error"
)

type logEntry struct {
  time      time.Time
  severity  string
  message   string
}

var logCh  = make(chan logEntry, 50)
var doneCh = make(chan struct{})      // Creates the signal channel

func main() {
  go logger()
  logCh <- logEntry{time.Now(), logInfo, "App is starting"}
  logCh <- logEntry{time.Now(), logInfo, "App is shutting down"}

  time.Sleep(100 * time.Millisecond)
  doneCh <- struct{}{}                // Sends the signal through the channel
}

func logger() {
  for {                               // Infinite loop
    select {                          // Checks if a given message was received
      case entry := <-logCh:          // If received through logCh
        fmt.Println(/* code to print the log */)
      case <-doneCh:                  // If received through signal channel
        break;                        // Breaks out of the loop and exits the goroutine
      // default: ...                 // It is possible to add a default behaviour
    }
  }
}
```

If we were to add the "default" statement to our select, then our select wouldn't
be a blocking select statement anymore. Also, if multiples channels receive messages
simultaneously, behaviour is undefined.
