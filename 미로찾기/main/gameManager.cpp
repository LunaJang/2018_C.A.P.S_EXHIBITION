#include "main.h"

using namespace std;

GameManager::GameManager() {
	// 각 멤벼 변수에 객체를 동적 할당하기
}

void GameManager::startGame() {
	// board 객체 동적 할당
	// player 객체 동적 할당, 매개변수로 이름 보내기
	// 시간 재기 준비
	// 뭐... 시작 멘트같은거 출력? 출력은 UI의 프린트 함수 사용
	// 멘트는 전해 했듯이 gameManager 헤더나 script헤더를 생성하여 저장, 불러올것

	// 끝나면 onGame 함수 호출
}

bool GameManager::onGame() {
	// 시간 다되거나 도착할때까지 반복
	// player에게 현재 위치 받아오기
	// gameBoard에게 현재 위치 알려주고 맵 받아오기
	// 사용자에게서 이동 방향 받아오기
	// gmaeBoard 한테 이동 가능한지 함수로 물어보기
	// 된다고 하면 player의 위치 갱신
	// 도착했는지 gameBoard한테 물어보기

	// 끝나면 finishGame 함수 호출
}

Player& GameManager::finishGame() {
	// 끝나기 멘트 출력, 출력은 UI의 프린트 함수 사용
	// 점수 계산해서 player 객체 반환
	// Board 동적할당 해제
	// player는 하면 안댐
}