# Memo

# Project Vision:
## 1. Executive Summary

This report will discuss about the new memo application we have proposed to build. As a team, we are aiming to build an application that would solve an issue which people face on a daily basis. The proposed solution to tackle this issue should aid in further enhancing and enriching everyone’s daily lives.

## 2. Problem:

We are aiming to build an application that will address a critical issue that currently exists in our society.

In today’s world, we have so many things going on in our daily lives that not all humans are capable of keeping everything in mind – our memory power capacity only remembers certain things and because of this we tend to forget. Hence after careful consideration, our team has elected to build a memo application that people can use to store their own memos for each day. These memos can be saved and retrieved whenever opening the application.

## 3. Initial Architecture:
 
The proposed memo application will be written in the C# object oriented programming language. Most of the team members are currently undertaking the Object-Oriented Programming language C# module hence this will be a great to develop our OOP programming skills.

We have elected to use a combination of MVC and the Entity Framework. A Model View Controller (MVC) is a three-component based architecture and each component performs various different tasks. The Model is responsible for storing and handling the application data and this is where interaction would occur with the database server. View is what the end user would see and so it performs operations to present the data in a user perceivable format and layout. Finally, the Controller receives and manages the user requests and performs whatever’s appropriate to carry them out.

As this is a simple memo application, we were initially considering writing the memo data to a plain text file however this wouldn’t be feasible for various reasons.

When considering the application’s development roadmap from a futuristic standpoint, it wouldn’t be feasible as it would restrict us on the number of features that can be implemented.

•	The application data can only be accessible from the device it is running on
•	Syncing will not be possible
•	Not all types of data can be easily read and written. For example, it will be quite complicated if we were to store images and 
•	Plain text files lack encryption and this could pose a threat if the user were to store confidential data.

Thus, by implementing a database solution and using MVC, we can store the data in various other methods. In addition, we could also further expand the functionalities of this memo app by migrating it to a cloud based service where the memo data can be synced across multiple devices (smartphone, personal computer and also tablet computer). This is something which cannot be done with plain text files.


