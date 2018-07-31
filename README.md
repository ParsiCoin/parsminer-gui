# PARSMinerGUI

One click miner GUI for xmr-stak to specifically mine ParsiCoin (PARS)

Based on [TurtleCoin One Click Miner](https://github.com/turtlecoin/one-click-miner)

Adapted for CryptoNight_v7 Coins by ParsiCoin Devs.

## How to build
Download the repository and extract it. Open the .sln file with a recent version of Visual Studio (Community Edition / make sure you have C# packages installed). Build the project using the green run button or "Build Solution" in the Build menu. You'll find the binaries inside the project folder in the directory `bin/debug/` or `bin/release/`. Copy `PARSMinerGUI.exe` to your preferred directory and make sure to have the miner executables in the same folder as described below.

## Important
* Mine at your own risk concerning your hardware
* Refer to [xmr-stak](https://github.com/fireice-uk/xmr-stak)
* The program needs a specific folder structure to work. Upcoming releases wil have it bundled with the miners.

Folder structure from where the PMG is placed:

* miners
  * xmr-stak
    * xmr-stak.exe *(alongside all other necessary files)*

## Pool Owners

Please put your Parsicoin Pool on parsicoin-pools.json and open a pull request. or you can send an email.

(PARSMinerGUI works with your pool only if you setup mining on this ports : 3333, 5555, 7777)
