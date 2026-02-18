# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## 프로젝트 개요

LGD 터치검사 설비용 SECS-II 통신 프로그램(TOECIM). C# / .NET 8.0 / Windows Forms + MaterialSkin 2 기반.
설비(Equipment) ↔ TOECIM ↔ CIM(ECS) 간 HSMS 프로토콜로 통신하며, 설비와는 JSON/TCP(localhost:3501)로 통신.

## 빌드 및 실행

```bash
# 빌드
dotnet build TOECIM.sln

# 메인 앱 실행
dotnet run --project TOECIM/TOECIM.csproj

# 시뮬레이터 실행 (CIM/EQP 양측 시뮬레이션)
dotnet run --project LGCIMSimul/LGCIMSimul.csproj
```

테스트 프레임워크 없음. `LGCIMSimul` 시뮬레이터로 동작 검증.

## 개발 규칙

- 모든 답변/계획/보고는 **한글**로 작성
- 통신 로직 수정 시 `Docs/LGD_CIM사양서/` 문서를 최우선 참고
- 실제 장비 로그(`Docs/CIM_LOG/`)와 대조하여 검증
- Microsoft .NET Design Guidelines 준수
- `LGCIMSimul/` 시뮬레이터로 실장비 투입 전 검증
- `LGDef.cs` (`LGHSMSBase/LGDef.cs`, 63KB)는 **자동 생성 파일** — 직접 수정 금지

## 아키텍처

```
CIM(ECS) ←──HSMS──→ TOECIM ←──JSON/TCP──→ 설비(Console)
                       │
              ┌────────┼────────┐
              ▼        ▼        ▼
          AlarmMgr  CIMStatus  lastValue.json
```

### 솔루션 프로젝트 구조 (의존 순서: 하위→상위)

| 프로젝트 | 역할 |
|----------|------|
| **MmiHsms** | HSMS 소켓/메시지 처리, 프로토콜 상수(CIMDef). `AllowUnsafeBlocks` 사용 |
| **LGHSMSBase** | GEM 프로토콜 핸들러(Gem01~15), LGD 변수 정의(`LGDef.cs` 자동생성) |
| **TOECIMLib** | 데이터 모델 (AlarmInfo, ECSData, GlassInfo, CIMCommand, ETimeout 등) |
| **TOECIM** | 메인 앱 — ClientCIM(통신 오케스트레이터), S*Funcs(스트림별 핸들러), WinForms UI |
| **LGCIMSimul** | CIM/EQP 양방향 시뮬레이터 (ECSSimul, EQPSimul, ScenarioRunner) |

### 핵심 클래스

- **ClientCIM** (`TOECIM/CIMClient/ClientCIM.cs`): GEM 초기화, SECS-II 메시지 핸들러 등록, 통신 전체 오케스트레이션. **partial class**로 선언되어 S*Funcs.cs 파일들과 분리
- **CIMStatus** (`TOECIM/CIMClient/CIMStatus.cs`): 설비 상태(RUN/IDLE/DOWN), Glass/Lot, 알람, RCS 모드 등 전역 상태 관리. JSON 직렬화로 `lastValue.json`에 상태 영속화
- **S1Funcs~S15Funcs** (`TOECIM/CIMClient/S*Funcs.cs`): `ClientCIM`의 partial class로 SECS-II 스트림별 함수 구현
  - S1=Equipment상태, S2=원격제어/RCS, S5=알람, S6=Glass이송, S7=레시피, S9=타임아웃, S10=터미널, S15=인터페이스
  - 패턴: `InitSX()` 이벤트 등록 → `SendSXFYY()` 메시지 송신 → `Gem_OnSXFYYEvent()` 수신 처리
