# core.mvc.ids
Identity Server, .Net Core and EF Core

To obtain the code, clone the solution on your local drive. 
This is a .Net Core 3.1 ASP.NET MVC application that makes use of the Identity Server 4.0 with custom (owner password) implementation. 
This mechanism allows us to manage our own authentication and claims retrieval 
The database is created code first in this particular application but it can be easily modified to use an existing database by simply using the database first EF.

To install the latest version, go to Package Manager Console in visual studio) 
PM> Install-Package Microsoft.EntityFrameworkCore.SqlServer

Before you run the solution, make sure you have SQL Server instance somewhere local or remote Edit the UserContext.cs to change the connection strings as per your Sql Server.
Also modify the OnModelCreating method as per your desire.

Then go to the Package Manager Console and type the following command to create the migration script
PM> Add-Migration InitialMigration 

Once migration is successful, run the following command to apply the migration onto a physical database and import data 
PM> Update-Database

Once everything is configured, run the application. A browser window should pop up. 
Run POSTMAN application (install if you dont have it !)
1) Select method POST
2) In the address url, type the address of the application (copy from the browser address bar) and at the end type connect/token
so that it looks something like https://localhost:{port}/connect/token
3) click Body and click x-www.form-urlencoded
Add the following KEY and VALUE pairs one by one
grant_type = password
client_id = resClient
client_secret = topsecret
username = [should be in the database]
password = [should be in the database]

The /connect/token endpoint is responsible for generating a token for this client for this session based on the provided information. 
It checks for the client identification data against its own configuration for the same client. It also checks for the scope information.
So for example in this case, we claim that we are authorised to access an API called myApi. The IDS will check for what the client believes 
to have claim for and then checks for the scope of that client in its own configuration set.
It then calls then ValidateAsync method on the custom validator, ResourceOwnerPasswordValidator which then calls the dbcontext method to authenticate.
Once authenticated, a token is generated which can then be used for the session on the client's side. This token is stored in client's machine in the session cookies. 
A session is also maintained at the server end (where the identity server application is hosted). 
The whole purpose of this token is to have the ablity to use the API, myApi which should be secured by Authorize attribute. Once the API received a request from the client
with the access token, it makes a call to the identity server to ask if this token is valid. The identity server validates the token and the API can respond to subsequent requests.  
This scenario is not a single sign on scenario but it is a very effective way to secure your APIs fully or partially depending on the desired usage. 

In a single sign on scenario, there would be multiple clients each having its own scopes (scopes / APIs can be shared across the clients). All the clients in a federation
are associated with the same identity. Consider there are 2 clients. An ASP.NET MVC form and a React page. Both clients need to be configured in the identity server. 
When a user hits either of the client's url, they can be redirected to a central login (or the login could be within the identity server). Lets say client 1 got redirected to the 
login page, the user logs in with its credentials. The client1 get an access token. The identity server knows that this access token is associated with both the clients, client1
and client2. Now when the client2 goes to its URL, there is no access token passed because it doesnt know about the login attempt from client1, however it passed its client id
and the secret to the identity server as part of the request. The identity server identifies the client and checks its secrets. The identity server rather than redirecting it to 
the login page, simply returns the same access token back to client2. Now both client1 and client2 have the same access token and gets the authority to call its scopes etc.
If client1 is scoped for Api1 only and client2 is scoped for Api2 only, when the request goes to access the APIs, the Api1 will ask the identity server that I have a request
for access from client1 with this token, the IDS validates it by checking the scope information for that client. If however client2 tries to access the api1, then the identity
server will not allow it because it can check the scopes for client2 and will figure out that client2 is not authorised for Api1.

Hope that makes sense !
