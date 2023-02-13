# homa_test
I added tools to the steps that could be simplified in the workflow :

- When a FBX is imported, the script "FBX_Preset_Apply.cs" applies the preset stored in the Resources folder

- If anyone wants to create a prefab from any FBX, they can go to the "Tools" tab and select "Create Prefab from selected FBX", it will create a ready-to-use prefab (I took already existing prefabs as reference for the components required)

- When a png texture is exported, a window will appear and ask if the person importing wants to set it as "Sprite (2D and UI)"; if yes it will, if not no settings will be changed.

- Once everything is imported, to add the new character(s) to the game, the person in charge can go to the "Tools" tab and select "Auto Add New Store Product". 
The script will read the spritesheet provided by the Game Designer and add the character(s) according to the infos (name & price), and will search & add the icon and prefab corresponding to the name(s).

The scripts that I created are in the "Editor Tools" folder and I added a simple spritesheet in the Resources folder as example

If you have any other question you can send me an email
