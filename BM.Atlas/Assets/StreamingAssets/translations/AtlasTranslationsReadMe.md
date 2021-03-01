### Atlas Translations

Assumed knowledge: Basic understanding of the json syntax.

The Atlas application will supports whichever languages have translation files in this folder.

The translations must have the name pattern “AtlasText_hebrew.json” (where ‘hebrew’ should be replaced with the name of the language).

## How to create a new translation file

1. Duplicate the "AtlasText_english.json". 
2. Replace the word "english" with the name of the new language.
3. Open the new translation file in a text editor.
4. Be sure not to change or remove any of the 'keys' in the file otherwise you will have missing translations when you run the application.
5. If the new language should be displayed from right to left (for example like Hebrew or Arabic), please change the value of "text_anchor" from "left" to "right".
6. Change the value of "display_language" from English to the name of the language you are inserting as it is known in that language (for example German is "Deutsch").
7. Carefully go through the rest of the file and translate the values for each item into the language you are adding.
8. Save the file.
9. Test your changes by running the application and switching into the language you have added. Test all possible menus to make sure there are no missing translations. Please note there is not currently the possibility to translate the disclaimer.
10. If you see any issues, such as missing translations, error messages or other bugs, please refer to the Troubleshooting section below.

## Troubleshooting

# If you see blank texts or the "No Translation Found" message. 

1. Please check your translation file and carefully compare it with the English translation file to see if there are any missing keys or values or mistyped keys.
2. Save the changes to your file and retry.

# If you see the red error message "The following JSON file was unable to be parsed.."

1. Your translation file may contain syntax errors. Open a JSON validator (for example https://jsonformatter.org/) and copy the contents of your file into the validator.
2. If the validator points out any errors in your translation file (missing or extra commas or brackets for example) please correct these.
3. Save the changes to your file and retry.

# Other issues

If you have any other issues or the application doesn't run please do the following:
1. Please check your translation file and carefully compare it with the English translation file to see if there are any missing keys or values or mistyped keys.
2. Check the syntax of your translation file using a JSON validator (see the instructions in the previous section).
3. Contact Jessica Palmer or Chloê Langford at the Charité Brain Simulation Unit.

## How to remove unwanted translations

1. Delete the unwanted translation file from this folder or move them to another folder on your computer. 

Please note that if you delete the English translation, the first language in alphabetical order will be chosen as the default language (for example, if the folder contained "AtlasText_arabic.json" and "AtlasText_german.json", Arabic will be the default language.) 



