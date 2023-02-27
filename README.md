# Same game still but I tried adding a high score list

For week 3 I toyed around with save and load systems in Unity to display a highscore. The datafile wasn't updating, though. Then I realized the problem might be that my 'score' int doesn't actually freeze at any point, it keeps counting up with the timer. Only after adding a delay before loading the new scene did I notice this issue, I tried working with a timerActive boolean but couldn't fix it yet. 
As for the high score, I initially wanted to understand the code so I wrote our class code into this game, changing variables where necessary. What I want is a highscore screen that shows the high score per level, and potentially the name of the player. I don't need a list of high scores for at the end of a timer countdown which is what we did for class. My timer counts up; I want it to function like a stopwatch counting lap times and I want an overview of those lap times at the end of the game. Right now the score system isn't working at all, whoops. But at least I don't have error messages. Might have overcomplicated this. 
