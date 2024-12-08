# Connecting with MongoDB for VS Code 

1. Install MongoDB for VS Code.

In VS Code, open "Extensions" in the left navigation and search for "MongoDB for VS Code." Select the extension and click install.

2. In VS Code, open the Command Palette.
Click on "View" and open "Command Palette."
Search "MongoDB: Connect" on the Command Palette and click on "Connect with Connection String."

3. Connect to your MongoDB deployment.
Paste your connection string into the Command Palette.

```sh

mongodb+srv://dbUser:<db_password>@cluster-azure-001.9ktma.mongodb.net/

```

Replace <db_password> with the password for the dbUser user. Ensure any options are URL encoded.  You can edit your database user password in Database Access. 
4. Click “Create New Playground” in MongoDB for VS Code to get started.