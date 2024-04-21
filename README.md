# GestionHotel API
Projet mené par ROJAS Célia, GUICHON Maxime, BERNIGOLE Quentin - EISI I1 C2

## Description
GestionHotel API est un service de gestion des chambres, des réservations et des utilisateurs pour un hôtel. Il permet de gérer les opérations courantes telles que la création, la lecture, la mise à jour et la suppression (CRUD) des chambres, des réservations et des utilisateurs.

## Authentification
Le service utilise JWT (JSON Web Tokens) pour l'authentification. Les utilisateurs peuvent s'authentifier en utilisant l'endpoint `/api/utilisateur/authentification`, en fournissant leur email et leur mot de passe. Un jeton JWT est généré et renvoyé en réponse, ce jeton doit être inclus dans les en-têtes des requêtes pour accéder aux autres endpoints protégés.

## Technologies utilisées
- ASP.NET Core 8.0
- MongoDB
- JWT pour l'authentification
- Swagger pour la documentation des API
- PostMan / Insomnia pour les test de l'API

## Configuration
Le service utilise une base de données MongoDB pour stocker les données. La configuration de la base de données et des autres paramètres peut être modifiée dans le fichier `appsettings.json`.


## Utilisation
1. Clonez ce dépôt.
2. Configurez les paramètres dans le fichier `appsettings.json`.
3. Exécutez la solution GestionHotelApi.sln
4. Explorez et testez les routes disponibles à l'aide de Swagger ou d'un outil similaire.

## Design Patterns utilisés
Dans cette application, deux design patterns sont largement utilisés pour structurer le code et faciliter la maintenance et l'évolutivité :
- **Pattern Repository**: Ce pattern est implicitement utilisé dans l'architecture des services qui interagissent avec la base de données MongoDB. Le Pattern Repository permet d'encapsuler la logique d'accès aux données, en fournissant une interface uniforme pour communiquer avec la base de données, tout en cachant les détails de stockage spécifiques. Cela permet de rendre le code métier indépendant de la couche de persistance et facilite les tests et les modifications.
- **Singleton**: Les services sont ajoutés en tant que singletons dans le fichier `program.cs` en utilisant la méthode `AddSingleton()`. Cela garantit qu'une seule instance de chaque service est créée et réutilisée tout au long du cycle de vie de l'application. Les singletons sont utilisés pour partager des instances de services entre différentes parties de l'application, assurant ainsi une gestion efficace des ressources et une cohérence des données.

## Problèmes rencontrés
Étant novices en C#, nous avons dû commencer de zéro et apprendre tous les concepts du langage. Nous avons rencontré de nombreux problèmes lors de la mise en œuvre de la logique, mais nous pensons avoir réussi à surmonter la plupart d'entre eux. L'une des difficultés majeures a été l'intégration du JWT (JSON Web Token), une fonctionnalité que nous n'avons pas totalement achevée, la rendant donc non fonctionnelle. De plus, choisir MongoDB comme base de données a également posé des défis, car seul l'un d'entre nous avait une connaissance préalable limitée. La mise en place de MongoDB a nécessité un apprentissage supplémentaire et n'a pas été aussi facile que prévu.
