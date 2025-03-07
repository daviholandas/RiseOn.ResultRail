# RiseOn.ResultRail

RiseOn.ResultRail is a NuGet package that provides functional programming constructs using the Railway-Oriented Programming model. It simplifies error handling and operation branching by encapsulating success and failure states, promoting a functional approach in C#.

## Installation

Install the package via NuGet:
## Features

- **Railway-Oriented Programming:** Seamlessly switch between success and failure rails.
- **Result Type Handling:** Use `Upshot` and `Upshot<T>` types to represent operation outcomes.
- **Extension Methods:** Enjoy helper methods like `OnRail`, `OnRailSuccess`, `OnRailFail`, and `Map` to chain operations.

## Examples

### Using Upshot

The `Upshot` type represents the result of an operation, indicating success or failure. Here's an example of using `Upshot`:
For operations with a value, use `Upshot<T>`:
### Using UpshotExtensions

The extension methods help to branch logic based on the upshot state.
## License

This project is licensed under the [MIT License](LICENSE).

## Contributing

Contributions are welcome! Please see the [contributing guidelines](CONTRIBUTING.md) for more information.
