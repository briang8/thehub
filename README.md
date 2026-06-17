# The Hub

## GCGO 

My grand challenge is access to quality tech education in Africa. The mission behind The Hub is to co-found a physical tech space where aspiring learners can walk in, connect with mentors, access devices like laptops, and develop real tech skills regardless of their background or resources.

## Problem Context

Most young people in Africa who want to break into tech face the same wall. They have the drive but no access. No devices, no guidance, no community. Online resources exist but they assume you already have a laptop and stable internet. Mentorship is either expensive or nonexistent. This means a lot of talent never gets developed simply because of where someone was born.

The Hub addresses this directly by creating a space where those barriers are removed. Learners walk in and get access to everything they need in one place. This simulation is a digital representation of that experience, designed to show what The Hub offers and why it matters.

## Simulation Overview

The Hub is a top-down 2D interactive simulation built in Unity. The player enters the hub as a new member and explores four zones, each representing a core offering of the real-world hub. At each zone they interact with a mentor NPC, answer a challenge question related to that zone, and unlock it. Completing all four zones triggers a welcome screen confirming the player as a full Hub member.

Target users are students, educators, and anyone curious about what a community tech hub looks like in practice.

Key interactions include walking to each zone, triggering zone highlights through raycasting, pressing E to interact with mentor NPCs, answering challenge questions, and watching a journey line draw across the hub as zones are unlocked.

## Unity Mechanics Implemented

### UI
The simulation uses multiple UI systems including a main menu with a mission statement panel, an in-game interact prompt that appears when the player enters a zone, a challenge panel with a question and three answer buttons, wrong answer feedback text that appears and disappears using a coroutine, and a win screen that appears when all four zones are completed.

### Scripting
7 custom scripts power the simulation. PlayerMovement handles input and physics movement. CameraFollow keeps the camera centered on the player. ZoneTrigger detects when the player enters a zone and listens for the interact input. ChallengeManager handles opening the correct challenge per zone, checking answers, and triggering feedback. ProgressTracker tracks which zones are complete and checks the win condition. JourneyLine manages the Line Renderer and adds new points as zones are unlocked. GameManager handles scene loading and restarting.

### Collision
Box Collider 2D components with Is Trigger enabled are placed on each zone. When the player enters a zone collider, the interact prompt appears. When the player exits, it disappears. Wall objects use solid Box Collider 2D components to block the player from walking through them.

### Raycasting
Physics2D.RaycastAll shoots from the player position in the direction of movement every fixed update frame. When the ray hits a zone object the zone's sprite color brightens slightly to give visual feedback that the player is facing it. Debug.DrawRay makes the ray visible in the Unity Scene view during play mode. The raycast correctly skips the player's own collider using a gameObject check on each hit.

### Line Renderer
A Line Renderer component sits on a dedicated JourneyLine object in the scene. It starts at the player spawn position. Each time the player completes a zone challenge, the zone's world position is added as a new point on the line. By the end of the game a path is drawn across the entire hub connecting every zone the player visited, visualising their journey through the space.

## Additional Features

### Player Animation
The player character uses a Unity Animator Controller with two states, Idle and Walk. The PlayerMovement script sets an isWalking boolean parameter each frame based on whether there is movement input. The Sprite Renderer flipX property is toggled to mirror the character when moving left.

### Particle System
A gold particle burst prefab spawns at the zone's world position each time the player answers a challenge correctly. The particle system is configured with a single burst emission of 30 particles, a lifetime of one second, and Stop Action set to Destroy so it cleans itself up automatically.

### Coroutine-based Feedback
When the player selects a wrong answer, a coroutine runs that turns the selected button red, displays a "Wrong answer! Try again." message in the challenge panel, waits 1.5 seconds using WaitForSecondsRealtime so it works independently of Time.timeScale, then resets the button color and clears the feedback text.

## Build Information

WebGL deployment link: https://play.unity.com/en/games/de693c4b-4163-41fd-86ab-d0a04c3f425e/thehub

Android APK link: https://drive.google.com/drive/u/1/folders/1rV9Czy5Rfeb1_oy1zJuqmGFtAKipTVf4

### Running the WebGL Build
Open the link in a browser. The game loads directly in the browser window. Use WASD to move and E to interact with zones.

### Running the Android Build
Download the APK file and install it on your Android device. You may need to enable installation from unknown sources in your device settings. Use the on-screen joystick to move and the interact button to trigger zone challenges.

### Running in Unity Editor
Open the project in Unity 6. Open the MainMenu scene from Assets/Scenes. Press Play. To test the full flow open MainMenu first so scene loading works correctly. Both scenes must be added to Build Settings with MainMenu at index 0 and HubScene at index 1.

## Video Demo

https://youtu.be/9vESxjUTqR4