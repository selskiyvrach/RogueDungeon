
# Overview

### **Name**

Rogue dungeon (working title)

### **Genre**

Rogue-lite, dungeon crawler, action

### **Synopsis**

You die and face a creature that says it is Death itself.
To get your life back you need to win in an ancient card game - The Game Of Fate. 
The problem is that you do not have any good cards. 
To get them you will do some tasks for Death and other death realm inhabitants.

### Core loop
Player goes on a run. When he dies the run is over.  
During the run player might reach certain places or complete certain activities that will lead the story forward  


### Controls

#### Common

- inventory
- map

#### Combat

- attack
- heavy attack
- dodge left
- dodge right
- block
- consumable_1
- consumable_2
- consumable_3
- consumable_4

#### Exploration

- interact
- move forward
- turn left
- turn right
- turn around

# Glossary

# Constants

# style

# feel

# systems 
## Combat system
### Glossary
- frontline - the enemy (always one), that stands in the front row
- backline - enemy(ies) that stands in the second row


- player fights 1-3 enemies at a time
- one enemy is always a fronliner. only one enemy can be in a frontline at the same time
- extra enemies form the backline
- fronline can be attacked by player via any type of weapon, backline can be attacked by player only via ranged weapons
- frontline always uses full attack pattern, left backliner uses only left attack, right backliner only uses right attack
- if frontliner dies one of the backliner takes his place in the frontline
- enemy behaviour during the fight is defined in sequences called patterns. each pattern represents a series of actions
that enemy executes one after another
- an enemy may have multiple attack patterns and wait intervals. in that case each time it needs to pick the next pattern it takes a random one
- between action patterns enemy might be idle for some time to give player time to attack
- when player faces multiple enemies at once their patterns and waiting intervals change
    - 

# Mechanics

## On-hit effects
- attacks have status effects based on their damage type. the effect may be triggered with a certain probability on any hit
- Slash attacks can trigger bleed, blunt attacks can stun, and pierce attacks can deal organ damage.
- **Stun** stops any enemy activity for the duration of the stun
- **Bleed** deals max hp percent damage for duration of a few seconds
- **Organ damage** deals huge amount of damage 
- Enemies of different types will have resistances, immunities or vulnerabilities to certain attack effects
- Heavy attack has increased chance of applying the on-hit effect
## Dodge
## Block

## Potions 
## Poisons 
- poisons should be applied to a slash or pierce damage weapon 
- poisons will then have effect when on-hit effect of the weapon is triggered
- poisons may deal damage or amplify vulnerabilities or reduce resistances. but they cannot affect immunities
- another way of using poisons is throwing the whole bottle at the enemy. If there are multiple enemies 
you fight at the moment the poison effect will be applied to all of the enemies
## Acids
- acids are substances in bottles. to get the effect you should throw the bottle at the enemy
- acids deal damage and significantly reduce resistance to physical damage of any subtype
- this effect applies to both worn armor and bodily features of the target (e.g. hard skin or a shell)


***Create prototype here***

# Story
- there are multiple death - Noble, Cowardly, Unexpected, Peaceful, Martyrish
- player tries to unravel which way he dies
- cards from The Game Of Fate tell some part of that story

# parameters (balance values), better tweaked based on their feel in prototype
# design (sounds, levels, art, story draft)
# progression
- Eternal Library. Books and scrolls with the lore found during runs are put there and give permanent bonuses
- Card deck. Cards found during runs also stay with you even after death
- Altars Of Sacrifice. Allow to pledge some valuable items to get bonuses to the next run
- 

# ui