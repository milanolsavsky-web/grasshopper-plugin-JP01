param(
    [switch]$Debug,
    [switch]$Release,
    [switch]$Clean
)

$ErrorActionPreference = "Stop"

$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$RootDir = Split-Path -Parent $ScriptDir
$Project = Join-Path $RootDir "src\PluginName.csproj"
$BuildDir = Join-Path $RootDir "build"

$Config = "Release"
if ($Debug) { $Config = "Debug" }

Write-Host "=== Building PluginName ($Config) ==="

if ($Clean) {
    Write-Host "Cleaning build output..."
    if (Test-Path $BuildDir) { Remove-Item -Recurse -Force $BuildDir }
    $ObjDir = Join-Path $RootDir "src\obj"
    if (Test-Path $ObjDir) { Remove-Item -Recurse -Force $ObjDir }
}

Write-Host "Restoring dotnet tools..."
dotnet tool restore --tool-manifest (Join-Path $RootDir ".config\dotnet-tools.json")

Write-Host "Restoring NuGet packages..."
dotnet restore $Project

Write-Host "Building..."
dotnet build $Project --configuration $Config --no-restore

$GhaFile = Join-Path $BuildDir "PluginName.gha"
if (Test-Path $GhaFile) {
    Write-Host ""
    Write-Host "Build succeeded: $GhaFile"
    Get-Item $GhaFile | Format-Table Name, Length -AutoSize
} else {
    Write-Host ""
    Write-Host "ERROR: Expected output not found: $GhaFile" -ForegroundColor Red
    exit 1
}
