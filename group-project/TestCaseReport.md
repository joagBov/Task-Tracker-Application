TEST CASE DOCUMENT



Tester Name: - Navjot Singh Bhullar
Base URL: - https://localhost:7279/
Date: - Thursday, October 30, 2025



Test ID 1.4: Test Task Creation Functionality
Test ID	Test Case	Input	Expected Result	Actual Result	Status
1.4.1	Create task via UI - valid data	Title: "New Task", Description: "Test description", Assignee: "John", Priority: "High"	Task created successfully, redirect to task list, success message shown	Task created, redirected to list, success message displayed	✔ PASS
1.4.2	Create task via UI - empty title	Title: "", Description: "Test", Assignee: "John"	Form validation error, cannot submit, error message shown	Validation error displayed, submit disabled	✔ PASS
1.4.3	Create task via API - valid data	{"title": "API Task", "description": "API test", "priority": "Medium"}	201 Created + task object in response	201 Created, task returned with ID	✔ PASS
1.4.4	Create task via API - missing title	{"description": "No title task"}	400 Bad Request + error message	400 Bad Request, "Title is required"	✔ PASS

Test ID 2.4: Test View All Tasks Functionality
Test ID	Test Case	Input	Expected Result	Actual Result	Status
2.4.1	Display task list - with tasks	Navigate to /Tasks	Table shows all tasks with ID, Title, Assignee, Priority, Status, Created Date	All tasks displayed in table format	✔ PASS
2.4.2	Priority color coding	View task list	High=Red, Medium=Yellow, Low=Green, None=Gray	Colors displayed correctly for each priority	✔ PASS
2.4.3	Empty task list	Delete all tasks, view list	"No tasks available" message shown	Empty state message displayed	✔ PASS
2.4.4	Completed task styling	Mark task as completed	Completed tasks show strikethrough text	Strikethrough applied to completed tasks	✔ PASS

Test ID 3.4: Test Task Update Functionality
Test ID	Test Case	Input	Expected Result	Actual Result	Status
3.4.1	Edit task - form pre-population	Click Edit on task ID=1	Form shows current task data in all fields	All fields pre-populated with current data	✔ PASS
3.4.2	Update task via UI - valid data	Change title, description, priority	Task updated, redirect to list, success message	Task updated successfully	✔ PASS
3.4.3	Update task via UI - empty title	Clear title field, try to save	Validation error, cannot save	Validation prevents saving empty title	✔ PASS
3.4.4	Update task via API	PUT /api/tasks/1 with new data	200 OK + updated task	200 OK, task returned with updates	✔ PASS

Test ID 4.4: Test Task Deletion Functionality
Test ID	Test Case	Input	Expected Result	Actual Result	Status
4.4.1	Delete task - confirmation dialog	Click Delete on task	Confirmation page shows task details and warning	Details shown with "cannot be undone" warning	✔ PASS
4.4.2	Confirm deletion	Click Confirm Delete	Task removed, redirect to list, success message	Task deleted, removed from list	✔ PASS
4.4.3	Cancel deletion	Click Cancel on confirmation	Return to task list, task not deleted	Returned to list, task still exists	✔ PASS
4.4.4	Delete via API	DELETE /api/tasks/1	204 No Content	204 No Content returned	✔ PASS

Test ID 5.4: Test Task Details Functionality
Test ID	Test Case	Input	Expected Result	Actual Result	Status
5.4.1	View task details	Click Details on task	Card layout shows all task information	All fields displayed in card format	✔ PASS
5.4.2	Details page navigation	From details page	Edit, Delete, Back to List buttons available	All navigation buttons functional	✔ PASS
5.4.3	Details content	Check displayed data	ID, Title, Description, Assignee, Priority, Status, Dates shown	All information displayed correctly	✔ PASS
5.4.4	Priority color in details	View high priority task	Priority color matches list view	Red color for High priority displayed	✔ PASS

