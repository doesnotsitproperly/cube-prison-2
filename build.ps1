param (
    [switch] $Verbose,
    [switch] $LinuxX11,
    [switch] $MacOSX,
    [switch] $WindowsDesktop
)

if ((-not $LinuxX11) -and (-not $MacOSX) -and (-not $WindowsDesktop)) {
    [Console]::WriteLine("Please specify -LinuxX11, -MacOSX, and/or -WindowsDesktop")
    return
}

function CreateDirectory {
    param ([String] $Path)

    if (-not (Test-Path $Path)) {
        New-Item $Path -ItemType "directory" | Out-Null
    }
}

$buildsDirectory = Join-Path $PSScriptRoot "builds"
CreateDirectory $buildsDirectory

if ($LinuxX11) {
    CreateDirectory (Join-Path $buildsDirectory "linux_x11")

    if ($Verbose) {
        godot --no-window --export "Linux/X11"
    } else {
        godot --no-window --export "Linux/X11" | Out-Null
    }
}

if ($MacOSX) {
    CreateDirectory (Join-Path $buildsDirectory "mac_osx")

    if ($Verbose) {
        godot --no-window --export "Mac OSX"
    } else {
        godot --no-window --export "Mac OSX" | Out-Null
    }
}

if ($WindowsDesktop) {
    CreateDirectory (Join-Path $buildsDirectory "windows_desktop")

    if ($Verbose) {
        godot --no-window --export "Windows Desktop"
    } else {
        godot --no-window --export "Windows Desktop" | Out-Null
    }
}
