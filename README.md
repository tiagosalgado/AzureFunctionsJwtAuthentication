## Azure Function v1 and v2 with Jwt access token

### Request Token from Identity Server

~~~
curl -X POST http://localhost:5000/connect/token -H 'content-type: multipart/form-data; -F grant_type=client_credentials -F client_id=fx -F client_secret=secret -F scope=azurefunction
~~~
