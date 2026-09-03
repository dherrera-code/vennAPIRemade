## Daniel Herrera
The purpose of this new repository is to redo my fullstack API based on what I've learned after the internship.
This will be created via a N-Tier API and code will be consistent.
SQL Queries will be recorded within a different Repository.

### Notes
Look into Install automapper function to map Entity to DTO and vice-versa! ✓ 

Look into Potential Bug where Account Created is changing when logging in.
    Look into having SQL generate the date rather than the Entity!

Controllers needed to be recreated:
Blob Controller                             (DONE)
Room Controller: With Entity and DTO        (DONE)
Room Member Controller                      (DONE)
Friend Controller                           (DONE)
Avalability Controller

When ReDeploying API to Azure's Web Services, Make sure to run
dotnet clean
dotnet restore
dotnet build 
dotnet publish
before deploying web app!

Create a look up table to hold Status for friend requests: Pending, Accepted, Deleted/Rejected! 

Researching CI/CD workflow to automatically deploy web app into Azure using Github Action workflow!