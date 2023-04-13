# Customer Health Dashboard

FGCU Senior Project 2022 Sponsored By Testimonial Tree
Team Members:
- Alessandra Guerra
- Ethan Kramer
- Patricia Andreica

## Description
* Uses a C# backend REST API to query the company’s database, aggregate data through various analytical means, and project the results to a React front-end helping them pinpoint real live issues.
* Gives a high-level, summarized view of key user activities, while supporting the ability to view details on an account-by-account level to determine whether a client is healthy, borderline, or at risk for leaving the company.
* Provides a searchable, clickable list of accounts with key figures that determine that account’s health.
* Clicking on an account shows trends in the past year of activity including graphs depicting weekly testimonials sent vs. completed in the past year, average star rating, and average number of testimonials completed.

### Application Structure

The application has two separate parts: the frontend application (GUI) and the backend applicaiton (API). We are keeping them in the same repository for simplicity.

#### Frontend Appliction

The front end application located in [/app](/app) is a [React](https://reactjs.org/) application built with [Vite](https://vitejs.dev/). It uses the [MUI](https://mui.com/) component library for page layouts and standard components. 

#### Backend Application

The backend application is located at customer-health-dashboard\api\customer-health-dashboard-webapi\customer-health-dashboard-webapi.sln

## Screenshots

### Hompeage with Searchable, Clickable List of Accounts
![Screenshot 2023-04-12 162812](https://user-images.githubusercontent.com/62119661/231651864-b875d7c2-3c62-4527-9ce2-ccc85ac603bd.png)

### User Trend Page for WSBadcock
The trends in WSBadcock's user page indicate that the number of survey requests going out have gone from roughly 20k down to 2.5k in the last 3-4 weeks.
![badcock_fullpage](https://user-images.githubusercontent.com/62119661/231655261-1eef58c6-466c-4e95-88be-7b4ac968eaea.png)

### User Trend Page for Allentate
The trends in Allentate's user page indicate the start of an issue delivering data to the platform as well as when this issue was resolved.
![allentate_fullpage](https://user-images.githubusercontent.com/62119661/231655302-9eef2cab-d11b-4055-a7a3-8693f9bf5d0d.png)

