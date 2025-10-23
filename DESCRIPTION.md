Exercise: “Patrolling guard” (Unity -C#)

Duration: (1 hour) -> 24 hour Level: Intermediate





Requisites

• Unity (2021+)

• Language: C#

• Basic AI

• Navigation using NavMesh

• Player detection.





Objective:

In a 3D world setting, create a Guard enemy type that patrols between two points. Upon detecting the
player, the guard will stop patrolling and start pursuing the player. If the guard loses sight of the player,
it will resume patrolling after a 5 seconds.


Must have:



1. Create a small level with some obstacles.
• Use NavMesh, including mesh, obstacles, and offlinks



3. Create the Guard object.
   
• It uses NavMeshAgent to move.

• Its script, GuardAI.cs



5. Guard Behavior:
   
• The guard patrols in a loop from point A to point B.

• Player is detected using a trigger collider.

• If the player is out of sight, e.g. Is behind an obstacle, the guard will return to patrol behavior
after a 5-second wait.

Nice to Have (Bonus)
• Visual component: The guard changes color depending on its state (patrol, pursuit, attack).

• Design pattern: Implement a design pattern that communicates the state change between the
enemy and the player. This could be the Observer pattern, but it is not limited to that.

• Modular and scalable: Ensure that all components are modular and scalable.

• Second check on player detection: The guard makes a second check to ensure it only
pursues the player if the player is in front of it.

• Attack range: Define an attack range for the guard. If the player is within this range, the guard
must reduce the player's HP.

• UI: Implement a visible HP bar for the player.
