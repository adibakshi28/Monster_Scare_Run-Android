# Monster Scare Run

Monster Scare Run is a thrilling endless runner game, inspired from 'Jetpack Joyride' and designed for Android devices. Developed using the Unity engine, this repository includes all the necessary files and resources to build, run, and modify the game. Players control a monster, navigating through various environments while avoiding obstacles and collecting power-ups to achieve high scores. It also features and in-game shop for upgrades with in-game currency system.
It has various levels which are unlocked as you progress and complete previous level objectives.

## Table of Contents
- [Introduction](#introduction)
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Gameplay](#gameplay)
- [Controls](#controls)
- [Scripts Overview](#scripts-overview)
  - [GameController.cs](#gamecontrollercs)
  - [PlayerController.cs](#playercontrollercs)
  - [ObstacleController.cs](#obstaclecontrollercs)
  - [PowerUpController.cs](#powerupcontrollercs)
  - [UIController.cs](#uicontrollercs)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

## Introduction

Monster Scare Run is an exciting endless runner game where players control a monster, navigating through different environments, avoiding obstacles, and collecting power-ups. The game is designed with Unity, providing a seamless and engaging experience for Android users.

<div style="display: flex; justify-content: space-between;">
  <img src="Game%20Screenshot/MS4.png" alt="Game Screenshot 4" style="width: 32%;">
  <img src="Game%20Screenshot/MS1.png" alt="Game Screenshot 1" style="width: 32%;">
  <img src="Game%20Screenshot/MS2.png" alt="Game Screenshot 2" style="width: 32%;">
</div>

<div style="display: flex; justify-content: space-between;">
  <img src="Game%20Screenshot/MS3.png" alt="Game Screenshot 3" style="width: 47%;">
  <img src="Game%20Screenshot/MS5.png" alt="Game Screenshot 5" style="width: 47%;">
</div>

## Features

- **Endless Running:** Infinite gameplay with increasing difficulty.
- **High-Quality Graphics:** Stunning 2D graphics and animations.
- **Intuitive Controls:** Smooth touch controls for an enhanced gaming experience.
- **Power-Ups:** Collect power-ups to enhance your abilities.
- **Immersive Sound Effects:** High-quality sound effects to enhance the gaming experience.
- **Real-time Feedback:** Instant feedback on player performance.

## Installation

To set up and run the project locally, follow these steps:

### Prerequisites

- Unity Hub installed.
- Unity Editor (v5.4).
- Android SDK configured.

### Steps

1. Clone the repository:

    ```sh
    git clone https://github.com/adibakshi28/Monster_Scare_Run-Android.git
    ```

2. Open the project in Unity:
    - Open Unity Hub.
    - Click on "Add" and select the cloned project directory.
    - Open the project.

3. Configure Build Settings for Android:
    - Navigate to `File > Build Settings`.
    - Select Android and click on `Switch Platform`.
    - Adjust player settings, including package name and version.

4. Build the Project:
    - Connect your Android device or set up an emulator.
    - Click on `Build and Run` to generate the APK and install it on the device.

## Usage

After building the project, install the APK on your Android device. Launch the game and follow the on-screen instructions to start playing. Use touch controls to navigate through environments, avoid obstacles, and collect power-ups to achieve high scores.

## Project Structure

- **Assets:** Contains all game assets, including:
    - **Scenes:** Different menus and UI elements.
    - **Scripts:** C# scripts for game logic.
    - **Prefabs:** Pre-configured game objects.
    - **Animations:** Animation controllers and clips.
    - **Audio:** Sound effects and music files.
    - **UI:** User interface elements.
- **Packages:** Unity packages used in the project.
- **ProjectSettings:** Project settings including input, tags, layers, and build settings.
- **.gitignore:** Specifies files and directories to be ignored by Git.
- **LICENSE:** The license under which the project is distributed.
- **README.md:** This readme file.

## Gameplay

Players control a monster running endlessly through different environments. The game includes:

- **Endless Running:** Infinite gameplay with increasing difficulty.
- **Objectives:** Navigate through environments, avoid obstacles, and collect power-ups to achieve high scores.
- **Scoring:** Points are awarded based on the distance traveled and power-ups collected.
- **Feedback:** Real-time feedback to help players improve.

<div style="display: flex; justify-content: space-between;">
  <img src="Game%20Screenshot/MS7.png" alt="Game Screenshot 7" style="width: 32%;">
  <img src="Game%20Screenshot/MS8.png" alt="Game Screenshot 8" style="width: 32%;">
  <img src="Game%20Screenshot/MS6.png" alt="Game Screenshot 6" style="width: 32%;">
</div>

## Controls

- **Touch Controls:** Use touch inputs to navigate the monster and interact with the game environment.

## Scripts Overview

The `Assets/Scripts` directory contains essential C# scripts that drive the game's functionality. Here's a detailed overview:

### GameController.cs

Manages the overall game state, including game flow, starting and ending sessions, tracking player progress, and updating the UI with scores and other information.

### PlayerController.cs

Defines the behavior of the monster, including movement and interactions with the environment and obstacles.

### ObstacleController.cs

Controls the behavior of obstacles within the game, including movement and collision detection.

### PowerUpController.cs

Manages the spawning and behavior of power-ups within the game.

### UIController.cs

Manages the user interface, handling interactions with menus, buttons, and other UI elements.


## Contributing

Contributions are welcome and greatly appreciated. To contribute:

1. Fork the repository:
    - Click the "Fork" button at the top right of the repository page.

2. Create a feature branch:

    ```sh
    git checkout -b feature/AmazingFeature
    ```

3. Commit your changes:

    ```sh
    git commit -m 'Add some AmazingFeature'
    ```

4. Push to the branch:

    ```sh
    git push origin feature/AmazingFeature
    ```

5. Open a pull request:
    - Navigate to your forked repository.
    - Click on the "Pull Request" button and submit your changes for review.

## License

This project is licensed under the MIT License. See the LICENSE file for more information.

## Contact

For any inquiries or support, feel free to contact:

[Adibakshi28 - GitHub Profile](https://github.com/adibakshi28)

Project Link: [Monster Scare Run-Android](https://github.com/adibakshi28/Monster_Scare_Run-Android)
