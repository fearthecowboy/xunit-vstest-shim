# Visual Studio Test shim to use xUnit to run tests.

## Build Requirements
Dotnet-cli tools : https://dotnetcli.blob.core.windows.net/dotnet/beta/Installers/Latest/dotnet-win-x64.latest.exe


## Nuget Package
Find the NuGet package at : https://www.nuget.org/packages/Xunit.Microsoft.VisualStudio.TestTools.UnitTesting/

## Notes:
This is *very* new code. It works for essentially three test projects that I have to test against.
    
There's a lot that's *not* implemented (VSTest `assert` calls that aren't actually used in my project.)

It's really not hard to add in the missing asserts, so if you can and can send me a pull request that'd be awesome.

If there are other things that are more complicated, file an issue, and we can see what we can do.
    
## How to use it

If you're consuming it from a __dotnet-cli__ compiled app, your `project.json` file for your test project should look something like this:
    
``` js
{
    // NOTE: comments are for illustration sake. 
    // YOU CAN NOT KEEP COMMENTS IN A JSON FILE
    //
    // TAKE THEM ALL OUT. I WARNED YOU.
    
    "name": "my.test.project",
    
    "dependencies": {
        "NETStandard.Library": "1.0.0-rc2-23909",
        "System.Runtime": "4.1.0-rc2-23909",
        "System.Reflection": "4.1.0-rc2-23909",
        
        // These are kinda important
        "Xunit.Microsoft.VisualStudio.TestTools.UnitTesting": "1.0.0-*",
        "xunit": "2.1.0"
    },

    // tell dotnet that you want to use xunit to test with.
    "testRunner": "xunit",

    // until we have desktop clr test runner support
    // you have to run the tests for desktop clr using the console test runner
    // this makes sure it gets installed with dotnet restore
    "tools": {
        "xunit.runner.console": "2.1.0"
    },


    "frameworks": {
         "netstandardapp1.5": {
            "imports": [ "dnxcore50", "portable-net45+win8" ],
            "dependencies": {
                "System.Reflection.TypeExtensions": "4.1.0-rc2-23909",
                "System.Linq": "4.1.0-rc2-23909",
                
                // this is the test runner for the core clr framework
                // must have this!
                "dotnet-test-xunit": "1.0.0-dev-79755-47"
            }
        },
        
        "net46": {
            "dependencies": {
            }
        }
    }
}    
```

Once that's done, you should be able to run the tests :

``` powershell
# from your unit test project directory
cd MYPROJECT

# restore at least once!
dotnet restore

# build the binaries
dotnet build

# run tests for coreclr
dotnet test 

# Until we have support for desktop clr in dotnet-cli test runner
# run using the xunit.console.exe test runner:

# (from powershell)
& "$env:userprofile\.nuget\packages\xunit.runner.console\2.1.0\tools\xunit.console.exe" "bin\Debug\net46\win7-x64\MYPROJECT.dll"

# (from cmd.exe)
"%userprofile%\.nuget\packages\xunit.runner.console\2.1.0\tools\xunit.console.exe" "bin\Debug\net46\win7-x64\MYPROJECT.dll"

```
    
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
