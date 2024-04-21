# GestionHotel API

## Description
GestionHotel API est un service de gestion des chambres, des réservations et des utilisateurs pour un hôtel. Il permet de gérer les opérations courantes telles que la création, la lecture, la mise à jour et la suppression (CRUD) des chambres, des réservations et des utilisateurs.

## Authentification
Le service utilise JWT (JSON Web Tokens) pour l'authentification. Les utilisateurs peuvent s'authentifier en utilisant l'endpoint `/api/utilisateur/authentification`, en fournissant leur email et leur mot de passe. Un jeton JWT est généré et renvoyé en réponse, ce jeton doit être inclus dans les en-têtes des requêtes pour accéder aux autres endpoints protégés.

## Technologies utilisées
- ASP.NET Core
- MongoDB
- JWT pour l'authentification
- Swagger pour la documentation des API

## Configuration
Le service utilise une base de données MongoDB pour stocker les données. La configuration de la base de données et des autres paramètres peut être modifiée dans le fichier `appsettings.json`.

## Utilisation
1. Clonez ce dépôt.
2. Configurez les paramètres dans le fichier `appsettings.json`.
3. Exécutez le service à l'aide de la commande `dotnet run`.
4. Explorez et testez les routes disponibles à l'aide de Swagger ou d'un outil similaire.
