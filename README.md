# GalactaTEC
Project for Principles of modeling in engineering course (CE1105)

# Software: 

## Requirements

* The player may be able to move to any direction in within the screen
* The player must be able to shoot enemies and protect themselves
* The player must die if shot or hit by an enemy. And the, it starts the other player turn
* The player dies with 2 normal enemy shots or with one special shot. Each enemy has only one special shot by level
* Enemies must have several predefined movement and attack patterns. When they touch the bottom of the screen they bounce back, they don't disappear from the screen, except if they go up, right or left, then they could disappear and reappear
* The game must support at least 2 players initially.
* The player must register themselves and their data must be saved. Username, password, email and photo
* Each player must be able to customize their ship
* Each player must have 5 or 3 lifes. After lose a life, it the other player turn to play.
* When an enemy is shot it may die with sound and an animation. Also it incerases the players score by 200 points
* The game must have at least 3 levels with different enemies, music and shoot sounds.
* Each game level must have at least 20 enemies. And the movement pattern should be configurable before the game
* The player's score should never be decreased
* After an enemy shoots, the enemy waits until the other enemies to shoot one time before shoot again
* The game should have a retro/modern style
* At the beggining of the game, the player who starts playing is selected randomly
* At the end of the game wins the player with the higher score. It should be a ceremony or podium for first and second place
* The player can buy shields or weapons from the menu with its score
* The game must be able to pause
* The shield can stop 3 normal shots or 1 normal shot and 1 special shot
* The player may buy spasive shots, persuing shots and others but with only one use per level
* On each game, it can appear 5 bonus falling on the screen while being protected by enemies. There are 5 bonuses types
* The game must have a saved best scores
  
## User histories

* As a player I want to be able to move to any direction within the screen, shoot, and use special shots
* As player I must die if i got shot with 2 normal shots, hit by collision or shot with 1 special bullet
* As a player I want to gain 200 points for each killed enemy
* As the client I want several predefined patterns for the enemies and select this patterns from the menu before start the game
* As a player I may be able to play with at least another friend with our own controllers
* As a player I need to register/login with my username, password, email and photo and customize my ship
* As a client I want 3 levels with their own music and 20 enemies
* As a client I want the player who start playing to be selected randomly
* As a player I want a podium at the end of the game
* As a player I want to get randomly 5 bonuses what falls over the screen while protected by enemies
* As a Client I want to get saved the scores 

## Use Cases

# Hardware:

## Requirements

* The player must be able to play with an analogical joystick. One control for each player (initially 2 players)
* The controls must be able to move, shoot, use powers and pause. IT would be great if the controller allows vibration feedback


# Preguntas:

* Son 5 o 3 vidas? en el enunciado se contradice.
* Al empezar el turno del siguiente jugador, este empieza en el mismo nivel que estaba su compañero? o desde cero?
* Al comprar se gasta el score? O más bien el score solo desbloquea ciertos items segun se progresa?
* El juevo es local, LAN u onnline?
