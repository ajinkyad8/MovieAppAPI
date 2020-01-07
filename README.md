To preview the built project click <a href="http://themoviezone.herokuapp.com/">here</a>.

**Stack**


- .NET Core 2.2
- Angular 8

For the Angular code click <a href="https://github.com/ajinkyad8/MovieAppSPA">here</a>.


## Table of Contents

- [Introduction](#Introduction)
- [Requirements](#Requirements)
- [Steps](#Steps)
- [Flow](#Flow)

## Introduction
This is a simple web application which can be used to make entries for Movies, Artists and Movie Roles.

The application allows users to register, make new entries, edit them and add photos for Movies and Artists.

Moderation makes sure that only entires or edits approved by moderators are visible on the application.

Admins can do moderation as well as assign roles to users. Admins can also remove user roles.

Entity Framework Core is used for managing the database.

ASP.NET Identity is used for registration and authentication.

JWT Tokens are used for validating users.

Cloudinary is used for Photo Management.

Angular 8 was used for creating the client side single page application used in tandem with the .NET Core 2.2 Web API.

The built files of the Angular code are present in the wwwroot folder.



## Requirements

- <a href="https://dotnet.microsoft.com/download/dotnet-core/2.2">.NET Core 2.2 SDK </a>
- <a href="https://git-scm.com/downloads">Git (optional) </a>
- <a href="https://dev.mysql.com/downloads/mysql/">MySQL (optional)</a>

## Steps
### 1) Copy the project
In your machine go to the directory where you want to download the project.

Open <b>Git bash</b> in that directory (option available on right-click in Windows 10).

Run the following code

<i> Requires <a href="https://git-scm.com/downloads">Git</a> to be downloaded.</i>

`git clone https://github.com/ajinkyad8/MovieAppAPI.git`

<b>OR</b>

In the repository on Github click on <b>Clone or Download</b>, then <b>Download ZIP</b> and on downloading extract it in the directory of your choice.

### 2) Configure the Database.
You can choose to either go with MySQL or SQLite. If you are familiar with .NET and can configure the database yourself then feel free to choose the provider of your choice.
 #### SQLite
In <b>appsettings.json</b> in the following line
```shell
6: "DefaultConnection": "Data Source=$DATASOURCE"
```
Replace `$DATASOURCE` with the name you want the database to be created with.
Remove the next line (no. 7) from the file.

In <b>Startup.cs</b> uncomment the following line 
```shell
40: // services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
```
Remove the next line (no. 41) from the file.

From <b>MovieAppAPI.csproj</b> remove line no. 12.

 #### MySQL
 <i> <a href="https://dev.mysql.com/downloads/mysql/">MySQL server</a> needs to be installed and configured for this.</i>
 
 
In <b>appsettings.json</b> in the following line
```shell
7: "DefaultConnection": "Server = $SERVER; Database = $DATABASE; Uid = $UID; Pwd = $PWD"
```
Replace `$SERVER` with the name of your server, `$DATABASE` with the name you want the database to be created with,`$UID` with your Uid and `$PWD` with your password .

Remove the previous line (no. 6) from the file.

In <b>Startup.cs</b> uncomment the following line 
```shell
41: // services.AddDbContext<DataContext>(x => x.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
```
Remove the previous line (no. 40) from the file.

From <b>MovieAppAPI.csproj</b> remove line no. 9.

### 3) Create the database.
<b>After</b> you have configured the database of your choice, run the following commands.

<i>Make sure you are in the project folder while running them.</i>
```shell
dotnet restore
dotnet ef migrations add InitialMigration
dotnet ef database update
```

### 4) Set up your JWT secret key.
In <b>appsettings.json</b> in the following line
```shell
3:     "Token": "$TOKEN"
```
Replace `$TOKEN` with the key of your choice.

You can choose a random key from <a href="https://www.grc.com/passwords.htm">here</a>.

### 5) Add your cloudinary credentials.
This application makes use of <a href="https://cloudinary.com/">cloudinary</a> for storing images.

You can create a free account there unless you already have one.
In <b>apsettings.json</b>
```shell
16:    "CloudName": "$CLOUDNAME",
17:    "ApiKey": "$APIKEY",
18:    "ApiSecret": "$APISECRET"
```
Replace `$CLOUDNAME` with your cloud name, `$APIKEY` with your Api Key and `$APISECRET` with your Api Secret. All this information will be available in your home page once you login.

<i>You can run the application without cloudinary but it will throw errors everytime you attempt to upload a photo and will also hamper the complete experience of the application.</i>
### 6) Configure Admin and Moderator Credentials.
In <b>Helpers/Seed.cs</b> the following lines create an Admin and Moderator account when you run the app for the first time or with an empty database
```shell

 32:       var admin = new User { UserName = "Admin" };
 33:       var moderator = new User { UserName = "Moderator" };
 34:       var result = userManager.CreateAsync(admin, "Admin@123").Result;
 40:       result = userManager.CreateAsync(moderator, "Moderator@123").Result;
 ```
 If you want these accounts to have different credentials you can change them here.
 
 <i>The password will have to be of minimum 8 characters and require at least one lowercase letter, one uppercase letter, one number and one special character.
 </i>
 ### 7) Build the application
 Run the following command to build the application.
 
 <i>Again, make sure you are in the project folder while running them.</i>

 ```shell
 dotnet run
 ```
if the build is successfull you will see the following message on your terminal.
> Hosting environment: Development<br>
> Content root path: $PATH<br>
> Now listening on: http://localhost:5000<br>
> Application started. Press Ctrl+C to shut down.

Open <i>localhost:5000</i> on your browser to view the application.

### 8) Add some role types and get started.
Login as <b>Admin</b>. Click on the Admin option on the navbar. Select <b>Movie Role Types</b>, click on <b>Add</b> and add some role types to get started.

You can add <b>Actor</b> as a role type for starters. Add some movies and artists with their photos, add some movie roles and explore the admin and moderator panels to see all the features of the application.

## Flow

- There are 2 primary entities <b>Movies</b> and <b>Artists</b>.
- There are 3 roles for users- <b>User</b>, <b>Admin</b> and <b>Moderator</b>. Every registered user is assigned the <b>User</b> role by default. Only the <b>Admin</b> can modify the roles.
- Movies and Artists are related by <b>Role Types</b> (Actor, Director, etc)
- Role Types can only be modified by the admin from the <b>Admin</b> page.
- A user can add/edit and Artist from the <b>Artists</b> page and Movie from <b>Movies</b> page.
- Movie Roles can be added from the page of the movie by clicking on <b>Edit Movie</b>.
- <b>Photos</b> can also be added by clicking on <b>Edit Movie</b> from movie's page or <b>Edit Artist</b> from artist's page.
- Once a user adds/edits anything, a moderator or admin needs to approve it from <b>Moderator</b> page for the other users or anonymous users to be able to see the additions/edits.
- A user can make a <b>Delete Request</b> For a movie,artist, movie role or photo from the <b>Edit Movie</b> or <b>Edit Artist</b> pages.
- Only after a moderator or admin approves the request from the <b>Moderator</b> page is the entry deleted.
- Users can see their contributions by clicking on <b>Your Contributions</b> below the dropdown option on the top right corner after logging in.
- Users can directly delete their <b>Pending</b> add/edit requests as well without requiring any approval.
