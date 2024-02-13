# Hacker News API Test
Bamboo Card Hacker News API Test

## Description
This project is a .NET 8 web API application developed in Visual Studio 2022. 

## Hosting URL
- [Hacker News - Zubair's Portfolio Website](https://hackernews.zubairrana.com/swagger/index.html)

## Prerequisites For Local Testing and Code Review
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

## Getting Started
1. Clone this repository to your local machine.
2. Open the solution file (`HackerNews.sln`) in Visual Studio 2022.
3. Ensure that the correct .NET SDK version (8.0) is selected for the solution.
4. Build the solution by selecting `Build` > `Build Solution` from the Visual Studio menu.
5. Set the startup project by right-clicking the desired project in the Solution Explorer, then selecting `Set as Startup Project`.
6. Press `F5` or select `Debug` > `Start Debugging` to run the project.
7. Alternatively, you can run the project without debugging by pressing `Ctrl` + `F5` or selecting `Debug` > `Start Without Debugging`.

## Additional Notes
- As it is a basic test, I have not added exception handling because there were no details on what details should be returned to the customer if something is broken.
- To protect the Hacker News server I have used the MemoryCache for the best story details. Still, there are no details on how often the best stories ID list is updated so I have assumed it is constantly updated and decided not to use cache for always getting the latest IDs.
