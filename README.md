# Giao Hàng Bất Chấp (Deliver Anything)

A 3D ragdoll physics comedy delivery game for Windows PC, built by a small student team in Saigon.
You're a shipper for a fictional company. Customers order anything, from bubble tea to a live water buffalo to an entire building.

**Status:** pre-production. Free 15–20 min demo (VI + EN) planned for itch.io, target May 2027.

## Core loop
Take order → pick up item → pack it (physics minigame: box S/M/L/Container, shrink machine, tape) → drive with physics cargo → get rated + tipped → upgrade

## Tech
Unity 6 LTS, C#, URP, ScriptableObjects for item/order data, event bus between modules, Git LFS.

## How we use Devin
- Devin works only from GitHub issues labeled `devin`, posts a plan first, a human approves before code
- Max 2 open Devin PRs at a time
- CI (GameCI): EditMode + PlayMode tests + Windows build must pass, plus 1 human review
- Physics settings and ragdoll prefabs are protected by CODEOWNERS

## Roadmap
- [ ] Procedural order generator (items = attribute combos: size, mass, fragility, alive, guarded, hazard, anchored)
- [ ] Packing minigame + shrink machine (ragdoll → baked proxy rigidbody)
- [ ] Physics cargo vehicle
- [ ] PlayMode physics sanity tests (no NaN, no clipping, joints intact)
- [ ] CI build → itch.io via butler

All countries, leaders, buildings and companies in this game are fictional.
