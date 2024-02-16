$root = (Get-Location).Path;
dotnet run --project ./min/min/min.csproj -c Release -- "$root"
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE;
}
