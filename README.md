# Progetto: Open‑World Fantasy (work in progress)

## Descrizione
Questo è un progetto personale per sperimentare e imparare creando un gioco open‑world 3D fantasy in Unity. L'obiettivo è costruire un prototipo giocabile (MVP) e poi estenderlo in un mondo vivo, dinamico e conflittuale dove il bene e il male si scontrano ciclicamente.

## Tema e tonality
Un mondo fantasy ma non strettamente medievale: razze diverse (umani, elfi, orchi, nani, goblin, ecc.) coesistono con livelli diversi di tecnologia. Alcune regioni sono tecnologicamente avanzate (dirigibili, mezzi motorizzati) mentre altre rimangono più selvagge. I predoni e le fazioni ostili rappresentano una minaccia costante e la presenza di combattimenti e scontri è parte integrante del gameplay.

## Combattimenti
- Sistema di combattimento già impostato come prototipo: melee, ranged, proiettili, status effects, probabilità di bleed/dismember.
- Le statistiche base (Health, Stamina, AttackPower, Defense, ecc.) sono influenzate dalla razza e dal livello.
- MeleeWeapon, RangedWeapon e Projectile sono modellati come dati separati; il CombatSystem gestisce logica di danno e effetti.
- Obiettivo: evolvere il sistema (critici, hitboxes, animazioni, parry, combo) e integrare IA nemica con behavior tree/state machine.

## Fazioni, lore e mondo
- Esistono regni e 'kingdoms' che determinano la distribuzione delle specie e la tecnologia prevalente.
- Il mondo è conflittuale: alcune fazioni sono allineate al 'bene', altre al 'male', altre sono neutrali o mercenarie.
- I predoni sono anarchici e cercano ricchezza; altre specie hanno scopi propri (territorialità, sopravvivenza, conquista).
- I regni avranno strutture enterabili (città, fortezze, dungeon). Gli interni saranno gestiti con scene additive per ottimizzazione.

## Mappa, biomi e mondo "senza confini"
- La mappa sarà costruita con chunk/scene additive e streaming per permettere un mondo molto esteso senza perdita di precisione (floating origin).
- Biomi principali: coste e mare, pianure, foreste, montagne, deserti, paludi. Ogni bioma avrà regole di spawn e vegetazione.
- Le transizioni tra biomi saranno graduali (mask + noise) per realismo.

## Mezzi di trasporto
- Verranno implementati mezzi vari: cavalli (mounts), carrozze, barche (con gestione della galleggiabilità), dirigibili e mezzi motorizzati (furgoncini) per mescolare elementi tecnologici al fantasy.
- Ogni veicolo avrà dati parametrizzati (velocità, maneggevolezza, capacità) e possibili vincoli per specie.

## Dati e struttura del progetto
- Uso di ScriptableObjects per razze, armi, veicoli e biomi.
- Addressables per asset pesanti e streaming asincrono.
- Folder suggerite: Assets/Scripts, Assets/Scenes, Assets/Prefabs, Assets/Art, Assets/Audio, Assets/Streaming, Assets/Addressables.

## Roadmap (MVP)
1. Setup progetto URP, Input System, Cinemachine, Addressables.
2. Player controller + camera + combat prototype.
3. Chunk streaming + floating origin (prototipo A).
4. Biome generator + terrain prototyping (prototipo B).
5. Veicoli base (prototipo C): mount, boat, simple land vehicle.
6. NPC, quest e salvataggi.

## Licenza
Il codice è sotto MIT License (vedi LICENSE). Per gli asset di terze parti, controllare le singole licenze e aggiungerle in THIRD‑PARTY‑LICENSES.md.

## Contribuire
Questo è un progetto personale ma aperto a suggerimenti: se vuoi contribuire o descrivere razze e fazioni, inviami le descrizioni e le inserirò nella documentazione e nel design.