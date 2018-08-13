# TReportsProviderSample
[![Build Status][travis-image]][travis-url] 

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

1) - Protocolo:** http

2) - Host:** <nome ou ip da máquina onde está rodando o sample>

3) - Porta:** 4987

4) - Provedor com autenticação OpenId
  ** - Tipo de autenticação: OpenID
  ** - Rota: api/treportsproviderbearer

5) - Provedor com autenticação Basic
  ** - Tipo de autenticação: Basic
  ** - Rota: api/treportsprovideranonymous


  
6) - Parâmetros:**

* *Usuário:* treports

* *Senha:* treports

* *Upper case:* Um parâmetro de teste que altera o comportamento do retorno, se será em upper case ou não.

* *Formato resposta (JSON/XML):* Indica qual o formato da resposta do servidor Json ou Xml.

Obs: Para utilização no swagger, deve-se utilizar o controller sem autenticação: [TReportsProviderAnonymousController]

Caso queira testar um relatório, existe um template no diretório ReportSample e pode ser importado pela tela de relatórios.

[travis-image]:https://travis-ci.org/totvs/treports-provider-sample.svg?branch=master
[travis-url]:https://travis-ci.org/totvs/treports-provider-sample
