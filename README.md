# Sproject
## 조경찬 개인 프로잭트
> 1인칭 FPS end code block.
1인칭이 힘들면 3인칭
스테이지 식으로 진행
적을들 잡을 시 자원을 드랍 ex)상점에서 사용할 수 있는 자원, 충돌 시 즉시 체력회복이되는 아이템, 충돌 시 즉시 보유 탄약이 늘어나는 아이템 
가만히 있으면 트리거
상점에서 자원을 사용해서 플레이어 능력치 강화 ex) 탄창발 수, 최대체력, 공격력
적 Assete Creature
보스, 일반 몬스터
일반몬스터 ex)2~3타입
보스는 패턴 눈에 잘 보이는
NevMeshAgent
상태 -> idle, trace, attack, Hit, Die;
플레이어 
상태 -> idle, shoot, work, Die;
CheracterController 컴포넌트
Move, Jump
상점 
상호작용시 상점 창 
스크립터블 오브젝트로 아이템 정보 저장
