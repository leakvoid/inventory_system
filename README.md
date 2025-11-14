# inventory_system
Inventory system implementation with a few advanced features made with Unity.

Youtube link: https://www.youtube.com/watch?v=C5cvbBBZtSo

Implementation includes following features:
- drag & drop
- stacking and splitting items
- item removal
- item swap
- crafting with following recipes (shapeless): axe (3 wood, 2 sticks), pickaxe (5 sticks), hammer (4 iron, 1 stick), stick (2 wood)
- tooltips
- item counter
- fill empty slots / delete all buttons
- searing and filtering items
- selecting cathegories
- sorting

Architecture:
Implementation is located in folder Assets/Game/Scripts.
Inventory is controlled via inventoryManager. Crafting via craftingManager. Data and UI information about items is stored inside InventoryItem.
Items are created via ItemFactory and stored inside VirtualInventoryContainer as well as inside ItemSlots for UI representation.
Implemented using OOP with some composition, design patterns used: factory, singleton, observer.
