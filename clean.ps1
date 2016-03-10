


function nuke {
    param ( [string]$folder ) 
    if( test-path $folder )  {
        $folder = resolve-path $folder
        write-host -fore DarkYellow "    Removing $folder"
        $null = mkdir "$env:temp\mt" -ea 0 
        $shh = robocopy /mir "$env:temp\mt" "$folder" 
        $shh += rmdir -recurse -force "$folder"  -ea 0 
        if( test-path $folder ) {
            write-host -fore red "    In Use: '$FOLDER'"
        }
    }
}

function nuke-knownfolders {
   param ( [string]$folder ) 
    
    nuke "$env:temp\$((get-item $folder).Name)"
    nuke "$folder\bin" 
    nuke "$folder\intermediate" 
    nuke "$folder\generated" 
    nuke "$folder\obj" 
    nuke "$folder\objd" 
    nuke "$folder\output" 
    nuke "$folder\artifacts"
    nuke "$folder\packages"
   
}

function clean-project ([string] $prj ){ 
    if( test-path "$pwd\$prj\project.json"  )  {
        write-host -fore Yellow "Cleaning project ($prj)"
        if( (test-path "$pwd\$prj") ) {
            nuke-knownfolders (resolve-path "$pwd\$prj")
        }
    } else {
        if( test-path "$pwd\$prj" ) {
            pushd $prj
            $items = (dir "$pwd\*\project.json").Directory.Name
            
            $items |% { 
                write-host -fore Yellow "Cleaning project ($_)"
                nuke-knownfolders $_ 
            } 
            
        popd
        }
    } 
}

if ( (get-process devenv -ea 0).count -gt 0  ) {
    write-host -fore DarkMagenta "Visual Studio is running. (This may fail if this project is open)"
}

pushd .

try {
    do {
        $last = $pwd
        if( (test-path .\global.json ) ) {
            write-host -fore green "Using global file: $(resolve-path .\global.json)"
            write-host -fore green "Cleaning projects"
            (dir dtar*) |% { nuke "$_" }
            
            (convertfrom-json (get-content -raw .\global.json )).projects |% {
               clean-project $_
            }
            
            write-host -fore Yellow "Cleaning global folders"
            nuke-knownfolders  (resolve-path "$pwd")
            return
        }
        cd ..
        if( $pwd.path -eq $last.path )  {
            popd 
            pushd 
            do {
                $last = $pwd
                if( (test-path .\project.json ) ) {
                    write-host -fore green "Using project file: $(resolve-path .\project.json)"
                    nuke-knownfolders (resolve-path "$pwd")
                    
                    if( ((cmd /c dir /s/b $pwd\*.resx 2> $env:temp\null).Count) -gt 0 ) {
                        write-host -fore magenta "    Generating resource .Designer.cs files in project ($pwd)"
                        . "$psscriptroot\generate-resourcebindings.ps1" "$pwd"
                        
                    }

                    
                    return
                }
                if( $pwd.path -eq $last.path )  {
                    write-host -fore red "Didn't find project.json or global file in tree."
                    return;
                }
            } while( $true )    
        }
    } while( $true ) 
} finally {
    popd
}