# RiseOn.ResultRail

RiseOn.ResultRail is a NuGet package that provides functional programming constructs using the Railway-Oriented Programming model. It simplifies error handling and operation branching by encapsulating success and failure states, promoting a functional approach in C#.

## Installation

Install the package via NuGet:

```bash
dotnet add package RiseOn.ResultRail 
```

or

```powershell
PM> Install-Package RiseOn.ResultRail
```

## Features

- **Railway-Oriented Programming:** Seamlessly switch between success and failure rails.
- **Result Type Handling:** Use `Upshot` and `Upshot<T>` types to represent operation outcomes.
- **Extension Methods:** Enjoy helper methods like `OnRail`, `OnRailSuccess`, `OnRailFail`, and `Map` to chain operations.

## Examples

### Using Upshot

```csharp
Upshot upshot = Upshot.Success();
Upshot upshot = Upshot.Fail("An error occurred"); or Upshot upshot = Upshot.Fail(new Exception("An error occurred"));

if (upshot.IsSuccess)
{
    //Do something
}
else
{
    Console.WriteLine($"Operation failed: {upshot.Error.Message}");
    
    \\ or just use ToString() method
    Console.WriteLine($"Operation failed: {upshot.Error.ToString()}");
    or
    Console.WriteLine($"Operation failed: {(string)upshot.Error}");
    output: Operation failed: { Message: An error occurred }
}
````

The `Upshot` type represents the result of an operation, indicating success or failure. Here's an example of using `Upshot`:
For operations with a value, use `Upshot<T>`:

```csharp
Upshot<int> upshot = Upshot<int>.Success(42);
Upshot<int> upshot = Upshot<int>.Fail("An error occurred"); or Upshot<int> upshot = Upshot<int>.Fail(new Exception("An error occurred"));

var result = upshot.Value;

if (upshot.IsSuccess)
{
    Console.WriteLine($"Result: {result}");
    output: Result: 42
}
else
{
    Console.WriteLine($"Operation failed: {upshot.Error.Message}");
}
````
### Using UpshotExtensions
- ### OnRailSuccess
In `OnRailSuccess` extension to execute an action only if the operation is successful.
Whether the operation has a value, you can use the `OnRailSuccess` extension to transform the value and return a new `Upshot` with the new value, otherwise, it will return the same `Upshot` instance.

```csharp
Upshot upshot = Upshot.Success(); or Upshot upshot = Upshot<int>.Success(10);
upshot.OnRailSuccess(() => Console.WriteLine("Operation succeeded"));
or
var result = upshot.OnRailSuccess(value => value + 10);
output: result = 20;
````

- ### OnRailFail
In `OnRailFail` extension to execute an action only if the operation fails.When the operation fails, the `OnRailFail` extension will execute the specified action, otherwise, it will return the same `Upshot` instance.
```csharp
Upshot upshot = Upshot.Fail("An error occurred"); or Upshot upshot = Upshot<int>.Fail(new Exception("An error occurred"));
upshot.OnRailFail(() => Console.WriteLine("Operation failed"));
or
var result = upshot.OnRailFail(error => Console.WriteLine($"Operation failed: {error.Message}"));
````
- ### OnRail
In `OnRail` extension to execute different actions based on the operation's success or failure. The `OnRail` it's a wrapper for `OnRailSuccess` and `OnRailFail` extensions.
```csharp
Upshot upshot = Upshot.Success();
upshot.OnRail(
    success: () => Console.WriteLine("Operation succeeded"),
    fail: () => Console.WriteLine("Operation failed")
);
```` 
## License

This project is licensed under the [MIT License](LICENSE).

## Contributing

Contributions are welcome! Just create a Pull Request with your changes.
If you have any questions or suggestion, feel free to open an issue.