- **LGGem** (`LGHSMSBase/Gem.cs`): HSMSGem 상속. SECS-II 메시지 수신 시 S1~S15 스트림별 이벤트 발생 — ClientCIM이 이 이벤트를 S*Funcs에 연결
- **AlarmManager** (`TOECIM/CIMClient/AlarmManager.cs`): JSON 파일(`alarmHistory.json`) 기반 알람 이력 관리. DB(MySQL)에서 마이그레이션 완료
- **SystemSetup** (`TOECIM/CIMClient/SystemSetup.cs`): XML 기반 런타임 설정 (HSMS 포트, 타임아웃 등)
- **ETimeout** (`TOECIMLib/Stubs.cs`): 이벤트 기반 타임아웃 관리. **주의**: Stubs.cs의 `RegNewTimeout/AbortTimeout/ReleaseTimeout` 메서드 본체는 비어있음 — 실제 타임아웃 구현은 `ClientCIM`에 존재
- **Vars** (`TOECIM/Vars.cs`): 전역 static 변수 (ClientCIM 인스턴스, VersionManager)
- **MainForm** (`TOECIM/Forms/MainForm.cs`): 메인 UI — 상태 모니터링, 수동 제어, 알람 뷰어, 레시피 관리, 실시간 로그
- **Common** (`TOECIM/Common.cs`): JSON/TCP 직렬화 헬퍼. `ToConsole()` / `ToConsoleAsync()`로 설비(localhost:3501)에 `CimCommData` 전송

### 통신 흐름

- CIM(ECS) → TOECIM: HSMS/SECS-II 메시지 수신 → **LGGem** 이벤트 발생 → S*Funcs 핸들러 처리
- TOECIM → 설비: `CIMtoEQPData` JSON 직렬화 → TCP(localhost:3501) → 설비 콘솔 응답(`EQPtoCIMData`)
- 비동기 이벤트 기반 + 타임아웃 관리(`ETimeout` 타입 상수 — `toGlassDataLoad`, `toGlassUnloadData`, `S2F18`, `S6F12`, `T3/T5/T6` 등)

### LGCIMSimul 내부 구조

- `CIM/S*Funcs.cs`: TOECIM에 HSMS 메시지를 전송하는 CIM 역할 시뮬레이터
- `ECSSimul/ECSSimulForm.cs`: ECS 조작 UI (48KB — 가장 큰 시뮬레이터 폼)
- `EQPSimul/EQPSimulator.cs`: 설비 동작 시뮬레이션 (JSON/TCP 서버, localhost:3501)
- `ScenarioRunner.cs`: 자동화 시나리오 실행 엔진. 사전 정의 시나리오: `A`(Normal Loading), `B`(Alarm), `C`(Unload), `D`(Unload Glass Data S6F63), `E`(Lot End), `RECIPE`(레시피 생성/변경)

## 설정 파일

### 런타임 설정 (수동 편집)
- `Setup/systemSetup.xml`: HSMS IP/포트, 콘솔 포트(기본 3501), 타임아웃(T3/T5/T6) — 없으면 자동 생성
- `LGCIMSimul/../eqSimul.xml`: EQP 시뮬레이터 설정

### 런타임 생성 파일 (자동 생성, .gitignore 제외)
- `lastValue.json`: CIMStatus 마지막 상태 (앱 재시작 시 복구용)
- `alarmHistory.json`: AlarmManager 알람 이력
- `VersionHistory.json`: VersionManager 버전 이력

## 주요 데이터 모델

- **GlassInfo** (`TOECIMLib/ECSData.cs`): LOTID, CSTID, RECIPEID, GLSID, GLSODR(0~3), GLSCODE, PORTID, DICODE, GLSPROC, DEST
- **CimCommData**: 양방향 통신 래퍼 — `ToCIM`(EQPtoCIMData, 설비→TOECIM), `ToConsole`(CIMtoEQPData, TOECIM→설비)
- **LGDef.cs 타입**: StringType/IntType 상속 클래스(CSTID, LOTID, RECIPEID, GLSID 등) — 암묵적 변환 연산자로 string ↔ SECS-II 타입 자동 변환
