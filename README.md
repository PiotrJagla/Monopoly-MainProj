This is a project where I created a website with singleplayer and multiplayer games. I developed it using Blazor on the frontend, .NET API on the backend, and MySQL as the database. To test my code, I used MSTest.

When you eneter a website there is a page where you can choose if you want to login or register.
![image](https://user-images.githubusercontent.com/76881722/228301115-61d7c981-0362-4896-8809-05125e099ebe.png)

You can register entering name and password. I made this website with MySQL database where this data is stored.
![image](https://user-images.githubusercontent.com/76881722/228301214-6a0e804e-d397-4ed6-bd66-d76d41b6c99d.png)

You can also login and program will check if provided data is in database
![image](https://user-images.githubusercontent.com/76881722/228301778-e211a14a-0ec3-4d5a-9caa-4831b9d3bcc5.png)

When you login there is Main menu page. You can choose between singleplayer and multiplayer games.
![image](https://user-images.githubusercontent.com/76881722/228302432-3f777902-7fdd-4c4b-ad9a-deee7c3e13ea.png)

In singlplayer games there are two simple games; A simplified version of blackjack and Tic Tac Toe where you play with computer. In tic tac toe i implemented a minimax algorithm
that makes computer unbeatable.
![image](https://user-images.githubusercontent.com/76881722/228302774-01eeaec2-adef-46cc-acc6-9ed42d20baa0.png)
![image](https://user-images.githubusercontent.com/76881722/228302845-c9051c42-fe83-40de-ab71-eb88140d40ce.png)
![image](https://user-images.githubusercontent.com/76881722/228302927-d666093d-0a3c-4a81-870f-1096a0d37246.png)

The fun part is in the Multiplayer games. To handle the connection between clients i used SignalR library.
![image](https://user-images.githubusercontent.com/76881722/228304625-9a2a0ddd-41ee-4352-95c8-a2fd42dbdb8e.png)

Game called A demo button clicking game is just a two player connected that can click buttons:
![image](https://user-images.githubusercontent.com/76881722/228305130-e8a36184-65c6-437f-a7d2-759beb0ae705.png)

Second game is battleship. You can enter your ships and when the number or positions of ships are incorrect you cant start a game.
![image](https://user-images.githubusercontent.com/76881722/228305552-fd5511d3-f90f-40ce-b9ad-6388c437c838.png)

The last game is monopoly and this is the most complex game i implemented in this project(2-4 players). I implemented almost every feature of monopoly, exactly Business tour that is available on steam. You can buy towns, build houses, set championship, and so on. The last player to not bankrupt is a winner
(player 1 and 2)
![image](https://user-images.githubusercontent.com/76881722/228306428-be556bc7-516f-463d-b1f6-3b981e940986.png)
(player 3 and 4)
![image](https://user-images.githubusercontent.com/76881722/228306485-423510ac-24bb-4979-86a9-a265c87d2e87.png)
 
Game looks like this:
(player 1 and 2)
![image](https://user-images.githubusercontent.com/76881722/228307837-410ae034-0bb3-4c7d-ae08-cb969fdd9277.png)
(player 3 and 4)
![image](https://user-images.githubusercontent.com/76881722/228307902-1cb1eb2e-d38a-4d45-ba70-cc3f60b3bd57.png)

When you step on some cell, modal pops up and options are calculated based on cell type you stepped on and different other factors. Below there is modal that 
activates when you step on town cell.
![image](https://user-images.githubusercontent.com/76881722/228308277-867a1718-6129-474f-9acc-4389eb175cca.png)

The UI of this website is very basic, but when making this project, it was not my intention to make it pretty, but to implement something interesting that will work well.

