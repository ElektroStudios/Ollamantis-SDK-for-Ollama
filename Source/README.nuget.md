<!-- Common Project Tags:
api-analysis 
c#
c-sharp 
cli 
cli-tool 
code-analysis
codeanalysis
command-line 
command-line-tool 
console-applications 
csharp 
csharp-language 
desktop-app 
desktop-application 
dotnet 
dotnet-core 
dotnetcore 
netcore 
netframework 
netframework48 
public-apis 
roslyn
roslyn-analyzer
source-code-analysis 
source-files 
summarizer 
textfile 
tool 
tools 
vbnet 
visual-studio 
visualbasic-language 
visualbasicdotnet 
visualstudio 
vs-code
vscode
windows 
windows-app 
windows-application 
windows-applications 
 -->

# Ollamantis (CS-VB_PMS_Gen.exe)

![CS-VB_LINES Logo](https://raw.githubusercontent.com/ElektroStudios/CS-VB-Public-Members-Summary-Generator/main/Images/App.ico)

### Command-line utility to generate and integrate a summary of public API members in your C# or VB.NET source-code files.

------------------

## 👋 Introduction

**Ollamantis** is a command-line interface (CLI) tool designed to analyze C# (`*.cs`) and Visual Basic .NET (`*.vb`) source-code files to identify and document their public API structure. 

The application scans a given directory to process supported source-code files, identifying all publicly accessible members and inserting a clean overview inside a designated `#region` directive on top of each file using a safe, atomic file replacement operation.

## 👌 Features

- Identifies and summarizes public constructors, properties, fields, events, methods, operators, delegates, and enumerations.
- Automatically updates existing summary `#region` directives, or completely removes them if no public members remain in the file.
- Supports and preserves UTF-8, UTF-16, and UTF-32 file encodings in both Little Endian and Big Endian, with or without BOM.
- Files are read and rewritten preserving their exact text encoding, supporting UTF-8, UTF-16, and UTF-32 in both Little Endian and Big Endian byte orders, with or without a Byte Order Mark (BOM).
- Automatically skips empty files, white-space-only files, Visual Studio auto-generated files, and files with unrecognized or unsupported text encodings.
- Performs strict, paranoid validation checks, comparing line counts and content hashes, to guarantee all code outside the target summary `#region` remains byte-for-byte identical and is not modified or lost by mistake.
- Updates your source-code files safely by writing changes to a temporary file first, using an atomic replacement with a temporary `.bak` file to ensure data integrity.
- Supports optional recursive scanning (`-r` or `--recurse` flags).
- Provides a safe dry-run environment (`-t` or`--test` flags) to simulate changes without modifying your actual files.
- Uses [Natural sort order](https://en.wikipedia.org/wiki/Natural_sort_order) to process files, making the program execution sequence predictable and easy to follow.

## 🖼️ Screenshots

![screenshot1](https://raw.githubusercontent.com/ElektroStudios/CS-VB-Public-Members-Summary-Generator/main/Images/screenshot1.png)

![screenshot2](https://raw.githubusercontent.com/ElektroStudios/CS-VB-Public-Members-Summary-Generator/main/Images/screenshot2.png)

![screenshot3](https://raw.githubusercontent.com/ElektroStudios/CS-VB-Public-Members-Summary-Generator/main/Images/screenshot3.png)

![screenshot4](https://raw.githubusercontent.com/ElektroStudios/CS-VB-Public-Members-Summary-Generator/main/Images/screenshot4.png)

## 📝 Requirements

- Microsoft Windows OS.

## 🤖 Getting Started

Choose the installation method that best fits your workflow. You can either use the tool as a standalone portable executable or install it as a .NET Tool via NuGet.

### Option A: Standalone Executable (Portable)

This is the standard approach if you want a ready-to-use console application without relying on the .NET SDK.

1. Navigate to the [Releases page](https://github.com/ElektroStudios/CS-VB-Public-Members-Summary-Generator/releases/latest).
2. Download the latest `.zip` archive.
3. Extract the contents to your preferred directory.
4. Open your terminal and run the executable directly.

### Option B: .NET  Tool (NuGet)

If you are a developer with the .NET SDK installed, you can install the package globally. This is highly recommended as it allows you to invoke the tool directly from Visual Studio in the directory of your current solution.

Open your terminal and run the following command:

```bash
dotnet tool install --global CS-VB_Public_Members_Summary_Generator
```

Once installed successfully, you can invoke the tool from anywhere using its command name `CS-VB_PMS_Gen`.

## ⚙️ Usage

```bash
CS-VB_PMS_Gen.exe <directory_path> [options]
```

| Mandatory&nbsp;Arguments | Description |
| :--- | :--- |
| <directory_path> | The path to the root directory containing the `*.cs` or `*.vb` source-code files to process. |

| Options | Description |
| :--- | :--- |
| **-r**,&nbsp;**--recursive** | Recursively process source-code files in all subdirectories. |
| **-t**,&nbsp;**--test** | Runs the application in Test Mode (dry-run), simulating the entire process without modifying actual files. |

### 🚀 Examples

Generate and insert public member summaries only for files in the specified root directory:
```bash
CS-VB_PMS_Gen.exe "C:\MySolution"
```

Recursively generate and insert public member summaries for files within the specified root directory and its subdirectories:
```bash
CS-VB_PMS_Gen.exe "C:\MySolution" -r
```

Run a safe simulation dry-run recursively to validate the execution path without modifying actual files:
```bash
CS-VB_PMS_Gen.exe "C:\MySolution" -r --test
```

---

## 🔄 Change Log

Explore the complete list of changes, bug fixes, and improvements across different releases by clicking [here](https://github.com/ElektroStudios/CS-VB-Public-Members-Summary-Generator/blob/main/Docs/CHANGELOG.md).

## ⚠️ Disclaimer:

This Work (the repository and the content provided in) is provided "as is", without warranty of any kind, express or implied, including but not limited to the warranties of merchantability, fitness for a particular purpose and noninfringement. In no event shall the authors or copyright holders be liable for any claim, damages or other liability, whether in an action of contract, tort or otherwise, arising from, out of or in connection with the Work or the use or other dealings in the Work.

This Work has no affiliation, approval or endorsement by the author(s) of the third-party libraries used by this Work.

## 💪 Contributing

Your contribution is highly appreciated!. If you have any ideas, suggestions, or encounter issues, feel free to open an issue by clicking [here](https://github.com/ElektroStudios/CS-VB-Public-Members-Summary-Generator/issues/new/choose). 

Your input helps make this Work better for everyone. Thank you for your support! 🚀

## 💰 Beyond Contribution 

This project is distributed for educational purposes without any profit motive. However, if you find value in my efforts and wish to support financially my ongoing work, please consider visiting the main repository page on GitHub to view the full sponsorship section and explore the available financial contribution options.

Your support means the world to me! Thank you for considering it! 👍
