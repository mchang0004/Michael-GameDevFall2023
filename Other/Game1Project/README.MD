This save was created on ##/##/20##:


1.0.01a
- This contains basic inventory + hotbar
- Combat, Movement, and Enemies Scripts
- Functional Melee Weapon
- Basic Map Area
1.0.02a
- It implements loot and changes the attackspeed, damage, and repeating swings onto the actual weapon
- It modifies the Enemy Controller script to allow for dropped loot items
- Changes layering of the swinging weapon based on direction
1.0.03a
- Added New Floor Tileset
- Edited Basic Map Area
- Fixed Test Button for Taking Damage
- Allowed consuming using left click
- Added filtering to inventory slots
- Added new slots for armor and accessories, but currently they only filter for armor
1.0.04a
- Handled Full Inventory and Still Collecting Loot
   - Detects when inventory is full and when there is a stackable item that can still be filled
- IDK SOMETHING ELSE
1.0.05a
- Added code to loot to randomy delay to prevent 2 items from entering at the same time when 1 slot is available.
- Added Health UI
- Collectable Items will be Green for Bug Testing
1.0.06a
- Added new Item Types and organized the inventory to accept those new item types in specific equipment slots. (Started in 1.0.03a, but its now finished)
- Modified the collection to be faster to avoid items trailing behind the player when walking/running
- Added new Animator for TargetDummy/PotionDummy and TesterGoblin to allow to future enemy attacks.
- FIXED (Bug): When inventory is full and inventory UI is open, dragging an item out allows for new item to be picked up, causing the inventory to be full. Then releasing dragged item, causes said item to bind onto the recently added item. (Solution, disable pickup for items when dragging)
- FIXED (Bug): Swapping weapons does not change the attack point position to new position for weapon. (Solution, created new getLastDirection function in playerMovement and simplified existing code in player update).
1.0.07a
- Updated Version of Unity to 2021.3.30f
- Enemy Attacking 
- Enabled animator code for Enemycontroller
- Uploaded to Github
1.0.08a



BUGS:
_The currently 'commented out' isChatting conditional interferes with attacking and movement, forcing it to always be on or off depending on the bool. When enabled, it makes it so you can attack when inventory is open & can move items when inventory is closed. (May need to rethink the chatting function)
_ (LOW PRIORITY) Can tap corner directions to have player sprite face a different direction than the swinging direction

TODO:


_Better Enemy Pathing
_Abilities/Ranged Attacks/AOE
_ART/Map Design
_UI/Game Triggers for Quests
_Dialogue/NPC UI
_QuestItems
_Implemented URP (Universal Render Pipeline) or other RP
