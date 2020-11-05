@echo off
set curDir=%CD%

set OSLIB_LOADER_TRACE=1

call opm install -l
dotnet watch --project ..\src\OneScript\OneScriptWeb.csproj watch run --framework netcoreapp3.1 --ContentRoot %curDir%
