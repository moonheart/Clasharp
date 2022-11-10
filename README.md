<img src="images/clasharp.svg" alt="drawing" width="200"/>

# Clasharp, A clash meta kernel GUI written in C# 

Clasharp uses [Meta Kernel](https://github.com/MetaCubeX/Clash.Meta) as backend.

This is a WIP project.

## Screenshots

![dashboard](images/dashboard.png)
![proxies](images/proxies.png)
![logs](images/logs.png)
![rules](images/rules.png)
![connections](images/connections.png)

## Roadmap
- [x] Clash API
- [x] Clash CLI
  - [x] Start
  - [x] Stop
- [ ] User Interface
  - [x] Dashboard
    - [x] Traffic Stats
    - [x] Connections Stats
    - [x] Traffic Chart
  - [x] Proxy Groups
    - [x] Select Proxy
  - [x] Proxy Providers
      - [x] HealthCheck Proxy Provider
      - [x] Update Proxy Provider
  - [x] Rules
  - [x] Rules Providers
      - [x] Update Rules Provider
  - [x] Connections
    - [x] Sorting
    - [x] Close Connection
  - [x] Realtime Logs
  - [x] Clash Process Status
  - [ ] Settings
    - [x] System Proxy
    - [ ] TUN Mode
    - [x] Service Mode
    - [ ] Start With System
  - [ ] Subscriptions
    - [ ] Update
- [x] Service Mode
  - [x] Install Core Service
  - [x] Uninstall Core Service
- [ ] Builtin SubConverter
- [ ] Connect To Remote Backend
- [ ] Dark Theme
- [ ] Update Clash Core
- [ ] Self Update
- [ ] Tray Icon
- [ ] Set System Proxy
  - [x] Windows
  - [ ] Linux