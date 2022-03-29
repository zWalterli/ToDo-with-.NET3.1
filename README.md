# To Do
To Do is a task management app.

Table of Contents
=================
<!--ts-->
   * [Technology](#technology)
   * [Installation](#installation)
   * [How to use](#how-to-use)
   * [About](#about)
<!--te-->

### Features

- [x] CRUD Todo
- [x] Autenticação via JWT
- [x] Test Unitary
- [ ] Test Integration

### Technology
The following tools were used in the construction of the project:

- [ASP.NET 3.1](https://dotnet.microsoft.com/en-us/download/dotnet/3.1)
- [C#](https://docs.microsoft.com/pt-br/dotnet/csharp/)
- [JWT](https://jwt.io/)
- [NuGet](https://www.nuget.org/)
- [Git](https://github.com/)

### Installation

## Clone this repository
    git clone https://github.com/zWalterli/ToDo-with-.NET3.1.git

## Navigate to the folder
    cd ToDo-with-.NET3.1/Todo.API/

## Run the migrations
    dotnet tool install --global dotnet-ef
    dotnet ef database update

## Note
    If an error occurred in this step, the connection string is probably not correct (for your environment).
    
## Run the API
    dotnet run

### How to use

To use the API resources, it will be necessary to make a request requesting the access token on the route API/User/login.

</br>
To register with the API, the route that should be used is API/User/register.

</br>
If the API/User/login route turns the status code 200, there will be a field in the response called "accessToken", and all requests within the API must have the value "Bearer" concatenated with the token for the Authorization field in the Header.

</br></br>
Here is an example of how to use this API:

### URL to use Swagger

    http://localhost:3000/swagger/index.html


### Accessing the API with ADM

This is a json to access the API as an administrator.

    {
        "email": "admin@admin.com",
        "password": "123456"
    }

### Accessing the API with normal user

This is a json to access the API as an normal user.

    {
        "email": "teste_um@teste_um.com",
        "password": "123456"
    }
### About
---

<a href="https://www.linkedin.com/in/walterli-valadares-j%C3%BAnior-39807a165/" target="_blank">
 <img style="border-radius: 50%;" src="https://avatars.githubusercontent.com/u/46723190?s=460&u=9e52942eb8201675f594e1b24eae0afa22f1aef3&v=4" width="200px;" alt=""/>
 <br />
 <sub><b>Walterli Valadares Junior</b></sub></a> <a href="https://www.linkedin.com/in/walterli-valadares-j%C3%BAnior-39807a165/" title="Linkdlin">🚀</a>


Feito com ❤️ por Walterli Valadares
<br />👋🏽 Entre em contato!
