# Important

This solution has three projects:
BookMgmtWebApi - web api for Book CRUD operations.
BookMgmtWebApp - MVC app which talks to the above Web API.
BookMgmt.Test - Wrote some tests for BookMgmtWebApi Project.

## Prerequirements
* .NET 8

## How To Run

* Open solution in Visual Studio 2022 with .NET 8 
* Configure multiple startup projects, with webapi being first and webapp being second.

            The way we do the above steps is right click solution explorer
            configure startup projects
            select multiple startup projects
            set both api and web app action to start.
            
* Run the application.
* The solution also has BooksApi.postman_collection.json, to run the only the api's if we want.