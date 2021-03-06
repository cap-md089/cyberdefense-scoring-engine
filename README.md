# Cyber Defense Scoring Engine

This provides the baseline of a scoring engine which allows for management of instances and teams as well as their individual scores.

## Project Goals

1. Easy to set up in a production environment
2. Capable of handling multiple competing teams at once
3. Support for both Linux clients using systemd and Windows clients, and using Docker for the server

### Project Non-Goals

1. Competition Integrity
	- If someone is capable of reverse engineering this code in the middle of a competition, then they probably can pass the challenges given to them already. This is designed to teach people who haven't even heard of Linux yet

## Project Architecture Overview

1. ClientCommon/ClientCommonLibraries - libraries and services that are meant to be run in a platform independent way
	1. ClientData - data, interface, and configuration management
	2. ClientInstallerUtilities - common utilities for installing to both Linux and Windows
	3. ClientModels - data structures used in order to communicate with the controller and manage client services
	4. ClientService - a background event loop which will continuously run predefined scripts and check for whether or not points should be awarded
	5. ClientWebInterface - a web interface to show the current progress as well as the README and setup interfaces
2. Clients (Windows/Linux) - provides concrete implementations for the different operating systems
	1. Constants - stores constants that are necessary for installation and execution, such as installation folder paths
	2. Client - the project that implements interfaces for ClientService, ClientWebInterface, and ClientData to allow for executing in platform dependent ways
	3. Installer - programs to generate binaries to install the entirety of the service and user interfaces as well as configuring the services
3. Server - acts as the program which controls all of the clients as well as the admin control panel
	1. EngineController - the admin panel for the competition, which allows for setting the actual tasks and penalties as well as administering systems and teams
	2. EngineControllerData - provides models and database connections for the server
	3. TcpWorkerService - acts as a server for the individual clients above

## Project Development

### Setup

The following programs are needed for development:

- Node.js 14
- .NET 5 SDK
- Either Visual Studio or Visual Studio Code

In the repository, run:

```
# Install dependencies
dotnet restore
# Install the dotnet-format utility to keep files formatted consistently
dotnet tool install -g dotnet-format
# Install the pre-commit hook
git config core.hooksPath hooks
```

Docker is also a useful utility for developing the server programs

### Building and running

To build and run a project and its dependencies, go into a project folder and run `dotnet build` or `dotnet run` to build or build and run respectively

## Production Packaging

### EngineController

To build a Docker image for the engine controller, from the project root run: `docker build -t enginecontroller -f Server/EngineController/Dockerfile .`
Note: Do not run this command using WSL 2 on an NTFS drive!

To run it, run `docker run -d -p 80:80 -p 3050:3050 enginecontroller`

Alternatively, you can use `docker run -d -p 80:80 -p 3050:3050 ghcr.io/cap-md089/cyberdefense-scoring-engine/enginecontroller:latest` to run the latest build from the master branch

### WindowsClient

First, publish the WindowsClient project using `dotnet publish -c Release` inside Windows/WindowsClient
Then, run `dotnet publish -c Release` inside Windows/WindowsInstaller

The resulting `WindowsInstaller` executable should be located in either `Windows/WindowsInstaller/bin/Release/net5.0/windows-x64/publish` or `Windows/WindowsInstaller/bin/Release/net5.0/publish`

This file is automatically built using GitHub actions, and can be accessed by going to 'Actions' -> 'Build Windows Installer' workflow, and then clicking on the latest build.

### LinuxClient

First, publish the LinuxClient project using `dotnet publish -c Release` inside Linux/LinuxClient
Then, run `dotnet publish -c Release` inside Linux/LinuxInstaller

The resulting `LinuxInstaller` executable should be located in either `Linux/LinuxInstaller/bin/Release/net5.0/linux-x64/publish` or `Linux/LinuxInstaller/bin/Release/net5.0/publish`

This file is automatically built using GitHub actions, and can be accessed by going to 'Actions' -> 'Build Linux Installer' workflow, and then clicking on the latest build.
