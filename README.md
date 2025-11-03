# Progetto: Open‑World Fantasy (work in progress)

Descrizione
Questo è un progetto personale per sperimentare e imparare creando un gioco open‑world 3D fantasy in Unity. L'obiettivo è costruire un prototipo giocabile (MVP) e poi estenderlo.

Principi
- Iniziare piccolo e iterare.
- Separare contenuti e codice (Addressables, scene additive).
- Ottimizzare presto (profiling regolare).

Setup iniziale
1. Installare Unity LTS (consigliato 2022/2023 LTS).
2. Creare progetto URP.
3. Abilitare e importare:
   - Input System (Package Manager)
   - Cinemachine
   - Addressables
   - (Opzionale) Terrain Tools / Gaia
4. Inizializzare git, aggiungere .gitignore specifico per Unity e usare Git LFS per asset grandi.

Struttura suggerita cartelle (Assets/)
- Assets/Scripts
- Assets/Scenes
- Assets/Prefabs
- Assets/Art (modelli, texture)
- Assets/Audio
- Assets/Streaming (scene/chunk)
- Assets/Addressables

Roadmap (MVP)
- Player movement + camera
- Terrain + streaming semplice
- Raccogliere risorse + inventario
- 1 NPC + 1 quest
- Sistema salvataggi

Licenza
Il codice è sotto MIT License (vedi LICENSE). Per gli asset di terze parti, controllare le singole licenze e aggiungerle in THIRD‑PARTY‑LICENSES.md.

Contatti
Owner: dnl95-source