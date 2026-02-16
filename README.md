Where I left : 



Issues with bindings. AI did a horrible job with unbinds+rebinds everywhere in GameContext, removing .crosscontext at some places,etc… Currently stuck trying to load the scene

Cleared Lighting data, it solves the simple issue of white color



LOTS of NULL binding issues have been solved (and created) , might fix later those unbinds+rebinds later, for now I have already issues with the user data loading. 

(GameContext.cs, etc...)

Issues with the game's parser. No matter what JSON I input, it seems to fail to parse things. Even with a correct json, the program ends up messing it up one way or another, this is very 

problematic since it fails to load the scene properly.

I should later find the correct way to load the ambient sound effects ( bees, etc... ) since they are not loaded properly. 



IMPORTANT FOR THE SETUP !!! Install DirectX and correct Studio Nvidia drivers. 





To reset everything, remove all data here :

C:\\Users\\Pierr\\AppData\\LocalLow\\Electronic Arts

Unity logs can be found here : 

C:\\Users\\Pierr\\AppData\\Local\\Unity\\Editor\\Editor.log









--> NEXT ON THE LIST :

&nbsp;	- Enter the game

&nbsp;	- Find how to load zero DLC and only local assets (already done ? ) 

&nbsp;	- Fix shaders

&nbsp;	- Fix parsing issues. 

&nbsp;	- Make the server ACTUALLY store playerdata

&nbsp;	





I sadly Don't have a list of all the files I modified to get to this point. A diff with the untouched project in C:\\Users\\Pierr\\Documents\\Projects\\Decompiles\\Minions\_Paradise\\SANDBOX\\FRANKENSTEIN-UNITYPROJECT should reveal the différences with a Virgin project and the things I did to get to this stage. 

