# DaGoose - Ako hra funguje

## Zakladny gameplay loop

1. **Husy sa pohybuju sami:**
   - Idle (nic nerobia) -> nahodna sanca zniesť vajce -> chodia -> jedia travu -> repeat
   
2. **Zber vajec:**
   - Kliknes na vajce = dostanes peniaze (2-50$ podla typu)
   - Typy: Default, Gold, Diamond, Ruby, Rainbow (gambling)

3. **Kupovanie husi:**
   - Za 20$ kupis novu hus = viac vajec = viac penazi

4. **Interakcia:**
   - Kliknutie na hus = honk sound
   - Podržať klik = drag and drop husi

## Skripty vysvetlene

**Goose.cs** - AI husi (pohyb, jedenie, vajcia)  
**Egg.cs** - klikanie na vajcia, hodnoty  
**EatenGrass.cs** - trava dorasta späť po case  
**GameManager.cs** - peniaze, kupovanie husi  
**UIManager.cs** - zobrazenie penazi, button na shop

---

# Napady na vylepsenie

## 1. Upgrade system (MUST-HAVE)

### A) Pickup Range Upgrade
- **Level 1:** 0.5m radius (default)
- **Level 2:** 1m radius (50$)
- **Level 3:** 2m radius (150$)
- **Level 4:** 3m radius (400$)
- **Level 5:** 5m radius (1000$)

### B) Magnet Upgrade
- Vajcia pomaly letia k tebe -> neviem ako to spravim kedze je to ta ista vec ako pickup range, takze to asi bude to iste
- **Level 1:** OFF
- **Level 2:** Slow magnet (100$)
- **Level 3:** Medium speed (300$)
- **Level 4:** Fast magnet (800$)

### C) Goose Cost Reduction
- **Level 1:** 20$ za hus
- **Level 2:** 18$ (cost: 200$)
- **Level 3:** 15$ (cost: 500$)
- **Level 4:** 12$ (cost: 1000$)
- **Level 5:** 10$ (cost: 2000$)

### D) Goose Movement Speed
- Rychlejsie pohyby = viac vajec za cas
- **Level 1-5:** +20% speed za level

### E) Egg Spawn Rate
- Zvysiť sancu na vajce po idle action
- **Level 1:** 30% chance
- **Level 2:** 40% (100$)
- **Level 3:** 50% (300$)
- **Level 4:** 65% (700$)
- **Level 5:** 80% (1500$)

## 2. World/Prestige system (VElKy GAME CHANGER)

**Ako funguje:**
- Mas viac svetov (World 1, 2, 3, 4...)
- Každy svet ma vyssi earning multiplier ALE aj vyssiu cenu husi

**Priklad:**
```
World 1: Goose 20$, vajcia 2-50$
World 2: Goose 100$, vajcia 10-250$ (5x multiplier)
World 3: Goose 500$, vajcia 50-1250$ (25x multiplier)
World 4: Goose 2500$, vajcia 250-6250$ (125x multiplier)
```

**Prestige mechanika:**
- Keď mas dosť penazi, môžes "prestigenuť" do vyssieho sveta
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

## 5. Skin system (Cosmetic)

**Normalne skiny:**
- Golden Goose (100$)
- Black Goose (150$)
- Pink Goose (200$)
- Robot Goose (500$)

**Rare skiny:**
- Rainbow Goose (2000$) - animated rainbow colors
- Ghost Goose (3000$) - semi-transparent
- Fire Goose (5000$) - particle effects

## 6. Mini-hry (Extra obsah)

### A) Goose Racing
- Vyber svoju hus
- Pretekaj s AI
- Vyhras = bonus peniaze

### B) Egg Hunt Event
- 1x denne spawn special "Golden Hour"
- 60 sekund, spawn sa 3x viac vajec
- Multiplier 2x na vsetky vajcia

### C) Daily Quests
- "Collect 50 eggs today" - reward: 100$
- "Buy 5 geese" - reward: 200$
- "Reach $5000" - reward: 500$

## 7. Social/Meta features

### Leaderboard
- Top 10 hracov podla penazi
- Top 10 podla celkovo zozbieranych vajec
- Weekly reset s odmenami

### Friend system
- Pozvi kamosa = dostanes bonus hus
- Môžes navstiviť farm kamosa

### Trading (advanced)
- Trade vajcia s inymi hracmi
- Auction house na rare vajcia

## 8. Quality of Life features

- **Settings menu:** Volume, Graphics quality, Particle effects ON/OFF
- **Stats screen:** Total eggs collected, Total money earned, Play time, atď
- **Save/Load system:** Cloud save cez steam `>:)`
- **Tutorial:** Kratky guide pre novych hracov
- **Sound effects:** Viac zvukov (egg collect, purchase, upgrade)
- **Music:** Chill background music (lofi vibes?)
- **Particles:** Pri zbere vajca, pri kupe husi, atď
