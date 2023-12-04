# Treasure-Hunt
3d Maze Treasure Hunt - First Person View
Play link: [Webgl](https://devlovex.itch.io/treasure-hunt)


## Game Objective
- Collect final Treasure Chest at the end of level.(diagonally opposite to spawn point)
- Avoid Traps(fire) which reduces health.
- Avoid Field of View of Enemies patrolling.
- For unlocking doors, needs to be unlocked by keys once. Keys are  with each enemy for each door.
- For collecting chest, need final treasure key.

## Implementation Details

  - Using a maze generation algorithm (Hunt & Kill) for generating procedurally generated maze of custom sizes in scene hierarchy. Using a custom editor window script for handling the random maze generation.
  - MVC for player controller, and for enemy controller.
  - Enemy uses a finite state machine using a state manager for transitioning between different states(idle, patrolling)
  - Interfaces for interacting with game objects(door,key and chest), for taking damage(from fire trap) and getting detected by enemies.
  - Scriptable objects used for storing data which is useful for spawning game objects with specific required values, and for persistent data like soundData.
  - Generic Singleton used in two scripts - SoundManager and LevelLoader
  - Using a modified Unity standard template for First Person Controller for current projects needs and making it more performant.
  - UI Screens for Start menu, Pause Menu and Game over and Game win(both identical)
