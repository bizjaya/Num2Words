# Num2Words

## Overview

Num2Words is a .NET 8.0 application that converts numeric values into their full English text representation. The converter supports various input formats, including currency, and can handle amounts up to 1 CENTILLION.

## Features

The converter can handle the following input formats:

1. `123.45`
2. `$ 123.45`
3. `12345.678 dollars`
4. `564.2322`
5. `32 USD`
6. `2384732984573489573498573498573498573498573498573498573489573895734895734985734583745555555339453845345`

## Assumptions

- The converter supports numeric values with up to two decimal places for currency.
- Input can include symbols like `$`, `USD`, `dollars`, or `USDT` to denote currency.
- The converter can process and convert numbers up to 1 CENTILLION.

## How to Run

1. **Clone the Repository**

   ```bash
   git clone https://github.com/bizjaya/Num2Words.git
   ```
   

2. **Build And run**

   ```bash
   cd Num2Words
   dotnet build
   dotnet test
   ```


2. **Build And run**

   ```bash
   cd Num2Words
   dotnet build
   dotnet test
   ```

3. **Navigate to UI**

   ```bash
   https://localhost:7040/
   ```
   ![Image description](/Imgs/ui.png)

3. **Run Via cmd/Powershell**

   ```bash
   run test.ps1
   ```
   ![Image description](/Imgs/ps.png)

