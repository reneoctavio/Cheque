# Cheque

In this project, you can save informations of checks in your custody.
You can register them, keep track of values, dates, and whether they were cashed.
You can check reports of people related to the check, values, dates and deliquency.

Neste projeto, você poderá salvar informações de cheques em sua custódia.
Poderá registrá-los, manter informações de valores, datas e liquidação.
Poderá ver relatórios de pessoas relacionadas aos cheques, values, datas e inadimplência.

## Getting Started

### Requirements

- [.NET Framework 4.0](https://dotnet.microsoft.com/download/dotnet-framework)
- [Mono and GTK# 2.12](https://www.mono-project.com/download/stable/)
- [Sqlite3](https://www.sqlite.org/download.html)

### Build

1. It was built using [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/)
2. You can build using Visual Studio for Windows.
   - Make sure you set the reference paths to `C:\Program Files (x86)\Mono\lib\mono\4.0` and `C:\Program Files (x86)\Mono\lib\gtk-sharp-2.0`
   - Also make sure that the executable have access to `sqlite3.dll`. You can download and put it in the same folder.

## Run

### Windows

1. Install Mono and GTK#
2. Download and extract release (already contains sqlite3)
3. Run: `mono Cheque.GTK.exe`

### Mac

1. Install Mono and GTK#
2. Run: `mono Cheque.GTK.exe` (sqlite is already included by default in macOS)

### Linux (Ubuntu)

1. Install [Mono](https://www.mono-project.com/download/stable/#download-lin)
2. Install GTK and SQLite: `sudo apt-get install libgtk2.0-cil-dev sqlite3`
3. Run: `mono Cheque.GTK.exe`

## Screenshots

### Cadastro

![Cadastro](docs/screenshots/cadastro.png?raw=true)

### Relatórios

![Relatório 1](docs/screenshots/relatorio-1.png?raw=true)
![Relatório 2](docs/screenshots/relatorio-2.png?raw=true)

### Propriedades do Cheque

![Propriedades](docs/screenshots/propriedades.png?raw=true)
