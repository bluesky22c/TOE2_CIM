# TOECIM Simulation Scenarios

본 문서는 TOECIM 프로그램의 기능 검증을 위한 시나리오를 정의합니다. PDF 사양서의 텍스트 추출이 불가능하여, **TOECIM 소스 코드(`MainForm.cs`, `CIMClient` 등) 분석을 통해 실제로 구현된 기능을 기반으로 작성**되었습니다.

## 1. 개요
*   **목적**: TOECIM 프로그램의 정상 동작 여부를 시뮬레이션을 통해 검증
*   **방법**: LGCIMServer(Host)와 TOECIM(Client) 간의 통신 로그 분석 및 기능 동작 확인
*   **참고**: ECS-Equipment Communication Specification_HSMS_Ver1.6 forToE_v0.1.pdf (내용 확인 불가로 코드 분석 대체)

## 2. 테스트 시나리오 목록

### [기본 통신 및 상태]

#### TC-01. HSMS 통신 연결 (Connection)
*   **설명**: Host와 Equipment 간의 초기 통신 연결을 확인한다.
*   **예상 메시지**: `S1F13` (Comm Request) <-> `S1F14` (Comm Response)
*   **확인 사항**: `Communicating` 상태로 전환되는지 확인. 로그에 `S1F13`, `S1F14` 기록 확인.

#### TC-02. 시간 동기화 (Time Synchronization)
*   **설명**: Host의 시간을 Equipment에 동기화한다.
*   **예상 메시지**: `S2F31` (Date and Time Set Request) -> `S2F32` (Date and Time Set Acknowledge)
*   **확인 사항**: TOECIM 로그에 시간 설정 관련 메시지 확인.

#### TC-07. 루프백 테스트 (Loopback Test) [추가]
*   **설명**: 통신 선로의 건전성을 확인하기 위한 에코 테스트.
*   **구현 근거**: `S2Funcs.cs` 내 `SendS2F25` 메서드 존재.
*   **예상 메시지**: `S2F25` (Loopback Diagnostic Request) -> `S2F26` (Loopback Diagnostic Data)
*   **확인 사항**: 보낸 데이터와 동일한 데이터가 수신되는지 확인.

#### TC-08. 통신 모드 변경 (Online/Offline) [추가]
*   **설명**: 장비의 통신 모드를 Offline <-> Online으로 변경한다.
*   **구현 근거**: `MainForm.cs`의 `connectionButton` 및 `S1Funcs.cs`의 관련 핸들러.
*   **예상 메시지**: `S1F15`(Offline Req) / `S1F17`(Online Req) <-> `S1F16` / `S1F18`
*   **확인 사항**: UI 상의 통신 상태 아이콘 또는 텍스트 변경 확인.

### [장비 제어 및 정보]

#### TC-09. Ionizer 상태 요청 (Ionizer Status) [추가]
*   **설명**: 장비 내 Ionizer의 상태 정보를 요청한다.
*   **구현 근거**: `MainForm.cs`의 `ionizerToolStripMenuItem` 및 `S1Funcs.cs`의 `SendS1F75`.
*   **예상 메시지**: `S1F75` (Status Request) -> `S1F76` (Status Data)
*   **확인 사항**: Ionizer 상태 정보(ID, Status 등)가 로그에 기록되는지 확인.

#### TC-11. Lot End 처리 (Lot End) [추가]
*   **설명**: Lot 처리가 완료되었음을 보고한다.
*   **구현 근거**: `MainForm.cs`의 `lotEndButton` 및 `S2Funcs.cs`의 `SendS2F65`.
*   **예상 메시지**: `S2F65` (Lot End Report - 비표준) -> `S2F66` (Acknowledge)
*   **확인 사항**: Lot End 버튼 클릭 시 해당 메시지 전송 여부 확인.

#### TC-12. Cycle Stop (Cycle Stop) [추가]
*   **설명**: 장비의 Cycle Stop 기능을 활성화/비활성화한다.
*   **구현 근거**: `MainForm.cs`의 `cycleStopSetButton` 및 `S2Funcs.cs`의 `SendS2F67`.
*   **예상 메시지**: `S2F67` (Cycle Stop Command - 비표준) -> `S2F68` (Acknowledge)
*   **확인 사항**: Cycle Stop 설정/해제 시 메시지 전송 및 장비 상태 변화 확인.

### [레시피 및 글래스 처리]

#### TC-03. 레시피 확인 (Recipe Check)
*   **설명**: 현재 설정된 레시피가 Host와 일치하는지 확인한다.
*   **구현 근거**: `S7Funcs.cs` 및 `S15Funcs.cs` (Recipe Management). TC에서는 `S7F61` 사용 빈도 높음.
*   **예상 메시지**: `S7F61` (Process Program Load Inquiry - 비표준 추정) <-> `S7F62`
*   **확인 사항**: 레시피 ID 및 파라미터 일치 여부 확인.

#### TC-10. Glass 데이터 요청 (Glass Data Request) [추가]
*   **설명**: 특정 Glass에 대한 데이터를 요청한다.
*   **구현 근거**: `MainForm.cs`의 `glassDataReqButton` 및 `S2Funcs.cs`의 `SendS2F79`.
*   **예상 메시지**: `S2F79` (Glass Data Request - 비표준) -> `S2F80` (Glass Data)
*   **확인 사항**: 요청한 Glass ID/Code에 대한 데이터 수신 확인.

#### TC-04. 글래스 투입 (Glass Load)
*   **설명**: 글래스가 장비로 투입되는 시나리오.
*   **예상 메시지**: `S6F61` (Trace Report? / Custom Event) -> `S6F62`
*   **확인 사항**: Load 관련 이벤트 및 상태 보고 확인.

#### TC-05. 글래스 배출 (Glass Unload)
*   **설명**: 공정이 완료된 글래스가 배출되는 시나리오.
*   **예상 메시지**: `S6F63` (Unload Event - 비표준) -> `S6F64`
*   **확인 사항**: Unload 이벤트 발생 및 데이터 보고 확인.

### [알람]

#### TC-06. 알람 발생 및 해제 (Alarm Handling)
*   **설명**: 장비 알람 발생 시 Host로 보고 및 해제 과정을 검증한다.
*   **예상 메시지**: `S5F1` (Alarm Report Send) -> `S5F2` (Alarm Report Acknowledge)
*   **확인 사항**: 알람 발생 시 S5F1 전송, 해제 시 S5F1(Clear) 전송 확인.

---
**비고**:
*   `Remote Command (S2F41)`은 `S2Funcs.cs` 소스 코드 분석 결과 명시적인 핸들러가 발견되지 않아 시나리오에서 제외하였습니다. (필요 시 추후 구현 확인)
*   위 시나리오는 TOECIM 소스 코드를 기준으로 작성되었으므로, 실제 ECS 사양과 일부 메시지 번호(Stream/Function)가 표준과 다를 수 있습니다 (LGD Customization 반영).
