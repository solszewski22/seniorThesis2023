# Use Cases

## 1. Login
**Summary:** User logs into the application.

**Actors:** Client(s)

**Basic Course of Events:**
  1.	Landing page prompt user for a username.
  2.	User enters in a valid username.
  3.	Application verifies the username and logs the user in.

## 2.	Upload A Recipe
**Summary:** Users upload a recipe to the application to save or be used to auto-generate a grocery list of ingredients.

**Actors:** Client(s)

**Basic Course of Events:**
  1.	The homepage provides the user with a button to initiate the upload process.
  2.	Upon clicking the button, the user is prompted to upload an acceptable file of the recipe (PDF, TXT) from their computer.
  3.	After selecting the file, and approving the upload, a copy of the document is stored in the application, under “Saved Recipes”.

## 3. Create an Auto-Generated Grocery List
**Summary:** Users can select recipes from their saved library to create a list of ingredients organized by common grocery store food groups.

**Actors:** Client(s)

**Basic Course of Events:**
  1.	The user clicks a small button on the “Saved Recipes” page that allows them to select one or multiple recipes.
  2.	Once all recipes have been selected, another button is clicked by the user to initiate the consolidation and organization of ingredients into a single list.
  3.	Once the process is complete, a single list is generated for the user.

## 4.	Sharing the List
**Summary:** Users can send the auto-generated grocery list via email.

**Actors:** Client(s)

**Basic Course of Events:**		
  1.	Once the auto-generated list is made, a user selects a _share_ button.
  2.	A new page appears, allowing the user to enter a valid email address.
  3.	Then, clicking _send_ a pdf format of the list is sent to the entered email address.
     
**Alternative Courses:**

_Step 2:_ User wishes to print the list.
  1.	User selects a _print_ button that brings up a print window, allowing the pdf document to be printed.

## 5.	Modifying the List
**Summary:** Users can delete items from the list if they already have those ingredients on hand.

**Actors:** Client(s)

**Basic Course of Events:**
  1.	A user selects an _edit_ button when viewing the list in the application.
  2.	This brings the user to a new page, where the text on becomes editable.
  3.	The user can click on specific items to remove them from the list.
  4.	Once all items to be removed have been, a _save_ button on the page is clicked to save the current version of the list.

**Alternative Courses:**
	
 _Step 3:_ User wishes to add their own ingredients to the list.
  1.	The user can click at the bottom of the list to be presented with a cursor and type out their added ingredients. 
	
 _Step 4:_ User wishes to delete the entire list.
  1.	A _delete_ button on the page is clicked.
  2.	The user is prompted to verify discarding the entire draft.
  3.	After selecting “yes”, the list is deleted.

## 6.	Filter Recipes
**Summary:** User can organize saved recipes.

**Actors:** Client(s)

**Basic Course of Events:**
  1.	When viewing the “Saved Recipes” the user selects a _filter_ button.
  2.	A new window prompts the user for which type of filtration they would like to be applied.
  3.	The user selects “By Most Recently Added”.
  4.	The list of saved recipes is organized by their upload date.

**Alternative Courses:**
	
 _Step 3:_ The user selects “By Main Ingredient”.
  1.	The list of saved recipes is organized by the main meat ingredient in the dish (Fish, Pork, Beef, Chicken, Vegetarian, etc.).
	
 _Step 3:_ The user selects “By Alphabet”
  1.	The list of saved recipes is organized in alphabetical order.

## 7.	Search for Recipes
**Summary:** Users can search their “Saved Recipes” for specific dishes.

**Actors:** Client(s)

**Basic Course of Events:**
  1.	When viewing the “Saved Recipes” page the user selects the _search_ button.
  2.	A new window prompts the user to enter the name of a recipe.
  3.	Once finished, the user clicks the _search_ button.
  4.	A list of recipes that match the search criteria are displayed on the screen.

## 8.	Deleting a Recipe
**Summary:** If a recipe is no longer needed, users can delete recipes.

**Actors:** Client(s)

**Basic Course of Events:**
  1.	When viewing the “Save Recipes” page, a user can select a recipe.
  2.	A new page opens with the recipe, where the user can select a _delete_ button. 
  3.	Once clicked, the user is asked to approve the deletion.
  4.	Once “yes” is clicked, the recipe is deleted.
