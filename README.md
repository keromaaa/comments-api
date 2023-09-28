# Comments API

The Comments API is a web service that allows users to manage and interact with a hierarchical commenting system. It provides endpoints for creating, retrieving, updating, and deleting comments, as well as fetching the entire comment hierarchy.

## Table of Contents

- [Features](#features)
- [Technologies](#technologies)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [Contributing](#contributing)
- [License](#license)

## Features

- Create new comments.
- Edit existing comments.
- Delete comments.
- Retrieve comments by their ID.
- Fetch the entire comment hierarchy.
- Hierarchical commenting system with parent-child relationships.

## Technologies

- ASP.NET Core
- Entity Framework Core
- Microsoft SQL Server (or your preferred database)
- JSON for data exchange
- RESTful API design

## Usage

- Use a tool like [Postman](https://www.postman.com/) or [curl](https://curl.se/) to interact with the API endpoints.
- Refer to the [API Endpoints](#api-endpoints) section for detailed information on available endpoints and their usage.

## API Endpoints

- **Create Comment**: POST `/api/comments/create`
  - Create a new comment.
- **Edit Comment**: POST `/api/comments/edit`
  - Edit an existing comment.
- **Get Comment**: GET `/api/comments/get/{id}`
  - Retrieve a comment by its ID.
- **Delete Comment**: DELETE `/api/comments/delete/{id}`
  - Delete a comment by its ID.
- **Get All Comments**: GET `/api/comments/getall`
  - Retrieve the entire comment hierarchy.

## Contributing

Contributions are welcome! If you'd like to contribute to this project, please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Make your changes and test thoroughly.
4. Commit your changes with clear and concise commit messages.
5. Push your changes to your fork.
6. Create a pull request to the main repository.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
