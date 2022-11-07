# FGCU Senior Project - Testimonial Tree

Testimonial Tree is sponsoring a 2022 FGCU Senior Project. Testimonial Tree is partnering with 
- Alessandra Guerra
- Ethan Kramer
- Patricia Andreica

## Customer Health Dashboard

[Customer Health Dashboard One-Pager](Customer Health Dashboard One-Pager.pdf)

### Application Structure

The application has two separate parts: the frontend application (GUI) and the backend applicaiton (API). We are keeping them in the same repository for simplicity.

#### Frontend Appliction

The front end application located in [/app](/app) is a [React](https://reactjs.org/) application built with [Vite](https://vitejs.dev/). It will use the [MUI](https://mui.com/) componet library for page layouts and standard components. 

To get startted and runt he application a little local environment setup is required

1. Install nodejs (LTS) https://nodejs.org/en/
2. Install yarn (globally)
    
    `npm install -g yarn`

3. clone this repository (https or ssh)
    
    `git clone https://gitlab.com/testimonialtree-group/fgcu-senior-project/customer-health-dashboard.git`

4. change directory to /app
    
    `cd customer-health-dashboard/app`

5. install depedencies
    
    `yarn install`

6. run the applicaiton
    
    `yarn dev`

#### Backend Applicaiton

The backend application is located at C:\src\fgcu-senior-project\customer-health-dashboard\api\customer-health-dashboard-webapi\customer-health-dashboard-webapi.sln

1.  Open using Visual Studio and select the "Solution View" not the "Folder View"

2.  Right click the project titled "CustomerHealthDashboardWebApi" and choose Mangage User Secrets

3.  Save a json file that looks like this as your local configuration source:
{
  "tt-connection-string": "Data Source=devdb.testimonialtree.com;Initial Catalog=TestimonialTree;User ID=fgcu;Password=mypasswordhere;Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=True",
  "tt-connection-timeout":  30
}

4.  Debug..Start Debugging to launch the application.  You should be able to browse the following path:

https://localhost:7107/api/v1/parentusers
