# Little Mighty One

This project is a 2D top-down RPG developed in Unity as part of the *Metode de Dezvoltare Software* course. It includes two interconnected scenes, multiple enemy types, a dynamic weapon and inventory system, player health mechanics, and smooth scene transitions.

---

## 🎮 Game Overview
- Two playable scenes: Scene1 (Blue Slime, Purple Mob), Scene2 (Ghost)
- Player movement with idle/run animation
- Weapon system with Sword (melee), Bow (projectile), Staff (laser)
- Active inventory & weapon switching
- Health bar + collectible hearts
- Collectible coins
- Scene transitions with fade effects and player repositioning
- Camera bounds via Cinemachine Confiner
- Visual feedback: glow effects, fade-out on enemy death

---

## ⚙️ Technologies Used
- Unity 2022.3.6f1
- C# with Visual Studio 2022
- Git + GitHub (branches, PRs, issues)
- ChatGPT (AI-assisted debugging and concept learning)

---

## ✅ Key Features Implemented
- Modular architecture using **Singleton pattern** for global managers (UI, camera, player, etc.)
- Scene persistence using `DontDestroyOnLoad()`
- Tilemap-based environment with collision setup
- Clean C# codebase with commented logic and naming conventions
- GitHub collaboration: 10+ branches, 20+ PRs, over 19 user stories

---

## 🧠 AI Use (Prompt Engineering)
We used **ChatGPT** to:
- Fix the black screen bug during scene transitions
- Learn and implement Singleton<T> correctly
- Improve animation logic and sprite state changes
- Understand colliders, triggers, prefab instantiation
- Master Unity features like Inspector, Tilemap, Cinemachine, and Material effects

> Example prompts:
> - "Why does DontDestroyOnLoad not work for a GameObject with a parent in Unity?"
> - "How to switch player animations based on movement in Unity?"
> - "How to instantiate projectiles and link Prefabs in Inspector?"

---

## 📊 UML Diagram
A detailed UML diagram is provided to reflect system design: player logic, inventory, enemy behaviors, weapon structure (interface & inheritance).

---

## 🧪 Automated Testing
Planned but not fully implemented.

---

## 📁 Repository Structure Highlights
- `Assets/Scripts`: Organized in folders by role (Player, Enemies, Management)
- `Scenes/`: Contains `Scene1` and `Scene2`
- `Prefabs/`: Contains enemies, weapons, coins, hearts
- `Materials/`: Used for torches and death effects

---

## 📌 Contributors
Tunaru Ioana Alexandra - 232
Andruță Andra Mihaela - 232
Jiglău Fabrizzio
Iordache Tudor Dimitrie

---

## 📽 Demo
A recorded gameplay demo (offline) is provided along with this repository.

