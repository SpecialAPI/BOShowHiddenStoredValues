# Show Hidden Stored Values
Displays some normally hidden enemy and character stored values:
 * Shows the abilities "forgotten" by Forgetful.
 * Shows the turn counter for Fleeting.
 * Shows the amount of extra abilities granted by Abomination.

# Config
You can individually enable or disable these displays, as well as change their colors, in the mod's config file:
```
{Brutal Orchestra directory}/BepInEx/config/SpecialAPI.ShowHiddenStoredValues.cfg
```
By default, all displays are enabled.

# Interactions with other mods
By default, this mod's Fleeting display overrides the fleeting display added by [Salt Enemies](https://steamcommunity.com/sharedfiles/filedetails/?id=3461630998). This can be disabled in the config file.

# Localization Support
This mod supports localization. The displays can be localized by adding values with these IDs to your localization's UI file:
```
ShowHiddenStoredValues_ForgottenAbilities
ShowHiddenStoredValues_Fleeting
ShowHiddenStoredValues_Abomination
```
The displays use the same format as basegame stored values.