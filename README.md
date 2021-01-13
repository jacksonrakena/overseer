# 💚 Abyss
A Discord bot, capable of both interactions (slash-commands) and gateway commands.  
Command handling is built on top of the [Rowi & Rowi-JDA](https://github.com/abyssal/rowi).  
Gateway handling is built with [JDA](https://github.com/DV8FromTheWorld/JDA) and the interactions server utilises [Ktor](https://github.com/ktorio/ktor) (on [Netty](https://ktor.io/docs/netty.html))  
Mesh communication is powered by [Pixie](https://github.com/abyssal/pixie).

<img src="https://i.imgur.com/DF1ZIs2.png" height="150" />

| Prefix | Developer | Runtime | Library | Version | Platform | Invite |
|-|-|-|-|-|-|-|
| / | [Abyssal](https://github.com/abyssal) | JRE 1.8 | [ACE](https://github.com/abyssal/command-engine) & [JDA](https://github.com/DV8FromTheWorld/JDA) | 16.1 | Gradle | [Add me](https://abyss.abyssaldev.com/invite)
  
### 👮‍ Requirements
- A [Discord bot application](https://discordapp.com/developers/applications/) with registered user and token (app -> Bot -> Add Bot)
    - Interactions enabled, pointing to Abyss' `/discord/interactions` endpoint (configurable)
- A reverse proxy or middleman to apply SSL from 443 to Abyss' configurable port (Nginx recommended - [example config](example_nginx_server_conf.nginx))
- `appconfig.json` configuration file

### 🛠 Structure
The project is broken down into the following domains:     
**Core** 
- 💚 `com.abyssaldev.abyss` The core of Abyss. This project contains initialisation logic for the interaction controller and the gateway.
  
**Persistence**
- 📜 `com.abyssaldev.abyss.persistence` This package contains Abyss' persistence logic, including its database connections.

**Commands**  
- 🪐 `com.abyssaldev.abyss.commands.gateway` This package contains Abyss' default gateway (`/gw`) command set.
- 🤝 `com.abyssaldev.abyss.framework.interactions` This handles Abyss' interactions (also known as "slash commands"), handled over REST.  
  - `com.abyssaldev.abyss.commands.interactions` This package contains Abyss' default interactions set.
  
### 🖋 Copyright
Copyright (c) 2018-2021 Abyssal under the MIT License, available at [the LICENSE file.](LICENSE.md)  

[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2Fabyssal%2Fabyss.svg?type=large)](https://app.fossa.com/projects/git%2Bgithub.com%2Fabyssal%2Fabyss?ref=badge_large)
