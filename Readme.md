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
    
    \\ or just use Error property
    Console.WriteLine($"Operation failed: {upshot.Error.ToString()}");
    or
    Console.WriteLine($"Operation failed: {(string)upshot.Error}");
    
    \\ output: Operation failed: { Message: An error occurred }
}
````

The `Upshot` type represents the result of an operation, indicating success or failure. Here's an example of using `Upshot`:
For operations with a value, use `Upshot<T>`:

```csharp
Upshot<int> upshot = Upshot.Success(42);
Upshot<int> upshot = Upshot.Fail<int>("An error occurred"); or Upshot<int> upshot = Upshot.Fail<int>(new Exception("An error occurred"));

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

```csharp
Upshot upshot = Upshot.Success();
upshot.OnRailSuccess(() => Console.WriteLine("Operation succeeded"));
````
- ### OnRailFail
In `OnRailFail` extension to execute an action only if the operation fails.
```csharp
Upshot upshot = Upshot.Fail("An error occurred");
upshot.OnRailFail(() => Console.WriteLine("Operation failed"));
````
- ### OnRail
In `OnRail` extension to execute different actions based on the operation's success or failure.
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
