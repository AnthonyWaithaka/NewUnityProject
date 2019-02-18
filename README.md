# New Unity Project (3D)
Basic template for a new Unity3D game

#

## How to use:
1. Clone the repo to your desired directory OR download a zip file of the project
2. Open the Boot scene
3. Check that the build settings have both the [Boot](./Assets/Scenes/Boot.unity) scene (index 0) and the [SampleScene](./Assets/Scenes/SampleScene.unity) (index 1)
4. Hit the play button and try it out

ALTERNATIVELY: \
Import the included [Boilerplate](./Boilerplate.unitypackage) package into your existing project and add the [Boot](./Assets/Scenes/Boot.unity) scene to the build settings.

#

## Current Features:
#### GameManager GameObject
#### GameManager Script
- Keep track of what level the game is currently in
- Load and unload game levels
- Keep track of the game state
- Generate other persistent systems
- Enter and Exit pause mode
- Exit to Main Menu
- Exit to System

#### UIManager Prefab
- Main Menu
    *  Background
    *  Title Text
    *  Tagline_1 (animated)
    *  Tagline_2
- Pause Menu
    * Resume Button
    * Quit Button

#### UIManager Script
- Handle changes to game state
    * Display Main Menu
    * Display Pause Menu
- Start Game
- Exit To System

#### MainMenu Script
- Toggle Main Menu Fade in and Fade out animations

#### PauseMenu Script