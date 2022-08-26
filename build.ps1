#!/usr/bin/env pwsh

$buildDir = Join-Path $PSScriptRoot "build"

$templatesTpz = Join-Path $PSScriptRoot "templates.tpz"
$templatesDir = Join-Path $PSScriptRoot "templates"

$godotZip = Join-Path $PSScriptRoot "godot.zip"
$godotDir = Join-Path $PSScriptRoot "godot"

cd ~
Write-Output (Get-Location)

<#
# Get export templates

Invoke-WebRequest "https://github.com/godotengine/godot/releases/download/3.5-stable/Godot_v3.5-stable_mono_export_templates.tpz" -OutFile $templatesTpz
Expand-Archive $templatesTpz -DestinationPath $templatesDir

Copy-Item (Join-Path $templatesDir "templates" "webassembly_debug.zip") -Destination "/home/runner/.local/share/godot/templates/3.5.stable.mono/"
Copy-Item (Join-Path $templatesDir "templates" "webassembly_release.zip") -Destination "/home/runner/.local/share/godot/templates/3.5.stable.mono/"

# Build

Invoke-WebRequest "https://github.com/godotengine/godot/releases/download/3.5-stable/Godot_v3.5-stable_mono_linux_headless_64.zip" -OutFile $godotZip
Expand-Archive $godotZip -DestinationPath $godotDir

New-Item $buildDir -ItemType "directory" | Out-Null

Rename-Item (JOin-Path $PSScriptRoot "html5_export.cfg") -NewName "export_presets.cfg"

./godot/Godot_v3.5-stable_mono_linux_headless_64/Godot_v3.5-stable_mono_linux_headless.64 --no-window --export "HTML5" (Join-Path $buildDir "index.html")
#>
