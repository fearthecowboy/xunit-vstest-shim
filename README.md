# Visual Studio Test shim to use xUnit to run tests.

# Requirements
- Dotnet-cli tools : https://dotnetcli.blob.core.windows.net/dotnet/beta/Installers/Latest/dotnet-win-x64.latest.exe
    
    

# Notes:
- this is *very* new code. It works for essentially three test projects that I have to test against.
    
- there's a lot that's *not* implemented (VSTest `assert` calls that aren't actually used in my project.)
    
# Building

### Compiling Code

``` batch

dotnet restore


```

### Running tests 

``` powershell
# or call from cmd with PowerShell .\run-tests.ps1

.\run-tests.ps1

```
    
### Building NuGet package

``` batch

dotnet pack --configuration release src\Xunit.Microsoft.VisualStudio.TestTools.UnitTesting

```

### Cleaning 

``` powershell
# or call from cmd with PowerShell .\clean.ps1

.\clean.ps1

```
