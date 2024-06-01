<div align='center'>

<h1>Meeting Notes</h1>
<h3>Web app for easier 1v1 meeting management, made using ASP.NET Core MVC, C#</h3>

👋 Welcome to Meeting Notes! This project was initially created for educational purposes, during my internship at Valcon. 
After the internship has ended, I continued to refine and update it, to practice my skills in ASP.NET and C#. 
<br>
<br>

![image](https://github.com/ViktorijaSml/MeetingNotes/assets/73490593/f63bbf22-0abd-47a5-acb3-9773c4abf704)
</div>

# Table of Contents

- [About the Project](#about-the-project)
  - [Features](#features)
  - [Prerequisites](#prerequisites)
  - [Run Locally](#run-locally)
- [How to use](#how-to-use)

# About the project
This project is a web application designed to make managing Manager-Worker meetings easier. Additionally, it was made to teach basics of ASP.NET Core MVC, 
Linq library, and Razor Pages. It served as a practical guide under the mentor's supervision during an internship. However, after the internship has finished, 
the project remained incomplete. Motivated to learn more and enhance my skills, I took the initiative to complete the project. During that time i have used 
jQuery and enhanced my skills even more.

## Features
- **Worker and Manager Management:** maintain a comprehensive list of all workers and managers within the company.
- **Meeting Tracking:** Log details of meetings, including attendees, meeting notes and date when the meeting occured.
- **Meeting Summaries:** Keep important notes of each meeting for future reference.
- **User Authentication:** Secure login functionality to ensure data confidentiality and access control.

## Prerequisites

- Install `.NET 7.0 SDK` <a href="https://download.visualstudio.microsoft.com/download/pr/03507d55-fea4-40ed-bde7-2bb8904b614b/3582cdfc83133da5d330f3a80f6fb432/dotnet-sdk-7.0.409-win-x64.exe">Here</a>
- Install `Visual Studio 2022` <a href="https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Community&channel=Release&version=VS2022&source=VSLandingPage&cid=2030&passive=false">Here</a>

## Run Locally

- Clone the project

```bash
git clone https://github.com//ViktorijaSml/MeetingNotes.git
```

# How to use 
- Upon launching the app for the first time, it is required to login as admin to setup all the information needed.
  - Username: `me@example.com`
  - Password: `mypassword_ ?`
- Click on the `Workers` tab to open up the list of all workers
  - Add all workers with required information by clicking `Create New`
  - ❕ This section includes managers!
    
  ![image](https://github.com/ViktorijaSml/MeetingNotes/assets/73490593/873b7b0f-576e-400f-8574-545d3822ab59)
- After setting up all the workers, click the `Managers` tab
  - With `Create New` button, set up all existing managers
 
  ![image](https://github.com/ViktorijaSml/MeetingNotes/assets/73490593/3fd351a0-d063-4a4c-aafe-ed51ff213ef8)
- Every manager, worker or meeting has options to edit the information, see more details and delete from the list.
  
  ![image](https://github.com/ViktorijaSml/MeetingNotes/assets/73490593/44e06d19-7ce8-46ef-b931-e37ec8298abb)
- Finally, note down the 1v1 meetings of your choice in the `Meetings` tab
  
  ![image](https://github.com/ViktorijaSml/MeetingNotes/assets/73490593/e57e969e-99a9-4753-b90e-93a247983b77)  
- ❕When everything is set up properly, you can login as one of the workers or managers.  
