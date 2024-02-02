// 1.Instalar Dependências:

-> Abra um terminal no diretório do projeto.
# Por exemplo, você pode ir na pasta api em wwwroot e copiar o path na sua máquina (exemplo do meu)
# cd C:\Users\darle\Desktop\Acessidoc-NET\sistema-acessidoc\wwwroot\api\

// Dependências

-> PARA INSTALAR, bibliotecas do Node.js / Javascript (Descrição by Copilot):

npm install express 
# Express é um framework para aplicativo da web do Node.js mínimo

npm install multer 
# Multer é um middleware node.js para manipulação multipart/form-data, 
# que é usado principalmente para fazer upload de arquivos.

npm install pdf-lib 
# PDF-Lib é uma biblioteca para criar e modificar documentos PDF.

npm install @pdf-lib/fontkit 
# Fontkit é uma biblioteca avançada de manipulação de fontes para Node.js e o navegador.

npm install pdf-parse 
# PDF-Parse é uma biblioteca para extrair texto de arquivos PDF.

npm install axios 
# Axios é uma biblioteca JavaScript promissora usada para fazer solicitações HTTP.

npm install bcrypt 
# Bcrypt é uma biblioteca para ajudar você a codificar senhas.

npm install body-parser 
# Body-parser é um middleware que é responsável por 
# analisar o corpo das solicitações recebidas em middleware antes dos manipuladores.

npm install docxtemplater 
# Docxtemplater é uma biblioteca para gerar documentos .docx a partir de um modelo .docx.

npm install dotenv 
# Dotenv é um módulo de dependência zero que carrega variáveis de ambiente de um arquivo .env para process.env.

npm install ejs 
# EJS é uma linguagem de modelagem simples que permite gerar marcação HTML com JavaScript simples.

npm install fs 
# FS é um módulo do Node.js que fornece uma API para interagir com o sistema de arquivos.

npm install nodemon 
# Nodemon é uma ferramenta que ajuda a desenvolver aplicativos
# baseados em node.js ao reiniciar automaticamente o aplicativo node quando mudanças de arquivo no diretório são detectadas.

npm install path 
# Path é um módulo do Node.js que fornece utilitários para trabalhar com caminhos de arquivo e diretório.

npm install sequelize (Tipo o Entity Framework, mas do JS)
# Sequelize é um ORM baseado em promessa para Node.js e io.js.
# Ele suporta os dialetos PostgreSQL, MySQL, MariaDB, SQLite e MSSQL
# e recursos de transação sólida, relações, carregamento preguiçoso e ansioso, replicação de leitura e muito mais.

npm install sharp 
# Sharp é uma das bibliotecas mais populares para converter
# imagens de grande formato para formatos menores e mais otimizados para a web. (caso usemos nas imagens das provas)

npm install sqlite
# SQLite é uma biblioteca em C que fornece um banco de dados baseado em disco que é acessado via SQL.

npm install sqlite3
# Mesma descrição do SQLite a cima.

// PARA INSTALAR, bibliotecas do .NET (copiei o path, mas lá em pacotes do nuget digite
									   o nome da biblioteca e a versão que está ali):

system.data.sqlite\1.0.118\
microsoft.visualstudio.web.codegeneration.design\7.0.11\
microsoft.jquery.unobtrusive.validation\4.0.0\
microsoft.entityframeworkcore.tools\7.0.0\
microsoft.entityframeworkcore.sqlite\7.0.0\
microsoft.entityframeworkcore.design\7.0.0\
microsoft.entityframeworkcore\7.0.0\
microsoft.aspnetcore.spaservices.extensions\7.0.0\
packages\microsoft.aspnetcore.nodeservices\3.1.32\


// 2.Executar o Servidor:

# Ainda no terminal, execute 'node app.js' ou 'npm run dev' -> 
# nesse caso tem um script que será executado para iniciar o servidor.

# O servidor estará rodando na porta 3000 (ou na porta que você definiu),
# e você pode acessá-lo em http://localhost:3000/editor.html

-> OBESERVAÇÕES: 

# 1 - Faça um update-database depois para ver se funcionou;
# 2 - No código nessas partes não precisa mexer, ele está configurado
#	  na classe Program e no appsettings (bom dar uma olhada para ver como
#	  como o SQLite funciona, para ter como base em mais um banco);


Usar o Servidor:

# Para fazer upload de arquivos, você pode usar um cliente de API como 
# Postman ou Insomnia ou escrever um frontend que envie arquivos para o seu servidor.
