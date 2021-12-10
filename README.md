# Project Introduction

The goal of this project is to provide a fresh experience in the survival genre through an interesting gameplay mechanic that ties the player's moves to the intelligence of the AI opponent. The player has to survive 10 rounds in a row by hiding in closets and vents of the level or by defeating the robot in a battle. Every time the player wins a round (either by using a certain hiding spot to outmaneuver the robot or by defeating it with a particular weapon) next round the robot will be more capable of countering that strategy. This forces the player to use different strategies in order to complete the game.

The AI has been implemented from scratch with Behavior Trees.

# Instructions

## Hiding

There are two types of hiding spots: closets and ventilation openings. The game implements a noise system that both betrays and aids the player. When not hidden, it is possible to not be noticed by the robot if moving silently (Ctrl + WASD/Arrow keys). When the robot spots the player, it'll start chasing them and the player must quickly hide in a hiding spot that has a low probability of being checked by the robot. Likewise, the robot itself will constantly emit sounds that will inform the player of how close the robot is to them. Since the level is divided in multiple rooms (separated by blue force fields) the noise will be the loudest if the player and robot are in the same room and less so if the robot is in the room next to the one the player is in. If no sound is heard that means that the robot is 2 or more rooms away and the nearest rooms are safe to explore.

The robot can only detect the player if they are in the same room. If the player is detected they can take advantage of the blue force fields by passing to the next room. Doing this will prevent the robot from seeing where exactly the player is until it enters the current room. During this short period the player will be able to quickly hide or pick up a weapon to protect themselves.

## Items

**Flamethrower** 

<img src="https://i.postimg.cc/rpT2wXX4/image.png" width="400">

Deals continues damage while the left mouse button is pressed

**Shotgun**

<img src="https://i.postimg.cc/BvPpktwc/image.png" width="400">

Deals more damage than flamethrower but has a cooldown between rounds

**EMP**

<img src="https://i.postimg.cc/fb5W4Tty/image.png" width="200">

Disables the robot for a short period of time

**Slowdown disc**

<img src="https://i.postimg.cc/tTYs2VSp/image.png" width="200">

Reduces the speed of the robot for a short time

**Bomb**

<img src="https://i.postimg.cc/g2mr17SM/image.png" width="200">

Deals a large amount of damage when robot comes near it

**Heal**

<img src="https://i.postimg.cc/MTMGyJHG/image.png" width="200">

Heals 60 points

# Download game build

https://mega.nz/file/axgnlCYZ#PHnYDd9SOpnXPl9_XzCRTpiTQD5cLpvhZYqOpi5D_ts
