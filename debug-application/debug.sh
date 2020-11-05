#!/bin/bash

CUR_DIR=$(pwd)
dotnet watch --project ..\src\OneScript\OneScriptWeb.csproj watch run --framework netcoreapp3.1 --ContentRoot $CUR_DIR
