# **Car shop - Mechanic**

## Project Description
>### Car shop where you can register yourself and your vehicles :tractor: so you can depend on mechanics to find solutions :bulb: for your problem!

> User part => to use the  application of course you have to authenticate yourself and because this reason you should register your self by writing your email, username and password as usual.

> Vehicle part => as our registered and authenticated client you can register your vehicles by giving following information for a vehicle: 
Vehicle type(Car, Airplane..), Vehicle brand(Audi, Renault), Model(A3, Duster...) plate number, year even picture of it if you have  to do it.

>  Issue part => so you did registered and registered some tractors to our system now you can show to our mechanics the problem you have there gonna take description for  the issue so they can fix it as fast they could and you do not need us to call you for the issue status you  can always follow the status by our app.

![N|Solid](https://res.cloudinary.com/diihcd5cx/image/upload/v1642179470/Read-Me/car-shop-image_i8uktq.webp)

## Features - API

> ### API DOCUMENT USING SWAGGER!

> ### Accounts (used for registration and login mainly)
```sh
> GET: "/api/Accounts/login" => Logins/Authenticates user by taking information from body and setting identities with claims.
> POST: "/api/Accounts/register" => Registers user by taking information from body.
> PUT: "/api/Accounts/{id}" => Used to edit user profile information by selected arguments.
> GET: "/api/Accounts/logout" => Used to logout user.
> GET: "/api/Accounts/verification" => used to get pending user email and verification code to accept him as our client/user.
```

> ### Users (used to work with users)
```sh
> GET: "/api/Users/{id}" => Returns user selected by id.
> DELETE: "/api/Users/{id}" => Deletes user selected by id if it exists.
> GET: "/api/Users" => Returns all users.
> PATCH: "/api/Users/block/{id}" => Blocks user selected by id.
> PATCH: "api/Users/unblock/{id}" => Unblocks user selected by id.
> GET: "/api/Users/filter" => Filters users by selected criterias.
> GET: "/api/Users/sortby" => Orders users by selected criterias.
> DELETE: "/api/Users/role" => Deletes users role selected by taking user and role id as arguments.
> PATCH: "/api/Users/role" => Adds selected role to the user.
```

> ### Vehicles(used to work with vehicles)
```sh
> GET: "/api/Vehicles" => Returns all vehicles.
> GET: "/api/Vehicles/{id}" => Returns vehicle selected by id if it exists.
> POST: "/api/Vehicles" => Creates new vehicle if the arguments are valid.
> PATCH: "/api/Vehicles/{id}" => Updates vehicle if given arguments are valid.
> DELETE: "/api/Vehicles/{id}" => Deletes vehicle if the given id exists.
> GET: "/api/Vehicles/filter" => Filters vehicles by selected criterias.
> GET: "/api/Vehicles/sortby" => Orders vehicles by selected criterias.
```

> ### Issues (used to work with issues)
```sh
> GET: "/api/Issues" => Returns all issues.
> GET: "/api/Issues" => Returns issue selected by id if it exists.
> POST: "/api/Issues" => Creates new issue if passed arguments are valid.
> PATCH: "/api/Issues/{id}" =>Changes issue status if given arguments are valid.
> PUT: "/api/Issues/{id}" => Updates issue if given arguments are valid.
> DELETE: "/api/Issues/{id}" => Deletes issue if it exists.
> GET: "/api/Issues/filter" => Filters issues by selected criterias.
> GET: "/api/Issues/sortby" => Orders issues by selected criterias.
```

> ### Vehicle Brand s(used to work with vehicle brands)
```sh
> GET: "/api/VehicleBrands" => Returns all vehicle brands.
> POST: "/api/VehicleBrands" => Creates new vehicle brand if the arguments are valid.
> PATCH: "/api/VehicleBrands/{id}" => Updates vehicle brand if given arguments are valid.
> DELETE: "/api/VehicleBrands/{id}" => Deletes vehicle brand if the given id exists.
> GET: "/api/VehicleBrands/sortby" => Orders vehicle brands by selected criterias.
```

> ### Vehicle Types (used to work with vehicle types)
```sh
> GET: "/api/VehicleTypes" => Returns all vehicle types.
> POST: "/api/VehicleTypes" => Creates new vehicle type if the arguments are valid.
> PATCH: "/api/VehicleTypes/{id}" => Updates vehicle type if given arguments are valid.
> DELETE: "/api/VehicleTypes/{id}" => Deletes vehicle type if the given id exists.
> GET: "/api/VehicleTypes/sortby" => Orders vehicle types by selected criterias.
```

> ### Issue Statuses (used to work with issue statuses)
```sh
> GET: "/api/IssueStatuses" => Returns all issue statuses.
> POST: "/api/IssueStatuses" => Creates new issue status if the arguments are valid.
> PUT: "/api/IssueStatuses/{id}" => Updates issue status if given arguments are valid.
> DELETE: "/api/IssueStatuses/{id}" => Deletes issue status if the given id exists.
> GET: "/api/IssueStatuses/sortby" => Orders issue statuses by selected criterias.
```
> ### Issue Priorities (used to work with issue priorities)
```sh
> GET: "/api/IssuePriorities" => Returns all issue priorities.
> POST: "/api/IssuePriorities" => Creates new issue priority if the arguments are valid.
> PUT: "/api/IssuePriorities/{id}" => Updates issue priority if given arguments are valid.
> DELETE: "/api/IssuePriorities/{id}" => Deletes issue priority if the given id exists.
> GET: "/api/IssuePriorities/sortby" => Orders issue priorities by selected criterias.
```

> ### Exception Logs (used to register server exceptions and work with them)
```sh
> GET: "/api/ExceptionLogs" => Returns all non-deleted/removed exceptions.
> DELETE: "/api/ExceptionLogs/{id}" => Deletes exception selected by id.
> PATCH: "/api/ExceptionLogs/{id}" => Changes selected exceptions status from non-checked to checked.
> GET: "/api/ExceptionLogs/filter" => Filters and sorts exception by selected criterias.
> GET: "/api/ExceptionLogs/sortby" => Sorts exceptions by selected criterias.
```
---
## Installation
> If you wish to clone the app follow the steps below
```sh
> Download the app from the repository
> Add the connection string to your database
> Update database
> Run the application. The database will be automatically seed demo data
```

## TO DO
> Extend API to be shop too and implement reports because it is not possible to block user without reason.

## Technologies used: 
 - ASP.NET Core
 - Microsoft Entity Framework Core
 - MSSQL
 - Moq
 - Swagger
 - SQLite InMemory
 - Cloudinary
 - MailKit
 - SendGrid

## Contacts

Contacts if you have any questions to ask:

| Developer | Email | LinkedIn |
| ------ | ------ | ------ |
| Ahmed Yusuf | ahhmed.usuf@gmail.com | https://www.linkedin.com/in/ahmed-yusuf-0a22b1200/ |
