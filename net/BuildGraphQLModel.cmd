@echo off
msbuild sln\BuildGraphQLModel.sln
src\BuildGraphQLModel\bin\Debug\BuildGraphQLModel.exe -ns Sdl.Tridion.Api.Client.ContentModel -e http://localhost:8081/cd/api -o src\Sdl.Tridion.Api.Client\ContentModel\ContentModel.cs -f cs