Test ID 6.4: Test ID Search Functionality
Test ID	Test Case	Input	Expected Result	Actual Result	Status
6.4.1	Search by ID - valid	Select "Search by ID", enter "1"	Single task with ID=1 displayed	Correct task returned	✔ PASS
6.4.2	Search by ID - invalid	Select "Search by ID", enter "999"	"Task not found" message	"Task not found" displayed	✔ PASS
6.4.3	Search by ID - non-numeric	Select "Search by ID", enter "abc"	Validation error for numeric input	"Please enter a numeric ID" message	✔ PASS
6.4.4	Clear search results	Click Clear Search	Return to full task list	All tasks displayed again	✔ PASS

Test ID 7.4: Test Assignee Search Functionality
Test ID	Test Case	Input	Expected Result	Actual Result	Status
7.4.1	Search by assignee - exact match	Select "Search by Assignee", enter "Navjot"	All tasks assigned to John Doe	Correct tasks returned	✔ PASS
7.4.2	Search by assignee - partial match	Select "Search by Assignee", enter "Navjot"	Tasks with assignee containing "John"	Partial matching working	✔ PASS
7.4.3	Search by assignee - case insensitive	Select "Search by Assignee", enter "Navjot"	Tasks with "John" (case insensitive)	Case insensitive search working	✔ PASS
7.4.4	Search by assignee - no results	Select "Search by Assignee", enter "Unknown"	"No tasks found" message	Empty results message shown	✔ PASS

Test ID 8.4: Test Assignee Assignment Functionality
Test ID	Test Case	Input	Expected Result	Actual Result	Status
8.4.1	Assign during task creation	Create task with assignee "Navjot"	Task created with assignee set	Assignee saved correctly	✔ PASS
8.4.2	Assign during task edit	Edit task, set assignee "Navjot"	Task updated with new assignee	Assignee updated successfully	✔ PASS
8.4.3	Unassigned task display	Create task without assignee	Shows "Unassigned" in list	"Unassigned" label displayed	✔ PASS
8.4.4	Assign via API	PUT /api/tasks/1/assignee with "Navjot"	200 OK + task with assignee	200 OK, assignee updated	✔ PASS

Test ID 9.4: Test Assignee Removal Functionality
Test ID	Test Case	Input	Expected Result	Actual Result	Status
9.4.1	Remove assignee via UI	Edit task, clear assignee field	Task saved without assignee	Assignee removed, shows "Unassigned"	✔ PASS
9.4.2	Remove assignee via API	DELETE /api/tasks/1/assignee	204 No Content	204 No Content returned	✔ PASS
9.4.3	Verify assignee removal	Check task after removal	Assignee field empty in details	Assignee field shows empty	✔ PASS
9.4.4	List view after removal	View task list	Shows "Unassigned" for the task	"Unassigned" displayed correctly	✔ PASS

Test ID 10.4: Test Priority Setting Functionality
Test ID	Test Case	Input	Expected Result	Actual Result	Status
10.4.1	Set priority during creation	Create task with Priority="High"	Task created with high priority	Priority saved correctly	✔ PASS
10.4.2	Priority dropdown options	Check create form dropdown	Options: High, Medium, Low, None	All options available in dropdown	✔ PASS
10.4.3	Priority color coding - High	Set priority to High	Red color in list view	Red background/text displayed	✔ PASS
10.4.4	No priority setting	Set priority to None	Gray text in list view	Gray color for no priority	✔ PASS

Test ID 11.4: Test Priority Update Functionality
Test ID	Test Case	Input	Expected Result	Actual Result	Status
11.4.1	Change priority via edit form	Edit task, change priority to Medium	Task updated with new priority	Priority updated successfully	✔ PASS
11.4.2	Priority change reflection	Check list after priority change	Color coding updates immediately	Color changes reflected in real-time	✔ PASS
11.4.3	Priority update via API	PUT /api/tasks/1/priority with "Low"	200 OK + task with new priority	200 OK, priority updated	✔ PASS
11.4.4	Invalid priority via API	PUT /api/tasks/1/priority with "Invalid"	400 Bad Request	400 Bad Request returned	✔ PASS

