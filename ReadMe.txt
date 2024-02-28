Movie Api

endpoint for getting movies: 
http://localhost:5070/api/Movie

Quick Filter Json Object, required for the body of the request
{
	"PageNumber": 1,
	"PageSize": 15,
	"NameQuery": "spider",
	"IsDescending": true,
	"OrderBy": "ReleaseDate"
}

only really need the following to see it working

{
	"PageNumber": 1,
	"PageSize": 15,
}

There is a docker file for this. Im using docker hub. This will not work unfortunatly but i wanted to include it anyway (issue with reading the file for seeding)
	- docker build -t yourname/movieapi .
	- docker run -p 8080:8080 yourname/movieapi
	
best to run this locally as its going to run in an inmemory db anyway

MovieApi.Tests
run dotnet test
	- this will run the unit tests (nunit + Nsubstitute) 
		- not entirly comporehensive due to time constraints