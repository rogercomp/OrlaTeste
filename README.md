# Instruções
## 1- Deve-se gerar o database atraves dos comandos de migration:
> dotnet ef migrations add v1 --project .\Orla.API  
> dotnet ef database update --project .\Orla.API

## 2 - Filtros
> O filtro de **duration** no endpoint de v1/medicamento se preenchidos deve conter somente 1 dos 3 valores: 

- **long**: apenas vídeos com mais de 20 minutos.
- **medium**: apenas os vídeos que tenham entre 4 e 20 minutos de duração.
- **short**: apenas vídeos com menos de quatro minutos de duração.

> Os filtros de **publishedAfter** e **publishedBefore** se ambos estiverem preechidos deve estar no seguinte formato: 

yyyy-MM-ddTHH:mm:ssZ exemplo=> 2022-01-01T00:00:00Z
caso não esteja sera considerado o intervalo => 2022-01-01T00:00:00Z à 2022-12-31T00:00:00Z.

## 3 - Chave API e ConnectionString
> A chave da API(YouTube Data API v3) deve ser setada no appsettings.json do projeto Orla.API na variavel **ApiGoogleKey**.
> Assim como também a chave de  a ConnectionStrings::DefaultConnection
 



