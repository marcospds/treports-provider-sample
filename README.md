# TReportsProviderSample
Exemplo de provedor integrado para o TReports

Este projeto contem um provedor integrado para o TReports. O projeto implementa todos os métodos necessários para se ter um provedor integrado, 
porem não necessita de conectar-se a nenhuma base de dados, pois utiliza um dataset interno como fonte de dados de exemplo.

O provedor possui um swagger que pode ser visualizado bastando executar o projeto pelo visual studio ou executando o comando:

**dotnet TReportsProviderSample.dll**

O artefato está no diretório Publish.

*OBS.: É necessário ter instalado o .Net Core em seu computador para executar a dll e para abrir no visual studio é necessário baixar o SDK do .NET Core.
https://www.microsoft.com/net/download*

Caso o serviço não esteja funcionando com localhost ou o nome da máquina, altere o arquivo hosting.json.

Para criar o provedor no TReports informe os seguintes parâmetros na tela de provedores de dados:

**Protocolo:** http

**Host:** <nome ou ip da máquina onde está rodando o sample>

**Porta:** 4987

**Rota:** api/treportsprovider

**Tipo de autenticação:** deixe sem preenchimento

**Parâmetros:**

* *Usuário:* treports

* *Senha:* treports

* *Upper case:* Um parâmetro de teste que altera o comportamento do retorno, se será em upper case ou não.

* *Formato resposta (JSON/XML):* Indica qual o formato da resposta do servidor Json ou Xml.

Caso queira testar um relatório, existe um template no diretório ReportSample e pode ser importado pela tela de relatórios.
