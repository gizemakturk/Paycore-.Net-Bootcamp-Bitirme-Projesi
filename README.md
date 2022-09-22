# Paycore-.Net-Bootcamp-Bitirme-Projesi

[![dotnet](https://img.shields.io/badge/dotnet-6-blue)]()
[![rabbitmq](https://img.shields.io/badge/rabbitmq--green)]()

> The purpose of this project is to manage products of user like offering and selling.

## Installation

Depending on the platform you are using, you can download its file from the clone section.

## Usage

To use the program, first you need to change the configurations up to you.(like database settings).You can then log into the program. But first, you must the create account. When you log in to the program, you need to authoritize with token response.

![account](screenshots/user.png)

Users can have products. You can create with CreteProduct endpoint. But first you need to create category of product. Then you can give a id of category to product's category.

![products](screenshots/product.png)
![offers](screenshots/offer.png)
![categories](screenshots/category.png)

User also can get all categories and add or update the category.

Different users can offer the product that own someone else. Also users can directly buy the product without offering. User can see the offers of products. And can accept or decline the offer. When user accept the offer , then product now belongs to another user that offer.

![email](screenshots/email.png)

The system has a email service using RabbitMq to make this asyncroneusly without waiting. The purpose of email service is to send message when important things happen like user logged in, created acccount or something else.
But you need to set configuration of smpt settings.

Good Luck :)
