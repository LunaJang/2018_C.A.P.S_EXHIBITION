#pragma once
#include "main.h"

// 진짜 딱 여기있는 함수만 만들면 됨
// 매개변수는 알아서 넣으시오

class Player {
	int x, y;
	string name;
	int score;
public:
	Player(string tempName = "바보");

	int getX();

	int getY();

	int setX();


	int setY();

	int getName();

	int setScore();

	int getScore();
};