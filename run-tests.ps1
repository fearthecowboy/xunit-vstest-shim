pushd "$PSScriptRoot\src\Xunit.Microsoft.VisualStudio.TestTools.UnitTesting.Tests"

try {
    $xunit = "$env:userprofile\.nuget\packages\xunit.runner.console\2.1.0\tools\xunit.console.exe"
    # is the test runner installed into packages

    if ( -not (test-path $xunit )) {
        write-error "Could not find xunit.console.exe in the expected location '$xunit'"
        write-host -fore yellow "`nPlease run run 'dotnet-restore' to ensure packages are installed."
        return
    }


    # build tests for all platforms
    write-host -fore green "`nBuilding test suite"
    dotnet build

    # run tests on full clr
    write-host -fore green "`nRunning tests on Full CLR (net46)"
    & $xunit "$PSScriptRoot\src\Xunit.Microsoft.VisualStudio.TestTools.UnitTesting.Tests\bin\Debug\net46\win7-x64\Xunit.Microsoft.VisualStudio.TestTools.UnitTesting.Tests.dll"

    # run tests for coreclr
    write-host -fore green "`nRunning tests on Core CLR (netstandard1.5)"
    dotnet test 
} finally {
    popd
}