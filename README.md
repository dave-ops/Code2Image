# Code2Image - C# Code to Image Converter

## Overview
`Code2Image` is a C# console application that converts code files (e.g., `.js`, `.py`, `.java`, `.cpp`, `.c`) into syntax-highlighted PNG images and allows copying those images to the Windows clipboard for pasting into web upload controls or other applications. This project is inspired by a Node.js `code2img` tool and provides similar functionality using .NET.

## Features
- Convert code files to syntax-highlighted PNG images.
- Copy generated images to the Windows clipboard for easy pasting (e.g., into web upload forms).
- Maintain a history of generated images.
- Command-line interface (CLI) with `convert` and `copy` commands.

## Prerequisites
- **Operating System**: Windows (required for clipboard functionality).
- **.NET SDK**: Version 9.0 or higher (currently targeting `.NET 9.0`, a pre-release version as of February 2025). For stability, consider targeting `.NET 8.0`.
- **Development Environment**: Visual Studio, Visual Studio Code, or any IDE supporting C# with .NET.

## Installation

### 1. Clone the Repository
Clone this repository to your local machine:
```bash
git clone <repository-url>
cd Code2Image
```

### 2. Install Dependencies
Ensure the .NET SDK is installed. Then, restore the NuGet packages:
```bash
dotnet restore
```
This will install the required packages listed in `Code2Image.csproj`, including:
- `ColorCode` (for syntax highlighting),
- `System.CommandLine` (for CLI parsing),
- `System.Drawing.Common` (for image generation).

### 3. Verify .NET Version
Check your .NET SDK version:
```bash
dotnet --version
```
Ensure it’s `.NET 9.0` or higher. If targeting `.NET 8.0` for stability, update `Code2Image.csproj`:
```xml
<TargetFramework>net8.0</TargetFramework>
```
Then rerun `dotnet restore`.

## Usage
Run the application using the following commands:

### Convert a Code File to an Image
Generate a PNG image from a code file:
```bash
dotnet run -- convert <input-file> [--output <output-file>]
```
- `<input-file>`: Path to the code file (e.g., `src/server.js`).
- `--output` or `-o`: Optional output PNG file path (defaults to `output.png` or `<input-file>.png`).

Example:
```bash
dotnet run -- convert src/server.js --output server.png
```

### Copy Images to Clipboard
Copy all generated images from the history to the Windows clipboard for pasting:
```bash
dotnet run -- copy
```
- The most recent images can be pasted into web upload controls or other applications (e.g., Microsoft Word, Paint).

## Project Structure
- `Program.cs`: Main entry point with CLI logic.
- `CodeToImageConverter.cs`: Handles code-to-image conversion with syntax highlighting.
- `HistoryManager.cs`: Manages the history of generated images (stored in `image-history.json`).
- `ClipboardHelper.cs`: Handles copying images to the Windows clipboard.
- `Code2Image.csproj`: Project configuration file with dependencies and settings.

## Dependencies
- **ColorCode (1.0.1)**: For syntax highlighting of code.
- **System.CommandLine (2.0.0-beta4.22272.1)**: For command-line parsing (pre-release version).
- **System.Drawing.Common (9.0.2)**: For image generation.
- **Newtonsoft.Json**: For JSON serialization of history (add via `dotnet add package Newtonsoft.Json` if not included).
- **System.Windows.Forms**: For clipboard operations (add via `dotnet add package System.Windows.Forms` if not included).

## Known Issues and Troubleshooting
### Current Errors and Warnings
When building the project, you may encounter:

- **"The type or namespace name 'CommandLine' does not exist in the namespace 'System' (CS0234)"**  
  - Ensure `System.CommandLine` is installed with the pre-release flag:
    ```bash
    dotnet add package System.CommandLine --prerelease
    ```
  - Verify `.csproj` includes `<PackageReference Include="System.CommandLine" Version="2.0.0-beta4.22272.1" />` and run `dotnet restore`.

- **"Argument 1: cannot convert from 'string' to 'System.CommandLine.Argument<string>' (CS1503)"**  
  - Update `Program.cs` to use correct `Argument` and `Option` constructors, e.g.:
    ```csharp
    new Argument<string>("input", description: "Input code file path")
    new Option<string>(new[] { "--output", "-o" }, description: "Output image file path") 
    { 
        DefaultValueFactory = _ => "output.png" 
    }
    ```

- **"The type or namespace name 'CodeToImageConverter' could not be found (CS0246)"**, **"The name 'HistoryManager' does not exist (CS0103)"**, **"The name 'ClipboardHelper' does not exist (CS0103)"**  
  - Add the missing files (`CodeToImageConverter.cs`, `HistoryManager.cs`, `ClipboardHelper.cs`) to the project directory and include them in `.csproj`:
    ```xml
    <ItemGroup>
      <Compile Include="CodeToImageConverter.cs" />
      <Compile Include="HistoryManager.cs" />
      <Compile Include="ClipboardHelper.cs" />
    </ItemGroup>
    ```

- **"Operator '==' cannot be applied to operands of type 'method group' and 'int' (CS0019)"**  
  - Ensure `if (history.Count == 0)` uses `Count` (property) not `Count()` (method). Verify `history` is a `List<string>` or similar.

- **"Warning NU1701: Package 'ColorCode 1.0.1' was restored using '.NETFramework,...' instead of 'net9.0'"**  
  - `ColorCode 1.0.1` targets older .NET Framework versions. Options:
    - Update to a newer `ColorCode` version supporting `.NET 9.0` or `.NET Standard`:
      ```bash
      dotnet add package ColorCode --version <latest-version>
      ```
    - Target `.NET 8.0` for stability:
      ```xml
      <TargetFramework>net8.0</TargetFramework>
      ```
      Then reinstall `ColorCode`:
      ```bash
      dotnet remove package ColorCode
      dotnet add package ColorCode
      ```
    - Suppress the warning (not recommended):
      ```xml
      <PackageReference Include="ColorCode" Version="1.0.1">
        <NoWarn>NU1701</NoWarn>
      </PackageReference>
      ```

- **"This async method lacks 'await' operators (CS1998)"**  
  - Ignore this warning for now, as the async methods are simple. Optionally, remove `async`/`await` if no async operations are needed, or add `await` for async calls (e.g., `await Task.CompletedTask`).


---