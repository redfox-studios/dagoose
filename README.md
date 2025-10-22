# DaGoose - Ako hra funguje

## Zakladny gameplay loop

1. **Husy sa pohybuju sami:**
   - Idle (nic nerobia) -> nahodna sanca zniest vajce -> chodia -> jedia travu -> repeat
   
2. **Zber vajec:**
   - Kliknes na vajce = dostanes peniaze (2-50$ podla typu)
   - Typy: Default, Gold, Diamond, Ruby, Rainbow (gambling)

3. **Kupovanie husi:**
   - Za 20$ kupis novu hus = viac vajec = viac penazi

4. **Interakcia:**
   - Kliknutie na hus = honk sound
   - Podržat klik = drag and drop husi

## Skripty vysvetlene

**Goose.cs** - AI husi (pohyb, jedenie, vajcia)  
**Egg.cs** - klikanie na vajcia, hodnoty  
**EatenGrass.cs** - trava dorasta spat po case  
**GameManager.cs** - peniaze, kupovanie husi  
**UIManager.cs** - zobrazenie penazi, button na shop

---

# Napady na vylepsenie

## 1. Upgrade system (MUST-HAVE)

### B) Magnet Upgrade
- Vajcia pomaly letia k tebe (ku kurzoru) -> magnet si mozes togglovat
- **Level 1:** OFF
- **Level 2:** Small magnet (cost: 100$)
- **Level 3:** Medium speed (cost: 300$)
- **Level 4:** Strong magnet (cost: 800$)

### C) Auto sell eggs
- **Level 1:** default egg (cost: 1500$)
- **Level 2:** gold egg (cost: 2000$)
- **Level 3:** diamond egg (cost: 3000$)
- **Level 4:** ruby egg (cost: 4500$)
- **Level 5:** rainbow egg (cost: 6000$)

- Auto sell/collect bude mat niaky tax (napriklad 30%)
- Taktiez je drahy preto aby sa gameplay neminimalizoval hned na zaciatku.

### D) Egg Spawn Rate
- Zvysit sancu na spawn vajca
- **Level 1:** 30% chance
- **Level 2:** 40% (100$)
- **Level 3:** 50% (300$)
- **Level 4:** 65% (700$)
- **Level 5:** 80% (1500$)

## 2. World/Prestige system (VElKy GAME CHANGER)

**Ako funguje:**
- Mas viac svetov (World 1, 2, 3, 4...)
- Kazdy svet je v inej scene (tu momentalne rozmyslam ci kazdy svet by mal inu currency alebo ine veci... Taktiez rozmyslam ci kazdy svet bude mat vlastny egg type, tympadom by bol este 5 svet - rainbow world)
- Každy svet ma vyssi earning multiplier ALE aj vyssiu cenu husi

**Priklad:**
```
World 1: Goose 20$, 2x multiplier
World 2: Goose 100$, 5x multiplier
World 3: Goose 500$, 25x multiplier
World 4: Goose 2500$, 125x multiplier
```
- na toto bude asi treba fixnut to ze hus mozes draggovat IBA v jej svete, a nie do inych svetov. Po pripade dragging zrusit

**Prestige mechanika:**
- Keď mas dost penazi, môžes "prestigenut" do vyssieho sveta
- Stratis vsetko (peniaze, husy) ALE:
  - Keepnes upgrady
  - Zarabas ovela viac
  - Novy visual (ina farba husi, pozadie, trava)

**Special/Rare egg v každom svete:**
- 1% sanca na Golden Special Egg
- Da ti 10x viac ako Rainbow v tom svete
- Ma iny visual (blystiaci sa, animovany)

## 3. Pasivne zarabanie (Idle game mechanika)

### Auto-collect upgrade
- Vajcia sa zbieraju automaticky po X sekundach
- **Level 1:** OFF
- **Level 2:** Auto-collect po 10s (200$)
- **Level 3:** Auto-collect po 5s (600$)
- **Level 4:** Auto-collect po 2s (1500$)
- **Level 5:** Instant auto-collect (5000$)

### Offline earnings
- Keď nie si v hre, stale zarabas (menej ako online)
- Pri vrateni ti ukaže popup "You earned $XXX while away!"

## 4. Achievements system -> ak to dam na steam

Priklady:
- "First Egg" - zober prve vajce
- "Rich Goose" - ziskaj 1000$
- "Goose Army" - vlastni 10 husi naraz
- "Rainbow Collector" - zober 10 Rainbow vajec
- "World Traveler" - dosiahni World 3
- "Speed Demon" - kup max speed upgrade

**Reward za achievements:**
- Mala suma penazi (50-500$)
- Unlock specialnych skinov husi

## 7. Misc

- **Settings menu:** Volume, Graphics quality, Particle effects ON/OFF
- **Stats screen:** Total eggs collected, Total money earned, Play time, atď
- **Save/Load system:** Cloud save cez steam `>:)`
- **Tutorial:** Kratky guide pre novych hracov
- **Sound effects:** Viac zvukov (egg collect, purchase, upgrade)
- **Music:** Chill background music (lofi vibes?) -> Nah kamo, bude tam doom music
- **Particles:** Pri zbere vajca, pri kupe husi, atď

### Daily Quests
- "Collect 50 eggs today" - reward: 100$
- "Buy 5 geese" - reward: 200$
- "Reach $5000" - reward: 500$
