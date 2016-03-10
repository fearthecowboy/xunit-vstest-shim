# Visual Studio Test shim to use xUnit to run tests.

## Build Requirements
Dotnet-cli tools : https://dotnetcli.blob.core.windows.net/dotnet/beta/Installers/Latest/dotnet-win-x64.latest.exe
    

## Notes:
This is *very* new code. It works for essentially three test projects that I have to test against.
    
There's a lot that's *not* implemented (VSTest `assert` calls that aren't actually used in my project.)

It's really not hard to add in the missing asserts, so if you can and can send me a pull request that'd be awesome.

If there are other things that are more complicated, file an issue, and we can see what we can do.
    
## Building

### Compiling Code

``` powershell

# do a package restore first!
dotnet restore

# build the code
dotnet build 
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
