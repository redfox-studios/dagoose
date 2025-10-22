# DaGoose - Game Design Document

## Core Gameplay Loop

**Basic flow:**
Idle → (30% chance lay egg) → Walk → Eat → repeat

**Player actions:**
- Click eggs to collect money
- Click geese to hear honk sound
- Buy new geese with money
- Switch between worlds
- Buy upgrades (local per world)
- Buy global upgrades (apply to all worlds)

---

## World System

### 5 Worlds with separate currencies

**Plains World** (Green theme)
- Currency: Plains Gold
- Default unlocked
- Base earnings: 1x

**Desert World** (Yellow/Orange theme)
- Currency: Desert Gems
- Unlock: Earn 5,000 Plains Gold total
- Base earnings: 2x

**Snow World** (Blue/White theme)
- Currency: Snow Crystals
- Unlock: Earn 50,000 Desert Gems total
- Base earnings: 5x

**Lava World** (Red/Black theme)
- Currency: Lava Coins
- Unlock: Earn 500,000 Snow Crystals total
- Base earnings: 10x

**Void World** (Purple/Black theme)
- Currency: Void Essence
- Unlock: Earn 5,000,000 Lava Coins total + 50 global tokens
- Base earnings: 50x
- Special: Only 1 egg type, gives 10 mixed tokens
- Endgame world

---

## Egg System

### Rarity & Values per World

**Plains World:**
- Common (60%): Plain Egg - 2 Gold
- Uncommon (25%): Spotted Egg - 5 Gold
- Rare (10%): Green Gem Egg - 10 Gold
- Epic (4%): Emerald Egg - 20 Gold
- Legendary (1%): Golden Acorn Egg - 50 Gold + 1 Acorn Token

**Desert World:**
- Common (60%): Sand Egg - 4 Gems
- Uncommon (25%): Cactus Egg - 10 Gems
- Rare (10%): Amber Egg - 20 Gems
- Epic (4%): Topaz Egg - 40 Gems
- Legendary (1%): Golden Scarab Egg - 100 Gems + 1 Scarab Token

**Snow World:**
- Common (60%): Frost Egg - 8 Crystals
- Uncommon (25%): Icicle Egg - 20 Crystals
- Rare (10%): Sapphire Egg - 40 Crystals
- Epic (4%): Diamond Egg - 80 Crystals
- Legendary (1%): Golden Snowflake Egg - 200 Crystals + 1 Snowflake Token

**Lava World:**
- Common (60%): Obsidian Egg - 16 Coins
- Uncommon (25%): Magma Egg - 40 Coins
- Rare (10%): Ruby Egg - 80 Coins
- Epic (4%): Phoenix Egg - 160 Coins
- Legendary (1%): Golden Flame Egg - 400 Coins + 1 Flame Token

**Void World:**
- Void Egg (100%): 100 Essence + 10 random tokens (2 of each type)
- Special (5%): Cosmic Egg - 1,000 Essence + 50 tokens

---

## Token System (Global Currency)

**Token types:**
- Acorn Token (from Plains)
- Scarab Token (from Desert)
- Snowflake Token (from Snow)
- Flame Token (from Lava)

**Used for:** Global upgrades that work in ALL worlds

**How to earn:** Only from Legendary eggs (1% chance) in each world

---

## Upgrade System

### Local Upgrades (per world, bought with world currency)

**Magnet Upgrade:**
- Level 1: OFF
- Level 2: Small magnet - 100 currency
- Level 3: Medium magnet - 300 currency
- Level 4: Strong magnet - 800 currency

**Auto-Sell Eggs:**
- Level 1: Common eggs - 5,000 currency (30% tax)
- Level 2: Uncommon eggs - 15,000 currency (30% tax)
- Level 3: Rare eggs - 40,000 currency (30% tax)
- Level 4: Epic eggs - 100,000 currency (30% tax)
- Level 5: Legendary eggs - 250,000 currency (30% tax)

**Egg Spawn Rate:**
- Level 1: 30% chance (default)
- Level 2: 40% chance - 100 currency
- Level 3: 50% chance - 300 currency
- Level 4: 65% chance - 700 currency
- Level 5: 80% chance - 1,500 currency

**Goose Cost (per world):**
- Plains: 20 Gold
- Desert: 100 Gems
- Snow: 500 Crystals
- Lava: 2,500 Coins
- Void: 10,000 Essence

### Global Upgrades (bought with tokens, apply everywhere)

