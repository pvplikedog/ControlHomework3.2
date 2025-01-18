This program was developed as a control homework assignment 3.2, variant 13 by Anton Anohin, BPI2311. It allows reading a json file with players and modifying it. The majority of the work was dedicated to file modification. Additionally, this program automatically saves the file if less than 15 seconds have passed since the last modification. The temporary file is stored next to the exe file.

No notification about autosaving is sent to the user (there is no mention of it in the assignment), because in my opinion, from a UX perspective, if the user wants to save the file, they will do so deliberately, rather than searching for a file that was saved at an unknown time. Since I believe that autosaving the temp file is rather for file recovery in case of unexpected termination or more in-depth work with the program, and it is useless for the average user. Also, bothering the user with messages about autosaving, which they did not request, is not worth it.

The solution includes two class libraries: one is responsible for the JSON file with players (according to variant 13), the other is used for the "smart" menu.

All my events allow null values, thanks to which it is possible to avoid warnings, and since in my scenario I always check that they are not null before starting the event, this does not create problems.

Also, in the assignment (in my individual variant 13), it is said that the event (updating game_points) should be called only when the number of points for the achievement changes, but since the player's level also participates in the calculation of game_points, I call the event there as well.

Also, the assignment does not mention sorting by nested objects, only by fields of the main objects, but I think sorting by the number of nested objects (in my case, achievements) is more than natural.

Why negative numbers can be entered: in my opinion, setting the minimum level/points to zero is incorrect, as it is possible, for example, to create anti-achievements that will decrease game_points, and negative levels can also be created, for example, for inappropriate behavior.

Additional features implemented in this project:
- Comprehensive menu, from each page of which you can reach any other page, with a return button on each page (everywhere except for data entry, which I think is logical).
- Beautiful output of the file contents (not in JSON style).
- Adding a new player/achievement, deletion.
- Fine (CONVENIENT!) tuning of each field.
- Ability to generate a random name/description/entire object.
- Saving the file (Seems obvious, but not mentioned in the assignment).
