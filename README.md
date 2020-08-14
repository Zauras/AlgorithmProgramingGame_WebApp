# AlgorithmProgramingGame_WebApp
Web app of programming game where users can try to implement chosen algorithm

Requirements:
    NodeJs 10 +
    .NET Core 3.1 +
    MongoDb 4.4 +

Frontend: React
	Most of the logic you will find in ClientApp/src/features and /api folders

Backend: .NET Core 3
	Layers: 
		Controllers - validation and api management
		Services - data mappings and computation
		Providers - data management

How to run it:
	1) INSTALL NODE MODULES: 
	    cd to ./AlgorithmProgramingGame_WebApp/ClientApp -> run in terminal "npm install" (or probably IDE you do it for you)

    2) FEED DATABASE:
        Setup mongoDb and run migrations cli, the commands you will find in ./AlgorithmProgramingGame_WebApp/Migrations
        Just run mongo in you terminal and copy commands in command line which will feed the database

    3) DOWNLOAD NUGET PACKAGES & RUN PROJECT:
	 With IDE: run .sln file with IDE (JetBrains Rider or Visual Studio) and build-run project
        OR
	 Through terminal:
	 		cd to ./AlgorithmProgramingGame_WebApp
	 		dotnet restore
	 		* before using App make sure you have mongoDb ready and database feed
	 		dotnet run

	 Both Frontend UI and Server API reachable through - https://localhost:5001/
	 
	 
If you want to test Task Submission Form you can find Fibonacci solution code in ./TestAssets
* If provided code gives correct answer you will get score
* If provided code gives wrong or bad formatted answer it will be register as failed score attempt which would lower your success rate
* If provided code with compilation errors or code which does not fulfill requirements it won't be register as submit attempt and you will see error in provided Console

In Score List users are sorted by succeeded attempts
	 
	
	 