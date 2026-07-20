# MolkGame 開発計画

## 開発方針

# Phase 1: ゲームオブジェクト作成

## Commit 2: スキットルPrefab作成
- 円柱形を斜めにカットした形状
 1 ～ 12 までの数字が記載されている 12 本のスキットル。
 おおよそ高さ 15cm、直径 5.9cm であり、45°に面取りされている
- 木製を想定したMaterial

## Commit 3: スキットル配置システム作成


# Phase 2: 投擲システム

## Commit 4: 投擲オブジェクト作成
投げる棒の名前は「モルック」だが、ややこしいので、ここでは「投擲オブジェクト」と呼ぶ。
おおよそ長さ 22.5cm、直径 5.9cm である

## Commit 5: 投擲処理追加


# Phase 3: Joy-Con対応準備

## Commit 6: 入力抽象化

### Copilot指示

```
入力処理を抽象化してください。

InputProviderインターフェースを作成してください。

実装:
- KeyboardInputProvider

ゲームロジック側はInputProviderのみ利用し、
将来的にJoy-Con入力へ差し替え可能にしてください。
```

### Commit Message

```
Add input abstraction layer
```

---

## Commit 7: Joy-Con入力対応

### Copilot指示

```
Joy-Con入力対応を追加してください。

既存のInputProvider設計を利用してください。

ゲームロジックの変更は最小限にし、
入力部分だけ差し替えてください。
```

### Commit Message

```
Add Joy-Con input support
```

---

# Phase 4: ゲームルール

## Commit 8: スコア計算

### Copilot指示

```
モルックのスコア計算クラスを作成してください。

仕様:
- 1本のみ倒れた場合は番号を得点
- 複数倒れた場合は倒れた本数
- 50点以上で25点へ戻る

UIや入力処理には依存しない設計にしてください。
```

### Commit Message

```
Implement score calculation
```

---

## Commit 9: ターン管理

### Copilot指示

```
TurnManagerを作成してください。

管理対象:
- 現在プレイヤー
- 投擲回数
- ターン終了

ScoreManagerとは責務を分離してください。
```

### Commit Message

```
Add turn management
```

---

# Phase 5: UI

## Commit 10: スコア表示UI

### Copilot指示

```
現在のスコアを表示するUIを作成してください。

仕様:
- Canvasを使用
- ScoreManagerから値を取得
- ゲームロジックがUIへ依存しないようにする
```

### Commit Message

```
Add score UI
```

---

# MCP利用方針

## Unity MCPを利用する作業

* GameObject作成
* Prefab作成
* Component追加
* Scene編集
* Material設定
* Inspector設定

## GitHub Copilotのみで行う作業

* C# Script作成
* クラス設計
* リファクタリング
* ロジック実装
* コードレビュー

---

# 完了条件

各Commitごとに：

* Unity上で動作確認済み
* 変更内容を説明可能
* 不要な変更が含まれていない
* 次の機能追加に影響しない設計になっている

ことを確認する。
