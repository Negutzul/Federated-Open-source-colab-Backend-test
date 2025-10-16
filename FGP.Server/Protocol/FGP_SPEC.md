# Federated Git Protocol (FGP) — v0

## Overview
FGP is a simple HTTPS-based protocol that allows independent instances to exchange
Git-related collaboration messages (e.g. pull requests) in a federated way.

---

## 1. Protocol Basics
- **Name:** fgp.v0  
- **Transport:** HTTPS POST  
- **Content-Type:** `application/fgp+json`  
- **Message Format:** JSON envelope  
  ```json
  {
    "protocol": "fgp.v0",
    "type": "ping",
    "id": "uuid-or-ulid",
    "sentAt": "2025-10-14T12:00:00Z",
    "from": "fgp://source.instance",
    "to": "fgp://target.instance",
    "payload": { }
  }
