# kzRippper

A simple tool performing brute-force and dictionary attacks on Rar files using the [SharpCompress](https://github.com/adamhathcock/sharpcompress) library.

---

## Installation

Download the [executable file](https://github.com/KristalinZH/kzRipper/releases/download/v1.0.0/kzrip.exe) and you are ready. The tool is compiled into single executable file with its dependancies within. 

## How to use

To see which are the commands, run:

    kzrip

    kzrip -h

    kzrip --help

### Brute-Force

To perfrom brute-force attack, run:

    kzrip brip <Archive>
which by default will try passwords containing digits, lowercase and uppercase letters with lengths from 1 to maximum 50 characters. 
**Important: The tool does not utilize GPU usage.** 
If you wish to specify what characters to be used in passwords and set limits to the passwords' lengths you can add the following options to the command:

| Option   | Description |
|----------|-------------|
| `-d, --digits`   | Use digits in passwords |
| `-l, --lower`   | Use lowercase letters in passwords |
| `-u, --upper`   | Use uppercase letters in passwords |
| `-r, --range`   | Set passwords length. First argument sets the minimal length, second sets maximal length. If there is one, it sets the maximal length. |

### Dictionary

To perform dictionary attack, run:

    kzrip drip <Archive> <Dictionary>
