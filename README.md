# Tic Tac Toe

## Project Overview

Browser-based Tic Tac Toe application built with Angular and .NET Web API.

## Tech Stack

* Angular
* TypeScript
* .NET 9 Web API
* Swagger
* In-Memory Storage

## Features

* Tic Tac Toe Board
* Two Player Mode
* Move History
* Undo Last Move
* Reset Game
* Scoreboard
* REST APIs
* Swagger Documentation

## Backend Setup

```bash
cd TicTacToe.Api
dotnet restore
dotnet run
```

Swagger:

http://localhost:5084/swagger

## Frontend Setup

```bash
cd tic-tac-toe-ui
npm install
ng serve
```

Application:

http://localhost:4200

## API Endpoints

POST /api/games

GET /api/games/{id}

POST /api/games/{id}/moves

POST /api/games/{id}/undo

POST /api/games/{id}/reset

GET /api/scoreboard

POST /api/scoreboard/reset

## Design Decisions

* Backend is source of truth.
* In-memory storage used for simplicity.
* Angular consumes REST APIs.

## Known Limitations

* UI styling is basic.
* Winning cell highlighting pending.
* Computer mode pending.

## Future Improvements

* Smarter AI opponent
* Persistent storage
* Responsive UI improvements
* Automated unit tests
