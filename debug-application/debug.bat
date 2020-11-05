@echo off
set curDir=%CD%
call opm install -l
dotnet watch --project ..\src\OneScript\OneScriptWeb.csproj watch run --framework netcoreapp3.1 --ContentRoot %curDir%
