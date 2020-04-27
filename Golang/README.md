# Toll fee calculator 1.0 Golang Version

This is rewrite to Golang version from C# code. Some coding style is adjusted based on Golang idiom way.
For example:

* Write unit test each function: run using `go test -v -cover -race -covermode=atomic ./...` will return 100% code coverage test.
* Instead writing `if (bla = bla) {} else {}`, Golang use 
  ```
  defaultValue := 1
  if bla == bla {
    defaultValue = 2
  }
  ```
  
  See function `isTollFreeDate` where we first checked the year is not 2013 should be early returned as false.
  This also adding up the readability of the code itself.

* Use this simple syntax:
  ```
  for (statement) {
    if (condition) {
      // operation
      continue
    }
  
    // else operation
  }
  ```
  
  Instead,
  
  ```
  for (statement) {
     if (condition) {
       // operation
     } else {
       // else operation
     }
  }
  ```
  
  See function `GetTollFee`.

* Instead using `if () else if () else`, we should use `switch` statement, which early return the value once it match the first rule.
  It does not mean Golang not support `else if` statement, but it is the Go idiom way https://golang.org/doc/effective_go.html#switch.
  See function `isTollFreeDate` and `getTollFee`.
  
* Instead writing constant enum for Vehicle type, it must use the real implementation foreach type.
  It also add one more function `IsTollFree` in the `Vehicle` interface, since it easy to manage 
  which vehicle that eligible for fee-free in each vehicle type instead writing long `if` statement 
  which may will grow into more lines when type of vehicle is added.
  
* Separate function that have different job into single file to make code easy to follow. And we can add unit test foreach files.