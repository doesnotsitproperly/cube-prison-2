#!/usr/bin/env pwsh

$buildsDir = Join-Path $PSScriptRoot "builds"

# Linux/X11

$buildDir = Join-Path $buildsDir "linux_x11"
$executable = Join-Path $buildDir "CubePrison2.x86"
$log = Join-Path $buildDir "log.txt"

if (Test-Path $buildDir) {
    Remove-Item $buildDir -Recurse
}
New-Item $buildDir -ItemType "directory" | Out-Null

Write-Output "Creating `"Linux/X11`"               export at ${buildDir}"

godot --no-window --export "Linux/X11" $executable | Out-File $log

# Linux/X11 64 Bits

$buildDir = Join-Path $buildsDir "linux_x11_64_bits"
$executable = Join-Path $buildDir "CubePrison2.x86_64"
$log = Join-Path $buildDir "log.txt"

if (Test-Path $buildDir) {
    Remove-Item $buildDir -Recurse
}
New-Item $buildDir -ItemType "directory" | Out-Null

Write-Output "Creating `"Linux/X11 64 Bits`"       export at ${buildDir}"

godot --no-window --export "Linux/X11 64 Bits" $executable | Out-File $log

# Windows Desktop

$buildDir = Join-Path $buildsDir "windows_desktop"
$executable = Join-Path $buildDir "CubePrison2.exe"
$log = Join-Path $buildDir "log.txt"

if (Test-Path $buildDir) {
    Remove-Item $buildDir -Recurse
}
New-Item $buildDir -ItemType "directory" | Out-Null

Write-Output "Creating `"Windows Desktop`"         export at ${buildDir}"

godot --no-window --export "Windows Desktop" $executable | Out-File $log

# Windows Desktop 64 Bits

$buildDir = Join-Path $buildsDir "windows_desktop_64_bits"
$executable = Join-Path $buildDir "CubePrison2.exe"
$log = Join-Path $buildDir "log.txt"

if (Test-Path $buildDir) {
    Remove-Item $buildDir -Recurse
}
New-Item $buildDir -ItemType "directory" | Out-Null

Write-Output "Creating `"Windows Desktop 64 Bits`" export at ${buildDir}"

godot --no-window --export "Windows Desktop 64 Bits" $executable | Out-File $log
