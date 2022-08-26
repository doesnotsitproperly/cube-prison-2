#!/usr/bin/env pwsh

$rootDir = (Get-Item $PSScriptRoot).Parent
$buildDir = Join-Path $rootDir "build"
$godotDir = Join-Path $rootDir "godot"
$godotZip = Join-Path $rootDir "godot.zip"

Invoke-WebRequest "https://github.com/godotengine/godot/releases/download/3.5-stable/Godot_v3.5-stable_mono_linux_headless_64.zip" -OutFile $godotZip

Expand-Archive $godotZip -DestinationPath $godotDir

New-Item $buildDir -ItemType "directory" | Out-Null

Copy-Item (Join-Path $PSScriptRoot "html5_export.cfg") -Destination (Join -Path $rootDir "export_presets.cfg")

./godot/Godot_v3.5-stable_mono_linux_headless_64/Godot_v3.5-stable_mono_linux_headless.64 --no-window --export "HTML5" (Join-Path $buildDir "index.html")
