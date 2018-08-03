# treports-provider-sample
Exemplo de provedor integrado para o TReports

Este projeto contem um provedor integrado para o TReports. O projeto implementa todos os métodos necessários para se ter um provedor integrado, 
porem não necessita de conectar-se a nenhuma base de dados, pois utiliza um dataset interno como fonte de dados de exemplo.

O provedor possui um swagger que pode ser visualizado bastando executar o projeto pelo visual studio ou executando o comando:

dotnet TReportsProviderSample.dll

O artefato está no diretório Publish.

OBS.: É necessário ter instalado o .Net Core em seu computador para executar a dll e para abrir no visual studio é necessário baixar o SDK do .NET Core.
https://www.microsoft.com/net/download


Para criar o provedor no TReports informe os seguintes parâmetros na tela de provedores de dados:

protocolo: http

host: localhost

porta: 5000

rota: api/treportssample

Tipo de autenticação: vazio

usuário treports

senha: treports

Caso queira testar um relatório, existe um template no diretório ReportSample e pode ser importado pela tela de relatórios.
