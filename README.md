# Abyssal Stationery

<details>
  <summary>Table of Contents</summary>
  <ul>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ul>
</details>

## About the Project

This is a full-stack B2C shopping app where you sell stationery items. I have built this with [Blazor Web Assembly](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor). This time, I have used [Tailwind](https://tailwindcss.com/) for styling.

## Built with

![Blazor](https://img.shields.io/badge/blazor-%235C2D91.svg?style=for-the-badge&logo=blazor&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4.svg?style=for-the-badge&logo=dotnet&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-black?style=for-the-badge&logo=JSON%20web%20tokens)
![SQL](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927.svg?style=for-the-badge&logo=Microsoft-SQL-Server&logoColor=white)
![TailwindCSS](https://img.shields.io/badge/tailwindcss-%2338B2AC.svg?style=for-the-badge&logo=tailwind-css&logoColor=white)

## Get Started

## Usage

Enter your connection string for SQL Database connection and your Cloudinary information for image upload in `appsettings.json` file.

```json
  "ConnectionStrings": {
    "Stationery": "database-connection-string",
  },
  "Cloudinary": {
    "CloudName": "your_cloudinary_cloudname",
    "ApiKey": "your_cloudinary_api-key",
    "ApiSecret": "your_cloudinary_api-secret"
  }
```
To able to add products you need to add categories first so, a seeded admin user needed. You can reconfigure settings below in the `AppDbContext.cs` on Data folder.

```cs
		var hashedPw = PasswordCrypter.Encrypt("");
		modelBuilder.Entity<Admin>()
			.HasData(new Admin
				{
					Id = Guid.NewGuid(),
					Name = "",
					Email = "",
					Password = hashedPw
				});
```

After these changes, open "Package Manager Console" and add these migrations to create tables and update the database.

```pm
Add-Migration Initial
Update-Database
```

Finally, it is ready to run.

## License

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

MIT License

Copyright (c) 2024 Rhayaden

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

## Contact

Mert Evirgen - evrgnmert@gmail.com<br><br>
[![LinkedIn](https://img.shields.io/badge/linkedin-%230077B5.svg?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/evirgenmert/)

Project Link: [https://github.com/rhayaden/fire-kitchen](https://github.com/rhayaden/abyssal-stationery)
