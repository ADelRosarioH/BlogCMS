# BlogCMS

## Information:
Name: Anthony Del Rosario
Email: adelrosarioh@gmail.com 

# Table of Contents
- [BlogCMS](#blogcms)
	- [Information:](#information)
- [Table of Contents](#table-of-contents)
- [Installation instructions](#installation-instructions)
- [How to Use](#how-to-use)
		- [Management Tools](#management-tools)
		- [Shutting down app](#shutting-down-app)
- [Technologies used](#technologies-used)
		- [Back-End](#back-end)
		- [Front-End](#front-end)
		- [Libraries](#libraries)
- [Project Architecture](#project-architecture)
		- [Overview](#overview)
- [Requirements](#requirements)
- [Bonuses](#bonuses)
- [Development](#development)
		- [Design Rules](#design-rules)
		- [Runtime and SDKs](#runtime-and-sdks)
		- [Dependencies](#dependencies)
		- [Front-End - WebApp](#front-end---webapp)
		- [Back-End - WebAPI](#back-end---webapi)

# Installation instructions

1. Install Docker (you can download docker from [here](https://docs.docker.com/get-docker/))
2. Download zip or clone this repository using git (you can download git from [here](https://git-scm.com/))
3. Open repository folder into terminal
4. Execute the `setup.sh` for creating an https certificate needed for the API 

```
> sh setup.sh
```
or if you are on windows
```sh
C:\> setup.cmd
```

5. Execute `docker-compose up -d` to start the services in detached mode 
	- or execute `docker-compose up` to see the logs

The installation process should take about 5 minutes depending on your internet connection and computer specs.

# How to Use

The BlogCMS UI is deployed in Azure and can be accessed using the following URL
- https://wonderful-plant-0202f4110.2.azurestaticapps.net
  
and the API is deployed at 
- https://adelrosarioh-blog-cms-zemoga.azurewebsites.net

You can use one of the following users or create your own:
- Public:
  - username: public_user
  - password: Test123*

- Writer:
  - username: writer_user
  - password: Test123*

- Editor:
  - username: editor_user
  - password: Test123*

This repo also includes a Postman collection with tests. You can download and install Postman from [here](https://www.postman.com/downloads/).

You can have multiple user sessions login in from a different browser window (not browser tab).

Make sure you are using one of the following supported browsers:
- Chrome 	latest
- Safari 	latest
- Firefox 	latest and extended support release (ESR) 
- Edge 		latest

### Management Tools 
To connect to MSSQL Server 2017 using SQL Management Studio or other tool use the following connection parameters:
- **Server**: localhost,1433
- **User**: SA
- **Password**: Password123*

### Shutting down app
You can shutdown all services at once executing `docker-compose down` or `Ctrl + C` in your terminal.

# Technologies used

### Back-End

- .NET 6
- ASP.NET Core 6 Minimal API
- NodeJS v19.4.0
- MSSQL Server 2019
- NGINX
- Docker

### Front-End

- Angular 15

### Libraries

- Bootstrap 5
- ngx-markdown
- Entity Framework Core
- xUnit
- Moq
- FluentAssertions
- AutoMapper

# Project Architecture

The project is separated in two application packages. The FrontEnd web application developed using the Angular Framework and The BackEnd ASP.NET Core RESTful API Web API.

The BackEnd solution structure is divided into the following projects:
- **BlogCMS.Infrastructure**: includes database entities configuraiton, interfaces, constants, helpers, and services that support the business logic.
- **BlogCMS.WebAPI**: fulfill frontend requests like user signup, signin, post listing and creation, reviews and comments.

The FrontEnd web application is dockerized and served by NGINX which is also used as a reverse proxy, forwarding requests to the Web API.

### Overview

Build a minimal Blog Engine / CMS backend API, that allows to create, edit and publish text-
based posts, with an approval flow where two different user types may interact.

# Requirements

- [x] Build a RESTful API for a blog engine using .NET.
The API should use some authentication mechanism to identify valid users and authorize actions
based on the user’s role.
 - The roles for the application are: “Public”, “Writer” and “Editor”.

- [x] Retrieve a list of all published posts (all roles)
- [x] Add comments to a published post (all roles)
- [x] Get own posts, create and edit posts (Writer)
  - [x] Writers should be able to create new posts and retrieve the posts they have
written.
  - [x] Writers should be able to edit existing posts that haven't been published or
submitted.
- [x] Submit posts (Writer)
  - [x] When a Writer submits a post, the post should move to a “pending approval”
status where it’s locked and cannot be updated.
- [x] Get, Approve or Reject pending posts (Editor)
  - [x] Editors should be able to query for “pending” posts and approve or reject their
publishing. Once an Editor approves a post, it will be published and visible to all
roles. If the post is rejected, it will be unlocked and editable again by Writers.
 - [x] Editor should be able to include a comment when rejecting a post, and this
comment should be visible to the Writer only.
- [x] Each post must include a title, some text content, the date of publishing and its author.

# Bonuses

- [x] Build a UI web application to interact with the API. The UI should, as a minimum, display a list
of all published posts, and the full contents (including comments) of any particular post when
selected.
- [x] Postman collections OR curl commands to test the API operations.
- [x] Swagger definition for the API.
- [x] Relevant Unit Tests covering the business logic in the API.
- [x] If you have experience and access to Cloud Platforms, deploy the solution to it so it can
be tested without local install. You can deploy to Azure/AWS/GCP, etc.
	- Deployed to Azure using Github Actions.
      - app: https://wonderful-plant-0202f4110.2.azurestaticapps.net
      - api: https://adelrosarioh-blog-cms-zemoga.azurewebsites.net

# Development 

### Design Rules
- [x] You can use the API framework of your choice (ASP.NET, Serverless Functions,
ServiceStack, etc.)
	- API was build using .NET 6 and ASP.NET Core WebAPI.
- [x] You can use the Storage solution of your choice (SQL database with EF or other ORM,
NoSQL data stores, flat files, etc.)
	- MSSQL with Entity Framework Core was used as storage solution.
- [x] You must use a Dependency Injection/IoC container of your choice.
  - The Dependency Injection/IoC container used was the default provided by .NET framework.
- [x] Login storage can be as simple as hardcoded users/passwords, but the api must use an
authentication mechanism to secure its operations.
  - For user management ASP.NET Identity Core was used.
- [x] All requests and responses must be in JSON format.

### Runtime and SDKs

- Download and install Docker (https://docs.docker.com/get-docker/)
- Download and install .NET 6 SDK (https://dotnet.microsoft.com/download)
- Download and install NodeJS (https://nodejs.org/en/download/)

### Dependencies
- Start MSSQL Server docker container:
	```sh
	docker run -d --hostname mssqldb --name mssqldb_dev -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Password123*" -p 14331:1433 mcr.microsoft.com/mssql/server
	```

### Front-End - WebApp
- Open this repository folder in the terminal and change directory to `BlogCMS.UI/BlogCMS`
- Install project dependencies by running command:
	```sh
	npm install
	```

- Start development server by running command:
	```sh
	ng serve
	```

- Go to http://localhost:4200/

### Back-End - WebAPI
- Open this repository folder in the terminal and change directory to `BlogCMS/BlogCMS.WebAPI`
- Install project dependencies by running command:
	```sh
	dotnet restore "BlogCMS.WebAPI.csproj"
	```

- Start project by running command:
	```sh
	dotnet run 
	```
- Web API server is running on http://localhost:7173
