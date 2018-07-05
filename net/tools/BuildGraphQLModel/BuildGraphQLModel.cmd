@echo off
msbuild BuildGraphQLModel.sln
bin\Debug\BuildGraphQLModel.exe -ns Sdl.Web.PublicContentApi.ContentModel -e http://localhost:8081 -o ..\..\src\Sdl.Web.PublicContentApi\ContentModel\ContentModel.cs