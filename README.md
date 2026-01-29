# FileService — C# Console App (File I/O)

A simple C# console application that creates and reads `.txt` files.
Built to practice **File I/O**, **menus**, and **clean console UX**.

## Features
- Create a new `.txt` file and save text
- List existing `.txt` files in a folder
- Read and display the content of a selected file
- Clear console between menu screens for a clean experience

## Demo
![Demo](demo8.gif)

## How it works (Menu)
1) **Create file**
   - Asks for a file name
   - Saves text into a new file
   - If the file already exists, shows a warning

2) **List & read files**
   - Shows all `.txt` files in the target folder
   - Lets you pick a file and prints its content

## Storage path
By default, files are saved to:

`D:\C#Work`

> Note: If the folder doesn’t exist, the app creates it automatically.

### Change the folder
You can change the save directory in code (search for the folder path constant/variable) and set it to your preferred location.

## Run the project

### Option 1 — Visual Studio
1. Open `FileService.sln`
2. Click **Start**

### Option 2 — .NET CLI
```bash
dotnet run --project FileService
Tech

C#
.NET (Console App)

Author
Ro‘ziyev Eldor
