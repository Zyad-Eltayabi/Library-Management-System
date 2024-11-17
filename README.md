<h1 align="center" id="title">Library Management System</h1>

<h2>🚀 Overview</h2>

<p id="description">
Developed a robust Library Management System to streamline and automate library operations, offering comprehensive features for managing books, book copies, admins, users, authors, fines, borrowing and reservations while ensuring high usability and system efficiency.
</p>

<h2>⭐ Features</h2>

 📝 Here're some of the project's best features:

1. **Admin and User Management**  
   - User-friendly forms for creating, updating, deleting and managing admins and users.  
   - Validation and security features, including hashed password storage and admin verification for critical updates.
   - Features to filter admins by AdminID and FullName.  

2. **Book and Author Management**  
   - Manage books and authors, including adding, updating, and deleting records.  
   - Features to handle book images, filter by ISBN, title, or author, and dynamically display details.
   - feature to show full book details.  

3. **Book Copies Management**  
   - Add, update, and delete book copies to ensure accurate inventory tracking.  
   - Features to filter copies by CopyID, BookID,AuthorID, Title, or ISBN.  
   - Dynamic forms to manage book copy details and status, including availability statue for borrowing.
   - added borrow and reserve the book options in the form.

4. **Reservations and Borrowing**  
   - Create and manage book reservations and borrowing records, including validation checks for availability.  
   - Filtering options to efficiently search and manage reservation and borrowing history.  

5. **Dashboard**  
   - A dynamic dashboard displaying key metrics: total books, authors, users, admins, borrowing records, and reservations.  
   - Real-time data integration for instant library performance insights.  

6. **Fines and Settings Management**  
   - Fine tracking and payment management, with configurable settings for borrowing periods and default fine per day. 
   - Validation and error handling for seamless admin workflows.  


 
<h2> 📷 Project Screenshots:</h2>


<img src="https://github.com/user-attachments/assets/02001df9-cddd-48b0-ba4c-1eb4a058d987" alt="project-screenshot" width="100%" height="100%/">
<hr>
<img src="https://github.com/user-attachments/assets/4998a18f-d040-4e2a-86ab-e694543ac58b" alt="project-screenshot" width="100%" height="100%/">
<hr>


  <h2>🛠️ Installation Steps:</h2>

<p>
  1. Prerequisites 
  <li> sql server management studio.</li>
  <li> visual studio code </li>
  <li> .NET Framework 4.8 or later. </li>
</p>

<p>2. Clone the repository</p>

```
https://github.com/Zyad-Eltayabi/Library-Management-System.git
```
```
 cd "Presentation Tier"
```

<p>3. Restore LMS.bak file in your sql server management studio (you will find the file in Database folder)</p>

<p>4. in Presentation Tier Project, update App.config file with your sql server settings (user Id and password)</p>

 <p>5. click on "Presentation Tier.sln" file and build the solution </p>
 
<p>6. in login screen set ( user name : admin and password : 123456 ) </p>

  
<h2>🔷 Technologies used in the project</h2>

*   Architecture: 3-Tier Architecture 
*   Programming Language: C#
*   Database: MS SQL Server
*   Data Access: ADO.NET
*   User Interface: Windows Forms


