## Use Cases

| 1.0 | Search for Venue |
| --- | --- |
| Summary | Users can search for venues before selecting seats get images of the view. |
| Actors | Client |
| Basic Course of Events | 1.	The user is prompted with a button on the landing page that allows them to start the process of finding their view. <br>2.	Once this button is clicked, the user is taken to a new page containing a search bar. <br>3.	The user inputs some text into the search bar and clicks a go button. <br>4.	The page is rendered to reflect a list of venues that match the input search from the user. |

| 2.0 | Getting a Seat View |
| --- | --- |
| Summary | Users can search for venues before selecting seats get images of the view. |
| Actors | Client |
| Basic Course of Events | 1.	On the venue search page, the user selects a venue from the list. <br>2.	The user is then taken to a new page, where an image of the venue’s layout is presented. <br>3.	Clicking on a seat in the layout, the system shows an image on the page of the view from that seat. |

| 3.0 | Login |
| --- | --- |
| Summary | Users can login to their account. |
| Actors | Owner |
| Basic Course of Events | 1.	The user is presented with a login button on the landing page. <br>2.	Once clicked, the user is taken to a new page that prompts for a username and password. <br>3.	The user can then enter in a valid username and password. <br>4.	The system validates the entered credentials, and upon verification the application displays the home page. |
| Alternative Courses | _Step 4_: The system finds an invalid username and password. <br> 1.	An error message is displayed on the page informing the user that the entered credentials are incorrect. |

| 3.1 | Create an Account |
| --- | --- |
| Summary | Users can create an account. |
| Actors | Client |
| Basic Course of Events | 1.	The user is prompted with a create account button on the landing page. <br>2.	Once clicked, a new page is rendered asking for the user’s first name, last name, company name, email, and password. <br>3.	The user fills in the required fields on the page. <br>4.	The system validates the email and password, and the application displays the home page. |
| Alternative Courses | _Step 4_: The system finds an invalid username and password. <br>1.	An error message is displayed on the page informing the user than an entered email or password is invalid. |


| 4.0 | Adding a Venue |
| --- | --- |
| Summary | Users can add a venue to their account. |
| Actors | Owner |
| Basic Course of Events | 1.	Once logged in, a user will be presented with an add button on the screen. <br>2.	Once the button is clicked, a new page is rendered prompting the user to fill in the required fields. <br>3.	A user enters the name of the venue, address. <br>4.	The user selects the upload button on the page, where another window opens, prompting the user to select an acceptable image file from their computer. <br>5.	Once the file is selected and approved, the system adds the uploaded picture to the page. <br>6.	A user then will select the save button to successfully add a new venue, and the page will return to its previous state. |
| Alternative Courses | _Step 4_: User decides to cancel the addition process. <br>1.	The user selects the cancel button on the page and the application returns to its previous state. |


| 5.0 | Deleting a Venue |
| --- | --- |
| Summary | Users can delete a venue from their library.  |
| Actors | Owner |
| Basic Course of Events | 1.	Once the user is logged in, they select a venue from the library. <br>2.	A new page is presented with all the information about the venue. <br>3.	The user then selects a delete button on the page. <br>4.	Once the button is clicked, the current venue is removed from the library. |


| 6.0 | Edit Venue Information |
| --- | --- |
| Summary | Users can modify the name and address of a venue. |
| Actors | Owner |
| Basic Course of Events | 1.	Once the user is logged in, they select a venue from the library. <br>2.	A new page is rendered showing the selected venue’s information. <br>3.	The user can select an edit button next to the name of the venue. <br>4.	Once clicked, the text on the page becomes editable for the user to change. <br>5.	When the user is done editing the text, a save button is clicked to solidify the changes and the page returns to its previous state.|
| Alternative Courses | _Step 3_: The user selects an edit button next to the address of the venue. <br>1.	Once clicked, the text on the pages becomes editable for the user to change. <br>2.	When the user is done editing the text, a save button is clicked to solidify the changes and the page returns to its previous state. |


| 7.0 | Edit Seat Image |
| --- | --- |
| Summary | Users can modify the pictures for a seat. |
| Actors | Owner |
| Basic Course of Events | 1.	Once the user is logged in, they select a venue from the library. <br>2.	A new page is rendered showing the selected venue’s information. <br>3.	The user can select an edit seats button, which when clicked takes the user to a new page. <br>4.	The new page displays a layout of the venue. <br>5.	When a user selects an individual seat, the system verifies the selection and presents a confirmation message. <br>6.	The user can select an upload image button. <br>7.	A new window opens prompting the user to choose an acceptable file from their computer to upload. <br>8.	Once the upload is complete, the system displays a confirmation message. |
