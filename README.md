# Search Selected

A Visual Studio extension that lets you search the currently selected text without leaving your editor workflow.

Instead of manually copying text into a browser, you can select code, an exception, or any text in the editor and launch a search immediately from the context menu or with a keyboard shortcut.

## What It Does

- Searches the text currently selected in the active editor.
- Supports a context-menu command.
- Supports a keyboard shortcut: `Alt+S`.
- Opens results either inside Visual Studio or in an external browser.
- Lets you choose the search provider from the Visual Studio options page.
- Falls back to clipboard text when no editor selection is available.

## Current Search Providers

- Google
- Bing
- Stack Overflow

## How To Use

### Option 1: Context Menu

1. Select text in the editor.
2. Right-click the selection.
3. Choose the search command.

### Option 2: Keyboard Shortcut

1. Select text in the editor.
2. Press `Alt+S`.

The extension will build a search URL using the selected text and open it with your configured behavior.

## Configuration

Open:

`Tools -> Options -> Search Extension -> General`

Available settings:

- `Use Visual Studio Browser`
  Opens the result inside Visual Studio when enabled.

- `Search Engine`
  Chooses the provider used to build the search URL.

- `Browser Path`
  Used when Visual Studio browser mode is disabled.
  The default path points to Microsoft Edge.

## Default Behavior

- Default provider: `Google`
- Default browser path: Microsoft Edge
- Default open mode: inside Visual Studio

## Development

This project is a Visual Studio extension (`VSIX`) built with:

- `.NET Framework 4.8`
- `Community.VisualStudio.Toolkit.17`
- `Microsoft.VSSDK.BuildTools`

Main project:

- [SearchExtension.sln](/f:/Search-Selected/SearchExtension.sln)

Key source files:

- [SearchCommand.cs](/f:/Search-Selected/SearchExtension/Commands/SearchCommand.cs)
- [SearchOptions.cs](/f:/Search-Selected/SearchExtension/Options/SearchOptions.cs)
- [SearchExtensionPackage.cs](/f:/Search-Selected/SearchExtension/SearchExtensionPackage.cs)

## Build And Run

1. Open [SearchExtension.sln](/f:/Search-Selected/SearchExtension.sln) in Visual Studio.
2. Restore NuGet packages if prompted.
3. Build the solution.
4. Start debugging to launch the Experimental Instance of Visual Studio.
5. Test the extension by selecting text and using the command or `Alt+S`.

## Example Use Cases

- Search a C# exception message.
- Search an unfamiliar API or class name.
- Search a compiler error.
- Search a SQL snippet.
- Search a package name or library.

## Screenshots

### Open The Options Page

![Step 1](https://user-images.githubusercontent.com/52336027/156454680-2c529a73-920b-48a7-9873-7eae5181bf7b.png)

### Find The Extension Settings

![Step 2](https://user-images.githubusercontent.com/52336027/156454682-87ab2fef-432c-4074-bb7f-69fe463a857b.png)

### Update Search Settings

![Step 3](https://user-images.githubusercontent.com/52336027/156454676-26276cb6-5537-45ce-a969-7f79d8c8d79d.png)

## Limitations

- Search provider choices are currently fixed in code.
- Search text is URL-assembled in a simple way today.
- The extension does not yet classify text types such as errors, symbols, or stack traces automatically.

## Roadmap Ideas

- Smarter query encoding and cleanup
- More search providers such as GitHub, Microsoft Learn, and NuGet
- Search history and favorites
- Local solution search before web search
- ML.NET-based intent classification for developer-focused routing

## License

This project is licensed under the [MIT License](./LICENSE).
