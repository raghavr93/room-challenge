# room-challenge

## 1. User management: The server should be able to register and authenticate users.
User has: username, password, and an optional mobile_token (string)
Required routes:

- /api/user(Get) - Get users (no auth required): returns a list of all users.

- /api/user/{username}(Get) - Get users (no auth required): takes a username and return the user with
matching username.

- /api/register(Post) - Register (no auth required): takes a username, password and optional string for
mobile_token. Registers the user and authenticates the client as the newly created user.

- /api/authenticate(Post)- Sign in/authenticate: takes a username and password, and authenticates the
user.

- /api/user/update(Patch) - Update User (must be signed in as the user): updates password and/or
mobile_token of the user.

- /api/user/delete(Delete) - Delete User (must be signed in as the user): deletes the user.

## 2. Room management: The server should be able to handle creating conference rooms
Room has: name (non-unique), guid, host user, participants (users) in the room, and a
capacity limit. Number of users in the room must not exceed the capacity
Required routes:

- /api/room/create(Post) - Create a room (signed in as a user): creates a room hosted by the current user,
with an optional capacity limit. Default is 5.

- /api/room/changehost(Patch) - Change host (must be signin as the host): changes the host of the user from the
current user to another user.

- /api/room/addremove (Post) - Join/leave (signed in as a user): joins/leaves the room as the current user.

- /api/room/roominfo/{guid} - Get info (no auth): given a room guid, gets information about a room.

- /api/room/allrooms/{username} - Search for the rooms that a user is in: given a username, returns a list of rooms
that the user is in.