Test ID 12.4: Test Priority Removal Functionality
Test ID	Test Case	Input	Expected Result	Actual Result	Status
12.4.1	Remove priority via UI	Edit task, set priority to "None"	Task saved without priority	Priority removed, shows "None"	✔ PASS
12.4.2	Remove priority via API	DELETE /api/tasks/1/priority	204 No Content	204 No Content returned	✔ PASS
12.4.3	List view after priority removal	View task list	Shows "None" with gray color	"None" with gray styling displayed	✔ PASS
12.4.4	Details view after removal	Check task details	Priority shows "None"	"None" displayed in details page	✔ PASS

Test ID 13.4: Test Task API Endpoints
Test ID	Test Case	Input	Expected Result	Actual Result	Status
13.4.1	GET /api/tasks	No parameters	200 OK + array of all tasks	200 OK, all tasks returned	✔ PASS
13.4.2	GET /api/tasks/{id} - valid	id=1	200 OK + single task	200 OK, task details returned	✔ PASS
13.4.3	GET /api/tasks/{id} - invalid	id=999	404 Not Found	404 Not Found returned	✔ PASS
13.4.4	POST /api/tasks - validation	Empty title	400 Bad Request + error message	400 Bad Request, validation error	✔ PASS
13.4.5	PUT /api/tasks/{id} - update	id=1 with new data	200 OK / 204 No Content	204 No Content returned	✔ PASS
13.4.6	DELETE /api/tasks/{id}	id=1	204 No Content	204 No Content returned	✔ PASS

Test ID 14.4: Create Automated Tests for Assignee API
Test ID	Test Case	Input	Expected Result	Actual Result	Status
14.4.1	Unit test - assignee assignment	Mock PUT /api/tasks/1/assignee	Returns 200 OK with updated task	Unit test passes	✔ PASS
14.4.2	Unit test - assignee removal	Mock DELETE /api/tasks/1/assignee	Returns 204 No Content	Unit test passes	✔ PASS
14.4.3	Integration test - assignee flow	Complete assign/remove cycle	All operations succeed	Integration test passes	✔ PASS
14.4.4	Validation test - empty assignee	PUT with empty assignee	Returns 400 Bad Request	Validation test passes	✔ PASS

Test ID 15.4: Create Automated Tests for Priority API
Test ID	Test Case	Input	Expected Result	Actual Result	Status
15.4.1	Unit test - priority setting	Mock PUT /api/tasks/1/priority	Returns 200 OK with updated task	Unit test passes	✔ PASS
15.4.2	Unit test - priority removal	Mock DELETE /api/tasks/1/priority	Returns 204 No Content	Unit test passes	✔ PASS
15.4.3	Integration test - priority flow	Complete set/change/remove cycle	All operations succeed	Integration test passes	✔ PASS
15.4.4	Validation test - invalid priority	PUT with invalid priority	Returns 400 Bad Request	Validation test passes	✔ PASS

Test ID 16.4: Create Automated Tests for Search API
Test ID	Test Case	Input	Expected Result	Actual Result	Status
16.4.1	Unit test - ID search	Mock GET /api/tasks/search?id=1	Returns 200 OK with matching task	Unit test passes	✔ PASS
16.4.2	Unit test - assignee search	Mock GET /api/tasks/search?assignee=Navjot	Returns 200 OK with matching tasks	Unit test passes	✔ PASS
16.4.3	Integration test - search scenarios	Multiple search parameters	Returns correct results for each	Integration test passes	✔ PASS
16.4.4	Validation test - empty search	GET /api/tasks/search without params	Returns 400 Bad Request	Validation test passes	✔ PASS
