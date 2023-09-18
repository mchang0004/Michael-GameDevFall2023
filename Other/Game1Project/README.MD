# Game Project #1
## Stars of Sorellia
A 2D RPG Game

Current Version 1.0.08a (as of 9/18/23)

This game is created in Unity.

The game will have a dungeon that the player must explore and kill the boss. The boss will drop a Star which is the current end game for this version. 

### **Currently the game implements:**

Player
- 4 direction movement

Simple Combat System:
- Player's melee attack
- Enemy's melee attack
- Health for both player and nemy
- Short period of invulnerability after getting hit

Inventory System:
- Drag and drop inventory
- Q to drop items
- Swinging animation scripts which swings the melee weapon and changes the sprite of the equipped weapon.
- Slots for armor/accessories
- Healing consumble items

Enemies
- Simple following script (no pathfinding/navmap)
- Enemy drops items which can be collected

Game
- Lighting system using URP


### **Planned Additions:**

Diologue/Chat UI
- Need to freeze game and show a diologue screen
- Maybe have a section to select yes/no/other options of text
- Maybe have a sprite portrait of the character that is speaking

Pathing
- Implement a navmap for better enemy movement

Combat
- Add a ranged attack
- Add spells/abilities

UI
- Add an options/exit/extras menu
- Add a main menu screen

### Extra
- Add a saving/save script to save the game state
- Create a map area for the dungeon and starting area
- Add shops and currency
- Add chests that the player can put items into for storage