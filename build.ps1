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
    param ([String] $Path, [switch] $Remove)

    if (Test-Path $Path) {
        if ($Remove) {
            Remove-Item $Path -Recurse
            New-Item $Path -ItemType "directory" | Out-Null
        }
    } else {
        New-Item $Path -ItemType "directory" | Out-Null
    }
}

$buildsDirectory = Join-Path $PSScriptRoot "builds"
CreateDirectory $buildsDirectory

$thirdPartyLicensesFile = Join-Path $PSScriptRoot "THIRD_PARTY_LICENSES.txt"

if ($LinuxX11) {
    $linuxDirectory = Join-Path $buildsDirectory "linux_x11"
    CreateDirectory $linuxDirectory -Remove

    if ($Verbose) {
        godot --no-window --export "Linux/X11"
    } else {
        godot --no-window --export "Linux/X11" | Out-Null
    }

    Copy-Item $thirdPartyLicensesFile -Destination $linuxDirectory
}

if ($MacOSX) {
    $macDirectory = Join-Path $buildsDirectory "mac_osx"
    CreateDirectory $macDirectory -Remove

    if ($Verbose) {
        godot --no-window --export "Mac OSX"
    } else {
        godot --no-window --export "Mac OSX" | Out-Null
    }

    Copy-Item $thirdPartyLicensesFile -Destination $macDirectory
}

if ($WindowsDesktop) {
    $windowsDirectory = Join-Path $buildsDirectory "windows_desktop"
    CreateDirectory $windowsDirectory -Remove

    if ($Verbose) {
        godot --no-window --export "Windows Desktop"
    } else {
        godot --no-window --export "Windows Desktop" | Out-Null
    }

    Copy-Item $thirdPartyLicensesFile -Destination $windowsDirectory
}
