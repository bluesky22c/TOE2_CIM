# TOEConsole 통신 분석 보고서

## 1. 통신 아키텍처 (Communication Architecture)

TOEConsole과 TOECIM 간의 통신은 **양방향 클라이언트-서버 (Bidirectional Client-Server)** 모델을 기반으로 사용합니다. 두 애플리케이션 모두 서버 리스너를 열어두고, 상대방에게 데이터를 보낼 때 클라이언트를 생성하여 접속합니다.

### 1.1 연결 구조 (Connection Structure)

| 방향 (Direction) | Sender (Client) | Receiver (Server) | Port 설정 변수 | Data Class |
| :--- | :--- | :--- | :--- | :--- |
| **Console → CIM** | TOEConsole | TOECIM | `Vars.systemSetup.CIMPortNo` | `CimCommData` (ToCIM) |
| **CIM → Console** | TOECIM | TOEConsole | `Vars.SystemSetup.FromCIMPortNo` | `CimCommData` (ToConsole) |

*   **TOECIM Server**: `TOECIM.MainForm`에서 `Program.CIMPort` 포트로 리스닝 (`EServer<CimCommData>`).
*   **TOEConsole Server**: `TOEConsole.MainForm`에서 `Vars.CimtoConsolePort` 포트로 리스닝 (`EServer<CimCommData>`).

### 1.2 데이터 구조 (Data Structure)

모든 통신 데이터는 `CimCommData` 클래스로 캡슐화되어 전송됩니다.

```csharp
[Serializable]
public class CimCommData : TENetData
{
    public ConsoletoCIMData ToCIM { get; set; }       // Console -> CIM 데이터
    public CIMtoConsoleData ToConsole { get; set; }   // CIM -> Console 데이터
}
```

---

## 2. 주요 통신 시퀀스 (Communication Sequences)

### 2.1 글라스 로드 및 데이터 전달 (Glass Load & Data Transfer)

CIM이 호스트(ECS)로부터 글라스 데이터를 수신(`S2F87`)하고, 이를 Console로 전달하는 과정입니다.

1.  **[CIM] S2F87 수신**: 호스트로부터 Link Grant를 받음.
2.  **[CIM] ACK 전송**: `S2F88` Accept 전송.
3.  **[CIM → Console] Glass Data Send**:
    *   `Common.ToConsole` 호출.
    *   Command: `CIMtoConsole.GlassDataSend` (또는 `GlassDataReq`)
    *   Data: `GlassInfo` (A/B Glass 정보), `ContainGlass` (글라스 유무).
4.  **[Console] 데이터 수신**: `MainForm`에서 수신 후 내부 변수 업데이트.

### 2.2 공정 진행 및 상태 보고 (Process & Status Reporting)

Console이 공정(검사)을 진행하면서 상태 변화를 CIM에 보고하는 과정입니다.

1.  **Load Ready / Done**:
    *   **[Console → CIM]**: `ConsoletoCIM.LoadReadyDone`
    *   **[CIM]**: 상태 업데이트 및 호스트 보고 (`S6F61` - Equipment Port State Change 등).
    *   **[Console → CIM]**: `ConsoletoCIM.LoadDone`
    *   **[CIM]**: `MoveLoadtoUnload()`, `S1F67` (Glass Position) 보고.

2.  **Inspection (검사)**:
    *   **[Console → CIM]**: `ConsoletoCIM.InspectDone`
    *   **[CIM]**: 상태 업데이트.

3.  **Unload / Exchange**:
    *   **[Console → CIM]**: `ConsoletoCIM.ExchangeReadyDone` / `UnloadReadyDone`
    *   **[CIM]**: Glass Panel Judge 업데이트 (`data.GlassPanelJudgeA/B`), `S6F61`, `S6F63` 보고.
    *   **[Console → CIM]**: `ConsoletoCIM.ExchangeDone` / `UnloadDone`
    *   **[CIM]**: `MoveLoadtoUnload()`, `S1F67` 보고.

### 2.3 공정 결과 보고 (Process Result Reporting - S6F73)

CIM이 호스트에 공정 결과를 보고하기 전, Console로부터 최종 처리 데이터(APD)를 요청합니다.

1.  **[CIM] S6F73 보고 시점**: `StartProcess` 또는 Unload 시점.
2.  **[CIM → Console] APD Request**:
    *   `Common.ToConsole` 호출.
    *   Command: `CIMtoConsole.APDReq`.
3.  **[Console → CIM] APD Response**:
    *   `CimCommData` 반환 (동기 응답 또는 별도 전송).
    *   Data: `AcutAPD`, `BcutAPD` (시간, Align 정보, Defect Count, Tact Time 등).
4.  **[CIM] S6F73 전송**: 받은 APD 데이터를 조합하여 호스트로 `S6F73` 전송.

### 2.4 원격 명령 (Remote Command - S2F41/S2F49 -> S2F99)

호스트로부터 내려온 명령을 처리합니다.

*   **PAUSE**:
    *   **[CIM]**: `Gem_OnS2F99Event`에서 `RCMD.PAUSE` 수신.
    *   **[CIM → Console]**: `Common.ToConsole` (Command: `CIMtoConsole.Pause`).
    *   **[Console]**: 설비 일시 정지 처리.
*   **RESUME**:
    *   **[CIM]**: `cimStatus.Pause = false` 설정 및 상태 보고 (Console로 별도 명령 전송 없음, 내부 상태만 변경).

### 2.5 알람 처리 (Alarm Handling)

*   **알람 발생**:
    *   **[Console → CIM]**: `ConsoletoCIM.Alarm` (AlarmList 포함).
    *   **[CIM]**: `cim.AlarmOccur` 호출 -> `S5F1` (Alarm Report) 전송.
*   **알람 해제**:
    *   **[Console → CIM]**: `ConsoletoCIM.AlarmClear`.
    *   **[CIM]**: `S5F1` (Clear) 전송.

---

## 3. 관련 소스 코드 파일 (Reference Files)

*   **TOEConsole (Client/Server)**
    *   `Forms\MainForm\MainForm.Flow.cs`: 서버 시작 (`StartServer_Cim`), 포트 리스닝.
    *   `Forms\MainForm\MainForm.CIM.cs`: 데이터 수신 처리 (`OnReceiveData`), 명령 처리 로직.
    *   `ControlCim\CIMComm.cs`: 클라이언트 송신 (`ToCIM`).
    *   `Vars.cs`: 포트 정보 참조 (`CimtoConsolePort`).

*   **TOECIM (Client/Server)**
    *   `MainForm.cs`: 서버 시작 (`ListenFromConsole`), 데이터 수신 처리 (`OnReceiveConsoleData`).
    *   `Common.cs`: 클라이언트 송신 (`ToConsole`).
    *   `CIMClient\S6Funcs.cs`: APD 요청 (`APDReq`), `S6F73` 처리.
    *   `CIMClient\S2Funcs.cs`: 글라스 데이터 전달 (`GlassDataSend`), 원격 명령 처리.
