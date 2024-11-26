[Authentication Provider for Graph Client](https://learn.microsoft.com/en-us/graph/sdks/choose-authentication-providers?tabs=csharp#client-credentials-provider)


Adding service principal

`az ad sp create-for-rbac --name GitHubActions --role Contributor --scopes /subscriptions/8dc3aa21-5fcc-4c2e-837d-cf3c9fea564f --sdk-auth`


Add this to the secrets in GitHub Settings

Setup PAT

Profile->Settings->Develper Settings->Personal Access Tokens->Generate Token
