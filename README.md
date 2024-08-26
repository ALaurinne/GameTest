# Game System Test Task

## Overview

For the test task, I developed a game system featuring a `GameManager`, `InventoryManager`, `UIInventory`, and associated scripts. The core system includes player interactions, item management, and basic game mechanics. A key focus was on utilizing `ScriptableObjects` to manage item properties and functionalities effectively.

## System Components

### GameManager
- Manages the overall game state, including player lives, inventory, and game audio.
- Handles life updates, game over scenarios, and transitions between game states.
- Controls the inventory UI visibility and player interactions with items.

### InventoryManager
- Manages item collection and UI updates.
- Supports drag-and-drop functionality for item organization.
- Provides methods for adding, removing, and using items.
- The drag-and-drop mechanics were challenging but essential for a cohesive user experience.

### UIInventory
- Provides the user interface for inventory management.
- Handles item display, drag-and-drop operations, and interactions with inventory slots.
- Updates the UI based on inventory changes and player actions.

### Item and UsableItem
- Defines the properties and effects of items within the game.
- `UsableItems` include specific effects like gaining life or boosting jump abilities, influencing game mechanics directly.

### PlayerInteractions
- Handles player collision and trigger events.
- Manages interactions with enemies and game-end triggers.

## Development Focus

During the test, my focus was on demonstrating my proficiency in UI design and animations. Creating the inventory system was a new challenge, and I was interested in learning about drag-and-drop functionality. Although I intended to implement a comprehensive audio manager, time constraints limited my progress.

## Personal Assessment

Overall, I am satisfied with the deliverables. Balancing multiple roles, such as game designer, artist, audio specialist, and developer, proved challenging. I wish I had more time to refine the system further, but I am pleased with the progress and the functional system I managed to create within the constraints.
