# This is a challenge by Coodesh [![Awesome](https://cdn.jsdelivr.net/gh/sindresorhus/awesome@d7305f38d29fed78fa85652e3a63e154dd8e8829/media/badge.svg)](https://github.com/sindresorhus/awesome#readme)
> Provided by [`Coodesh`](https://coodesh.com/) through all their rules and requirements.

## Topics: 📜
- How to run this challenge on your local machine?:
   - [Requirements 🛠](#Requirements?🛠)
   - [Running 🛠](#Running-🛠)
   - [Personal considerations about the requirements and domain-problem🛠](#Personal-considerations-about-the-requirements-and-domain-problem-🛠)
- Personal section:
   - [How to run the project? 🛠](#How-to-run-the-project?🛠)
   - [Technical project description & technologies🛠](#Technical-project-description-&-technologies-🛠)
   - [Personal considerations about the requirements and domain-problem🛠](#Personal-considerations-about-the-requirements-and-domain-problem-🛠)
- Challenge section:
   - [Challenge Description](#Challenge-Description)
   - [Functional Requirements](#Functional-Requirements)
   - [Non-functional Requirements](#Non-functional-Requirements)
   - [Bonus Requirements](#Bonus-Requirements)
   - [Input File Format](#Input-File-Format)
   - [Evaluation](#Evaluation)

## How to run the project?🛠
### Requirements
Firstly, You'll have to install `Git` and `Docker` on your local machine. After that, clone the project by typing `git clone https://github.com/mariofelipefarhat/CoodeshChallenge.git` on your terminal. 

### Running
In the cloned project directory, open the terminal and type `docker-compose up`. After a while, you can access the container API through the link http://localhost:8001/swagger/index.html and check for the API documentation


## Technical project description & technologies🛠
This is a solution with two services (.NET 7 API and MYSQL Server) orchestrated by `Docker`. Both services are containers, and each container holds one service/image in its total independent ambient. Each container can communicate with each other through the docker network. For example, the backend API and the MySQL server image are sharing the same network called "backend." All image/service definitions were done in the **[`docker-compose.yml`](https://github.com/mariofelipefarhat/CoodeshChallenge/blob/main/docker-compose.yml)** file. `SOLID` principles, `Domain-Driven Design (DDD)`, `Command-Query Separation (CQS)`, `Clean Arch` (not all) and others were my guide.

## Challenge Description
A new urgent demand has emerged, and we need an exclusive area to upload a file containing transactions made from selling products by our customers. Our platform operates on the creator-affiliate model, where a creator can sell their products, and one or more affiliates can also sell these products, paying a commission for each sale. Your task is to build a web interface that allows users to upload a file containing transactions of sold products, normalize the data, and store it in a relational database. You should use the file [sales.txt](sales.txt) to test the application. The file format is described in the section "Input File Format."

## Functional Requirements
Your application should:
1. Have a screen (via form) to upload the file.
2. Parse the received file, normalize the data, and store it in a relational database, following the file interpretation definitions.
3. Display the list of imported product transactions by producer/affiliate, with a total value of the transactions.
4. Handle backend errors and display user-friendly error messages on the frontend.

## Non-functional Requirements
1. The application should be easy to set up and run, compatible with Unix environments. You should only use free or open-source libraries.
2. Use Docker for the different services that compose the application, making it easily runnable outside your personal environment.
3. Use any relational database.
4. Use small commits in Git and write a good description for each one.
5. Write unit tests for both the backend and frontend.
6. Write code and comments in English. Documentation can be in Portuguese if preferred.

## Bonus Requirements
Your application does not need to include these, but we will be impressed if it:
1. Has documentation for the backend APIs.
2. Uses docker-compose to orchestrate the services as a whole.
3. Includes integration or end-to-end tests.
4. Has all documentation written in easily understandable English.
5. Handles authentication and/or authorization.

## Input File Format
| Field    | Start  | End | Size    | Description                    |
| -------- | ------ | --- | ------- | ------------------------------ |
| Type     | 1      | 1   | 1       | Transaction type               |
| Date     | 2      | 26  | 25      | Date - ISO Date + GMT          |
| Product  | 27     | 56  | 30      | Product description            |
| Amount   | 57     | 66  | 10      | Transaction amount in cents    |
| Seller   | 67     | 86  | 20      | Seller's name                  |

### Transaction Types
These are the possible values for the Type field:
| Type | Description         | Nature   | Sign  |
| ---- | ------------------- | -------- | ----- |
| 1    | Producer Sale       | Input    | +     |
| 2    | Affiliate Sale      | Input    | +     |
| 3    | Paid Commission     | Output   | -     |
| 4    | Received Commission | Input    | +     |

## Evaluation
Your project will be evaluated based on the following criteria:
1. Documentation of environment setup and execution that successfully runs the application.
2. Fulfillment of [functional requirements](#Functional-Requirements) and [non-functional requirements](#Non-functional-Requirements).
3. Well-structured components and code layout without over-engineering.
4. Code readability.
5. Good test coverage.
6. Clarity and extent of documentation.
7. Fulfillment of any [bonus requirements](#Bonus-Requirements).
