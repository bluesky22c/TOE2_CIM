# CIM 통신 흐름 분석 보고서

이 보고서는 CIM(Communication & Interface Module)의 핵심 통신 시퀀스와 메시지 흐름을 분석한 결과입니다. `Log_2025-11-06_Log.txt` 분석을 바탕으로 작성되었습니다.

## 1. 개요
CIM 시스템은 상위 Host 시스템과 장비 시뮬레이터(Simulator) 사이의 가교 역할을 수행하며, SECS-II 프로토콜을 사용하여 데이터를 주고받습니다. 주요 시나리오별 통신 흐름은 다음과 같습니다.

---

## 2. 주요 통신 시나리오

### A. Glass 로딩 및 공정 시작 (Single Cycle)
Host로부터 레시피 확인 및 Glass 데이터를 수신하고, 장비로 이동 명령을 내린 후 공정을 시작하는 단계입니다.

```mermaid
sequenceDiagram
    participant Host as Host (CIM Server)
    participant CIM as TOECIM (Main)
    participant Sim as EQP Simulator

    Note over Host, Sim: 1. Recipe & Data Receive
    Host->>CIM: S7F61 (Recipe ID Confirm Req)
    CIM-->>Host: S7F62 (Ack)
    Host->>CIM: S2F87 (Load Glass Data)
    CIM-->>Host: S2F88 (Ack)

    Note over Host, Sim: 2. Glass Movement (LR)
    CIM->>Sim: Glass Move Req (LR)
    CIM->>Host: S6F67 (Move Request: LR)
    Host-->>CIM: S6F68 (Ack)

    Note over Host, Sim: 3. Load Done & Status Report
    Sim->>CIM: CMD: LoadDone
    CIM->>Host: S6F91 (Status Change: RUN)
    Host-->>CIM: S6F92 (Ack)
    CIM->>Host: S1F67 (All Glass Position Report)
    Host-->>CIM: S1F68 (Ack)
```

---

### B. Glass 교체 및 언로딩 (Exchange Flow)
검사가 완료된 Glass를 배출하고 새로운 Glass를 투입하는 시퀀스입니다.

```mermaid
sequenceDiagram
    participant Host as Host (CIM Server)
    participant CIM as TOECIM (Main)
    participant Sim as EQP Simulator

    Note over Host, Sim: 1. Inspection Done
    Sim->>CIM: CMD: InspectDone
    CIM->>Host: S6F11/S6F91 (Status Change Report)

    Note over Host, Sim: 2. Exchange Ready
    Sim->>CIM: CMD: ExchangeReadyDone
    CIM->>Host: S6F63 (Unload Glass Data Report)
    Host-->>CIM: S6F64 (Ack)
    CIM->>Host: S6F61 (Load Glass Data Req)
    Host-->>CIM: S6F62 (Ack)

    Note over Host, Sim: 3. Move Req (UW/ER)
    Host->>CIM: S2F87 (New Glass Data)
    CIM->>Sim: Move Req (UW/ER)
    CIM->>Host: S6F67 (Move Request)
    Host-->>CIM: S6F68 (Ack)

    Note over Host, Sim: 4. Exchange Done
    Sim->>CIM: CMD: ExchangeDone
    CIM->>Host: S1F67 (Glass Position Report)
    Host-->>CIM: S1F68 (Ack)
```

---

### C. LOT 종료 (LOT End)
해당 LOT의 모든 공정이 완료되어 데이터를 최종 보고하는 단계입니다.

```mermaid
sequenceDiagram
    participant Host as Host (CIM Server)
    participant CIM as TOECIM (Main)

    Host->>CIM: S6F65 (LOT End Data Send)
    CIM-->>Host: S6F66 (Ack)
    CIM->>Host: S6F83 (LOT Actual Processing Data Report)
    CIM-->>Host: S6F84 (Ack)
```

---

