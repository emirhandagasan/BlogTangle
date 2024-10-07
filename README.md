# Blog Platform for Devolopers: BlogTangle

![Ekran görüntüsü 2024-10-07 211910](https://github.com/user-attachments/assets/0b3d1c68-8763-4d6f-b8d1-74a8fe29f984)

## Technologies
- Asp.Net Core 8.0
- MSSQL Server
- Html
- Bootstrap
- JavaScript

## Getting Started
1. Clone the Repository

```bash
  git fork https://github.com/emirhandagasan/BlogTangle.git
```
2. Add two Connection String on Application.json folder for ApplicationDbContext and AuthDbContext

```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=BlogTangle;Trusted_Connection=True;TrustServerCertificate=Yes",
  "AuthConnection": "Server=localhost\\SQLEXPRESS;Database=BlogTangleAuthDb;Trusted_Connection=True;TrustServerCertificate=Yes"
}
```
3. To create our databases, run these 2 commands in the Nuget Package Manager Console:

```bash
Update-Database -Context "ApplicationDbContext"
```

```bash
Update-Database -Context "AuthDbContext"
```

4. To Login as Super Admin
   
```bash
Username: superadmin@blogtangle.com
Password: Superadmin@123!
```

5. To Add Posts, you must have a Cloudinary account. Register for a [Cloudinary Account](https://cloudinary.com/users/register/free) (%100 free) and add Cloudname, ApiKey, and Api secret to appsettings.json.



# Images of the Application

## Admin Functionalities
![Ekran görüntüsü 2024-10-05 211214](https://github.com/user-attachments/assets/48616f50-77c7-4a2c-9aab-3f71be835f24)

## Add New Tag, Edit Tag and Show All Tags(Only Admins can access)
![Ekran görüntüsü 2024-10-05 211225](https://github.com/user-attachments/assets/5a25a93f-a8c8-4060-887e-ed6a59a1aa45)
![Ekran görüntüsü 2024-10-07 214247](https://github.com/user-attachments/assets/3041124a-e7c7-4898-ada5-07209854b261)
![Ekran görüntüsü 2024-10-05 211230](https://github.com/user-attachments/assets/141e3261-8885-4060-83b4-66f99b0fa030)

## Add New Post, Edit Post and Show All Posts(Only Admins can access)
![Ekran görüntüsü 2024-10-05 211236](https://github.com/user-attachments/assets/67fa5299-0a4f-45f0-b5ab-83da7b635256)
![Ekran görüntüsü 2024-10-07 212522](https://github.com/user-attachments/assets/7520803f-7bd8-4182-9f05-08cba4225ad3)
![Ekran görüntüsü 2024-10-05 211304](https://github.com/user-attachments/assets/08a2837e-ff51-49c5-9a29-51dc472a0b10)

## Register and Login Pages
![Ekran görüntüsü 2024-10-05 211337](https://github.com/user-attachments/assets/24b97115-c645-4b9a-b4b3-23809239ddac)
![Ekran görüntüsü 2024-10-07 214303](https://github.com/user-attachments/assets/df179f2b-02c6-4b2f-832e-e76c6acacbc1)

## Users Page
![Ekran görüntüsü 2024-10-05 211311](https://github.com/user-attachments/assets/ac80c24b-67f5-4a2f-b44f-845e18d69004)

## Create User as an Admin
![Ekran görüntüsü 2024-10-05 211325](https://github.com/user-attachments/assets/3a73856c-453e-4e83-bed8-d8d3ab340062)

## Comment Section
![Ekran görüntüsü 2024-10-05 213608](https://github.com/user-attachments/assets/c7671ab9-7f1b-4c63-97fd-c5fa0bd69649)

## A Post Example
![Ekran görüntüsü 2024-10-07 212547](https://github.com/user-attachments/assets/ad255a05-5cdb-4198-a438-b997c1636371)





