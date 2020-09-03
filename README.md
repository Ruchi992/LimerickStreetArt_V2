

Readme 
Requirement: 

Step 1: Installed the MySQL server

Step 2: dot Net Core version 3: https://dotnet.microsoft.com/download

Step 3: GitHub version 2.2 : https://git-scm.com/downloads

Step 4: Clone with GitHub

Step 5: Install SQL script:

Step 6: Navigate to LimerickStreetArt/LimerickStreet.web

Open a command prompt build/run the program with : dotnet run

Setup to run the API in development mode:

-open the port in firewall for both inbound and outbound

-change the API url according to address of the PC at which the API is running

-Run the visual studio as administrator for the API to work on local network

App workflow:
login and registration is done by UserAccountController in API

At homepageStreetArtController gives a list of streetart data.

which is then rendered in list at homepage.

thumbnail is used in list of street arts in homepage..without thumbnail
 
list will become less responsive due to a large memory heap.

picture uploading is handled by StreetArtController in API

when the picture is uploaded with data ..API creates thumbnail of the pictures.

location is get when picture is taken from the app and user can also modify it by clicking map button

location has to be entered when picture is selected from the gallery
