# RitualNow

![Ritual Warehouse Title](https://github.com/terrehbyte/RitualNow/raw/master/.github/title.gif?raw=true)

This houses the code repository for Ritual Warehouse, a 2D drag n' drop game about
ploppin' toys and doodads wherever they belong. Items come on an overhead
conveyor belt and must be sorted or assembled into the correct portions before
being shipped off to who knows where.

# Repository Structure

```
README.md                   This file! :)
Assets/                     Special folder for assets for Unity project.
    Rituals/                Assets relating to the Ritual Warehouse game.
    Zenject/                A dependency injection framework for Unity3D projects.
ProjectSettings/            Special folder for settings for Unity project.
```

# Roadmap
- [ ] REDESIGN: Explore an "assembly-line"-esque concept...
  - Parts come down the line for the player to assemble
  - Player must assemble parts according to the blueprint assembled
  - Game could explore other roles of factory process, such as QA and packing...
- [ ] Add a property drawer for ease in adding ItemHandles
- [ ] Fix touch support for WebGL builds
- [x] Fix a bug where players can score points while the game is not in session
- [ ] Add a gravity well (right click? long touch?)
- [ ] Think about a way to let players grab toys buried under other toys
- [ ] Think about a way to get rid of completed boxes (and introduce new ones)?
- [ ] Add multiple levels

## Technical Debt
- [ ] Add support for default variables for UnityEvent func calls
- [ ] Add a proper Input Manager

# License

Copyright (c) Terry Nguyen 2016

See THIRDPARTY.md for credits or attributions for third party work.