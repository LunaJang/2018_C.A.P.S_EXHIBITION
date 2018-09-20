#pragma once
#include "main.h"

// 여기는 값을 반환하는 함수가 많이 필요하지 않으니 맘대로 짜시오
// 단, 차후 Ranking 등의 추가 기능을 넣을 여지가 있게 설계하시오
// 기본 함수 외에 넣고 싶은 함수는 만대로 넣어서 짜도 ㄱㅊ

class GameManager {
	Player* user;
	Board* gameBoard;
	// 시간 관련 객체등 알아서 처리~
public:
	GameManager();
	
	void startGame();
	bool onGame();
	Player& finishGame();
};