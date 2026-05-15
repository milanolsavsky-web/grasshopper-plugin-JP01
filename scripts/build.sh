#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(cd "$SCRIPT_DIR/.." && pwd)"
PROJECT="$ROOT_DIR/src/PluginName.csproj"
BUILD_DIR="$ROOT_DIR/build"

# Defaults
CONFIG="Release"
CLEAN=false

usage() {
    echo "Usage: build.sh [--debug|--release] [--clean]"
    echo ""
    echo "Options:"
    echo "  --debug     Build in Debug configuration"
    echo "  --release   Build in Release configuration (default)"
    echo "  --clean     Clean build output before building"
    exit 1
}

while [[ $# -gt 0 ]]; do
    case "$1" in
        --debug)   CONFIG="Debug"; shift ;;
        --release) CONFIG="Release"; shift ;;
        --clean)   CLEAN=true; shift ;;
        -h|--help) usage ;;
        *) echo "Unknown option: $1"; usage ;;
    esac
done

echo "=== Building PluginName ($CONFIG) ==="

if $CLEAN; then
    echo "Cleaning build output..."
    rm -rf "$BUILD_DIR"
    rm -rf "$ROOT_DIR/src/obj"
fi

# Restore dotnet tools (CSharpier)
echo "Restoring dotnet tools..."
dotnet tool restore --tool-manifest "$ROOT_DIR/.config/dotnet-tools.json"

# Restore NuGet packages
echo "Restoring NuGet packages..."
dotnet restore "$PROJECT"

# Build
echo "Building..."
dotnet build "$PROJECT" --configuration "$CONFIG" --no-restore

# Verify output
GHA_FILE="$BUILD_DIR/PluginName.gha"
if [[ -f "$GHA_FILE" ]]; then
    echo ""
    echo "Build succeeded: $GHA_FILE"
    ls -lh "$GHA_FILE"
else
    echo ""
    echo "ERROR: Expected output not found: $GHA_FILE"
    exit 1
fi
