# BasicMvc
Basic MVC app with generic repository with Bootstrap

Db: PostgreSql

Run without installing PostgreSql:

1. dowload Microsoft Tye https://github.com/dotnet/tye/blob/main/docs/getting_started.md
2. Have docker desktop running 
3. Build solution `dotnet build` or in IDE
3. Go to project folder where `tye.yaml` file is and in cmd enter: `tye run`
This will create you container of this app and postgreSql container configured with current db setup in this app;

App will run on: `http:/localhost:5000/`