- +10% earnings in all worlds - 10 tokens (any type)
- +5% egg spawn rate everywhere - 15 tokens
- Unlock auto-collect in all worlds - 50 tokens
- +20% movement speed everywhere - 30 tokens
- Permanent 2x multiplier - 100 tokens (requires mix of all types)
- Reduce auto-sell tax to 20% - 75 tokens
- Reduce auto-sell tax to 10% - 150 tokens

---

## Technical Implementation

### Scripts Overview

**Goose.cs** - Goose AI (movement, eating, laying eggs)
**Egg.cs** - Egg click detection, value, token drops
**EatenGrass.cs** - Despawn after time
**GameManager.cs** - Money, goose spawning, save/load
**WorldManager.cs** - World switching, currency management
**UpgradeManager.cs** - Local & global upgrades
**UIManager.cs** - Display money, buttons, upgrade UI

### Key Features to Implement

**World System:**
- Each world = separate scene OR one scene with asset swapping
- Save/load currency for each world separately
- Track total earnings per world for unlock conditions

**Goose Behavior:**
- Same animations for all worlds
- Void world goose uses transparency/glitch shader (optional)
- Other worlds can use SpriteRenderer.color tint (optional)

**Eating Mechanic:**
- Generic "foraging" action
- EatenGrass prefab changes color based on world
- Goose cannot eat on EatenGrass (collision detection)

**Token System:**
- Track 4 token types separately
- Legendary eggs drop 1 token + normal currency
- Void eggs drop 10 random tokens (2 of each)
- Cosmic eggs (5% in Void) drop 50 tokens

**Upgrade System:**
- Local upgrades stored per world
- Global upgrades stored once, apply to all worlds
- Auto-sell has 30% tax by default (reducible via upgrades)

---

## Art Assets Required

### Per World (5 worlds):
- Tileset/background sprites
- 5 egg sprites for worlds 1-4 (Common, Uncommon, Rare, Epic, Legendary)
- 2 egg sprites for Void world (Void Egg, Cosmic Egg)
- EatenGrass sprite (can be color-swapped)

### Shared Assets:
- 1 Goose sprite (used in all worlds)
- UI elements (buttons, panels, icons)
- Sound effects (honk, egg collect, purchase)

### Simplification Option:
- Create 5 base egg shapes
- Recolor them for each world (saves time)
- Total: 5 base sprites × 5 colors = 25 egg variants

---

## Additional Features (Optional)

**Stats Screen:**
- Total eggs collected
- Total money earned
- Play time
- Total tokens collected

**Settings Menu:**
- Volume control
- Graphics quality
- Particle effects toggle

**Daily Quests:**
- "Collect 50 eggs in Desert" - reward: 100 Desert Gems
- "Buy 5 geese" - reward: 200 currency
- "Collect 10 Legendary eggs" - reward: 5 random tokens

**Offline Earnings:**
- Earn reduced currency while offline
- Popup on return: "You earned $XXX while away!"

**Save System:**
- Auto-save every 30 seconds
- Save currency per world
- Save upgrade levels
- Save token counts
- Save world unlock status

**Particles:**
- Egg collection effect
- Purchase confirmation effect
- World switch transition
- Void world ambient particles (stars/cosmic dust)

---

## Endgame

**Trigger Condition:**
- Reach 10,000,000 Void Essence
- Collect 100 total tokens (all types combined)

**Cutscene Sequence:**
1. Screen fades to black
2. Goose sprite floats upward slowly
3. Particle effects (stars/void particles)
4. Text appears:
   ```
   "The geese have transcended.
   No longer bound by eggs or worlds.
   They are... eternal."
   ```
5. Goose dissolves into stars
6. Fade to white
7. "Thank you for playing DaGoose!"
8. Two buttons: [Play Again] [Quit]

**Play Again:**
- Full game reset
- Return to Plains World with 0 currency, 0 geese, 0 tokens
- All upgrades reset
- Fresh start (no bonuses)

**Quit:**
- Close application

---

## Balancing Notes

**Early game (Plains):**
- Start with 0 Gold
- First goose costs 20 Gold (need ~10 eggs)
- First upgrade accessible after ~50 eggs

**Mid game (Desert/Snow):**
- Significantly higher earnings
- Local upgrades more expensive
- Global upgrades become accessible

**Late game (Lava/Void):**
- Focus shifts to token farming
- Global upgrades are primary goal
- Void world is final challenge
- Reaching endgame cutscene is victory condition

**Economy:**
- Each world roughly 2-5x more expensive than previous
- Auto-sell tax prevents full automation early
- Global upgrades require playing multiple worlds
