#!/usr/bin/env pwsh

$buildDir = Join-Path $PSScriptRoot "build"
$godotDir = Join-Path $PSScriptRoot "godot"
$godotZip = Join-Path $PSScriptRoot "godot.zip"

Invoke-WebRequest "https://github.com/godotengine/godot/releases/download/3.5-stable/Godot_v3.5-stable_mono_linux_headless_64.zip" -OutFile $godotZip

Expand-Archive $godotZip -DestinationPath $godotDir

New-Item $buildDir -ItemType "directory" | Out-Null

Rename-Item (JOin-Path $PSScriptRoot "html5_export.cfg") -NewName "export_presets.cfg"

./godot/Godot_v3.5-stable_mono_linux_headless_64/Godot_v3.5-stable_mono_linux_headless.64 --no-window --export "HTML5" (Join-Path $buildDir "index.html")
