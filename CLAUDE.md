# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## 프로젝트 개요

LGD 터치검사 설비용 SECS-II 통신 프로그램(TOECIM). C# / .NET 8.0 / Windows Forms + MaterialSkin 2 기반.
설비(Equipment) ↔ TOECIM ↔ CIM(ECS) 간 HSMS 프로토콜로 통신하며, 설비와는 JSON API(localhost)로 통신.

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

## 아키텍처

```
CIM(ECS) ←──HSMS──→ TOECIM ←──JSON/TCP──→ 설비(Console)
                       │
              ┌────────┼────────┐
              ▼        ▼        ▼
          AlarmMgr  CIMStatus  DB(MySQL)
```

### 솔루션 프로젝트 구조 (의존 순서: 하위→상위)

| 프로젝트 | 역할 |
|----------|------|
| **MmiHsms** | HSMS 소켓/메시지 처리, 프로토콜 상수(CIMDef) |
| **LGHSMSBase** | GEM 프로토콜 핸들러(Gem01~15), LGD 변수 정의 |
| **TOECIMLib** | 데이터 모델 (AlarmInfo, ECSData, GlassInfo, CIMCommand 등) |
| **DBMulti** | MySQL ORM(ServiceStack.OrmLite), UDP 통계 전송 |
| **TOECIM** | 메인 앱 — ClientCIM(통신 오케스트레이터), S*Funcs(스트림별 핸들러), WinForms UI |
| **LGCIMSimul** | CIM/EQP 양방향 시뮬레이터 |

### 핵심 클래스

- **ClientCIM** (`TOECIM/CIMClient/ClientCIM.cs`): GEM 초기화, SECS-II 메시지 핸들러 등록, 통신 전체 오케스트레이션
- **CIMStatus** (`TOECIM/CIMClient/CIMStatus.cs`): 설비 상태(RUN/IDLE/DOWN), Glass/Lot, 알람, RCS 모드 등 전역 상태 관리
- **S1Funcs~S15Funcs** (`TOECIM/CIMClient/S*Funcs.cs`): SECS-II 스트림별 함수 구현 (S1=상태, S2=원격제어, S5=알람, S6=Glass이송, S7=레시피, S10=터미널, S15=인터페이스)
- **AlarmManager** (`TOECIM/CIMClient/AlarmManager.cs`): JSON 파일 기반 알람 이력 관리
- **SystemSetup** (`TOECIM/CIMClient/SystemSetup.cs`): XML 기반 런타임 설정 (HSMS 포트, 타임아웃 등)
- **Vars** (`TOECIM/Vars.cs`): 전역 static 변수 (ClientCIM 인스턴스, VersionManager)
- **MainForm** (`TOECIM/Forms/MainForm.cs`): 메인 UI — 상태 모니터링, 수동 제어, 알람 뷰어, 레시피 관리, 실시간 로그

### 통신 흐름

- CIM(ECS) → TOECIM: HSMS/SECS-II 메시지 수신 → LGGem 이벤트 → S*Funcs 핸들러 처리
- TOECIM → 설비: JSON 직렬화 → TCP(localhost:3501) → 설비 콘솔
- 비동기 이벤트 기반 + 타임아웃 관리(`ETimeout` 클래스)

## 설정 파일

- `Setup/systemSetup.xml`: HSMS IP/포트, 콘솔 포트(기본 3501), 타임아웃(T3/T5/T6) 등 런타임 설정 (없으면 자동 생성)
