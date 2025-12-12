## Major Challenges
### Camera and Movement
*Challenge:* We wanted a camera system similar to Don't Starve where the 2D sprites can be seen easily in a 3D world. This was challenging, especially when we tried using Cinemachine.
* *Fix*: We opted for the regular Unity camera, except we scripted it so that it follows the player at a certain angle, creating a better effect.
### Farming System
*Challenge:* Implementing a farming system on top of our current system proved to be a challenge. We wanted to use the garden beds to actually plant the seeds, but due to time constraints, this wasn't possible.
* *Fix:* Instead we opted to use the pre-existing placement system to have the player place the seeds in the world as a buildable object. And as a compromise to include the garden bed, we made it a pre-requisite in order to craft seeds.
