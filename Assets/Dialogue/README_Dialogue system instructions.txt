

In order to create a dialogue asset (please do this in the dialogue folder), right click in assets, go to Create / ScriptableObjects(at the top) / Dialogue Asset.

Currently, you are able to create lines by adding to the "Lines" array. Each line has a speaker and dialogue, make sure to fill both in for each line.

If you want the dialogue to change some variables, add a new entry into the variable arrayThen you must:
	- assign the character, 
	- the name of that character's variable you want to change (make sure this is exact, including capitalization), 
	- how you want it to modify the variable (if the variable you want to affect is a bool, set it to equal),
	- the value you want to modify the variable with (if the variable is a bool, 0 is false and 1 is true)

You can put dialogue into the call manager, located in the Switchboard scene -> Switchboard -> callManager. Select the day that this dialogue can happen in, which call it can happen in, then open 
connections and put it with the character you call for the dialogue